using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using AboutMe.Domain.Service.Point;
using AboutMe.Domain.Entity.Point;
using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.Controllers.Mypage
{
    [RoutePrefix("MyPage/MyPoint")]
    [Route("{action=Index}")]
    public class MyPointController : BaseMobileController
    {
        private IPointService _service;

        public MyPointController(IPointService s)
        {
            _service = s;
        }


        [ChildActionOnly]
        public string CurrentTotal(string mid)
        {
            return string.Format("{0:#,##0}", _service.GetMyPointTotal(mid)); 
        }
    }
}