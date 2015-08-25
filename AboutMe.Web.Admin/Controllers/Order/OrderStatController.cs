using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Domain.Service.OrderStat;
using AboutMe.Domain.Entity.OrderStat;

namespace AboutMe.Web.Admin.Controllers.Order
{
    [CustomAuthorize]
    public class OrderStatController : BaseAdminController
    {
        private IOrderStatService _orderstatservice;

        public OrderStatController(IOrderStatService _orderstatservice)
        {
            this._orderstatservice = _orderstatservice;
        }

        // GET: 일별매출현황
        public ActionResult Index(OrderStatDay param)
        {

            OrderStatIndex M = new OrderStatIndex();
            M.searchParam = param;
            M.OrderStatDayList = _orderstatservice.OrderStatDayList(param.FROM_DATE, param.TO_DATE, param.PAT_GUBUN1, param.PAT_GUBUN2, param.MEMBER_GBN1, param.MEMBER_GBN2, param.MEMBER_GBN3);
            return View(M);
        }

        // GET: 카테고리별 매출통계
        public ActionResult OrderStatCate(OrderStatCate param)
        {

            OrderStatCateIndex M = new OrderStatCateIndex();
            M.searchParam = param;
            M.OrderStatCateList = _orderstatservice.OrderStatCateList(param.FROM_DATE, param.TO_DATE, param.PAT_GUBUN1, param.PAT_GUBUN2, param.MEMBER_GBN1, param.MEMBER_GBN2, param.MEMBER_GBN3, param.cateCode);

            param.cateCode = Convert.ToString(param.cateCode);

            if(!String.IsNullOrEmpty(param.cateCode) )
            {
                this.ViewBag.cateCode1 = param.cateCode.Length < 3 ? "" : param.cateCode.Substring(0, 3);
                this.ViewBag.cateCode2 = param.cateCode.Length < 3 ? "" : param.cateCode.Substring(3, 3);
                this.ViewBag.cateCode3 = param.cateCode.Length < 3 ? "" : param.cateCode.Substring(6, 3);
            }
            return View(M);
        }
    }
}