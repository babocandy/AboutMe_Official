using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.AdminProduct
{
    class AdminProduct_Metadata
    {

        [Required(ErrorMessage = "키를 입력해주세요")]
        public string P_CATE_CODE { get; set; }
    }
}
