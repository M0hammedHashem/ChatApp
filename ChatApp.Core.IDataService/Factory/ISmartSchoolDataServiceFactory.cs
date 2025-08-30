namespace ChatApp.Core.IDataService
{
    public interface IChatAppDataServiceFactory
    {

        ISchoolBranchDataService CreateSchoolBranchService { get; }
        ICurriculumDataService CreateCurriculumService { get; }
        IDepartmentDataService CreateDepartmentService { get; }
        ICurriculumDepartmentDataService CreateCurriculumDepartmentService { get; }
        ISchoolClassDataService CreateSchoolClassService { get; }
        ISectionDataService CreateSectionService { get; }
        IStaffDataService CreateStaffService { get; }
        IStaffLoginDataService CreateStaffLoginService { get; }
        IStaffJobDetailsDataService CreateStaffJobDetailsService { get; }
        ITeacherDataService CreateTeacherService { get; }
        IStudentDataService CreateStudentService { get; }
        IStudentLoginDataService CreateStudentLoginService { get; }
        IStudentSchoolDetailsDataService CreateStudentSchoolDetailsService { get; }
        IGuardianDataService CreateGuardianService { get; }
        IGuardianLoginDataService CreateGuardianLoginService { get; }
        ISubjectDataService CreateSubjectService { get; }



        IChatRoomDataService CreateChatRoomService { get; }
        IChatRoomUserDataService CreateChatRoomUserService { get; }
        IChatRoomMessageDataService CreateChatRoomMessageService { get; }
        IChatRoomSettingsDataService CreateChatRoomSettingsService { get; }


        #region M2MEntities
        IChatRoomMembersDataService CreateChatRoomMembersService { get; }
        ICurriculumChatRoomsDataService CreateCurriculumChatRoomsService { get; }
        IClassChatRoomsDataService CreateClassChatRoomsService { get; }
        ISectionChatRoomsDataService CreateSectionChatRoomsService { get; }
        ISubjectChatRoomsDataService CreateSubjectChatRoomsService { get; }
        IUserChatRoomsDataService CreateUserChatRoomsService { get; }




        #endregion

    }
}
