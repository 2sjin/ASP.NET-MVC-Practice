using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers {
    public class BoardController : Controller {     // 컨트롤러
        public ActionResult List(int? id) {                // 액션
            if (id == null)
                return Content("Error!!!");

            return View();
        }
    }
}
