using Tutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Tutorial.Controllers
{
    public class homeController : Controller
    {
        private readonly ILogger<homeController> _logger;

        public homeController(ILogger<homeController> logger)
        {
            _logger = logger;
        }

        public IActionResult index()
        {
            return View();
        }

        public IActionResult privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}