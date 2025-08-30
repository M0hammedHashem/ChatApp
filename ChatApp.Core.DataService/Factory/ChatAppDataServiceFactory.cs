using ChatApp.Core.IDataService;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Core.DataService
{
    public class ChatAppDataServiceFactory : IChatAppDataServiceFactory
    {


        // In ChatApp.Core.DataService/ChatAppDataServiceFactory.cs

        private readonly IServiceProvider _serviceProvider; // Inject IServiceProvider

        public ChatAppDataServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IStudentDataService CreateStudentService
        {
            get
            {
                // Resolve the service from the DI container
                return _serviceProvider.GetRequiredService<IStudentDataService>();

            }
        }

        public IStudentLoginDataService CreateStudentLoginService
        {
            get
            {
                // Resolve the service from the DI container
                return _serviceProvider.GetRequiredService<IStudentLoginDataService>();

            }
        }
        public IStudentSchoolDetailsDataService CreateStudentSchoolDetailsService
        {
            get
            {
                // Resolve the service from the DI container
                return _serviceProvider.GetRequiredService<IStudentSchoolDetailsDataService>();

            }
        }
        // ... similar properties for other services
        public IGuardianDataService CreateGuardianService
        {
            get
            {
                // Resolve the service from the DI container
                return _serviceProvider.GetRequiredService<IGuardianDataService>();

            }
        }

        public IGuardianLoginDataService CreateGuardianLoginService
        {
            get
            {
                // Resolve the service from the DI container
                return _serviceProvider.GetRequiredService<IGuardianLoginDataService>();

            }
        }

        public IChatRoomDataService CreateChatRoomService
        {
            get
            {
                return _serviceProvider.GetRequiredService<IChatRoomDataService>();

            }
        }
        public IChatRoomMembersDataService CreateChatRoomMembersService
        {
            get
            {
                return _serviceProvider.GetRequiredService<IChatRoomMembersDataService>();

            }
        }
        public IChatRoomUserDataService CreateChatRoomUserService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IChatRoomUserDataService>();
            }
        }
        public IChatRoomMessageDataService CreateChatRoomMessageService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IChatRoomMessageDataService>();
            }
        }

        public IChatRoomSettingsDataService CreateChatRoomSettingsService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IChatRoomSettingsDataService>();
            }
        }


        public ISchoolBranchDataService CreateSchoolBranchService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ISchoolBranchDataService>();
            }
        }

        public ICurriculumDataService CreateCurriculumService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ICurriculumDataService>();
            }
        }
        public IDepartmentDataService CreateDepartmentService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IDepartmentDataService>();
            }
        }
        public ICurriculumDepartmentDataService CreateCurriculumDepartmentService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ICurriculumDepartmentDataService>();
            }
        }

        public ISchoolClassDataService CreateSchoolClassService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ISchoolClassDataService>();
            }
        }



        public ISectionDataService CreateSectionService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ISectionDataService>();
            }
        }


        public IStaffDataService CreateStaffService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IStaffDataService>();
            }
        }

        public IStaffLoginDataService CreateStaffLoginService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IStaffLoginDataService>();
            }
        }
        public IStaffJobDetailsDataService CreateStaffJobDetailsService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IStaffJobDetailsDataService>();
            }
        }



        public ITeacherDataService CreateTeacherService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ITeacherDataService>();
            }
        }

        public ISubjectDataService CreateSubjectService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ISubjectDataService>();
            }
        }


        public ICurriculumChatRoomsDataService CreateCurriculumChatRoomsService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ICurriculumChatRoomsDataService>();
            }
        }
        public IClassChatRoomsDataService CreateClassChatRoomsService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IClassChatRoomsDataService>();
            }
        }
        public ISectionChatRoomsDataService CreateSectionChatRoomsService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ISectionChatRoomsDataService>();
            }
        }
        public ISubjectChatRoomsDataService CreateSubjectChatRoomsService
        {
            get
            {

                return _serviceProvider.GetRequiredService<ISubjectChatRoomsDataService>();
            }
        }
        public IUserChatRoomsDataService CreateUserChatRoomsService
        {
            get
            {

                return _serviceProvider.GetRequiredService<IUserChatRoomsDataService>();
            }
        }






    }
}
