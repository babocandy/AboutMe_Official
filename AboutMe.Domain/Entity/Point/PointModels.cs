using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.Point
{
    public class MyPointRouteParam
    {
        public MyPointRouteParam()
        {
            PAGE = 1;
            PAGE_SIZE = 10;
        }

        public int? PAGE { get; set; }
        public int? PAGE_SIZE { get; set; }
        public string M_ID { get; set; }
        
    }

    public class MyPointListViewModel
    {
        public List<SP_POINT_MY_HISTORY_SEL_Result> List { get; set; }
        public int Total { get; set; }
        public int MyTotalPoint { get; set; }
        public string MyName { get; set; }
        public MyPointRouteParam Route { get; set; }
    }
}
