using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers {
    public class BoardController : Controller {     // 컨트롤러
        public ActionResult List(int? id) {         // 액션
            if (id == null)
                return Content("Error!!!");

            DocumentActs documentActs = new DocumentActs();
            var documents = documentActs.GetDocuments();

            return View(documents);
        }
    }
}
