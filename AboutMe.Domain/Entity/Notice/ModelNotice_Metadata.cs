using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.Notice
{
    class ModelNotice_Metadata
    {
        [Required(ErrorMessage = "제목을 입력해주세요.")]
        public string TITLE { get; set; }
        [Required(ErrorMessage = "내용을 입력해주세요.")]
        public string CONTENTS { get; set; }
    }
}
