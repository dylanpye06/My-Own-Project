using Microsoft.AspNetCore.Mvc;

namespace Osiansdrystonewalls.com.Controllers
{
    public class CreateAccountController : Controller
    {
        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }
    }
}
