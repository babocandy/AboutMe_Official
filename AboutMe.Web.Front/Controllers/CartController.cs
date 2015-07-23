using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Cart;
using AboutMe.Domain.Entity.Cart;
using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers
{
    public class CartController : BaseFrontController
    {
        private ICartService _cartservice;
 
        public CartController(ICartService _cartservice)
        {
            this._cartservice = _cartservice;
        }

        // GET: Cart
        public ActionResult Index()
        {
            List<SP_TB_CART_LIST_Result> lst = new List<SP_TB_CART_LIST_Result>();
            lst = _cartservice.CartList(_user_profile.M_ID, _user_profile.SESSION_ID);

            CART_INDEX_MODEL viewModel = new CART_INDEX_MODEL();
            viewModel.UserProfile = _user_profile;
            viewModel.BannerUrl = "/aboutCom/images/sample/thum_banner.jpg";
            viewModel.CartCnt = _cartservice.CartListCount(_user_profile.M_ID, _user_profile.SESSION_ID);
            viewModel.CartList = lst;
            viewModel.SumPoint = (lst.Count() < 1) ? 0 : lst.Sum(x => x.P_POINT);
            viewModel.SumPrice = (lst.Count() < 1) ? 0 : lst.Sum(x => x.ORDER_PRICE);
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

        public ActionResult FlyingCart(int page=1)
        {
            int PageSize = 1;
            List<SP_TB_CART_LIST_Result> lst = new List<SP_TB_CART_LIST_Result>();
            lst = _cartservice.CartList(_user_profile.M_ID, _user_profile.SESSION_ID);

            var cart_list = lst.Skip((page - 1) * PageSize).Take(PageSize);
            int cartCnt = _cartservice.CartListCount(_user_profile.M_ID, _user_profile.SESSION_ID);
            int totalPage = cartCnt / PageSize;

            CART_FLYING_MODEL viewModel = new CART_FLYING_MODEL();
            viewModel.CurrentPage = page;
            viewModel.TotalPage = totalPage;
            viewModel.PrevPage = (page > 1) ? page - 1 : 1;
            viewModel.NextPage = (page < totalPage) ? page + 1 : totalPage;
            viewModel.CartCnt = cartCnt;
            viewModel.CartList = cart_list;

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult FlyCartDelete(string IDX)
        {
            _cartservice.CartDelete(_user_profile.M_ID, _user_profile.SESSION_ID, IDX);

            int cnt = _cartservice.CartListCount(_user_profile.M_ID, _user_profile.SESSION_ID);
            var jsonData = new { result = "true", cart_count = cnt };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }


    }
}