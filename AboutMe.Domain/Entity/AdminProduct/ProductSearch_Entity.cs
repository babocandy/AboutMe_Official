using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AboutMe.Domain.Entity.AdminProduct
{

    #region 상품검색 파라메터
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
            this.BatchIconYn = "";
            this.searchStatus = "N"; //검색상태 초기화는 N
            this.searchDisplayY = ""; //초기값은 전시상품만
            this.cateCode = "";
            
        }
        //public int IDX { get; set; }
        public string SearchKey { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string searchStatus { get; set; }
        
        [DefaultValue(1)]
        public int Page { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }


        private string _iconYn;
        private string _BatchIconYn;
        private string _cateCode;
        private string _searchDisplayY;
        private string _searchDisplayN;
        private string _soldoutYn;
        private string _POutletYn;
        private string _SearchKeyword;

        
        public string iconYn
        {
            get{ 
                return this._iconYn ?? "";
            }
            set{
                this._iconYn = value;
            }
        }
        public string BatchIconYn
        {
            get
            {
                return this._BatchIconYn ?? "";
            }
            set
            {
                this._BatchIconYn = value;
            }
        }
        public string cateCode
        {
            get
            {
                return this._cateCode ?? "";
            }
            set
            {
                this._cateCode = value;
            }
        }
        public string searchDisplayY
        {
            get
            {
                return this._searchDisplayY ?? "";
            }
            set
            {
                this._searchDisplayY = value;
            }
        }
        public string searchDisplayN
        {
            get
            {
                return this._searchDisplayN ?? "";
            }
            set
            {
                this._searchDisplayN = value;
            }
        }
        public string soldoutYn
        {
            get
            {
                return this._soldoutYn ?? "";
            }
            set
            {
                this._soldoutYn = value;
            }
        }
        public string POutletYn
        {
            get
            {
                return this._POutletYn ?? "";
            }
            set
            {
                this._POutletYn = value;
            }
        }
        public string SearchKeyword
        {
            get
            {

                //if (this._SearchKeyword != null)
                //{
                //    this._SearchKeyword  = this._SearchKeyword.Replace(" ", ",");
                //}
                //return this._SearchKeyword == null ? "" : this._SearchKeyword.Replace(" ", ",");
                return this._SearchKeyword ?? "";
            }
            set
            {
                this._SearchKeyword = value;
            }
        }
       
    }
    #endregion

    #region 상품 리스트 엔티티
    public class PRODUCT_INDEX_MODEL
    {
        public ProductSearch_Entity productSearch_Entity { set; get; }
        public List<SP_ADMIN_PRODUCT_LIST_Result> ProductList { get; set; }
        public Int32 TotalCount { get; set; }
    }
    #endregion

    #region 상품 상세페이지 엔티티
    public class PRODUCT_DETAIL_MODEL
    {
        public string pcode { get; set; }
        public ProductSearch_Entity productSearch_Entity { set; get; }
        public SP_ADMIN_PRODUCT_DETAIL_VIEW_Result ProductDetailView { get; set; }
    }
    #endregion

    #region 상품 update 엔티티
    public class PRODUCT_UPDATE_MODEL
    {
        public string pcode { get; set; }
        public ProductSearch_Entity sroductSearch_Entity { set; get; }
        public TB_PRODUCT_INFO tb_product_info { get; set; }

    }
    #endregion

}
