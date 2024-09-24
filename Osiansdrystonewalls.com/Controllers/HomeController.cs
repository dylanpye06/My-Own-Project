using Microsoft.AspNetCore.Mvc;
using Osiansdrystonewalls.com.Models;
using System.Diagnostics;

namespace Osiansdrystonewalls.com.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        [ActionName("HomePage")]
        public IActionResult HomePage()
        {
            return View("HomePage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ExamplesAndPrices() 
        {
            return View("ExamplesAndPrices");
        }
    }
}
