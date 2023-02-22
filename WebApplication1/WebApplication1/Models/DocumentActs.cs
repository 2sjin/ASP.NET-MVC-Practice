using WebApplication1.Data;

namespace WebApplication1.Models {
    // 모델(Document)에 대한 Action(메소드)을 정의하기 위한 클래스
    public class DocumentActs {
        // Document 형태의 데이터를 불러오기 위한 Action
        public List<Document> GetDocuments() {
            DocumentData documentData = new DocumentData();
            return documentData.Documents;
        }
    }
}
