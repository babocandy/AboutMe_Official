using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Mobile.Common;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Mobile.Common.Filters;


namespace AboutMe.Web.Mobile.Controllers.MyPage
{
    public class MyPageController : BaseMobileController
    {
        private IMemberService _MemberService;

        public MyPageController(IMemberService _memberService)
        {
            this._MemberService = _memberService;
        }


        // GET: Mypage
        [CustomAuthorize]  //마이페이지 메인 으로 이동
        public ActionResult Index()
        {
            // return View();
            return RedirectToAction("Main", "MyPage"); //메인 으로 이동
        }

        [CustomAuthorize]  //마이페이지 메인
        public ActionResult Main()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + HttpContext.Request.Url.Host; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = Config.GetConfigValue("HTTP_PROTOCOL") + HttpContext.Request.Url.Host; //ex)https://www.aboutme.co.kr
            if (HttpContext.Request.Url.Port != 80)
            {
                strHTTPS_DOMAIN = strHTTPS_DOMAIN + ":" + HttpContext.Request.Url.Port.ToString();
                strHTTP_DOMAIN = strHTTP_DOMAIN + ":" + HttpContext.Request.Url.Port.ToString();
            }

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;  //탈퇴시 사용됨.
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;  //탈퇴시 사용됨.


            this.ViewBag.M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade();
            this.ViewBag.M_GRADE_NAME = MemberInfo.GetMemberGradeName();
            this.ViewBag.M_SKIN_TROUBLE_CD = MemberInfo.GetMemberSkinTroubleCD(); //피무고민코드


            return View();
        }
    }
}