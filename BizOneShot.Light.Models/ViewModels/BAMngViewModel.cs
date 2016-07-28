using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Models.ViewModels
{
    internal class BAMngViewModel
    {
    }

    public class BASuperviseViewModel
    {
        public Int32 COMP_SN { get; set; }      // comp 식별자
        public String WRITE_YN { get; set; }    // 작성자 식별자
        public String CON_CODE { get; set; }    // 컨설팅 코드
        public String BA_NM { get; set; }       // BA명
        public String BA_EMAIL { get; set; }    // 이메일주소
        public String BA_OWN_NM { get; set; }   // BA소유자
        public Int32 BA_SN { get; set; }        // BA_SN
        public String BA_TEL_NO { get; set; }   // BA전화번호
        public int MENTOR_SN { get; set; }
        public string MENTOR_NM { get; set; }   // 담당 멘토 이름
        public string NUM_SN { get; set; }
        public string SUB_NUM_SN { get; set; }
    }

    public class MentorDetailViewModel
    {
        public String NAME { get; set; }        // 멘토명
        public String EMAIL { get; set; }       // 멘토 email주소
        public String TEL_NO { get; set; }      // 멘토 전화번호
        public String MB_NO { get; set; }       // 멘토 핸드폰번호
        public String FAX_NO { get; set; }      // 멘토 팩스번호
        public String ADDR_1 { get; set; }      // 멘토 주소
        public String POST_NO { get; set; }     // 멘토 우편번호
    }
}
