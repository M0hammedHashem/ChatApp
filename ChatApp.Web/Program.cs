using ChatApp.Core.DataAccess;
using ChatApp.Core.DataService;
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;
using ChatApp.Web.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChatApp_DbContext>(opt => opt.UseSqlServer(cs));

// --- REMOVED: All Identity-related services are no longer needed ---
// builder.Services.AddDbContext<WebIdentityDbContext>(...);
// builder.Services.AddIdentity<IdentityUser, IdentityRole>(...);

// --- ADD: Configure Cookie Authentication ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // The path to your custom login page
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // How long the login session lasts
        options.SlidingExpiration = true;
    });

#region DI Services
// MOVED: ChatApp_DbContext registration here, BEFORE app.Build()
// All service registrations must occur before builder.Build()
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDataAccessFactory, DataAccessFactory>();
builder.Services.AddScoped<IChatApp_DbContext, ChatApp_DbContext>();
builder.Services.AddScoped<IChatAppDataServiceFactory, ChatAppDataServiceFactory>();
builder.Services.AddScoped<ITeacherDataService, TeacherDataService>();
builder.Services.AddScoped<IStudentDataService, StudentDataService>();
builder.Services.AddScoped<IStudentLoginDataService, StudentLoginDataService>();
builder.Services.AddScoped<IStudentSchoolDetailsDataService, StudentSchoolDetailsDataService>();
builder.Services.AddScoped<IGuardianDataService, GuardianDataService>();
builder.Services.AddScoped<IGuardianLoginDataService, GuardianLoginDataService>();
builder.Services.AddScoped<IChatRoomDataService, ChatRoomDataService>();
builder.Services.AddScoped<IChatRoomMembersDataService, ChatRoomMembersDataService>();
builder.Services.AddScoped<IChatRoomUserDataService, ChatRoomUserDataService>();
builder.Services.AddScoped<IChatRoomMessageDataService, ChatRoomMessageDataService>();
builder.Services.AddScoped<IChatRoomSettingsDataService, ChatRoomSettingsDataService>();
builder.Services.AddScoped<ISchoolBranchDataService, SchoolBranchDataService>();
builder.Services.AddScoped<ICurriculumDataService, CurriculumDataService>();
builder.Services.AddScoped<IDepartmentDataService, DepartmentDataService>();
builder.Services.AddScoped<ICurriculumDepartmentDataService, CurriculumDepartmentDataService>();
builder.Services.AddScoped<ISectionDataService, SectionDataService>();
builder.Services.AddScoped<IStaffDataService, StaffDataService>();
builder.Services.AddScoped<IStaffLoginDataService, StaffLoginDataService>();
builder.Services.AddScoped<IStaffJobDetailsDataService, StaffJobDetailsDataService>();
builder.Services.AddScoped<ISchoolClassDataService, SchoolClassDataService>();
builder.Services.AddScoped<ISubjectDataService, SubjectDataService>();
#endregion

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Ensure static files are served

app.UseRouting();

// **IMPORTANT:** These two must be in this order
app.UseAuthentication(); // First, who is the user?
app.UseAuthorization();  // Second, what are they allowed to do?

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Default to the login page

app.MapRazorPages();
app.MapHub<ChatHub>("/hubs/chat");

app.Run();

