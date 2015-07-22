using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using AboutMe.Domain.Entity.Review;

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

    public class MyReviewCompleteViewModel
    {
        public IList<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> CompleteList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get { return 10; } }
        public int TotalItem { get; set; }
        
    }
}