using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.Faq
{
    class ModelFaq_Metadata
    {
        [Required(ErrorMessage = "제목을 입력해주세요.")]
        public string TITLE { get; set; }
        [Required(ErrorMessage = "답변을 입력해주세요.")]
        public string ANSWER { get; set; }
        [Required(ErrorMessage = "질문을 입력해주세요.")]
        public string QUESTION { get; set; }
    }
}
