using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Web.ViewModels
{
    public class ChatRoomSettingsViewModel
    {
        public int ChatRoomId { get; set; }
        public string ChatRoomName { get; set; }

        [Display(Name = "Allow members to send messages")]
        public bool MembersCanSendMessage { get; set; }

        [Display(Name = "Allow members to send attachments")]
        public bool MembersCanSendAttachments { get; set; }


        public IEnumerable<SelectListItem> AllStaff { get; set; }


        public List<string> SelectedAdminUsernames { get; set; }

        public ChatRoomSettingsViewModel()
        {
            AllStaff = new List<SelectListItem>();
            SelectedAdminUsernames = new List<string>();
        }
    }
}
