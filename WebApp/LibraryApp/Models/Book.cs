using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models {
    // Book 모델
    public class Book {
        // MS-SQL과의 연동을 위한 Key 명시
        [Key] public int Book_U { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public string Summary { get; set; }
        public string Publisher { get; set; }
        public int Published_date { get; set; }

    }
}
