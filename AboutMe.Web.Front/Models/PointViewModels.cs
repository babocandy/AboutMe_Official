using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AboutMe.Domain.Entity.Point;

namespace AboutMe.Web.Front.Models
{
    public class MyPointListViewModel
    {
        public List<SP_POINT_MY_HISTORY_SEL_Result> List { get; set; }
        public int Total { get; set; }
        public int MyTotalPoint { get; set; }
        public string MyName { get; set; }
        public MyPointRouteParam Route { get; set; }
    }
}