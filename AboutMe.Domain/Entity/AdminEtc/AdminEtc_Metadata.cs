using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace AboutMe.Domain.Entity.AdminEtc
{
    public class AdminMemberMeatadata
    {
        public int IDX { get; set; }

        [Required(ErrorMessage = "왓더뻑 아이디는 꼭 필요해")]
        [StringLength(20,ErrorMessage="20자이하로 입력해~")]
        public string ADM_ID { get; set; }

        [Required(ErrorMessage = "패스워드 없이 어쩌려고 ")]
        public string ADM_PASS { get; set; }
        [StringLength(50)]
        public string ADM_NAME { get; set; }
        public string ADM_DEPT { get; set; }
        public string PHONE { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public Nullable<int> ADM_ROLE_GRADE { get; set; }
        public string MEMO { get; set; }
        public Nullable<System.DateTime> INS_DATE { get; set; }

        [Range(0, 100)]
        public Nullable<int> POINT { get; set; }
        public string ADM_PHOTO { get; set; }

    }
}
