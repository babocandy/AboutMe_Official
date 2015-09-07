using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Display;
using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Domain.Entity.Display
{

    /**
     * 
     * 프론트 전시 뷰모델
     */
    public class BaseDisplayerViewModel
    {
        public IList<SP_DISPLAY_SEL_Result> List { get; set; }
        public List<SP_PRODUCT_DETAIL_VIEW_Result> PdtList { get; set; }
        public SP_DISPLAY_SEL_Result One { get; set; }
    }

    public class ProductDisplayMainViewModel
    {
        public SP_DISPLAY_SEL_Result Header { get; set; }
        public SP_PRODUCT_DETAIL_VIEW_Result Product_1 { get; set; }
        public SP_PRODUCT_DETAIL_VIEW_Result Product_2 { get; set; }
        public SP_PRODUCT_DETAIL_VIEW_Result Product_3 { get; set; }
    }


    public class PopupMgrViewModel
    {
        public List<SP_POPUP_WEB_SEL_Result> List { get; set; }
    }

    public class PopupWindowViewModel
    {
        public SP_POPUP_DETAIL_Result Detail { get; set; }
    }

    public class PopupMgrMobileViewModel
    {
        public List<SP_POPUP_MOBILE_SEL_Result> List { get; set; }
    }

}
