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
}
