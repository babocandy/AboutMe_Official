using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Web.Front.Models;
using AboutMe.Domain.Service.Point;
using AboutMe.Domain.Entity.Point;
using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers.Mypage
{
    [RoutePrefix("MyPage/MyPoint")]
    [Route("{action=Index}")]
    public class MyPointController : BaseFrontController
    {
        private IPointService _service;

        public MyPointController(IPointService s)
        {
            _service = s;
        }


        // GET: MyPoint
        [CustomAuthorize]
        public ActionResult Index(MyPointRouteParam p)
        {
            MyPointListViewModel model = new MyPointListViewModel();

            p.M_ID = _user_profile.M_ID;

            var tp = _service.GetMyPointHistoryList(p);
            model.List = tp.Item1;
            model.MyTotalPoint = tp.Item2;
            model.Total = _service.GetMyPointHistoryListCnt(p);
            model.MyName = _user_profile.M_NAME;
            model.Route = p;
            return View(model);
        }

        [ChildActionOnly]
        public string CurrentTotal(string mid)
        {
            return string.Format("{0:#,##0}", _service.GetMyPointTotal(mid)); ;
        }
    }
}