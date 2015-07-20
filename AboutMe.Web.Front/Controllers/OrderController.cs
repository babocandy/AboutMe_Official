using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Order;
using AboutMe.Domain.Entity.Order;
using AboutMe.Domain.Entity.Cart;
using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers
{
    public class OrderController : BaseFrontController
    {
        private IOrderService _orderservice;

        public OrderController(IOrderService _orderservice)
        {
            this._orderservice = _orderservice;
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult InsertOrderStep1(string OrderList)
        {
            List<CART_INSERT> DataList = JsonConvert.DeserializeObject<List<CART_INSERT>>(OrderList);
            string P_CODE_LIST = "";
            string P_COUNT_LIST = "";

            string M_ID = _user_profile.M_ID;
            string SESSION_ID = _user_profile.SESSION_ID;

            foreach (CART_INSERT pData in DataList)
            {
                if (!string.IsNullOrEmpty(P_CODE_LIST))
                {
                    P_CODE_LIST += ",";
                    P_COUNT_LIST += ",";
                }
                P_CODE_LIST += pData.p_code;
                P_COUNT_LIST += pData.p_count;
            }
            Int32 Order_Idx = _orderservice.InsertOrderStep1(_user_profile.M_ID, _user_profile.SESSION_ID, P_CODE_LIST, P_COUNT_LIST);
            this.TempData["Order_Idx"] = Order_Idx;
            return Content("<form name='mysubmitform' action='/Order/Step1' method='POST'><input type='hidden' name='ORDER_IDX' value='" + Order_Idx.ToString() + "'></form> <script language='javascript'>document.mysubmitform.submit();</script>");

        }
        // GET: Order

        [HttpPost]
        public ActionResult Step1(Int32 ORDER_IDX)
        {
            ViewBag.Order_Idx = ORDER_IDX;
            return View();
        }
    }
}