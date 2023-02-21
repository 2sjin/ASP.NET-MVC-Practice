using WebApplication1.Models;

namespace WebApplication1.Data {
    public class DocumentData {
        public List<Document> Documents {
            get {
                List<Document> returnValue = new List<Document> {
                    new Document { Id=1, Title="공지사항입니다.", Writer="2sjin" },
                    new Document { Id=2, Title="제목입니다. #1", Writer="홍길동" },
                    new Document { Id=3, Title="제목입니다. #2", Writer="임꺽정" },
                    new Document { Id=4, Title="제목입니다. #3", Writer="이순신" },
                    new Document { Id=5, Title="제목입니다. #4", Writer="신사임당" },
                };
                return returnValue;
            }
        }
    }
}
