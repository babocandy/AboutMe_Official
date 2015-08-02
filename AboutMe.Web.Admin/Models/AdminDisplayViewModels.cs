using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AboutMe.Domain.Entity.AdminPoint;
using AboutMe.Domain.Entity.AdminDisplay;
using System.Web;


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

    public class AdminDisplayPopupListViewModel
    {
        public  List<PopupInfo> PopupList { get; set; }
        public int Total { get; set; }
        public PopupSearchParam SearchParam { get; set; }
    }

    public class AdminDisplayPopupSaveViewModel
    {
        public string IDX { get; set; }

        [Required(ErrorMessage = "*")]
        public string MEDIA_GBN { get; set; }

        [Required(ErrorMessage = "*")]
        public string TITLE { get; set; }
        
        public HttpPostedFileBase WEB_IMG_FILE { get; set; }
        public string WEB_IMG_NAME { get; set; }

        public int? POS_TOP { get; set; }
        public int? POS_LEFT { get; set; }

        public int? SIZE_WIDTH { get; set; }
        public int? SIZE_HEIGHT { get; set; }

        public string WEB_LINK { get; set; }
        public string WEB_TARGET { get; set; }

        public HttpPostedFileBase MOBILE_IMG_FILE { get; set; }
        public string MOBILE_IMG_NAME{ get; set; }
        public string MOBILE_LINK { get; set; }

        [Required(ErrorMessage = "*")]
        public string IS_DISPLAY { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [RegularExpression("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2} [0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "[형식 에러]")]
        public string DISPLAY_START { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [RegularExpression("^[0-9]{4}-[0-9]{1,2}-[0-9]{1,2} [0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "[형식 에러]")]
        public string DISPLAY_END { get; set; }

        public PopupSearchParam SearchParam { get; set; }

        /*
         * 
public class MyViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public bool IsPeriod { get; set; }

    public RouteValueDictionary RouteValues
    {
        get
        {
            var rvd = new RouteValueDictionary();
            rvd["name"] = Name;
            rvd["surname"] = Surname;
            rvd["isPeriod"] = IsPeriod;
            return rvd;
        }
    }
}
         */
    }
}