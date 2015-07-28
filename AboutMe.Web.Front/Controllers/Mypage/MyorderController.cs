using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Order;
using AboutMe.Domain.Entity.Order;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Front.Common.Filters;


namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyOrder")]
    [Route("{action=index}")]
    [NoMemberAuthorize]
    public class MyOrderController : BaseFrontController
    {
        private IOrderService _orderservice;

        public MyOrderController(IOrderService _orderservice)
        {
            this._orderservice = _orderservice;
        }

        public ActionResult Index(string FromDate, string ToDate, int? page)
        {
            if (page == null) page = 1;
            if (page <= 0) page = 1;
            int PageSize = 2;
            string OrderCode = _user_profile.NOMEMBER_ORDER_CODE;

            if (_user_profile.IS_LOGIN)
            {
                OrderCode = "";
            }

            MyORDER_INDEX M = new MyORDER_INDEX();
            M.Page = page;
            M.PageSize = PageSize;
            M.UserProfile = _user_profile;
            M.FromDate = FromDate;
            M.ToDate = ToDate;
            M.OrderCnt = _orderservice.MyOrderListCount(OrderCode, _user_profile.M_ID, FromDate, ToDate);
            M.OrderList = _orderservice.MyOrderList(OrderCode, _user_profile.M_ID, FromDate, ToDate, page, PageSize);
            return View(M);
        }

    }
}