using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Cart;
using AboutMe.Domain.Entity.Cart;

namespace AboutMe.Web.Front.Controllers
{
    public class CartController : Controller
    {
        private ICartService _cartservice;
        private string _m_id = "";
        private string _session_id = System.Web.HttpContext.Current.Session.SessionID;

        public CartController(ICartService _cartservice)
        {
            this._cartservice = _cartservice;
        }


        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.SessionId = _session_id;
            List<SP_TB_CART_LIST_Result> lst = new List<SP_TB_CART_LIST_Result>();
            lst = _cartservice.CartList(_m_id, _session_id);
            return View(lst);
        }
        
        public ActionResult CartCount()
        {
            int cnt = _cartservice.CartListCount(_m_id, _session_id);
            var jsonData = new { cart_count = cnt };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /*
        [HttpPost]
        public ActionResult CartInput(string data)
        {
            Json
            SP_TB_CART_PRODUCT_ADD_Param newItem = new SP_TB_CART_PRODUCT_ADD_Param{
                
            }
            _cartservice.CartInsert()
            int cnt = _cartservice.CartListCount(_m_id, _session_id);
            var jsonData = new { cart_count = cnt };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        */
        
    }
}