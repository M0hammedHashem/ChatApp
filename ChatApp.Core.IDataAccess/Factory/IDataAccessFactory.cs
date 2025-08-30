namespace ChatApp.Core.IDataAccess
{
    public interface IDataAccessFactory
    {

        ISchoolBranchDataAccess CreateSchoolBranchDataAccess { get; }
        ICurriculumDataAccess CreateCurriculumDataAccess { get; }
        IDepartmentDataAccess CreateDepartmentDataAccess { get; }
        ICurriculumDepartmentDataAccess CreateCurriculumDepartmentDataAccess { get; }
        ISchoolClassDataAccess CreateSchoolClassDataAccess { get; }
        ISectionDataAccess CreateSectionDataAccess { get; }
        IStaffDataAccess CreateStaffDataAccess { get; }
        IStaffLoginDataAccess CreateStaffLoginDataAccess { get; }
        IStaffJobDetailsDataAccess CreateStaffJobDetailsDataAccess { get; }
        ITeacherDataAccess CreateTeacherDataAccess { get; }
        IStudentDataAccess CreateStudentDataAccess { get; }
        IStudentLoginDataAccess CreateStudentLoginDataAccess { get; }
        IStudentSchoolDetailsDataAccess CreateStudentSchoolDetailsDataAccess { get; }
        IGuardianDataAccess CreateGuardianDataAccess { get; }
        IGuardianLoginDataAccess CreateGuardianLoginDataAccess { get; }
        ISubjectDataAccess CreateSubjectDataAccess { get; }
        IChatRoomDataAccess CreateChatRoomDataAccess { get; }
        IChatRoomUserDataAccess CreateChatRoomUserDataAccess { get; }
        IChatRoomMessageDataAccess CreateChatRoomMessageDataAccess { get; }
        IChatRoomSettingDataAccess CreateChatRoomSettingDataAccess { get; }


        #region M2MEntities
        IChatRoomMembersDataAccess CreateChatRoomMembersDataAccess { get; }
        ICurriculumChatRoomsDataAccess CreateCurriculumChatRoomsDataAccess { get; }
        IClassChatRoomsDataAccess CreateClassChatRoomsDataAccess { get; }
        ISectionChatRoomsDataAccess CreateSectionChatRoomsDataAccess { get; }
        ISubjectChatRoomsDataAccess CreateSubjectChatRoomsDataAccess { get; }
        IUserChatRoomsDataAccess CreateUserChatRoomsDataAccess { get; }





        #endregion

    }
}
