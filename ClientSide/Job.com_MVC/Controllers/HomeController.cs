using JobSearchApp_MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobSearchApp_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            StoredData.StoredJWToken = "";
            StoredData.StoredEmail = "";
            StoredData.StoredId = 0;
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

        public IActionResult ConsumerLogin()
        {
            return View();
        }

        public IActionResult VendorLogin()
        {
            return View();
        }

        public IActionResult CandidateLogin()
        {
            return View();
        }

        public IActionResult ConsumerRegistration()
        {
            return View();
        }

        public IActionResult VendorRegistration()
        {
            return View();
        }

        public IActionResult CandidateRegistration()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index");
        }
    }
}