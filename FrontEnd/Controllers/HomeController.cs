using System.Diagnostics;
using home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace home.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("")]
        public IActionResult PublicHome()
        {
            return View("Views/Home/public_home.cshtml");
        }

        [Route("dky_the")]
        public IActionResult DangKyThe()
        {
            return View("Views/Home/Dky_the.cshtml");
        }

        [Route("tracuu")]
        public IActionResult TraCuu()
        {
            return View("Views/Home/tracuu.cshtml");
        }

        [Route("huong_dan")]
        public IActionResult HuongDan()
        {
            return View("Views/Home/huong_dan.cshtml");
        }

        [Route("gop_y")]
        public IActionResult gop_y()
        {
            return View("Views/Home/gop_y.cshtml");
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
