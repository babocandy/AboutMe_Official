using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.AdminProduct
{

    #region 사은품 모델 엔티티
    public class AdminGiftModel
    {
        public AdminGiftModel()
        {
            this.IDX = 0;
            this.GIFT_NAME = "";
            this.GIFT_COUNT = 0;
            this.USED_COUNT = 0;
            this.START_PRICE = 0;
            this.END_PRICE = 0;
            this.GIFT_IMG = "";
            this.OLD_GIFT_IMG = "";
            this.DISPLAY_YN = "Y";
            this.INS_DATE = "";

        }

        public int IDX { get; set; }
        public string GIFT_NAME { get; set; }
        public int GIFT_COUNT { get; set; }
        public int USED_COUNT { get; set; }
        public int START_PRICE { get; set; }
        public int END_PRICE { get; set; }
        public string GIFT_IMG { get; set; }
        public string OLD_GIFT_IMG { get; set; }
        public string DISPLAY_YN { get; set; }
        public string INS_DATE { get; set; }

    }
    #endregion

    #region 사은품 파라메터 엔티티 
    public class SP_TB_FREE_GIFT_INFO_Param
    {
        public SP_TB_FREE_GIFT_INFO_Param()
        {
            this.SEARCH_DISPLAY_YN = "Y";
            this.SEARCH_KEY = "";
            this.SEARCH_KEYWORD = "";
            this.PAGE = 1;
            this.PAGESIZE = 10;
        }

        private string _search_key;
        private string _search_keyword;


        public string SEARCH_KEY 
        {
            get
            {
                return this._search_key ?? "";
            }
            set
            {
                this._search_key = value;
            }
        }
        public string SEARCH_KEYWORD
        {
            get
            {
                return this._search_keyword ?? "";
            }
            set
            {
                this._search_keyword = value;
            }
        }

        public int PAGE { get; set; }
        public int PAGESIZE { get; set; }
        public string SEARCH_DISPLAY_YN { get; set; }
    }
    #endregion

    #region 사은품 리스트 엔티티
    public class GIFT_INDEX_MODEL
    {
        public SP_TB_FREE_GIFT_INFO_Param SearchOption { set; get; }
        public List<SP_ADMIN_GIFT_SEL_Result> GiftList { get; set; }
        public Int32 TotalCount { get; set; }
    }
    #endregion

    #region 사은품 상세페이지 엔티티
    public class GIFT_DETAIL_MODEL
    {
        public Int32 GIFT_IDX { get; set; }
        public SP_TB_FREE_GIFT_INFO_Param SearchOption { set; get; }
        public SP_ADMIN_GIFT_DETAIL_VIEW_Result GiftDetailView { get; set; }
    }
    #endregion

    #region 사은품 update 엔티티
    public class GIFT_UPDATE_MODEL
    {
        public Int32 GIFT_IDX { get; set; }
        public SP_TB_FREE_GIFT_INFO_Param SearchOption { set; get; }
        public AdminGiftModel adminGiftModel { get; set; }

    }
    #endregion

}
