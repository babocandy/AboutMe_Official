using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AboutMe.Domain.Entity.AdminPoint;

namespace AboutMe.Web.Admin.Models
{
    public class AdminPointMemberViewModel
    {
        public IList<SP_POINT_MEMBER_SEL_Result> MemberList { get; set; }
        public int PageNo { get; set; }
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public int TotalItem { get; set; }
    }

    public class AdminPointInsertViewModel
    {
        [Required(ErrorMessage = "Mid Required.")]
        public string Mid { get; set; }
        [Required(ErrorMessage = "Type Required.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Reason Required.")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "Point Required.")]
        public int Point { get; set; }

        public string ResultNum { get; set; }
        public string ResultMessage { get; set; }

    }
}