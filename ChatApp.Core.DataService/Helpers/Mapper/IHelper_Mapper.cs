using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChatApp.Core.DataService
{
    public interface IHelper_Mapper<E, D> where E : class where D : class
    {
        #region Public Methods

        IEnumerable<D> MapDomainEntitiesToEntityDtos(IEnumerable<E> domainEntities);

        D MapDomainEntityToEntityDto(E domainEntity);

        IEnumerable<E> MapEntitiesDtosToDomainEntities(IEnumerable<D> dtosEntities);

        E MapEntityDtoToDomainEntity(D dtoEntity);

        Expression<Func<E, bool>> MapExpressionFromDtoToDominEntity(Expression<Func<D, bool>> predicate);

        IEnumerable<D> MapList(IEnumerable<E> sourceList);

        D MapObject(E sourceObj);

        Expression<Func<E, object>> MapSelectorExpression(Expression<Func<D, object>> selector);

        Expression<Func<E, TResult>> MapSelectorExpression<TResult>(Expression<Func<D, TResult>> selector);

        #endregion Public Methods
    }
}