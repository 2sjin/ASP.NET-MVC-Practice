using WebApplication1.Data;

namespace WebApplication1.Models {
    // 모델(Member)의 데이터 처리 메소드를 정의하기 위한 클래스
    public class MemberActs {
        // Member 형태의 데이터를 반환하는 메소드
        public Member GetMember(int paramNum) {
            MemberData memberData = new MemberData();
            // LINQ: 리스트 내 데이터 중 Num이 paramNum에 해당하는 데이터만 리턴 
            // SingleOrDefault는 조건에 맞는 데이터가 2개 이상이면 오류 발생
            // (FirstOrDefault는 조건에 맞는 데이터가 2개 이상이어도 1개의 데이터만 조회)
            return memberData.Members.Where(x => x.Num == paramNum).SingleOrDefault();
        }
    }
}
