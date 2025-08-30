using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    public class DataAccessFactory : IDataAccessFactory
    {
        private readonly IUnitOfWork _unitOfWork;

        public DataAccessFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
        private IGuardianDataAccess _guardianDataAccess;
        private IGuardianLoginDataAccess _guardianLoginDataAccess;
        private IStudentSchoolDetailsDataAccess _studentSchoolDetailsDataAccess;
        private ISubjectDataAccess _subjectDataAccess;
        private IChatRoomUserDataAccess _chatRoomUserDataAccess;
        private IChatRoomDataAccess _chatRoomDataAccess;
        private IChatRoomMembersDataAccess _chatRoomMembersDataAccess;
        private IChatRoomMessageDataAccess _chatRoomMessageDataAccess;
        private IChatRoomSettingDataAccess _chatRoomSettingDataAccess;
        private ICurriculumChatRoomsDataAccess _curriculumChatRoomsDataAccess;
        private IClassChatRoomsDataAccess _classChatRoomsDataAccess;
        private ISectionChatRoomsDataAccess _sectionChatRoomsDataAccess;
        private ISubjectChatRoomsDataAccess _subjectChatRoomsDataAccess;
        private IUserChatRoomsDataAccess _userChatRoomsDataAccess;


        public ISchoolBranchDataAccess CreateSchoolBranchDataAccess
        {
            get
            {
                if (_schoolBranchDataAccess is null)
                {
                    _schoolBranchDataAccess = _unitOfWork.SchoolBranchDataAccess;
                }

                return _schoolBranchDataAccess;
            }
        }

        public ICurriculumDataAccess CreateCurriculumDataAccess
        {
            get
            {
                if (_curriculumDataAccess is null)
                {
                    _curriculumDataAccess = _unitOfWork.CurriculumDataAccess;
                }

                return _curriculumDataAccess;
            }
        }
        public IDepartmentDataAccess CreateDepartmentDataAccess
        {
            get
            {
                if (_departmentDataAccess is null)
                {
                    _departmentDataAccess = _unitOfWork.DepartmentDataAccess;
                }

                return _departmentDataAccess;
            }
        }
        public ICurriculumDepartmentDataAccess CreateCurriculumDepartmentDataAccess
        {
            get
            {
                if (_curriculumDepartmentDataAccess is null)
                {
                    _curriculumDepartmentDataAccess = _unitOfWork.CurriculumDepartmentDataAccess;
                }

                return _curriculumDepartmentDataAccess;
            }
        }

        public ISchoolClassDataAccess CreateSchoolClassDataAccess
        {
            get
            {
                if (_schoolClassDataAccess is null)
                {
                    _schoolClassDataAccess = _unitOfWork.SchoolClassDataAccess;
                }

                return _schoolClassDataAccess;
            }
        }


        public ISectionDataAccess CreateSectionDataAccess
        {
            get
            {
                if (_sectiontDataAccess is null)
                {
                    _sectiontDataAccess = _unitOfWork.SectionDataAccess;
                }

                return _sectiontDataAccess;
            }
        }


        public IStaffDataAccess CreateStaffDataAccess
        {
            get
            {
                if (_staffDataAccess is null)
                {
                    _staffDataAccess = _unitOfWork.StaffDataAccess;
                }

                return _staffDataAccess;
            }
        }
        public IStaffLoginDataAccess CreateStaffLoginDataAccess
        {
            get
            {
                if (_staffLoginDataAccess is null)
                {
                    _staffLoginDataAccess = _unitOfWork.StaffLoginDataAccess;
                }

                return _staffLoginDataAccess;
            }
        }
        public IStaffJobDetailsDataAccess CreateStaffJobDetailsDataAccess
        {
            get
            {
                if (_staffJobDetailsDataAccess is null)
                {
                    _staffJobDetailsDataAccess = _unitOfWork.StaffJobDetailsDataAccess;
                }

                return _staffJobDetailsDataAccess;
            }
        }

        public ITeacherDataAccess CreateTeacherDataAccess
        {
            get
            {
                if (_teacherDataAccess is null)
                {
                    _teacherDataAccess = _unitOfWork.TeacherDataAccess;
                }

                return _teacherDataAccess;
            }
        }

        public IStudentDataAccess CreateStudentDataAccess
        {
            get
            {
                if (_studentDataAccess is null)
                {
                    _studentDataAccess = _unitOfWork.StudentDataAccess;
                }

                return _studentDataAccess;
            }
        }

        public IStudentLoginDataAccess CreateStudentLoginDataAccess
        {
            get
            {
                if (_studentLoginDataAccess is null)
                {
                    _studentLoginDataAccess = _unitOfWork.StudentLoginDataAccess;
                }

                return _studentLoginDataAccess;
            }
        }
        public IGuardianDataAccess CreateGuardianDataAccess
        {
            get
            {
                if (_guardianDataAccess is null)
                {
                    _guardianDataAccess = _unitOfWork.GuardianDataAccess;
                }

                return _guardianDataAccess;
            }
        }

        public IGuardianLoginDataAccess CreateGuardianLoginDataAccess
        {
            get
            {
                if (_guardianLoginDataAccess is null)
                {
                    _guardianLoginDataAccess = _unitOfWork.GuardianLoginDataAccess;
                }

                return _guardianLoginDataAccess;
            }
        }
        public IStudentSchoolDetailsDataAccess CreateStudentSchoolDetailsDataAccess
        {
            get
            {
                if (_studentSchoolDetailsDataAccess is null)
                {
                    _studentSchoolDetailsDataAccess = _unitOfWork.StudentSchoolDetailsDataAccess;
                }

                return _studentSchoolDetailsDataAccess;
            }
        }
        public ISubjectDataAccess CreateSubjectDataAccess
        {
            get
            {
                if (_subjectDataAccess is null)
                {
                    _subjectDataAccess = _unitOfWork.SubjectDataAccess;
                }

                return _subjectDataAccess;
            }
        }


        public IChatRoomMessageDataAccess CreateChatRoomMessageDataAccess
        {
            get
            {
                if (_chatRoomMessageDataAccess is null)
                {
                    _chatRoomMessageDataAccess = _unitOfWork.ChatRoomMessageDataAccess;
                }

                return _chatRoomMessageDataAccess;
            }
        }

        public IChatRoomDataAccess CreateChatRoomDataAccess
        {
            get
            {
                if (_chatRoomDataAccess is null)
                {
                    _chatRoomDataAccess = _unitOfWork.ChatRoomDataAccess;
                }

                return _chatRoomDataAccess;
            }
        }
        public IChatRoomMembersDataAccess CreateChatRoomMembersDataAccess
        {
            get
            {
                if (_chatRoomMembersDataAccess is null)
                {
                    _chatRoomMembersDataAccess = _unitOfWork.ChatRoomMembersDataAccess;
                }

                return _chatRoomMembersDataAccess;
            }
        }
        public IChatRoomUserDataAccess CreateChatRoomUserDataAccess
        {
            get
            {
                if (_chatRoomUserDataAccess is null)
                {
                    _chatRoomUserDataAccess = _unitOfWork.ChatRoomUserDataAccess;
                }

                return _chatRoomUserDataAccess;
            }
        }
        public IChatRoomSettingDataAccess CreateChatRoomSettingDataAccess
        {
            get
            {
                if (_chatRoomSettingDataAccess is null)
                {
                    _chatRoomSettingDataAccess = _unitOfWork.ChatRoomSettingDataAccess;
                }

                return _chatRoomSettingDataAccess;
            }
        }

        public ICurriculumChatRoomsDataAccess CreateCurriculumChatRoomsDataAccess
        {
            get
            {
                if (_curriculumChatRoomsDataAccess is null)
                {
                    _curriculumChatRoomsDataAccess = _unitOfWork.CurriculumChatRoomsDataAccess;
                }

                return _curriculumChatRoomsDataAccess;
            }
        }

        public IClassChatRoomsDataAccess CreateClassChatRoomsDataAccess
        {
            get
            {
                if (_classChatRoomsDataAccess is null)
                {
                    _classChatRoomsDataAccess = _unitOfWork.ClassChatRoomsDataAccess;
                }

                return _classChatRoomsDataAccess;
            }
        }

        public ISectionChatRoomsDataAccess CreateSectionChatRoomsDataAccess
        {
            get
            {
                if (_sectionChatRoomsDataAccess is null)
                {
                    _sectionChatRoomsDataAccess = _unitOfWork.SectionChatRoomsDataAccess;
                }

                return _sectionChatRoomsDataAccess;
            }
        }

        public ISubjectChatRoomsDataAccess CreateSubjectChatRoomsDataAccess
        {
            get
            {
                if (_subjectChatRoomsDataAccess is null)
                {
                    _subjectChatRoomsDataAccess = _unitOfWork.SubjectChatRoomsDataAccess;
                }

                return _subjectChatRoomsDataAccess;
            }
        }

        public IUserChatRoomsDataAccess CreateUserChatRoomsDataAccess
        {
            get
            {
                if (_userChatRoomsDataAccess is null)
                {
                    _userChatRoomsDataAccess = _unitOfWork.UserChatRoomsDataAccess;
                }

                return _userChatRoomsDataAccess;
            }
        }
    }
}