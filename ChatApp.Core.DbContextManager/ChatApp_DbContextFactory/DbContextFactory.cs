namespace ChatApp.Core.DbContextManager
{
    public sealed class DbContextFactory
    {
        private static ChatApp_DbContext ChatApp_DbContext { get; set; }
        private DbContextFactory(ChatApp_DbContext _DbContext)
        {
            ChatApp_DbContext = _DbContext;
        }




        public static async Task EnsureChatAppDatabaseAsync()
        {

            //using var ctx = new ChatApp_DbContext();
            //await ctx.Database.EnsureCreatedAsync();

        }

        public static IChatApp_DbContext Create_ChatApp_DbContext()

        {
            return ChatApp_DbContext;
        }

    }
}

