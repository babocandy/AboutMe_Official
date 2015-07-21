using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AboutMe.Web.Front.Models
{
    public class MyReviewInsertViewModel
    {
        [Required]
        public string Mid { get; set; }

        [Required]
        public int? OrderDetailIdx { get; set; }

        [Required]
        public string Pcode { get; set; }

        [Required(ErrorMessage = "*")]
        public string SkinType { get; set; }

        [Required(ErrorMessage = "*")]
        public string Comment { get; set; }

        public string AddImage { get; set; }

        public string ResultNum { get; set; }
        public string ResultMessage { get; set; }
    }
}