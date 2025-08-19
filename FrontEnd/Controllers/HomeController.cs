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

        [Route("TrangChu")]
        public IActionResult TrangChu()
        {
            return View("Areas/Admin/Views/TrangChu.cshtml");
        }

        [Route("login")]
        public IActionResult login()
        {
            return View("Areas/Admin/Views/Login.cshtml");
        }

        [Route("register")]
        public IActionResult register()
        {
            return View("Areas/Admin/Views/Register.cshtml");
        }

        [Route("forgot-password")]
        public IActionResult forgot_password()
        {
            return View("Areas/Admin/Views/Forgot-password.cshtml");
        }

        [Route("404")]
        public IActionResult NotFound()
        {
            return View("Areas/Admin/Views/404.cshtml");
        }

        [Route("DuyetDangKy")]
        public IActionResult DuyetDangKy()
        {
            return View("Areas/Admin/Views/DuyetDangKy.cshtml");
        }

        [Route("TraCuuBaoCao")]
        public IActionResult TraCuuBaoCao()
        {
            return View("Areas/Admin/Views/TraCuu-BaoCao.cshtml");
        }

        [Route("QuanLyGopY")]
        public IActionResult QuanLyGopY()
        {
            return View("Areas/Admin/Views/QuanLyGopY.cshtml");
        }
        [Route("Activity")]
        public IActionResult Activity()
        {
            return View("Areas/Admin/Views/Activity.cshtml");
        }
        public IActionResult Privacy()
        {
            return View();
        }

    }
}
