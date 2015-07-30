using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AboutMe.Domain.Entity.AdminPoint;
using AboutMe.Domain.Entity.AdminDisplay;


namespace AboutMe.Web.Admin.Models
{
    public class AdminDisplayWebMainViewModel
    {
        public List<SP_ADMIN_DISPLAY_SEL_Result> WebMainBannerList { get; set; }
    }

}