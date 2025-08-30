using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ChatApp.Core.DataService
{
    public class Helper_Mapper<E, D> : ExpressionVisitor, IHelper_Mapper<E, D>
        where E : class
        where D : class
    {
        #region Private Fields

        private readonly Dictionary<object, object> _mappedObjectsCache;
        private readonly ParameterExpression _parameter;

        #endregion Private Fields

        #region Public Constructors

        public Helper_Mapper()
        {
            _parameter = Expression.Parameter(typeof(E), "x");
            _mappedObjectsCache = new Dictionary<object, object>();
        }

        #endregion Public Constructors

        #region Public Methods

        public IEnumerable<D> MapDomainEntitiesToEntityDtos(IEnumerable<E> domainEntities)
        {
            return domainEntities?.Select(d => MapObject<E, D>(d)).ToList() ?? new List<D>();
        }

        public D MapDomainEntityToEntityDto(E domainEntity)
        {
            return MapObject<E, D>(domainEntity);
        }

        public IEnumerable<E> MapEntitiesDtosToDomainEntities(IEnumerable<D> dtoEntities)
        {
            return dtoEntities?.Select(e => MapObject<D, E>(e)).ToList() ?? new List<E>();
        }

        public E MapEntityDtoToDomainEntity(D dtoEntity)
        {
            return MapObject<D, E>(dtoEntity);
        }

        public Expression<Func<E, bool>> MapExpressionFromDtoToDominEntity(Expression<Func<D, bool>> predicate)
        {
            var expressionBody = Visit(predicate.Body);
            return Expression.Lambda<Func<E, bool>>(expressionBody, _parameter);
        }

        public IEnumerable<D> MapList(IEnumerable<E> sourceList)
        {
            return sourceList?.Select(d => MapObject<E, D>(d)).ToList() ?? new List<D>();
        }

        public D MapObject(E sourceObj)
        {
            return MapObject<E, D>(sourceObj);
        }

        public Expression<Func<E, object>> MapSelectorExpression(Expression<Func<D, object>> selector)
        {
            var newBody = Visit(selector.Body);
            return Expression.Lambda<Func<E, object>>(newBody, _parameter);
        }

        public Expression<Func<E, TResult>> MapSelectorExpression<TResult>(Expression<Func<D, TResult>> selector)
        {
            var newBody = Visit(selector.Body);
            return Expression.Lambda<Func<E, TResult>>(newBody, _parameter);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null)
            {
                // Case 1: Direct property on D
                if (node.Expression.NodeType == ExpressionType.Parameter && node.Expression.Type == typeof(D))
                {
                    var entityProperty = typeof(E).GetProperty(node.Member.Name);
                    if (entityProperty != null)
                    {
                        return Expression.Property(_parameter, entityProperty);
                    }
                }

                // Case 2: Nested property
                if (node.Expression is MemberExpression parent)
                {
                    var parentPropertyInfo = typeof(E).GetProperty(parent.Member.Name);
                    if (parentPropertyInfo != null)
                    {
                        var parentExpression = Expression.Property(_parameter, parentPropertyInfo);
                        var childPropertyInfo = parentPropertyInfo.PropertyType.GetProperty(node.Member.Name);

                        if (childPropertyInfo != null)
                        {
                            return Expression.Property(parentExpression, childPropertyInfo);
                        }
                    }
                }
            }

            return base.VisitMember(node);
        }

        #endregion Protected Methods

        #region Private Methods

        private T CreateInstance<T>() where T : class
        {
            return (T)CreateInstance(typeof(T));
        }

        private object CreateInstance(Type type)
        {
            var constructor = type.GetConstructor(Type.EmptyTypes) ?? type.GetConstructors().FirstOrDefault();
            if (constructor == null)
                throw new InvalidOperationException($"No valid constructor found for {type.Name}");

            var args = constructor.GetParameters()
                .Select(p => p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null)
                .ToArray();

            return constructor.Invoke(args);
        }

        private object MapCollection(IEnumerable sourceCollection, Type targetCollectionType)
        {
            if (sourceCollection is byte[])
                return sourceCollection;

            Type targetElementType = targetCollectionType.IsGenericType
                ? targetCollectionType.GetGenericArguments().FirstOrDefault()
                : null;

            if (targetElementType == null)
                throw new InvalidOperationException($"Could not determine element type for {targetCollectionType.Name}");

            var mappedList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(targetElementType));

            foreach (var sourceItem in sourceCollection)
            {
                mappedList.Add(MapObject(sourceItem, targetElementType));
            }

            return mappedList;
        }

        private TTarget MapObject<TSource, TTarget>(TSource source)
                                    where TSource : class
            where TTarget : class
        {
            if (source == null) return null;

            if (_mappedObjectsCache.TryGetValue(source, out var cachedTarget))
                return (TTarget)cachedTarget;

            TTarget target = CreateInstance<TTarget>();
            _mappedObjectsCache[source] = target;

            foreach (var sourceProp in typeof(TSource).GetProperties())
            {
                var targetProp = typeof(TTarget).GetProperty(sourceProp.Name);
                if (targetProp != null && targetProp.CanWrite)
                {
                    var sourceValue = sourceProp.GetValue(source);

                    if (sourceValue == null)
                    {
                        targetProp.SetValue(target, null);
                    }
                    else if (sourceProp.PropertyType.IsClass && sourceProp.PropertyType != typeof(string))
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(sourceProp.PropertyType))
                        {
                            var mappedCollection = MapCollection((IEnumerable)sourceValue, targetProp.PropertyType);
                            targetProp.SetValue(target, mappedCollection);
                        }
                        else
                        {
                            var nestedTarget = MapObject(sourceValue, targetProp.PropertyType);
                            targetProp.SetValue(target, nestedTarget);
                        }
                    }
                    else
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(sourceProp.PropertyType) && targetProp.PropertyType.IsAssignableFrom(sourceValue.GetType()))
                        {
                            targetProp.SetValue(target, sourceValue);
                        }
                        else if (typeof(IEnumerable).IsAssignableFrom(sourceProp.PropertyType))
                        {
                            var sourceCollection = (IEnumerable)sourceValue;
                            var targetCollectionType = targetProp.PropertyType;

                            var mappedCollection = MapCollection(sourceCollection, targetCollectionType);

                            // Ensure the collection is of the expected type
                            if (targetCollectionType.IsAssignableFrom(mappedCollection.GetType()))
                            {
                                targetProp.SetValue(target, mappedCollection);
                            }
                            else if (typeof(ICollection<>).IsAssignableFrom(targetCollectionType.GetGenericTypeDefinition()))
                            {
                                var newCollection = Activator.CreateInstance(targetCollectionType) as IList;
                                foreach (var item in (IEnumerable)mappedCollection)
                                {
                                    newCollection.Add(item);
                                }
                                targetProp.SetValue(target, newCollection);
                            }
                        }
                        else
                        {
                            targetProp.SetValue(target, sourceValue);
                        }
                    }
                }
            }
            return target;
        }

        private object MapObject(object sourceValue, Type targetType)
        {
            if (sourceValue == null)
                return null;

            if (_mappedObjectsCache.TryGetValue(sourceValue, out var cachedTarget))
                return cachedTarget;

            var targetInstance = CreateInstance(targetType);
            _mappedObjectsCache[sourceValue] = targetInstance;

            foreach (var sourceProp in sourceValue.GetType().GetProperties())
            {
                var targetProp = targetType.GetProperty(sourceProp.Name);
                if (targetProp != null && targetProp.CanWrite)
                {
                    var sourcePropValue = sourceProp.GetValue(sourceValue);

                    if (sourcePropValue == null)
                    {
                        targetProp.SetValue(targetInstance, null);
                        continue;
                    }

                    var sourceType = sourceProp.PropertyType;
                    var targetPropType = targetProp.PropertyType;

                    // Handle collections
                    if (sourceType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(sourceType))
                    {
                        var sourceCollection = sourcePropValue as IEnumerable;
                        var targetElementType = targetPropType.IsGenericType
                            ? targetPropType.GetGenericArguments().First()
                            : typeof(object);

                        var mappedListType = typeof(List<>).MakeGenericType(targetElementType);
                        var mappedList = (IList)Activator.CreateInstance(mappedListType);

                        foreach (var item in sourceCollection)
                        {
                            mappedList.Add(MapObject(item, targetElementType));
                        }

                        // Try to assign list directly
                        if (targetPropType.IsAssignableFrom(mappedList.GetType()))
                        {
                            targetProp.SetValue(targetInstance, mappedList);
                        }
                        else
                        {
                            // Try to cast into the expected collection type
                            var castedCollection = Activator.CreateInstance(targetPropType);
                            var addMethod = targetPropType.GetMethod("Add");

                            if (castedCollection is IEnumerable && addMethod != null)
                            {
                                foreach (var item in mappedList)
                                {
                                    addMethod.Invoke(castedCollection, new[] { item });
                                }

                                targetProp.SetValue(targetInstance, castedCollection);
                            }
                            else
                            {
                                // As a fallback, assign null
                                targetProp.SetValue(targetInstance, null);
                            }
                        }
                    }
                    // Handle complex nested types
                    else if (sourceType.IsClass && sourceType != typeof(string))
                    {
                        var nestedMappedValue = MapObject(sourcePropValue, targetPropType);
                        targetProp.SetValue(targetInstance, nestedMappedValue);
                    }
                    // Handle primitive or simple types
                    else
                    {
                        targetProp.SetValue(targetInstance, sourcePropValue);
                    }
                }
            }

            return targetInstance;
        }


        #endregion Private Methods
    }
}