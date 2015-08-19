using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace AboutMe.Domain.Entity.AdminDisplay
{


    /**
     * 관리자 - 전시물 조회시 필요함
     */
    public class DisplayerParam
    {
        public int? IDX { get; set; }
        public string URL { get; set; }
        public string KIND { get; set; }
        public string SUB_KIND { get; set; }
        public int? SEQ { get; set; }
        public string P_CODE { get; set; }
        public string TITLE1 { get; set; }
        public string TITLE2 { get; set; }
        public string IMG_NAME { get; set; }
        public HttpPostedFileBase IMG_FILE { get; set; }
    }

    /**
     * 관리자 웹메인
     */
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

    /**
     * 관리자 모바일 메인
     */
    public class AdminDisplayMobileMainViewModel
    {
        public List<SP_ADMIN_DISPLAY_SEL_Result> MobileMainBannerList { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result MobileTalkOnBeauty { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result MobileBest { get; set; }

    }

    /**
     * 관리자 장바구니 전시
     */
    public class AdminDisplayCartViewModel
    {
        public SP_ADMIN_DISPLAY_SEL_Result Web { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Mobile { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay12 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result CommonDisplay13 { get; set; }
    }


    /**
     * 관리자  전시 gbn 메뉴
     */
    public class AdminDisplayGBNViewModel
    {
        public SP_ADMIN_DISPLAY_SEL_Result Banner10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Banner11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Banner12 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Banner13 { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result Link10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Link11 { get; set; }
    }

    /**
     * 관리자  전시 상세
     */
    public class AdminDisplayProductBannerViewModel
    {
        public SP_ADMIN_DISPLAY_SEL_Result Web10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Web11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Web12 { get; set; }

        public SP_ADMIN_DISPLAY_SEL_Result Mobile10 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Mobile11 { get; set; }
        public SP_ADMIN_DISPLAY_SEL_Result Mobile12 { get; set; }
    }

    
    /**
     * 관리자 팝업검색 파람
     */
    public class PopupSearchParam
    {
        public PopupSearchParam()
        {
            PAGE = 1;
        }


        public int? PAGE { get; set; }


        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }

        public string IS_DISPLAY { get; set; }
        public string IS_EXPIRE { get; set; }

    }

    /**
     * 팝업 목로 화면
     */
    public class AdminDisplayPopupListViewModel
    {
        public List<SP_ADMIN_POPUP_SEL_Result> PopupList { get; set; }
        public int Total { get; set; }
        public PopupSearchParam SearchParam { get; set; }
    }

    /**
     * 팝업 저장,수정화면
     */
    public class AdminDisplayPopupInputViewModel
    {
        public int? IDX { get; set; }

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
        public string MOBILE_IMG_NAME { get; set; }
        public string MOBILE_LINK { get; set; }

        [Required(ErrorMessage = "*")]
        public string IS_DISPLAY { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [RegularExpression("^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}$", ErrorMessage = "* [형식 에러]")]
        public string DISPLAY_START { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [RegularExpression("^[0-9]{4}-[0-9]{2}-[0-9]{2} [0-9]{2}:[0-9]{2}$", ErrorMessage = "* [형식 에러]")]
        public string DISPLAY_END { get; set; }

        public PopupSearchParam SearchParam { get; set; }


    }

}
