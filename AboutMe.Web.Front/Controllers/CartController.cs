using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Cart;
using AboutMe.Domain.Entity.Cart;

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
            ViewBag.SessionId = _user_profile.SESSION_ID;
            List<SP_TB_CART_LIST_Result> lst = new List<SP_TB_CART_LIST_Result>();
            lst = _cartservice.CartList(_user_profile.M_ID, _user_profile.SESSION_ID);
            return View(lst);
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
        
        
    }
}