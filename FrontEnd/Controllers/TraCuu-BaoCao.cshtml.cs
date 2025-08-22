using System.Diagnostics;
using home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TraCuuBaoCao.Controllers
{
    public class TraCuuBaoCaoController : Controller
    {
        private readonly ILogger<TraCuuBaoCaoController> _logger;

        public TraCuuBaoCaoController(ILogger<TraCuuBaoCaoController> logger)
        {
            _logger = logger;
        }

        [Route("TraCuuBaoCao")]
        public IActionResult DuyetDangKy()
        {
            return View("Areas/Admin/Views/TraCuu-BaoCao.cshtml");
        }
    }
}