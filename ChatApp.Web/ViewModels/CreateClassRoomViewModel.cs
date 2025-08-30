using ChatApp.Core.DbContextManager;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatApp.Web.ViewModels
{
    public class CreateClassRoomViewModel
    {
        public int? SelectedSchoolId { get; set; }
        public int? SelectedCurriculumId { get; set; }
        public int? SelectedClassId { get; set; }
        public int? SelectedSectionId { get; set; }
        public int? SelectedSubjectId { get; set; }

        public string RoomName { get; set; }
        public ChatRoomType RoomType { get; set; }

        public List<SelectListItem> Schools { get; set; } = new();
        public List<SelectListItem> Curriculums { get; set; } = new();
        public List<SelectListItem> Classes { get; set; } = new();
        public List<SelectListItem> Sections { get; set; } = new();
        public List<SelectListItem> Subjects { get; set; } = new();
    }
}
