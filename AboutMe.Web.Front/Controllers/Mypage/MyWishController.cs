using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Cart;
using AboutMe.Domain.Entity.Cart;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Front.Common.Filters;


namespace AboutMe.Web.Front.Controllers
{
    [RoutePrefix("MyPage/MyWish")]
    [Route("{action=index}")]
    [CustomAuthorize]
    public class MyWishController : BaseFrontController
    {
        private ICartService _cartservice;
 
        public MyWishController(ICartService _cartservice)
        {
            this._cartservice = _cartservice;
        }

        public ActionResult Index(int? page = 1)
        {
            int? pagesize = 8;
            page = (String.IsNullOrEmpty(Convert.ToString(page)) ? 1 : page);
            if (page <= 0) page = 1;

            MYPAGE_WISHLIST_MODEL M = new MYPAGE_WISHLIST_MODEL();
            M.UserProfile = _user_profile;
            M.Page = page;
            M.PageCnt = pagesize;
            M.WishCnt = _cartservice.WishListCount(_user_profile.M_ID);
            M.WishList = _cartservice.WishList(_user_profile.M_ID, page, pagesize);
            return View(M);
        }

    }
}