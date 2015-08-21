using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.Faq
{
    class ModelFaq_Metadata
    {
        [Required(ErrorMessage = "제목을 입력해주세요.")]
        public string TITLE { get; set; }
        [Required(ErrorMessage = "답변을 입력해주세요.")]
        public string ANSWER { get; set; }
        [Required(ErrorMessage = "질문을 입력해주세요.")]
        public string QUESTION { get; set; }
    }

    #region 검색조건Model
    public class FaqSearchModel
    {
        private bool? categorysearch; //카테고리검색인지여부
        private string searchcol;
        private string searchkeyword;
        private string displayyn;
        private int? page;
        private int? pagesize;

        public bool CategorySearch
        {
            get
            {
                return this.categorysearch ?? false;
            }
            set
            {
                this.categorysearch = value;
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

    public class FaqUserModel
    {
        public FaqSearchModel searchOption { get; set; }
        public int faqCnt { get; set; }
        public List<SP_TB_FAQ_SEL_Result> faqList { get; set; }
    }
}
