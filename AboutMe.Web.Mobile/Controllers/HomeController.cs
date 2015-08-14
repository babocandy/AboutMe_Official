using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using AboutMe.Common.Helper;

using AboutMe.Web.Mobile.Common;

using AboutMe.Domain.Service.Cart;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.Controllers
{
    public class HomeController : BaseMobileController
    {
        private IMemberService _MemberService;
        private ICartService _Cartservice;


        public HomeController(IMemberService _memberService, ICartService _cartservice)
        {
            this._MemberService = _memberService;
            this._Cartservice = _cartservice;
        }


        public ActionResult Index()
        {
            this.ViewBag.IS_LOGIN=MemberInfo.IsMemberLogin();//로그인 여부 T/F
            this.ViewBag.M_ID = MemberInfo.GetMemberId();  //계정
            this.ViewBag.M_NAME = MemberInfo.GetMemberName(); //이름
            this.ViewBag.M_GBN = MemberInfo.GetMemberGBN(); //임직원구분 A,S
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade(); //등급코드
            this.ViewBag.M_GRADE_NAME = MemberInfo.GetMemberGradeName(); //등급명
            this.ViewBag.M_SKIN_TROUBLE_CD = MemberInfo.GetMemberSkinTroubleCD(); //피부고민코드

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test01()
        {
            string s = DetectingDevice();
            ViewBag.device = s;
            return View();
        }


    } //class
} //namespace