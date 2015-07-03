using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AboutMe.Domain.Entity.AdminProduct
{
    //상품검색 파라메터 entity
    public class ProductSearch_Entity
    {
        public ProductSearch_Entity()
        {
            Page = 1;
            PageSize = 10;
        }
        public int IDX { get; set; }
        public string SearchKey { get; set; }
        public string SearchKeyword { get; set; }
        public string cateCode { get; set; }
        public string iconYn { get; set; }
        public string searchDisplayY { get; set; }
        public string searchDisplayN { get; set; }
        public string soldoutYn { get; set; }
        public string POutletYn { get; set; }
        [DefaultValue(1)]
        public int Page { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}
