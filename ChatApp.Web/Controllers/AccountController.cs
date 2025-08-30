using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataService;
using ChatApp.Web.ViewModels;
using Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IChatAppDataServiceFactory _ds;
        private readonly IConfiguration _configuration;

        public AccountController(IChatAppDataServiceFactory ds, IConfiguration configuration)
        {
            _ds = ds;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                // If user is already logged in, redirect them to the appropriate dashboard
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Chat");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            model.Password = EncryptionDecryption.Encrypt(model.Password);


            var claims = new List<Claim>();
            string redirectController = "Chat";
            string redirectAction = "Index";

            // 1. Check for Super Admin
            if (model.UserName == _configuration["SuperAdmin:Username"] && model.Password == _configuration["SuperAdmin:Password"])
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, model.UserName));
                claims.Add(new Claim(ClaimTypes.Name, "Super Administrator"));
                claims.Add(new Claim(ClaimTypes.Role, "Admin")); // Super Admin gets the "Admin" role
                claims.Add(new Claim("Password", model.Password));

                redirectController = "Admin";
            }
            else
            {
                // 2. Check for Staff (including School Admins)
                var staffService = _ds.CreateStaffLoginService;
                var staffMember = (await staffService.Where(s => s.UserName == model.UserName && s.Password == model.Password)).FirstOrDefault();

                if (staffMember != null)
                {
                    var staff = (await _ds.CreateStaffService.Where(s => s.StaffId == staffMember.StaffId)).FirstOrDefault();

                    claims.Add(new Claim(ClaimTypes.NameIdentifier, staffMember.UserName));
                    claims.Add(new Claim(ClaimTypes.Name, staff?.StaffEnglishName ?? "Staff Does't Saved in the Staff Table"));

                    claims.Add(new Claim("Password", model.Password));

                    // Check if the staff member is a School Admin
                    if (staffMember.StaffType == StaffType.Administrator) // Assuming a boolean 'IsAdmin' property on your Staff DTO
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        if (staffMember.SchoolId != 1000000)
                            claims.Add(new Claim("SchoolId", staffMember.SchoolId.ToString())); // Add their SchoolId claim

                        redirectController = "Admin";
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Staff"));
                    }
                }
                else
                {
                    // 3. Check for Students
                    var studentLoginService = _ds.CreateStudentLoginService;
                    var studentMember = (await studentLoginService.Where(u => u.Username == model.UserName && u.Password == model.Password)).FirstOrDefault();

                    if (studentMember != null)
                    {
                        var student = (await _ds.CreateStudentService.Where(s => s.StudentId == studentMember.StudentId)).FirstOrDefault();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, studentMember.Username));
                        claims.Add(new Claim(ClaimTypes.Name, student?.StudentEnglishName ?? "UnSaved Student in Table Students")); // Or a full name if available
                        claims.Add(new Claim(ClaimTypes.Role, "Student"));
                        claims.Add(new Claim("Password", model.Password));


                    }
                    else
                    {
                        //Guardians
                        var GuardianMember = (await _ds.CreateGuardianLoginService.Where(g => g.UserName == model.UserName && g.Password == model.Password)).FirstOrDefault();

                        if (GuardianMember != null)
                        {

                            var Guardian = (await _ds.CreateGuardianService.Where(g => g.GuardianId == GuardianMember.WebUserId)).FirstOrDefault();
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, GuardianMember.UserName));
                            claims.Add(new Claim(ClaimTypes.Name, Guardian.GuardianEnglishName)); // Or a full name if available
                            claims.Add(new Claim(ClaimTypes.Role, "Guardian"));
                            claims.Add(new Claim("Password", model.Password));

                        }

                    }
                }
            }

            // If any user was found and claims were created, sign them in.
            if (claims.Any())
            {
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = model.RememberMe };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction(redirectAction, redirectController);
            }

            // If no user was found, the login fails.
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Chat");
            }
        }
    }
}
