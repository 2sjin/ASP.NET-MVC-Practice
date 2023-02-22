using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            ViewBag.Arr = new string[] { "alpha", "beta", "gamma" };
            ViewBag.Book = new Book {
                Title = "칼의 노래",
                Writer = "김훈"
            };
            return View();
        }

        /*
        public string Index() {
            return "Hello World!";
        }
        */
    }
}
