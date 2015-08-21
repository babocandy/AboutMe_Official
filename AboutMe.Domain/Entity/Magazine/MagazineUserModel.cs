using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Magazine
{
    public class MAGAZINE_ADMIN_SEARCH
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

    public class MAGAZINE_ADMIN_EDIT
    {
        public int IDX { get; set; }
        public string Mode { get; set; }
        public MAGAZINE_ADMIN_SEARCH SearchOption { get; set; }
        public SP_ADMIN_MAGAZINE_VIEW_Result ViewInfo { get; set; }
    }

    public class MAGAZINE_ADMIN_INDEX
    {
        public int ListCnt { get; set; }
        public MAGAZINE_ADMIN_SEARCH SearchOption { get; set; }
        public List<SP_ADMIN_MAGAZINE_SEL_Result> ListInfo { get; set; }
    }

    public class MAGAZINE_SAVE_PARAM
    {
        public int IDX { get; set; }
        public string TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        public string CONTENT_GBN { get; set; }
        public string MOVIE_URL { get; set; }
        public HttpPostedFileBase IMG_FILE { get; set; }
        public string OLD_IMG_PATH { get; set; }
        public string IMG_PATH_DEL { get; set; }
        public HttpPostedFileBase THUMB_IMG_FILE { get; set; }
        public string OLD_THUMB_IMG_PATH { get; set; }
        public string THUMB_IMG_PATH_DEL { get; set; }
        public string USE_YN { get; set; }
        public string ADM_ID { get; set; }
    }

    public class MAGAZINE_VIEW
    {
        public SP_MAGAZINE_VIEW_Result MagazineInfo { get; set; }
    }
}
