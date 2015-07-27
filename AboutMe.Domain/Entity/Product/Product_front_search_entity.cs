using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.Product
{
    public class Product_front_search_entity
    {
        public Product_front_search_entity()
        {
            PAGE = 1;
            PAGESIZE = 6;
            SEARCH_KEYWORD = "";
            SORT_GBN = "NEW";
        }
        public int IDX { get; set; }
        public string P_CATE_CODE { get; set; }
        public string P_CATE_CODE_3DEPTH { get; set; }
        public string C_CATE_CODE { get; set; }
        public string L_CATE_CODE { get; set; }
        public string SORT_GBN { get; set; }
        public string P_OUTLET_YN { get; set; }
        [DefaultValue(1)]
        public int PAGE { get; set; }
        [DefaultValue(10)]
        public int PAGESIZE { get; set; }
        public string SEARCH_KEYWORD { get; set; }
    }
}
