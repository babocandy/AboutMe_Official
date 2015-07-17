using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Promotion;
using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Web.Front.Controllers
{
    public class EventController : BaseFrontController
    {
        private IPromotionService _PromotionService;

        public EventController(PromotionService _PromotionService)
        {
            this._PromotionService = _PromotionService;
        }

        protected override void HandleUnknownAction(string actionName)
        {
            this.View(actionName).ExecuteResult(this.ControllerContext);
        }

        public class MyMultiModelForPromotionProduct
        {
            public TB_PROMOTION_BY_TOTAL inst_TB_PROMOTION_BY_TOTAL { get; set; }
            public List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> inst_PROMOTION_TOP_1_Result { get; set; }
            public TB_PROMOTION_BY_PRODUCT inst_TB_PROMOTION_BY_PRODUCT { get; set; }
            public List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result> inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result { get; set; }
        }


        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        //세트상품 
        public ActionResult SetPromotion()
        {

            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //이미지디렉토리경로

            var mMyMultiModelForPromotionProduct = new MyMultiModelForPromotionProduct
            {
                inst_PROMOTION_TOP_1_Result = new List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result>(),
                inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result = new List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result>()
            };


            string aas = "";
            if (TempData["jsMessage"] != null)
            {
                aas = TempData["jsMessage"].ToString();
            }

            string CdPromotionProduct = ""; //상품별 프로모션 코드 

            //세트상품 프로모션 정보중 유효한 TOP 1 가져오기 
            mMyMultiModelForPromotionProduct.inst_PROMOTION_TOP_1_Result = _PromotionService.GetPromotionByProductTop1_Info("01").ToList();

            if (mMyMultiModelForPromotionProduct.inst_PROMOTION_TOP_1_Result.Count != 0)
            {
                CdPromotionProduct = mMyMultiModelForPromotionProduct.inst_PROMOTION_TOP_1_Result[0].CD_PROMOTION_PRODUCT;
                mMyMultiModelForPromotionProduct.inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result = _PromotionService.GetPromotionByProductList(CdPromotionProduct).ToList();

            }

            return View(mMyMultiModelForPromotionProduct);

            //return View();
        }


    }
}