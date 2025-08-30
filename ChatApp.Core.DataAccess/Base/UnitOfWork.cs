using ChatApp.Core.DataAccess.DataAccesses;
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{


    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private ISchoolBranchDataAccess _schoolBranchDataAccess;
        private ICurriculumDataAccess _curriculumDataAccess;
        private IDepartmentDataAccess _departmentDataAccess;
        private ICurriculumDepartmentDataAccess _curriculumDepartmentDataAccess;
        private ISchoolClassDataAccess _schoolClassDataAccess;
        private ISectionDataAccess _sectiontDataAccess;
        private IStaffDataAccess _staffDataAccess;
        private IStaffLoginDataAccess _staffLoginDataAccess;
        private IStaffJobDetailsDataAccess _staffJobDetailsDataAccess;
        private ITeacherDataAccess _teacherDataAccess;
        private IStudentDataAccess _studentDataAccess;
        private IStudentLoginDataAccess _studentLoginDataAccess;
        private IStudentSchoolDetailsDataAccess _studentSchoolDetailsDataAccess;
        private IGuardianDataAccess _guardianDataAccess;
        private IGuardianLoginDataAccess _guardianLoginDataAccess;
        private ISubjectDataAccess _subjectDataAccess;
        private IChatRoomDataAccess _chatRoomDataAccess;
        private IChatRoomUserDataAccess _chatRoomUserDataAccess;
        private IChatRoomMessageDataAccess _chatRoomMessageDataAccess;
        private IChatRoomSettingDataAccess _chatRoomSettingDataAccess;

        private IChatRoomMembersDataAccess _chatRoomMembersDataAccess;

        private ICurriculumChatRoomsDataAccess _curriculumChatRoomsDataAccess;
        private IClassChatRoomsDataAccess _classChatRoomsDataAccess;
        private ISectionChatRoomsDataAccess _sectionChatRoomsDataAccess;
        private ISubjectChatRoomsDataAccess _subjectChatRoomsDataAccess;
        private IUserChatRoomsDataAccess _userChatRoomsDataAccess;

        private readonly IChatApp_DbContext _dbContext; // This will now be injected

        // Inject IChatApp_DbContext here
        public UnitOfWork(IChatApp_DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }



        public ISchoolBranchDataAccess SchoolBranchDataAccess
        {
            get
            {
                if (_schoolBranchDataAccess is null)
                {
                    _schoolBranchDataAccess = new SchoolBranchDataAccess(_dbContext);
                }

                return _schoolBranchDataAccess;
            }
        }
        public ICurriculumDataAccess CurriculumDataAccess
        {
            get
            {
                if (_curriculumDataAccess is null)
                {
                    _curriculumDataAccess = new CurriculumDataAccess(_dbContext);
                }

                return _curriculumDataAccess;
            }
        }
        public IDepartmentDataAccess DepartmentDataAccess
        {
            get
            {
                if (_departmentDataAccess is null)
                {
                    _departmentDataAccess = new DepartmentDataAccess(_dbContext);
                }

                return _departmentDataAccess;
            }
        }
        public ICurriculumDepartmentDataAccess CurriculumDepartmentDataAccess
        {
            get
            {
                if (_curriculumDepartmentDataAccess is null)
                {
                    _curriculumDepartmentDataAccess = new CurriculumDepartmentDataAccess(_dbContext);
                }

                return _curriculumDepartmentDataAccess;
            }
        }

        public ISchoolClassDataAccess SchoolClassDataAccess
        {
            get
            {
                if (_schoolClassDataAccess is null)
                {
                    _schoolClassDataAccess = new SchoolClassDataAccess(_dbContext);
                }

                return _schoolClassDataAccess;
            }
        }


        public ISectionDataAccess SectionDataAccess
        {
            get
            {
                if (_sectiontDataAccess is null)
                {
                    _sectiontDataAccess = new SectionDataAccess(_dbContext);
                }

                return _sectiontDataAccess;
            }
        }


        public IStaffDataAccess StaffDataAccess
        {
            get
            {
                if (_staffDataAccess is null)
                {
                    _staffDataAccess = new StaffDataAccess(_dbContext);
                }

                return _staffDataAccess;
            }
        }
        public IStaffLoginDataAccess StaffLoginDataAccess
        {
            get
            {
                if (_staffLoginDataAccess is null)
                {
                    _staffLoginDataAccess = new StaffLoginDataAccess(_dbContext);
                }

                return _staffLoginDataAccess;
            }
        }
        public IStaffJobDetailsDataAccess StaffJobDetailsDataAccess
        {
            get
            {
                if (_staffJobDetailsDataAccess is null)
                {
                    _staffJobDetailsDataAccess = new StaffJobDetailsDataAccess(_dbContext);
                }

                return _staffJobDetailsDataAccess;
            }
        }

        public ITeacherDataAccess TeacherDataAccess
        {
            get
            {
                if (_teacherDataAccess is null)
                {
                    _teacherDataAccess = new TeacherDataAccess(_dbContext);
                }

                return _teacherDataAccess;
            }
        }
        public IStudentDataAccess StudentDataAccess
        {
            get
            {
                if (_studentDataAccess is null)
                {
                    _studentDataAccess = new StudentDataAccess(_dbContext);
                }

                return _studentDataAccess;
            }
        }
        public IStudentLoginDataAccess StudentLoginDataAccess
        {
            get
            {
                if (_studentLoginDataAccess is null)
                {
                    _studentLoginDataAccess = new StudentLoginDataAccess(_dbContext);
                }

                return _studentLoginDataAccess;
            }
        }
        public IGuardianDataAccess GuardianDataAccess
        {
            get
            {
                if (_guardianDataAccess is null)
                {
                    _guardianDataAccess = new GuardianDataAccess(_dbContext);
                }

                return _guardianDataAccess;
            }
        }
        public IGuardianLoginDataAccess GuardianLoginDataAccess
        {
            get
            {
                if (_guardianLoginDataAccess is null)
                {
                    _guardianLoginDataAccess = new GuardianLoginDataAccess(_dbContext);
                }

                return _guardianLoginDataAccess;
            }
        }
        public IStudentSchoolDetailsDataAccess StudentSchoolDetailsDataAccess
        {
            get
            {
                if (_studentSchoolDetailsDataAccess is null)
                {
                    _studentSchoolDetailsDataAccess = new StudentSchoolDetailsDataAccess(_dbContext);
                }

                return _studentSchoolDetailsDataAccess;
            }
        }


        public ISubjectDataAccess SubjectDataAccess
        {
            get
            {
                if (_subjectDataAccess is null)
                {
                    _subjectDataAccess = new SubjectDataAccess(_dbContext);
                }

                return _subjectDataAccess;
            }
        }

        public IChatRoomDataAccess ChatRoomDataAccess
        {
            get
            {
                if (_chatRoomDataAccess is null)
                {
                    _chatRoomDataAccess = new ChatRoomDataAccess(_dbContext);
                }

                return _chatRoomDataAccess;
            }
        }
        public IChatRoomMembersDataAccess ChatRoomMembersDataAccess
        {
            get
            {
                if (_chatRoomMembersDataAccess is null)
                {
                    _chatRoomMembersDataAccess = new ChatRoomMembersDataAccess(_dbContext);
                }

                return _chatRoomMembersDataAccess;
            }
        }
        public IChatRoomUserDataAccess ChatRoomUserDataAccess
        {
            get
            {
                if (_chatRoomUserDataAccess is null)
                {
                    _chatRoomUserDataAccess = new ChatRoomUserDataAccess(_dbContext);
                }

                return _chatRoomUserDataAccess;
            }
        }

        public IChatRoomMessageDataAccess ChatRoomMessageDataAccess
        {
            get
            {
                if (_chatRoomMessageDataAccess is null)
                {
                    _chatRoomMessageDataAccess = new ChatRoomMessageDataAccess(_dbContext);
                }

                return _chatRoomMessageDataAccess;
            }
        }
        public IChatRoomSettingDataAccess ChatRoomSettingDataAccess
        {
            get
            {
                if (_chatRoomSettingDataAccess is null)
                {
                    _chatRoomSettingDataAccess = new ChatRoomSettingDataAccess(_dbContext);
                }

                return _chatRoomSettingDataAccess;
            }
        }


        public ICurriculumChatRoomsDataAccess CurriculumChatRoomsDataAccess
        {
            get
            {
                if (_curriculumChatRoomsDataAccess is null)
                {
                    _curriculumChatRoomsDataAccess = new CurriculumChatRoomsDataAccess(_dbContext);
                }

                return _curriculumChatRoomsDataAccess;
            }
        }

        public IClassChatRoomsDataAccess ClassChatRoomsDataAccess
        {
            get
            {
                if (_classChatRoomsDataAccess is null)
                {
                    _classChatRoomsDataAccess = new ClassChatRoomsDataAccess(_dbContext);
                }

                return _classChatRoomsDataAccess;
            }
        }
        public ISectionChatRoomsDataAccess SectionChatRoomsDataAccess
        {
            get
            {
                if (_sectionChatRoomsDataAccess is null)
                {
                    _sectionChatRoomsDataAccess = new SectionChatRoomsDataAccess(_dbContext);
                }

                return _sectionChatRoomsDataAccess;
            }
        }
        public ISubjectChatRoomsDataAccess SubjectChatRoomsDataAccess
        {
            get
            {
                if (_subjectChatRoomsDataAccess is null)
                {
                    _subjectChatRoomsDataAccess = new SubjectChatRoomsDataAccess(_dbContext);
                }

                return _subjectChatRoomsDataAccess;
            }
        }
        public IUserChatRoomsDataAccess UserChatRoomsDataAccess
        {
            get
            {
                if (_userChatRoomsDataAccess is null)
                {
                    _userChatRoomsDataAccess = new UserChatRoomsDataAccess(_dbContext);
                }

                return _userChatRoomsDataAccess;
            }
        }



        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}