using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Promotion;
using AboutMe.Domain.Entity.AdminPromotion;
using AboutMe.Domain.Service.Event;
using AboutMe.Domain.Entity.Event;
using AboutMe.Domain.Service.Exhibit;
using AboutMe.Domain.Entity.Exhibit;
using AboutMe.Domain.Service.Sample;
using AboutMe.Domain.Entity.Sample;

using System.Data.Entity.Core.Objects;
using AboutMe.Common.Data;
using Newtonsoft.Json;
using AboutMe.Web.Mobile.Common;
using AboutMe.Web.Mobile.Common.Filters;


namespace AboutMe.Web.Mobile.Controllers
{
    public class EventController : BaseMobileController
    {
        // GET: Event

        private IPromotionService _PromotionService;
        private IEventService _eventservice;
        private IExhibitService _exhibitservice;
        private ISampleService _sampleservice;

        public EventController(PromotionService _PromotionService, IEventService _eventservice, IExhibitService _exhibitservice, ISampleService _sampleservice)
        {
            this._PromotionService = _PromotionService;
            this._eventservice = _eventservice;
            this._exhibitservice = _exhibitservice;
            this._sampleservice = _sampleservice;
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


        #region 이벤트메인/ 이벤트 / 기획전 (gxen) ================================================================

            // GET: Main
        public ActionResult Index()
        {
            List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result> timesaleList = new List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result>();

            //타임세일 프로모션 정보중 유효한 TOP 1 가져오기 
            timesaleList = _PromotionService.GetPromotionByProductTop1_Info("03").ToList();
            EVENT_MAIN_INDEX M = new EVENT_MAIN_INDEX
            {
                TimeSaleCnt = timesaleList.Count(),
                MainInfo = _eventservice.EventMainView(),
                IngListInfo = _eventservice.EventMainIngList(),
                EndListInfo = _eventservice.EventMainEndList()
            };
            return View(M);
        }

        public ActionResult EventView(int IDX)
        {
            EVENT_VIEW M = new EVENT_VIEW
            {
                EventInfo = _eventservice.EventView(IDX),
                IngListInfo = _eventservice.EventMainIngList()
            };
            return View(M);
        }

        public ActionResult ExhibitView(int IDX)
        {
            List<SP_ADMIN_EXHIBIT_TAB_SEL_Result> Tablist = _exhibitservice.ExhibitAdminTabList(IDX);
            List<EXHIBIT_TAB_PRODUCT> TabProduct = new List<EXHIBIT_TAB_PRODUCT>();
            foreach (SP_ADMIN_EXHIBIT_TAB_SEL_Result item in Tablist)
            {
                EXHIBIT_TAB_PRODUCT tp = new EXHIBIT_TAB_PRODUCT
                {
                    Tabinfo = item,
                    ProductList = _exhibitservice.ExhibitTabProductList(item.IDX)
                };
                TabProduct.Add(tp);
            }

            EXHIBIT_VIEW M = new EXHIBIT_VIEW
            {
                ExhibitInfo = _exhibitservice.ExhibitView(IDX),
                TabProductList = TabProduct,
                IngList = _eventservice.EventMainIngList()
            };
            return View(M);
        }

        //상품상세 : 우측하단의 이벤트/기획전 목록
        public ActionResult EventInProductDetail(string p_code)
        {
            List<SP_EXHIBIT_PCODE_LINK_ALL_Result> M = _exhibitservice.ExhibitProductLinkAll(p_code);
            return PartialView(M);
        }

        //샘플.체험단 신청
        public ActionResult SampleView(int IDX)
        {
            SAMPLE_VIEW M = new SAMPLE_VIEW
            {
                SampleInfo = _sampleservice.SampleView(IDX),
                IngListInfo = _eventservice.EventMainIngList()
            };
            return View(M);
        }
        // 샘플/체험단 신청
        [HttpPost]
        [CustomAuthorize]
        public ActionResult RegistSample(int IDX)
        {
            try
            {
                _sampleservice.SampleRequest(IDX, _user_profile.M_ID);

                var jsonData = new { result = "true" };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var jsonData = new { result = "false", msg = e.InnerException.Message };

                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }

        }

        //메인 > 이벤트
        public ActionResult MainEvent()
        {
            List<SP_EVENT_ING_LIST_Result> ingList = _eventservice.EventMainIngList();
            List<MOBILE_MAIN_EVENT_INFO> M = new List<MOBILE_MAIN_EVENT_INFO>();
            int cnt = 0;
            foreach (var item in ingList)
            {
                MOBILE_MAIN_EVENT_INFO EventMainInfo = new MOBILE_MAIN_EVENT_INFO();

                EventMainInfo.EventInfo = item;

                if (item.GBN == "EXHIBIT")
                {
                    List<SP_ADMIN_EXHIBIT_TAB_SEL_Result> Tablist = _exhibitservice.ExhibitAdminTabList(item.IDX);
                    List<SP_EXHIBIT_TAB_PRODUCT_LIST_Result> TabProducts = new List<SP_EXHIBIT_TAB_PRODUCT_LIST_Result>();
                    foreach (SP_ADMIN_EXHIBIT_TAB_SEL_Result tab in Tablist)
                    {
                        List<SP_EXHIBIT_TAB_PRODUCT_LIST_Result> list2 =_exhibitservice.ExhibitTabProductList(tab.IDX);

                        foreach (SP_EXHIBIT_TAB_PRODUCT_LIST_Result pd in list2)
                        {
                            TabProducts.Add(pd);
                        }
                    }
                    EventMainInfo.ProductList = TabProducts;
                }
                else
                {
                    EventMainInfo.ProductList = null;
                }

                M.Add(EventMainInfo);
                cnt++;
                if (cnt == 3) //상위3개만
                {
                    break;
                }
            }
            return PartialView(M);
        }

        #endregion

        //타임세일 - 모바일
        public ActionResult TimeSale()
        {
            ViewBag.PromotionPhotoPath = AboutMe.Common.Helper.Config.GetConfigValue("PromotionPhotoPath"); //이미지디렉토리경로

            var mMyMultiModelForPromotionProduct = new MyMultiModelForPromotionProduct
            {
                inst_PROMOTION_TOP_1_Result = new List<SP_PROMOTION_BY_PRODUCT_TOP_1_DETAIL_SEL_Result>(),
                inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result = new List<SP_PROMOTION_BY_PRODUCT_PRICE_LIST_SEL_Result>()
            };


            string CdPromotionProduct = ""; //상품별 프로모션 코드 

            //타임세일 프로모션 정보중 유효한 TOP 1 가져오기 
            mMyMultiModelForPromotionProduct.inst_PROMOTION_TOP_1_Result = _PromotionService.GetPromotionByProductTop1_Info("03").ToList();

            if (mMyMultiModelForPromotionProduct.inst_PROMOTION_TOP_1_Result.Count != 0)
            {
                CdPromotionProduct = mMyMultiModelForPromotionProduct.inst_PROMOTION_TOP_1_Result[0].CD_PROMOTION_PRODUCT;
                mMyMultiModelForPromotionProduct.inst_PROMOTION_BY_PRODUCT_PRICE_SEL_Result = _PromotionService.GetPromotionByProductList(CdPromotionProduct).ToList();

                return View(mMyMultiModelForPromotionProduct);

            }
            else
            {
                return RedirectToAction("Index", "");
            }

        }



        //세트상품 
        public ActionResult SetPromotion()
        {

            ViewBag.PRODUCT_PATH = AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath"); //상품 이미지디렉토리경로
            ViewBag.PROMOTION_IMG_PATH = AboutMe.Common.Helper.Config.GetConfigValue("PromotionPhotoPath"); //프로모션 이미지디렉토리경로

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


                return View(mMyMultiModelForPromotionProduct);
            }
            else
            {
                return RedirectToAction("Index", "");
            }


            //return View();
        }
    }
}