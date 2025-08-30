using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;
using System.Linq.Expressions;

namespace ChatApp.Core.DataService
{

    // In ChatApp.Core.DataService/BaseDataService.cs

    public abstract class BaseDataService<TEntity, TDataTransferObject> : IBaseDataService<TDataTransferObject>
       where TEntity : class where TDataTransferObject : class
    {
        private readonly Dictionary<Type, dynamic> _unitOfWorkCreators;
        private readonly IHelper_Mapper<TEntity, TDataTransferObject> _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new Helper_Mapper<TEntity, TDataTransferObject>();

            // This dictionary maps a DTO type to its corresponding repository in the Unit of Work.
            _unitOfWorkCreators = new Dictionary<Type, dynamic>
            {

                { typeof(SchoolBranch_DTO), _unitOfWork.SchoolBranchDataAccess },
                { typeof(Curriculum_DTO), _unitOfWork.CurriculumDataAccess },
                { typeof(SchoolClass_DTO), _unitOfWork.SchoolClassDataAccess },
                { typeof(Section_DTO), _unitOfWork.SectionDataAccess },
                { typeof(Staff_DTO), _unitOfWork.StaffDataAccess },
                { typeof(StaffLogin_DTO), _unitOfWork.StaffLoginDataAccess },
                { typeof(StaffJobDetails_DTO), _unitOfWork.StaffJobDetailsDataAccess },
                { typeof(Teacher_DTO), _unitOfWork.TeacherDataAccess },
                { typeof(Student_DTO), _unitOfWork.StudentDataAccess },
                { typeof(StudentLogin_DTO), _unitOfWork.StudentLoginDataAccess },
                { typeof(Guardian_DTO), _unitOfWork.GuardianDataAccess },
                { typeof(GuardianLogin_DTO), _unitOfWork.GuardianLoginDataAccess },
                { typeof(StudentSchoolDetails_DTO), _unitOfWork.StudentSchoolDetailsDataAccess },
                { typeof(Subject_DTO), _unitOfWork.SubjectDataAccess },
                { typeof(ChatRoom_DTO), _unitOfWork.ChatRoomDataAccess },
                { typeof(ChatRoomMembers_DTO), _unitOfWork.ChatRoomMembersDataAccess },
                { typeof(ChatRoomUser_DTO), _unitOfWork.ChatRoomUserDataAccess },
                { typeof(ChatRoomMessage_DTO), _unitOfWork.ChatRoomMessageDataAccess },
                { typeof(ChatRoomSettings_DTO), _unitOfWork.ChatRoomSettingDataAccess },
                { typeof(CurriculumChatRooms_DTO), _unitOfWork.CurriculumChatRoomsDataAccess },
                { typeof(ClassChatRooms_DTO), _unitOfWork.ClassChatRoomsDataAccess },
                { typeof(SectionChatRoomsDataService), _unitOfWork.SectionChatRoomsDataAccess },
                { typeof(SubjectChatRoomsDataService), _unitOfWork.SubjectChatRoomsDataAccess },
                { typeof(UserChatRoomsDataService), _unitOfWork.UserChatRoomsDataAccess },

            };
        }

        public virtual async Task<TDataTransferObject> AddAsync(TDataTransferObject entity)
        {
            var domainEntity = _mapper.MapEntityDtoToDomainEntity(entity);
            // This now returns the tracked entity.
            var trackedEntity = await UnitOfWorkCreator().AddAsync(domainEntity);
            // After SaveChangesAsync is called, trackedEntity will have the new ID.
            // We map it back to a DTO to return it.
            return _mapper.MapDomainEntityToEntityDto(trackedEntity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TDataTransferObject> entities)
        {
            var domainEntities = _mapper.MapEntitiesDtosToDomainEntities(entities);
            await UnitOfWorkCreator().AddRangeAsync(domainEntities);
            // return entities;
        }

        public virtual Task DeleteAsync(TDataTransferObject entity)
        {
            var domainEntity = _mapper.MapEntityDtoToDomainEntity(entity);
            return UnitOfWorkCreator().DeleteAsync(domainEntity);
        }

        public virtual Task DeleteByIdAsync(int id)
        {
            return UnitOfWorkCreator().DeleteByIdAsync(id);
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TDataTransferObject> entities)
        {
            var domainEntities = _mapper.MapEntitiesDtosToDomainEntities(entities);
            return UnitOfWorkCreator().DeleteRangeAsync(domainEntities);
        }

        public virtual async Task<TDataTransferObject> FindAsync(int id)
        {
            var domainEntity = await UnitOfWorkCreator().FindAsync(id);
            return _mapper.MapDomainEntityToEntityDto(domainEntity);
        }

        public virtual async Task<IEnumerable<TDataTransferObject>> GetAllAsync()
        {
            var domainEntities = await UnitOfWorkCreator().GetAllAsync();
            return _mapper.MapDomainEntitiesToEntityDtos(domainEntities);
        }

        public virtual async Task<IEnumerable<TResult>> GetSpecificPropertyAsync<TResult>(Expression<Func<TDataTransferObject, bool>> predicate, Expression<Func<TDataTransferObject, TResult>> selector)
        {
            var domainPredicate = _mapper.MapExpressionFromDtoToDominEntity(predicate);
            var domainSelector = _mapper.MapSelectorExpression(selector);
            return await UnitOfWorkCreator().GetSpecificPropertyAsync(domainPredicate, domainSelector);
        }

        public virtual Task UpdateAsync(TDataTransferObject entity)
        {
            var domainEntity = _mapper.MapEntityDtoToDomainEntity(entity);
            return UnitOfWorkCreator().UpdateAsync(domainEntity);
        }

        public virtual Task UpdateOnlyPropertyAsync(TDataTransferObject entity, string propertyName)
        {
            var domainEntity = _mapper.MapEntityDtoToDomainEntity(entity);
            return UnitOfWorkCreator().UpdateOnlyPropertyAsync(domainEntity, propertyName);
        }

        public virtual async Task<IEnumerable<TDataTransferObject>> Where(Expression<Func<TDataTransferObject, bool>> predicate, params Expression<Func<TDataTransferObject, object>>[] includes)
        {
            var mappedIncludes = includes.Select(include => _mapper.MapSelectorExpression(include)).ToArray();
            var mappedPredicate = _mapper.MapExpressionFromDtoToDominEntity(predicate);
            var domainEntities = await UnitOfWorkCreator().Where(mappedPredicate, mappedIncludes);
            return _mapper.MapDomainEntitiesToEntityDtos(domainEntities);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        private dynamic UnitOfWorkCreator()
        {
            var dataTransferObjectType = typeof(TDataTransferObject);
            if (_unitOfWorkCreators.TryGetValue(dataTransferObjectType, out var createdUnitOfWork))
            {
                return createdUnitOfWork;
            }
            throw new InvalidOperationException($"No data access method found for DTO type: {dataTransferObjectType.Name}");
        }
    }

}