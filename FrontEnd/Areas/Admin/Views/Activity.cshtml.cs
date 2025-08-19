using System.Diagnostics;
using home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Activity.Controllers
{
    public class ActivityController : Controller
    {
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(ILogger<ActivityController> logger)
        {
            _logger = logger;
        }

        [Route("Activity")]
        public IActionResult Activity()
        {
            return View("Areas/Admin/Views/Activity.cshtml");
        }
    }
}