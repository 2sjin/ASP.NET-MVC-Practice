using WebApplication1.Data;

namespace WebApplication1.Models {
    // 모델(Document)의 데이터 처리 메소드를 정의하기 위한 클래스
    public class DocumentActs {
        // Document 형태의 데이터를 반환하는 메소드
        public List<Document> GetDocuments() {
            DocumentData documentData = new DocumentData();
            return documentData.Documents;
        }
    }
}
