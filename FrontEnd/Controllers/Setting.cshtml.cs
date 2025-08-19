using System.Diagnostics;
using home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Setting.Controllers
{
    public class SettingController : Controller
    {
        private readonly ILogger<SettingController> _logger;

        public SettingController(ILogger<SettingController> logger)
        {
            _logger = logger;
        }

        [Route("Setting")]
        public IActionResult Setting()
        {
            return View("Areas/Admin/Views/Setting.cshtml");
        }
    }
}