using System.Diagnostics;
using home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DuyetDangKy.Controllers
{
    public class DuyetDangKyController : Controller
    {
        private readonly ILogger<DuyetDangKyController> _logger;

        public DuyetDangKyController(ILogger<DuyetDangKyController> logger)
        {
            _logger = logger;
        }

        [Route("DuyetDangKy")]
        public IActionResult DuyetDangKy()
        {
            return View("Areas/Admin/Views/DuyetDangKy.cshtml");
        }
    }
}