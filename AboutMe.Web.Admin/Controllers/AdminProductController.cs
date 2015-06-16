using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminProduct;
using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminProductController : BaseAdminController
    {

        // GET: AdminProduct
        private IAdminProductService _AdminProductService;
        public AdminProductController(IAdminProductService _adminProductService)
        {
            this._AdminProductService = _adminProductService;
        }

        #region 카테고리
        public ActionResult Index()
        {
            return View(_AdminProductService.GetAdminCategoryOneList().ToList());
        }

        // GET: AdminProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string DEPTH1_NAME)
        {

            //System.Data.Entity.Core.Objects.ObjectParameter intResult;
            //int intResult = 0;
            try
            {
                // TODO: Add insert logic here

                //;
                int i = _AdminProductService.InsertAdminCategoryOne(DEPTH1_NAME);

                //return RedirectToAction("Index");
                //Redirect("/AdminMember/Index");
                //return View(Index("" ,"", "","", 1, 10));
                ViewBag.resultVal = i;
                return RedirectToAction("Index", new { SearchCol = ViewBag.resultVal });
            }
            catch
            {
                return View();
            }
        }

        // GET:  수정
        public ActionResult CategoryUpdate(int idx)
        {
            SP_ADMIN_CATEGORY_VIEW_Result categoryView = _AdminProductService.ViewAdminCategory(idx);
            return View(categoryView);
        }

        // POST
        [HttpPost]
        public ActionResult CategoryUpdate(int IDX, string DEPTH1_NAME, string DISPLAY_YN, int RE_SORT)
        {
            try
            {
                _AdminProductService.UpdateAdminCategoryOne(IDX, DEPTH1_NAME, DISPLAY_YN, RE_SORT);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: AdminProduct/CategoryDelete
        public ActionResult CategoryDelete(int idx)
        {
            try
            {
                // TODO: Add delete logic here
                _AdminProductService.DeleteAdminCategoryOne(idx);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

      

        #endregion

        #region 상품

        //리스트
        public ActionResult ProductIndex()
        {
            return View(_AdminProductService.GetAdminProductList().ToList());
        }

        //등록
        public ActionResult ProductInsert()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)] //"<" 같은 위지윅 게시판의 html코드 값을 가져올때 false로 세팅
        [ValidateAntiForgeryToken]
       
        //public ActionResult ProductInsert(string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST)
        public ActionResult ProductInsert(TB_PRODUCT_INFO tb_product_info)
        {

            try
            {
                //_AdminProductService.InsertAdminProduct(P_CATE_CODE, C_CATE_CODE, L_CATE_CODE, P_CODE, P_NAME, P_COUNT, P_POINT, P_PRICE, SELLING_PRICE, DISCOUNT_RATE, DISCOUNT_P_POINT, DISCOUNT_PRICE, SOLDOUT_YN, P_INFO_DETAIL_WEB, P_INFO_DETAIL_MOBILE, MV_URL, P_COMPONENT_INFO, P_TAG, MAIN_IMG, OTHER_IMG1, OTHER_IMG2, OTHER_IMG3, OTHER_IMG4, OTHER_IMG5, DISPLAY_YN, ICON_YN, WITH_PRODUCT_LIST);
                _AdminProductService.InsertAdminProduct(tb_product_info);
                return RedirectToAction("ProductIndex", new { SearchCol = "" });
            }
            catch
            {
                return View();
            }
        }

        #endregion

        
        // GET:  수정
        public ActionResult ProductUpdate(string pcode)
        {
            SP_ADMIN_PRODUCT_DETAIL_VIEW_Result productView = _AdminProductService.ViewAdminProduct(pcode);
            return View(productView);
        }

        // POST
        [HttpPost]
        //public ActionResult ProductUpdate(int IDX, string P_CATE_CODE, string C_CATE_CODE, string L_CATE_CODE, string P_CODE, string P_NAME, Nullable<int> P_COUNT, Nullable<int> P_POINT, Nullable<int> P_PRICE, Nullable<int> SELLING_PRICE, Nullable<int> DISCOUNT_RATE, Nullable<int> DISCOUNT_P_POINT, Nullable<int> DISCOUNT_PRICE, string SOLDOUT_YN, string P_INFO_DETAIL_WEB, string P_INFO_DETAIL_MOBILE, string MV_URL, string P_COMPONENT_INFO, string P_TAG, string MAIN_IMG, string OTHER_IMG1, string OTHER_IMG2, string OTHER_IMG3, string OTHER_IMG4, string OTHER_IMG5, string DISPLAY_YN, string ICON_YN, string WITH_PRODUCT_LIST)
        public ActionResult ProductUpdate(TB_PRODUCT_INFO tb_product_info)
        {
            try
            {
                _AdminProductService.UpdateAdminProduct(tb_product_info);
                return RedirectToAction("ProductIndex");
            }
            catch
            {
                return View();
            }
        }

      
        
    }
}
