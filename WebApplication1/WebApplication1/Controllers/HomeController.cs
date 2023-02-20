using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers {
    public class HomeController : Controller {
        /*
        public IActionResult Index() {
            return View();
        }
        */

        public string Index() {
            return "Hello World!";
        }
    }
}
