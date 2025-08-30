using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;
using ChatApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IChatAppDataServiceFactory _ds;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IChatAppDataServiceFactory ds, IUnitOfWork unitOfWork)
        {
            _ds = ds;
            _unitOfWork = unitOfWork;
        }

        private int? GetAdminSchoolId()
        {
            var schoolIdClaim = User.FindFirst("SchoolId");
            if (schoolIdClaim != null && int.TryParse(schoolIdClaim.Value, out int schoolId))
            {
                return schoolId;
            }
            return null; // This user is a Super Admin
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            // await CreateSchoolChatRoomUsers();
            var model = new AdminChatRoomViewModel();

            var adminSchoolId = GetAdminSchoolId();



            //await CreateSchoolChatRoomUsers();
            var schools = await _ds.CreateSchoolBranchService.GetAllAsync();

            if (adminSchoolId.HasValue)
            {
                schools = schools.Where(s => s.SchoolId == adminSchoolId.Value);
                model.SchoolId = adminSchoolId.Value;
            }

            model.Schools = schools
                .Where(s => s != null && !string.IsNullOrEmpty(s.SchoolEnglishName))
                .Select(s => new SelectListItem(s.SchoolEnglishName, s.SchoolId.ToString()))
                .ToList();

            var allCurriculums = await _ds.CreateCurriculumService.GetAllAsync();

            if (adminSchoolId.HasValue)
            {
                allCurriculums = allCurriculums.Where(c => c.SchoolId == adminSchoolId.Value);
            }

            model.Curriculums = allCurriculums
               .Where(c => c != null && !string.IsNullOrEmpty(c.CurriculumEnglishName))
               .Select(c => new SelectListItem(c.CurriculumEnglishName, c.CurriculumId.ToString()))
               .ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetMissingChatUsersCount()
        {
            try
            {
                // 1. Get all unique UserIDs from the main system tables
                var studentIds = (await _ds.CreateStudentLoginService.GetAllAsync()).Select(u => u.StudentId);
                var staffIds = (await _ds.CreateStaffLoginService.GetAllAsync()).Select(u => u.StaffId);
                var guardianIds = (await _ds.CreateGuardianLoginService.GetAllAsync()).Select(u => u.GuardianId);

                // Combine all unique IDs into a single set
                var allSystemUserIds = new HashSet<string>(studentIds);
                allSystemUserIds.UnionWith(staffIds);
                allSystemUserIds.UnionWith(guardianIds);

                // 2. Get all UserIDs that already exist in the ChatRoomUser table
                var existingChatUserIds = (await _ds.CreateChatRoomUserService.GetAllAsync()).Select(u => u.UserId).ToHashSet();

                // 3. Find the UserIDs that are in the system but not in the chat user table
                allSystemUserIds.ExceptWith(existingChatUserIds);

                // 4. Return the count of missing users
                return Json(new { success = true, count = allSystemUserIds.Count });
            }
            catch (Exception)
            {
                // Log the exception ex
                return Json(new { success = false, message = "An error occurred while checking for users." });
            }
        }

        /// <summary>
        /// Creates ChatRoomUser profiles for all system users who do not already have one.
        /// This method is now called via AJAX and returns a JSON result.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateAllChatUsers()
        {
            try
            {
                int createdCount = await CreateSchoolChatRoomUsers();
                if (createdCount > 0)
                {
                    return Json(new { success = true, message = $"Successfully created {createdCount} new chat user profiles." });
                }
                else
                {
                    return Json(new { success = true, message = "No new users needed to be created. All profiles are up to date." });
                }
            }
            catch (Exception)
            {
                // Log the exception ex
                return Json(new { success = false, message = "An error occurred while creating user profiles." });
            }
        }

        /// <summary>
        /// Gathers all users from different parts of the school system,
        /// checks if they exist as chat users by their unique UserId, creates profiles for those who are missing,
        /// and returns the count of newly created users.
        /// </summary>
        /// <returns>The number of users that were created.</returns>
        private async Task<int> CreateSchoolChatRoomUsers()
        {
            var allChatRoomUsers = new List<ChatRoomUser_DTO>();

            // 1. Process Students
            var allStudentLogins = await _ds.CreateStudentLoginService.GetAllAsync();
            var allStudents = await _ds.CreateStudentService.GetAllAsync();
            var studentUsers = from login in allStudentLogins
                               join student in allStudents on login.StudentId equals student.StudentId
                               select new ChatRoomUser_DTO
                               {
                                   UserId = login.StudentId,
                                   UserName = login.Username,
                                   Name = student.StudentEnglishName,
                                   UserType = ChatterType.Student
                               };
            allChatRoomUsers.AddRange(studentUsers);

            // 2. Process Staff (including Teachers)
            var allStaffLogins = await _ds.CreateStaffLoginService.GetAllAsync();
            var allStaff = await _ds.CreateStaffService.GetAllAsync();
            var teacherIds = (await _ds.CreateTeacherService.GetAllAsync()).Select(t => t.TeacherId).ToHashSet();

            var staffUsers = from login in allStaffLogins
                             join staff in allStaff on login.StaffId equals staff.StaffId
                             select new ChatRoomUser_DTO
                             {
                                 UserId = login.StaffId,
                                 UserName = login.UserName,
                                 Name = staff.StaffEnglishName,
                                 UserType = teacherIds.Contains(login.StaffId) ? ChatterType.Teacher : ChatterType.Staff
                             };
            allChatRoomUsers.AddRange(staffUsers);

            // 3. Process Guardians
            var allGuardianLogins = await _ds.CreateGuardianLoginService.GetAllAsync();
            var guardianUsers = allGuardianLogins.Select(g => new ChatRoomUser_DTO
            {
                UserId = g.GuardianId,
                UserName = g?.UserName ?? "",
                Name = g?.UserName ?? "", // Assuming Guardian name is their username for now
                UserType = ChatterType.Guardian
            });
            allChatRoomUsers.AddRange(guardianUsers);

            var count = allChatRoomUsers.Count(c => c.UserName == null);
            // 4. Save to database
            if (allChatRoomUsers.Any())
            {
                await _ds.CreateChatRoomUserService.AddRangeAsync(allChatRoomUsers);
                await _unitOfWork.SaveChangesAsync();
            }
            return 0;
        }



        #region Create Room Actions

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchoolRoom(AdminChatRoomViewModel model)
        {
            var targetSchoolId = GetAdminSchoolId() ?? model.SchoolId;
            var membersToAdd = new List<ChatRoomUser_DTO>();

            if (model.IncludeStudents) membersToAdd.AddRange(await GetChatRoomUsersForSchool(targetSchoolId, ChatterType.Student));
            if (model.IncludeTeachers) membersToAdd.AddRange(await GetChatRoomUsersForSchool(targetSchoolId, ChatterType.Teacher));
            if (model.IncludeStaff) membersToAdd.AddRange(await GetChatRoomUsersForSchool(targetSchoolId, ChatterType.Staff));
            if (model.IncludeGuardians) membersToAdd.AddRange(await GetChatRoomUsersForSchool(targetSchoolId, ChatterType.Guardian));

            return await CreateRoomAndAddMembers(model.RoomName, targetSchoolId, ChatRoomType.School, membersToAdd.Distinct().ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCurriculumRoom(AdminChatRoomViewModel model)
        {
            var targetSchoolId = GetAdminSchoolId() ?? model.SchoolId;
            var membersToAdd = new List<ChatRoomUser_DTO>();

            if (model.IncludeStudents) membersToAdd.AddRange(await GetChatRoomUsersForCurriculum(model.CurriculumId, ChatterType.Student));
            if (model.IncludeTeachers) membersToAdd.AddRange(await GetChatRoomUsersForCurriculum(model.CurriculumId, ChatterType.Teacher));

            return await CreateRoomAndAddMembers(model.RoomName, targetSchoolId, ChatRoomType.StudentCurriculum, membersToAdd.Distinct().ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClassRoom(AdminChatRoomViewModel model)
        {
            var targetSchoolId = GetAdminSchoolId() ?? model.SchoolId;
            var membersToAdd = new List<ChatRoomUser_DTO>();

            if (model.IncludeStudents) membersToAdd.AddRange(await GetChatRoomUsersForClass(targetSchoolId, model.SchoolClassId, ChatterType.Student));
            if (model.IncludeTeachers) membersToAdd.AddRange(await GetChatRoomUsersForClass(targetSchoolId, model.SchoolClassId, ChatterType.Teacher));

            return await CreateRoomAndAddMembers(model.RoomName, targetSchoolId, ChatRoomType.StudentClass, membersToAdd.Distinct().ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSectionRoom(AdminChatRoomViewModel model)
        {
            var targetSchoolId = GetAdminSchoolId() ?? model.SchoolId;
            var membersToAdd = new List<ChatRoomUser_DTO>();

            if (model.IncludeStudents) membersToAdd.AddRange(await GetChatRoomUsersForSection(model.SectionId, ChatterType.Student));
            if (model.IncludeTeachers) membersToAdd.AddRange(await GetChatRoomUsersForSection(model.SectionId, ChatterType.Teacher));

            return await CreateRoomAndAddMembers(model.RoomName, targetSchoolId, ChatRoomType.Section, membersToAdd.Distinct().ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubjectRoom(AdminChatRoomViewModel model)
        {
            var targetSchoolId = GetAdminSchoolId() ?? model.SchoolId;
            var membersToAdd = new List<ChatRoomUser_DTO>();

            if (model.IncludeStudents) membersToAdd.AddRange(await GetChatRoomUsersForSubject(model.SubjectId, ChatterType.Student));
            if (model.IncludeTeachers) membersToAdd.AddRange(await GetChatRoomUsersForSubject(model.SubjectId, ChatterType.Teacher));

            return await CreateRoomAndAddMembers(model.RoomName, targetSchoolId, ChatRoomType.Subject, membersToAdd.Distinct().ToList());
        }


        #endregion


        #region Helper Methods to Get Users


        #region Students Helpers
        private async Task<List<string>> GetStudentIdsForSchool(int schoolId)
        {
            var allDetails = await _ds.CreateStudentSchoolDetailsService.GetAllAsync();
            return allDetails.Where(s => s.SchoolId == schoolId).Select(s => s.StudentId).ToList();
        }

        private async Task<List<string>> GetStudentIdsForCurriculum(int curriculumId)
        {
            var classes = await _ds.CreateSchoolClassService.GetSpecificPropertyAsync(c => c.CurriculumId == curriculumId, c => c.SchoolClassId);
            var StudentIds = new List<string>();

            foreach (var classId in classes)
            {

                var students = await GetStudentIdsForClass(classId);
                StudentIds.AddRange(students);
            }
            return StudentIds;
        }

        private async Task<List<string>> GetStudentIdsForClass(int classId)
        {

            var allDetails = await _ds.CreateStudentSchoolDetailsService.Where(s => s.ClassId == classId);

            return allDetails.Select(s => s.StudentId).ToList();

        }
        private async Task<List<string>> GetStudentIdsForSection(int sectionId)
        {

            var allDetails = await _ds.CreateStudentSchoolDetailsService.Where(s => s.SectionId == sectionId);

            return allDetails.Select(s => s.StudentId).ToList();

        }

        #endregion


        #region Staff Helpers
        private async Task<List<string>> GetStaffIdsForSchool(int schoolId, bool teachersOnly = false, bool staffOnly = false)
        {
            var allStaffLogins = await _ds.CreateStaffLoginService.GetAllAsync();
            var schoolStaff = allStaffLogins.Where(s => s.SchoolId == schoolId).ToList();

            if (!teachersOnly && !staffOnly) return schoolStaff.Select(s => s.StaffId).ToList();

            var allTeachers = await _ds.CreateTeacherService.GetAllAsync();
            var teacherIds = allTeachers.Select(t => t.TeacherId).ToHashSet();

            if (teachersOnly) return schoolStaff.Where(s => teacherIds.Contains(s.StaffId)).Select(s => s.StaffId).ToList();
            if (staffOnly) return schoolStaff.Where(s => !teacherIds.Contains(s.StaffId)).Select(s => s.StaffId).ToList();

            return new List<string>();
        }

        private async Task<List<string>> GetStaffIdsForCurriculum(int curriculumId, bool teachersOnly = false, bool staffOnly = false)
        {
            var TeachersIds = (await _ds.CreateTeacherService.GetSpecificPropertyAsync(t => t.CurriculumId == curriculumId, t => t.TeacherId)).ToList();

            return TeachersIds;
        }
        private async Task<List<string>> GetStaffIdsForClass(int classId, bool teachersOnly = false, bool staffOnly = false)
        {
            var TeachersIds = (await _ds.CreateTeacherService.GetSpecificPropertyAsync(t => t.SchoolClassId == classId, t => t.TeacherId)).ToList();

            return TeachersIds;
        }
        private async Task<List<string>> GetStaffIdsForSubject(int subjectId, bool teachersOnly = false, bool staffOnly = false)
        {
            var TeachersIds = (await _ds.CreateTeacherService.GetSpecificPropertyAsync(t => t.SubjectId == subjectId, t => t.TeacherId)).ToList();

            return TeachersIds;
        }

        #endregion 


        #region Guardians
        private async Task<List<string>> GetGuardianIdsForSchool(int schoolId)
        {
            var allGuardians = await _ds.CreateGuardianLoginService.GetSpecificPropertyAsync(g => g.SchoolId == schoolId, g => g.GuardianId);
            return allGuardians.ToList()!;
        }
        private async Task<List<string>> GetGuardiansIdsForCurriculum(int curriculumId)
        {
            var CurriculumStudentsIds = await GetStudentIdsForCurriculum(curriculumId);
            var Students = await _ds.CreateStudentService.GetAllAsync();

            var GuardianIds = Students.Where(s => CurriculumStudentsIds.Contains(s.StudentId)).Select(s => s.GuardianId.ToString()).ToList();
            return GuardianIds;

        }
        private async Task<List<string>> GetGuardiansIdsForClass(int classId)
        {
            var CurriculumStudentsIds = await GetStudentIdsForClass(classId);
            var Students = await _ds.CreateStudentService.GetAllAsync();

            var GuardianIds = Students.Where(s => CurriculumStudentsIds.Contains(s.StudentId)).Select(s => s.GuardianId.ToString()).ToList();
            return GuardianIds;

        }
        private async Task<List<string>> GetGuardiansIdsForSection(int sectionId)
        {

            var CurriculumStudentsIds = await GetStudentIdsForClass(sectionId);
            var Students = await _ds.CreateStudentService.GetAllAsync();

            var GuardianIds = Students.Where(s => CurriculumStudentsIds.Contains(s.StudentId)).Select(s => s.GuardianId.ToString()).ToList();
            return GuardianIds;
        }


        #endregion

        #endregion


        #region ChatRoomUser Helpers
        private async Task<List<ChatRoomUser_DTO>> GetChatRoomUsersByIds(IEnumerable<string> userIds, ChatterType userType)
        {
            if (userIds == null || !userIds.Any()) return new List<ChatRoomUser_DTO>();

            var allUsersOfType = await _ds.CreateChatRoomUserService.Where(u => u.UserType == userType);
            return allUsersOfType.Where(u => userIds.Contains(u.UserId)).ToList();
        }


        // --- Get ChatRoomUsers by criteria ---
        private async Task<List<ChatRoomUser_DTO>> GetChatRoomUsersForSchool(int schoolId, ChatterType userType)
        {
            if (userType == ChatterType.Student)
            {
                var studentIds = await GetStudentIdsForSchool(schoolId);
                return await GetChatRoomUsersByIds(studentIds, ChatterType.Student);
            }
            if (userType == ChatterType.Guardian)
            {
                var guardianIds = await GetGuardianIdsForSchool(schoolId);
                return await GetChatRoomUsersByIds(guardianIds, ChatterType.Guardian);
            }
            if (userType == ChatterType.Teacher)
            {
                var teacherIds = await GetStaffIdsForSchool(schoolId, true);
                return await GetChatRoomUsersByIds(teacherIds, ChatterType.Teacher);
            }
            if (userType == ChatterType.Staff)
            {
                var staffIds = await GetStaffIdsForSchool(schoolId, false, true);
                return await GetChatRoomUsersByIds(staffIds, ChatterType.Staff);
            }
            return new List<ChatRoomUser_DTO>();
        }

        private async Task<List<ChatRoomUser_DTO>> GetChatRoomUsersForCurriculum(int curriculumId, ChatterType userType)
        {
            var allClassIdsInCurriculum = (await _ds.CreateSchoolClassService.Where(sc => sc.CurriculumId == curriculumId)).Select(sc => sc.SchoolClassId).ToList();

            if (userType == ChatterType.Student)
            {
                var allStudentDetails = await _ds.CreateStudentSchoolDetailsService.GetAllAsync();
                var studentIds = allStudentDetails.Where(s => allClassIdsInCurriculum.Contains(s.ClassId)).Select(s => s.StudentId).ToList();
                return await GetChatRoomUsersByIds(studentIds, ChatterType.Student);
            }
            else // Teacher
            {
                var allTeachers = await _ds.CreateTeacherService.GetAllAsync();
                var teacherIds = allTeachers.Where(t => allClassIdsInCurriculum.Contains(t.SchoolClassId)).Select(t => t.TeacherId).ToList();
                return await GetChatRoomUsersByIds(teacherIds, ChatterType.Teacher);
            }




        }

        private async Task<List<ChatRoomUser_DTO>> GetChatRoomUsersForClass(int schoolId, int classId, ChatterType userType)
        {
            if (userType == ChatterType.Student)
            {
                var allStudentDetails = await _ds.CreateStudentSchoolDetailsService.GetAllAsync();
                var studentIds = allStudentDetails.Where(s => s.SchoolId == schoolId && s.ClassId == classId).Select(s => s.StudentId).ToList();
                return await GetChatRoomUsersByIds(studentIds, ChatterType.Student);
            }
            else // Teacher
            {
                var allTeachers = await _ds.CreateTeacherService.GetAllAsync();
                var teacherIds = allTeachers.Where(t => t.SchoolClassId == classId).Select(t => t.TeacherId).ToList();
                return await GetChatRoomUsersByIds(teacherIds, ChatterType.Teacher);
            }
        }

        private async Task<List<ChatRoomUser_DTO>> GetChatRoomUsersForSection(int sectionId, ChatterType userType)
        {
            if (userType == ChatterType.Student)
            {
                var allStudentDetails = await _ds.CreateStudentSchoolDetailsService.GetAllAsync();
                var studentIds = allStudentDetails.Where(s => s.SectionId == sectionId).Select(s => s.StudentId).ToList();
                return await GetChatRoomUsersByIds(studentIds, ChatterType.Student);
            }
            else // Teacher
            {
                var teacherIds = new List<string>();
                return await GetChatRoomUsersByIds(teacherIds, ChatterType.Teacher);
            }
        }

        private async Task<List<ChatRoomUser_DTO>> GetChatRoomUsersForSubject(int subjectId, ChatterType userType)
        {
            if (userType == ChatterType.Student)
            {
                var allSubjects = await _ds.CreateSubjectService.GetAllAsync();
                var sectionId = allSubjects.FirstOrDefault(s => s.SubjectId == subjectId)?.SectionId;
                if (sectionId.HasValue)
                {
                    return await GetChatRoomUsersForSection(sectionId.Value, ChatterType.Student);
                }
                return new List<ChatRoomUser_DTO>();
            }
            else // Teacher
            {
                var allTeachers = await _ds.CreateTeacherService.GetAllAsync();
                var teacherIds = allTeachers.Where(t => t.SubjectId == subjectId).Select(t => t.TeacherId).ToList();
                return await GetChatRoomUsersByIds(teacherIds, ChatterType.Teacher);
            }
        }



        #endregion


        #region Core Room Creation Logic
        private async Task<IActionResult> CreateRoomAndAddMembers(string roomName, int schoolId, ChatRoomType type, List<ChatRoomUser_DTO> members)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                TempData["ErrorMessage"] = "Room name cannot be empty.";
                return RedirectToAction("Index");
            }
            try
            {
                var roomService = _ds.CreateChatRoomService;
                var roomToCreate = new ChatRoom_DTO
                {
                    EnglishChatRoomName = roomName,
                    ArabicChatRoomName = roomName,
                    SchoolId = schoolId,
                    ChatRoomType = type
                };

                // Step 2: Add the room to the context. 
                var savedRoom = await roomService.AddAsync(roomToCreate);




                if (savedRoom == null)
                {
                    TempData["ErrorMessage"] = "Error: Could not retrieve the newly created room.";
                    return RedirectToAction("Index");
                }

                if (!members.Any())
                {
                    TempData["SuccessMessage"] = $"Successfully created room '{roomName}', but no members were found to add.";
                }
                else
                {
                    // Step 4: Now that savedRoom.ChatRoomId has a valid ID, create the memberships.
                    var membersService = _ds.CreateChatRoomMembersService;
                    var newMemberships = members.Select(member => new ChatRoomMembers_DTO
                    {
                        ChatRoomId = savedRoom.ChatRoomId, // Use the ID from the re-fetched room
                        ChatRoomUserId = member.ChatRoomUserId,
                        ChatRoomUserType = ChatRoomUserType.Member
                    }).ToList();

                    if (newMemberships.Any())
                    {
                        await membersService.AddRangeAsync(newMemberships);
                        // Step 5: Save the new memberships. This is the second transaction.
                        await _unitOfWork.SaveChangesAsync();
                    }

                    TempData["SuccessMessage"] = $"Successfully created room '{roomName}' and added {newMemberships.Count} members. You can now configure its settings.";
                }
                return RedirectToAction("Edit", "ChatRoomSettings", new { roomId = savedRoom.ChatRoomId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        #endregion


        #region AJAX Endpoints
        [HttpGet]
        public async Task<JsonResult> GetCurriculumsForSchool(int schoolId)
        {
            var items = await _ds.CreateCurriculumService.Where(c => c.SchoolId == schoolId);
            return Json(items.Select(c => new { id = c.CurriculumId, name = c.CurriculumEnglishName }).ToList());
        }

        [HttpGet]
        public async Task<JsonResult> GetClassesForCurriculum(int curriculumId)
        {
            var items = await _ds.CreateSchoolClassService.Where(c => c.CurriculumId == curriculumId);
            return Json(items.Select(c => new { id = c.SchoolClassId, name = c.SchoolClassEnglishName }).ToList());
        }

        [HttpGet]
        public async Task<JsonResult> GetSectionsForClass(int classId)
        {
            var items = await _ds.CreateSectionService.Where(s => s.SchoolClassId == classId);
            return Json(items.Select(s => new { id = s.SectionId, name = s.SectionEnglishName }).ToList());
        }

        [HttpGet]
        public async Task<JsonResult> GetSubjectsForClass(int classId)
        {
            var items = await _ds.CreateSubjectService.Where(s => s.SchoolClassId == classId);
            return Json(items.Select(s => new { id = s.SubjectId, name = s.SubjectEnglishName }).ToList());
        }
        #endregion
    }
}
