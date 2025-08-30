namespace ChatApp.Core.IDataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repositories.

        ISchoolBranchDataAccess SchoolBranchDataAccess { get; }
        ICurriculumDataAccess CurriculumDataAccess { get; }
        IDepartmentDataAccess DepartmentDataAccess { get; }
        ICurriculumDepartmentDataAccess CurriculumDepartmentDataAccess { get; }
        ISchoolClassDataAccess SchoolClassDataAccess { get; }
        ISectionDataAccess SectionDataAccess { get; }
        IStaffDataAccess StaffDataAccess { get; }
        IStaffLoginDataAccess StaffLoginDataAccess { get; }
        IStaffJobDetailsDataAccess StaffJobDetailsDataAccess { get; }
        ITeacherDataAccess TeacherDataAccess { get; }
        IStudentDataAccess StudentDataAccess { get; }
        IStudentLoginDataAccess StudentLoginDataAccess { get; }
        IStudentSchoolDetailsDataAccess StudentSchoolDetailsDataAccess { get; }
        IGuardianDataAccess GuardianDataAccess { get; }
        IGuardianLoginDataAccess GuardianLoginDataAccess { get; }
        ISubjectDataAccess SubjectDataAccess { get; }
        IChatRoomDataAccess ChatRoomDataAccess { get; }
        IChatRoomUserDataAccess ChatRoomUserDataAccess { get; }
        IChatRoomMessageDataAccess ChatRoomMessageDataAccess { get; }
        IChatRoomSettingDataAccess ChatRoomSettingDataAccess { get; }




        #region M2MEntities
        IChatRoomMembersDataAccess ChatRoomMembersDataAccess { get; }
        ICurriculumChatRoomsDataAccess CurriculumChatRoomsDataAccess { get; }
        IClassChatRoomsDataAccess ClassChatRoomsDataAccess { get; }
        ISectionChatRoomsDataAccess SectionChatRoomsDataAccess { get; }
        ISubjectChatRoomsDataAccess SubjectChatRoomsDataAccess { get; }
        IUserChatRoomsDataAccess UserChatRoomsDataAccess { get; }

        #endregion


        #endregion

        Task<int> SaveChangesAsync();

    }
}
