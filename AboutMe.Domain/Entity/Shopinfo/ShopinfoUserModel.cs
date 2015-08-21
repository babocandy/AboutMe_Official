using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Shopinfo
{
    public class SHOPINFO_ADMIN_SEARCH
    {
        private string useyn;     // 전시구분 A:전체, Y:전시, N:비전시
        private string searchcol;
        private string searchkeyword;
        private int? page;
        private int? pagesize;

        public string UseYn
        {
            get
            {
                return this.useyn ?? "A";
            }
            set
            {
                this.useyn = value;
            }
        }

        public string SearchCol
        {
            get
            {
                return this.searchcol ?? "";
            }
            set
            {
                this.searchcol = value;
            }
        }


        public string SearchKeyword
        {
            get
            {
                return this.searchkeyword ?? "";
            }
            set
            {
                this.searchkeyword = value;
            }
        }


        public int? Page
        {
            get
            {
                return this.page ?? 1;
            }
            set
            {
                this.page = value;
            }
        }

        public int? PageSize
        {
            get
            {
                return this.pagesize ?? 10;
            }
            set
            {
                this.pagesize = value;
            }
        }
    }

    public class SHOPINFO_ADMIN_EDIT
    {
        public int IDX { get; set; }
        public string Mode { get; set; }
        public SHOPINFO_ADMIN_SEARCH SearchOption { get; set; }
        public SP_ADMIN_SHOPINFO_VIEW_Result ViewInfo { get; set; }
    }

    public class SHOPINFO_ADMIN_INDEX
    {
        public int ListCnt { get; set; }
        public SHOPINFO_ADMIN_SEARCH SearchOption { get; set; }
        public List<SP_ADMIN_SHOPINFO_SEL_Result> ListInfo { get; set; }
    }

    public class SHOPINFO_SAVE_PARAM
    {
        public int IDX { get; set; }
        public string SHOP_NAME { get; set; }
        public string TEL_NUM { get; set; }
        public string POST { get; set; }
        public string ADDR1 { get; set; }
        public string ADDR2 { get; set; }
        public string LOCATION_INFO { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public int DISPLAY_ORDER { get; set; }
        public string USE_YN { get; set; }
        public string ADM_ID { get; set; }
    }

}
