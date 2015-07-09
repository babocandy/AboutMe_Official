using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Front.Common.Filters;


namespace AboutMe.Web.Front.Controllers
{
    public class MypageController : BaseFrontController
    {
        // GET: Mypage
        [CustomAuthorize]  //마이페이지 메인 으로 이동
        public ActionResult Index()
        {
           // return View();
            return RedirectToAction("Main", "Mypage"); //메인 으로 이동
        }

        [CustomAuthorize]  //마이페이지 메인
        public ActionResult Main()
        {
            this.ViewBag.M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade();
            this.ViewBag.M_GRADE_NAME = MemberInfo.GetMemberGradeName();


            return View();
        }
    }
}