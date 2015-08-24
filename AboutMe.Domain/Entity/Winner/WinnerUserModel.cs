using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.Winner
{
    public class WINNER_SEARCH
    {
        private int? page;
        private int? pagesize;

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

    public class WINNER_INDEX
    {
        public WINNER_SEARCH SearchParam { get; set; }
        public int WinnerCount { get; set; }
        public List<SP_TB_WINNER_SEL_Result> WinnerList { get; set; }
    }

    public class WINNER_ADMIN_SEARCH
    {
        private string searchcol;
        private string searchkeyword;
        private string displayyn;
        private int? page;
        private int? pagesize;

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

        public string DisplayYn
        {
            get
            {
                return this.displayyn ?? "A";
            }
            set
            {
                this.displayyn = value;
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

    public class WINNER_ADMIN_EDIT
    {
        public int IDX { get; set; }
        public string Mode { get; set; }
        public WINNER_ADMIN_SEARCH SearchParam { get; set; }
        public SP_TB_WINNER_VIEW_Result WinnerInfo { get; set; }
    }

    public class WinnerUserView
    {
        public int IDX { get; set; }
        public WINNER_SEARCH SearchParam { get; set; }
        public SP_TB_WINNER_VIEW_Result WinnerInfo { get; set; }
    }

}
