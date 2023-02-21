using WebApplication1.Data;

namespace WebApplication1.Models {
    public class DocumentActs {
        public List<Document> GetDocuments() {
            DocumentData documentData = new DocumentData();
            return documentData.Documents;
        }
    }
}
