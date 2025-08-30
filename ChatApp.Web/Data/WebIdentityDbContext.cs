using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Web.Data
{
    public class WebIdentityDbContext : IdentityDbContext
    {
        public WebIdentityDbContext(DbContextOptions<WebIdentityDbContext> options)
            : base(options) { }
    }
}
