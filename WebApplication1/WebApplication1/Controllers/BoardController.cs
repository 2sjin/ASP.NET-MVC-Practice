using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers {
    public class BoardController : Controller {     // 컨트롤러
        public ActionResult List(int? id) {         // 액션
            if (id == null)
                return Content("Error!!!");

            // 각 모델의 Get 메소드를 사용하기 위한 Acts 객체 생성
            DocumentActs documentActs = new DocumentActs();
            MemberActs memberActs = new MemberActs();

            // 각 모델의 Get 메소드를 사용하여 데이터를 가져옴
            var documents = documentActs.GetDocuments();
            var member = memberActs.GetMember(1);


            // 데이터를 컨트롤러에서 뷰로 전달하는 방법

            // 1. ViewBag
            ViewBag.Member1 = member;

            // 2. ViewData
            ViewData["Member2"] = member;

            // 3. View의 매개변수로 전달
            return View(documents);
        }
    }
}
