namespace ChatApp.Core.DbContextManager
{
    internal class Helper_Connection
    {
        public static string ConnectionString() => "Data Source=.;Initial Catalog=ChatApp;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";

    }
}
