using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Cart;
using AboutMe.Domain.Entity.Cart;
using AboutMe.Domain.Service.Coupon;
using AboutMe.Domain.Entity.AdminCoupon;
using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.Controllers
{
    public class CartController : BaseMobileController
    {
        private ICartService _cartservice;
        private ICouponService _CouponService;

        public CartController(ICartService _cartservice, ICouponService _couponservice)
        {
            this._cartservice = _cartservice;
            this._CouponService = _couponservice;
        }

        // GET: Cart
        public ActionResult Index()
        {
            List<SP_TB_CART_LIST_Result> lst = new List<SP_TB_CART_LIST_Result>();
            lst = _cartservice.CartList(_user_profile.M_ID, _user_profile.SESSION_ID);

            CART_INDEX_MODEL viewModel = new CART_INDEX_MODEL();
            viewModel.UserProfile = _user_profile;
            viewModel.BannerUrl = "";
            viewModel.CartCnt = _cartservice.CartListCount(_user_profile.M_ID, _user_profile.SESSION_ID);
            viewModel.CartList = lst;
            viewModel.SumPoint = (lst.Count() < 1) ? 0 : lst.Sum(x => x.P_POINT);
            viewModel.SumPrice = (lst.Count() < 1) ? 0 : lst.Sum(x => x.ORDER_PRICE);
            //다운로드 가능한 쿠폰 수량 가져오기  
            if (_user_profile.IS_LOGIN == true)
            {
                List<SP_ADMIN_COUPON_COMMON_CNT_Result> cpnlst = _CouponService.GetDownloadableCouponCnt(_user_profile.M_ID);
                if (cpnlst.Count > 0)
                    viewModel.DownloadCouponCnt = cpnlst[0].CNT;
                else
                    viewModel.DownloadCouponCnt = 0;

                //사용가능한 쿠폰 수량 가져오기
                viewModel.DownloadedCouponCnt = _CouponService.GettCouponAvailableListCnt(_user_profile.M_ID, "", "");
            }
            else
            {
                viewModel.DownloadCouponCnt = 0;
                viewModel.DownloadedCouponCnt = 0;
            }

            return View(viewModel);
        }

        public ActionResult CartCount()
        {
            int cnt = _cartservice.CartListCount(_user_profile.M_ID, _user_profile.SESSION_ID);
            var jsonData = new { cart_count = cnt };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CartInput(string data)
        {

            List<CART_INSERT> cartDataList = JsonConvert.DeserializeObject<List<CART_INSERT>>(data);

            foreach  (CART_INSERT cartData in cartDataList)
            {
                SP_TB_CART_PRODUCT_ADD_Param newItem = new SP_TB_CART_PRODUCT_ADD_Param {
                    M_ID = _user_profile.M_ID,
                    SESSION_ID = _user_profile.SESSION_ID,
                    P_CODE_LIST = cartData.p_code,
                    P_COUNT_LIST = cartData.p_count,
                    MERGY_OPT = "Y"
                };

                _cartservice.CartInsert(newItem);
            }
            int cnt = _cartservice.CartListCount(_user_profile.M_ID, _user_profile.SESSION_ID);
            var jsonData = new { result="true",cart_count = cnt };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CartUpdateCnt(Int32 IDX, Int32 CNT)
        {
            _cartservice.CartUpdateCnt(_user_profile.M_ID, _user_profile.SESSION_ID,IDX, CNT);
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult CartDelete(string IDX)
        {
            _cartservice.CartDelete(_user_profile.M_ID, _user_profile.SESSION_ID, IDX);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult CartCalculatePrice(string data)
        {
            Int32 sum_point = 0;
            Int32 sum_price = 0;

            string sum_point_txt = "";
            string sum_price_txt = "";

            List<SP_TB_CART_LIST_Result> lst = new List<SP_TB_CART_LIST_Result>();
            lst = _cartservice.CartList(_user_profile.M_ID, _user_profile.SESSION_ID);

            List<Int32> cartDataList = JsonConvert.DeserializeObject<List<Int32>>(data);
            
            var q = lst.Where(x=> cartDataList.Contains(x.CART_IDX)).ToList();

            sum_point = q.Sum(x => x.P_POINT);
            sum_price = q.Sum(x => x.ORDER_PRICE);

            sum_point_txt = sum_point.ToString("#,#0.");
            sum_price_txt = sum_price.ToString("#,#0.");
            var jsonData = new { result = "true", sum_point = sum_point_txt, sum_price = sum_price_txt, chk_count  = q.Count()};

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #region 로그인시 장바구니 합치기 test용
        public void CartMerge()
        {
            _cartservice.CartMerge(_user_profile.M_ID, _user_profile.SESSION_ID, "Y");
            //var jsonData = new { result = "true" };
            //return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [CustomAuthorize]
        public ActionResult WishInput(string data)
        {

            List<String> PCodeList = JsonConvert.DeserializeObject<List<String>>(data);
            int cnt = 0;
            object jsonData = null;
            foreach (String Item in PCodeList)
            {
                cnt = _cartservice.WishInsert(_user_profile.M_ID, Item);
            }

            if (PCodeList.Count() == 1)
            {
                if (cnt == -99) //이미추가되어있는경우    
                {
                    cnt = _cartservice.WishListCount(_user_profile.M_ID);
                    jsonData = new { result = "false", wish_count = cnt, msg = "이미 찜하였습니다." };
                }
                else
                {
                    jsonData = new { result = "true", wish_count = cnt };
                }
            }
            else
            {
                jsonData = new { result = "true", wish_count = cnt };
            }
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult WishDelete(string data)
        {
            List<int> idxList = JsonConvert.DeserializeObject<List<int>>(data);
            int cnt = 0;
            foreach (int Item in idxList)
            {
                cnt = _cartservice.WishDelete(_user_profile.M_ID, Item);
            }
            var jsonData = new { result = "true", wish_count = cnt };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult WishCount()
        {
            int cnt = _cartservice.WishListCount(_user_profile.M_ID);
            var jsonData = new { wish_count = cnt };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public string ProductofCartCnt(string p_code)
        {
            return string.Format("{0:#,##0}", _cartservice.ProductofCartCnt(p_code));
        }

    }
}