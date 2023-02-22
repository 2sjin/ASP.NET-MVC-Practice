using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers {
    public class BoardController : Controller {     // 컨트롤러
        public ActionResult List(int? id) {         // 액션
            if (id == null)
                return Content("Error!!!");

            // Document 모델의 Action을 통해 데이터를 가져옴
            DocumentActs documentActs = new DocumentActs();
            var documents = documentActs.GetDocuments();

            // 매개변수 documents를 View에 전달해줌
            return View(documents);
        }
    }
}
