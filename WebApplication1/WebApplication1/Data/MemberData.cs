using WebApplication1.Models;

namespace WebApplication1.Data {
    public class MemberData {
        // 실제 데이터를 가져오기 위한 클래스
        public List<Member> Members {
            get {
                // 실제 데이터를 저장하는 List
                List<Member> returnValue = new List<Member> {
                    new Member { Num=1, Id="Lee", Name="이순신", Password="1234"},
                    new Member { Num=2, Id="Jung", Name="정약용", Password="1234"},
                    new Member { Num=3, Id="Hong", Name="홍길동", Password="1234"},
                };
                return returnValue;
            }
        }
    }
}
