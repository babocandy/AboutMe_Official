using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Reflection;
using System.Web.Routing;
using AboutMe.Common.Data;
using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminOrder;
using AboutMe.Domain.Entity.AdminOrder;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;

namespace AboutMe.Web.Admin.Controllers.Order
{
    [CustomAuthorize]
    public class OrderController : BaseAdminController
    {
        private IAdminOrderService _adminorderservice;

        public OrderController(IAdminOrderService _adminorderservice)
        {
            this._adminorderservice = _adminorderservice;
        }

        public ActionResult Index(SP_TB_ADMIN_ORDER_Param Param) {
            ORDER_INDEX_MODEL m = new ORDER_INDEX_MODEL();
            m.SearchOption = Param;
            m.TotalCount = _adminorderservice.OrderListCount(Param);
            m.OrderList = _adminorderservice.OrderList(Param);
            m.OrderStatusCnt = _adminorderservice.OrderStatusCnt(Param);
            return View(m);
        }

        public ActionResult Detail(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION)
        {
            ORDER_DETAIL_MODEL m = new ORDER_DETAIL_MODEL();
            m.ORDER_IDX = ORDER_IDX;
            m.SearchOption = SEARCH_OPTION;
            m.OrderDetailList = _adminorderservice.OrderDetailList(ORDER_IDX);
            m.BodyInfo = _adminorderservice.OrderDetailBodyInfo(ORDER_IDX);
            m.LogList = _adminorderservice.OrderDetailLogList(ORDER_IDX);
            return View(m);
        }

        [ChildActionOnly]
        public ActionResult OrderPaging(int TotalRecord, int RecordPerPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {
            PagingProps p_value = new PagingProps(TotalRecord, RecordPerPage, PagePerBlock, CurrentPage, QueryStr);
            return PartialView(p_value);
        }

        [HttpPost]
        public ActionResult DeliveryUpdate(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string RECEIVER_NAME, string RECEIVER_POST, string RECEIVER_ADDR1, string RECEIVER_ADDR2, string RECEIVER_TEL, string RECEIVER_HP)
        {
            string REG_ID = AdminUserInfo.GetAdmId();

            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.OrderReceiverUpdate(ORDER_IDX, RECEIVER_NAME, RECEIVER_POST, RECEIVER_ADDR1, RECEIVER_ADDR2, RECEIVER_TEL, RECEIVER_HP, REG_ID);
            return RedirectToAction("Detail", param );
        }
        
        [HttpPost]
        public ActionResult AdminMemoUpdate(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string MANAGER_MEMO)
        {
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.AdminMemoUpdate(ORDER_IDX, MANAGER_MEMO);
            return RedirectToAction("Detail", param );
        }
        
        //송장번호 변경
        [HttpPost]
        public ActionResult OrderDetailDeliveryUpdate(int ORDER_IDX, int ORDER_DETAIL_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string DELIVERY_NUM)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.OrderDetailDeliveryUpdate(ORDER_DETAIL_IDX, DELIVERY_NUM, REG_ID);
            return RedirectToAction("Detail", param );
        }
        
        //주문상세 상태 변경
        [HttpPost]
        public ActionResult OrderDetailStatusUpdate(int ORDER_IDX, int ORDER_DETAIL_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION, string TOBE_STATUS)
        {
            string REG_ID = AdminUserInfo.GetAdmId();
            RouteValueDictionary param = ConvertRouteValue(SEARCH_OPTION);
            param.Add("ORDER_IDX", ORDER_IDX);

            _adminorderservice.OrderDetailStatusUpdate(ORDER_DETAIL_IDX, TOBE_STATUS, REG_ID);
            return RedirectToAction("Detail", param );
        }
        
        
        
    }
}