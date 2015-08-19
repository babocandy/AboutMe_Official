using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Faq;

namespace AboutMe.Domain.Entity.Notice
{
    class ModelNotice_Metadata
    {
        [Required(ErrorMessage = "제목을 입력해주세요.")]
        public string TITLE { get; set; }
        [Required(ErrorMessage = "내용을 입력해주세요.")]
        public string CONTENTS { get; set; }
    }

    #region 검색조건Model
    public class NoticeSearchModel
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
    #endregion

    public class NoticeUserModel
    {
        public NoticeSearchModel searchOption { get; set; }
        public int NoticeCnt { get; set; }
        public List<SP_TB_NOTICE_SEL_Result> NoticeList { get; set; }
    }

    public class NoticeUserView
    {
        public NoticeSearchModel searchOption { get; set; }
        public int IDX { get; set; }
        public TB_NOTICE Notice { get; set; }
    }

    public class CustomerMainModel
    {
        public NoticeUserModel Notice { get; set; }
        public FaqUserModel Faq { get; set; }
    }
}
