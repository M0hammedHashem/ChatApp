using ChatApp.Core.DbContextManager;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Web.Data
{
    public static class ChatSeed
    {
        // In Data/ChatSeed.cs

        public static async Task RunAsync()
        {
            using var ctx = DbContextFactory.Create_ChatApp_DbContext();

            // 1. Ensure the "General" room exists
            var room = await ctx.Set<ChatRoom>().FirstOrDefaultAsync(r => r.EnglishChatRoomName == "General");
            if (room is null)
            {
                room = new ChatRoom { EnglishChatRoomName = "General" };
                ctx.Set<ChatRoom>().Add(room);
                await ctx.SaveChangesAsync();
            }

            // 2. Add your hardcoded admin user to the "General" room by their username
            var adminUsername = "9942004413"; // Your admin's username

            // Check if this user is already in this room

            bool isAdminMember = await ctx.Set<ChatRoomUser>()
                .AnyAsync(u => u.ChatRoomUserId == room.ChatRoomId && u.UserName == adminUsername);

            if (!isAdminMember)
            {
                // If not, add them
                ctx.Set<ChatRoomUser>().Add(new ChatRoomUser
                {
                    ChatRoomUserId = room.ChatRoomId,
                    UserName = adminUsername // Use the username string
                });
                await ctx.SaveChangesAsync();
            }
        }
    }
}
