using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Web.ViewModels
{
    public class AdminChatRoomViewModel
    {
        // --- Properties to Capture Form Input ---
        [Required]
        [Display(Name = "School Name")]

        public int SchoolId { get; set; }
        [Display(Name = "Curriculum Name")]

        public int CurriculumId { get; set; }
        [Display(Name = "Class Name")]

        public int SchoolClassId { get; set; }
        [Display(Name = "Section Name")]

        public int SectionId { get; set; }
        [Display(Name = "Subject Name")]

        public int SubjectId { get; set; }

        [Required]
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }

        // --- NEW: Checklist for user types ---
        [Display(Name = "Include Students")]
        public bool IncludeStudents { get; set; }

        [Display(Name = "Include Teachers")]
        public bool IncludeTeachers { get; set; }

        [Display(Name = "Include Other Staff")]
        public bool IncludeStaff { get; set; }

        [Display(Name = "Include Guardians")]
        public bool IncludeGuardians { get; set; }

        // --- Properties to Populate Dropdowns ---
        public IEnumerable<SelectListItem> Schools { get; set; }
        public IEnumerable<SelectListItem> Curriculums { get; set; }
        public IEnumerable<SelectListItem> Classes { get; set; }
        public IEnumerable<SelectListItem> Sections { get; set; }
        public IEnumerable<SelectListItem> Subjects { get; set; }

        public AdminChatRoomViewModel()
        {
            Schools = new List<SelectListItem>();
            Curriculums = new List<SelectListItem>();
            Classes = new List<SelectListItem>();
            Sections = new List<SelectListItem>();
            Subjects = new List<SelectListItem>();
        }
    }
}
