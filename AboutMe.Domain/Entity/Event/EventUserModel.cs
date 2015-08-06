using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Event
{
    public class EVENT_ADMIN_SEARCH
    {
        private string fromdate;
        private string todate;
        private string inggbn;  // 진행구분  0:전체, 1:예약, 2:진행, 3:종료
        private string useyn;     // 전시구분 A:전체, Y:전시, N:비전시
        private string searchcol;
        private string searchkeyword;
        private int? page;
        private int? pagesize;


        public string FromDate
        {
            get
            {
                return this.fromdate ?? "";
            }
            set
            {
                this.fromdate = value;
            }
        }

        public string ToDate
        {
            get
            {
                return this.todate ?? "";
            }
            set
            {
                this.todate = value;
            }
        }

        public string IngGbn
        {
            get
            {
                return this.inggbn ?? "0";
            }
            set
            {
                this.inggbn = value;
            }
        }


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

    public class EVENT_ADMIN_EDIT
    {
        public int IDX { get; set; }
        public string Mode { get; set; }
        public EVENT_ADMIN_SEARCH SearchOption { get; set; }
        public SP_ADMIN_EVENT_VIEW_Result EventInfo { get; set; }
    }
    
    public class EVENT_ADMIN_INDEX
    {
        public int ListCnt { get; set; }
        public EVENT_ADMIN_SEARCH SearchOption { get; set; }
        public List<SP_ADMIN_EVENT_SEL_Result> ListInfo { get; set; }
    }

    public class EVENT_SAVE_PARAM
    {

        public int IDX { get; set; }
        public string EVENT_NAME { get; set; }
        public string FROM_DATE { get; set; }
        public string FROM_TIME { get; set; }
        public string TO_DATE { get; set; }
        public string TO_TIME { get; set; }
        public string EVENT_GBN { get; set; }
        public string WEB_CONTENTS { get; set; }
        public HttpPostedFileBase MOBILE_FILE { get; set; }
        public HttpPostedFileBase WEB_BANNER { get; set; }
        public HttpPostedFileBase MOBILE_BANNER { get; set; }
        public string MOBILE_FILE_DEL { get; set; }
        public string WEB_BANNER_DEL { get; set; }
        public string MOBILE_BANNER_DEL { get; set; }
        public string OLD_MOBILE_FILE { get; set; }
        public string OLD_WEB_BANNER { get; set; }
        public string OLD_MOBILE_BANNER { get; set; }
        public string WEB_URL { get; set; }
        public string MOBILE_URL { get; set; }
        public string USE_YN { get; set; }
        public string ADM_ID { get; set; }

    }
}
