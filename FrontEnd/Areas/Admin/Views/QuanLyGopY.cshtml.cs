using System.Diagnostics;
using home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace QuanLyGopY.Controllers
{
    public class QuanLyGopYController : Controller
    {
        private readonly ILogger<QuanLyGopYController> _logger;

        public QuanLyGopYController(ILogger<QuanLyGopYController> logger)
        {
            _logger = logger;
        }

        [Route("QuanLyGopY")]
        public IActionResult DuyetDangKy()
        {
            return View("Areas/Admin/Views/QuanLyGopY.cshtml");
        }
    }
}