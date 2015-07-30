using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AboutMe.Domain.Entity.AdminPoint;
using AboutMe.Domain.Entity.AdminDisplay;


namespace AboutMe.Web.Admin.Models
{
    public class AdminDisplayWebMainViewModel
    {
        public List<SP_ADMIN_DISPLAY_SEL_Result> WebMainBannerList { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result WebMiddleBanner { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay12 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay13 { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay20 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay21 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay22 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result WebProductDisplay23 { get; set; }
    }

    public class AdminDisplayMobileMainViewModel
    {
        public List<SP_ADMIN_DISPLAY_SEL_Result> MobileMainBannerList { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result MobileTalkOnBeauty{ get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result MobileBest{ get; set; }

    }

    public class AdminDisplayCartViewModel
    {
        public SP_ADMIN_DISPLAY_SEL_Result Web { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Mobile { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay12 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay13 { get; set; }
    }


    public class AdminDisplayGBNViewModel
    {
        public SP_ADMIN_DISPLAY_SEL_Result Banner10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Banner11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Banner12 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Banner13 { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result Link10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Link11 { get; set; }
    }

    public class AdminDisplayProductBannerViewModel
    {
        public SP_ADMIN_DISPLAY_SEL_Result Web10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Web11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Web12 { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result Mobile10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Mobile11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Mobile12 { get; set; }
    }
}