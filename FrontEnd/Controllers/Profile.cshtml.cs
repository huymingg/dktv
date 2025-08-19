using System.Diagnostics;
using home.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Profile.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        [Route("Profile")]
        public IActionResult Profile()
        {
            return View("Areas/Admin/Views/Profile.cshtml");
        }
    }
}