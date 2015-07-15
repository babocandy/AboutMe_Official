using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View(m);
        }

        public ActionResult Detail(int ORDER_IDX, SP_TB_ADMIN_ORDER_Param SEARCH_OPTION)
        {
            return View();
        }



        [ChildActionOnly]
        public ActionResult OrderPaging(int TotalRecord, int RecordPerPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {
            PagingProps p_value = new PagingProps(TotalRecord, RecordPerPage, PagePerBlock, CurrentPage, QueryStr);
            return PartialView(p_value);
        }

    }
}