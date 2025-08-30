using ChatApp.Core.IDataService;
using ChatApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.Web.Controllers
{
    public class HomeController : Controller
    {

        private IChatAppDataServiceFactory _chatAppDataServiceFactory;
        public HomeController(IChatAppDataServiceFactory chatAppDataServiceFactory)
        {
            _chatAppDataServiceFactory = chatAppDataServiceFactory;
        }

        public async Task<IActionResult> Index()
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
