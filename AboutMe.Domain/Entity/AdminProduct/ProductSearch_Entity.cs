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
            this.Page = 1;
            this.PageSize = 10;
            this.TO_DATE = "";
            this.FROM_DATE = "";
            //this.TO_DATE = DateTime.Today.ToString().Substring(0, 10);
            //this.FROM_DATE = DateTime.Today.AddDays(-7).ToString().Substring(0, 10);
            this.iconYn = "";
            this.searchStatus = "N"; //검색상태 초기화는 N
            this.searchDisplayY = "Y"; //초기값은 전시상품만
            this.cateCode = "";
            
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
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string searchStatus { get; set; }
        
        [DefaultValue(1)]
        public int Page { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}
