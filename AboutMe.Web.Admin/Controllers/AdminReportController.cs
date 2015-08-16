using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Admin.Common;

using AboutMe.Domain.Service.AdminFrontMember;
using AboutMe.Domain.Entity.AdminFrontMember;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Admin.Common.Filters;

//관리자 -통계 ctl 

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminReportController : BaseAdminController
    {
        private IAdminFrontMemberService _AdminFrontMemberService;

        public AdminReportController(IAdminFrontMemberService _adminFrontMemberService)
        {
            this._AdminFrontMemberService = _adminFrontMemberService;
        }


        // GET: AdminReport
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("MemberGrade", "AdminReport"); //통계- 회원 등급별 현황 으로 이동
        }


        //관리자 - 통계- 회원 등급별 현황
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult MemberGrade()
        {

            return View(_AdminFrontMemberService.GetAdminReportMemberGradeList().ToList());
        }

        //관리자 - 통계- 회원 월별 가입/탈퇴 현황
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult MemberMonthly(string YEAR="")
        {

            if (YEAR == "")
                YEAR = DateTime.Now.ToString("yyyy");


            //년도 DropDown 용 DataList준비 :  현재년도 ~ 2012 까지
            List<SelectListItem> Yearitems = new List<SelectListItem>();
            for (int y = Int32.Parse(DateTime.Now.ToString("yyyy")); y >= 2012; y--)
            {
                if (YEAR == y.ToString())
                    Yearitems.Add(new SelectListItem { Text = y.ToString(), Value = y.ToString(), Selected = true });
                else
                    Yearitems.Add(new SelectListItem { Text = y.ToString(), Value = y.ToString() });
            }
            this.ViewBag.YEAR_LIST = Yearitems;

            this.ViewBag.YEAR = YEAR;

            return View(_AdminFrontMemberService.GetAdminReportMemberMonthlyList(YEAR).ToList());
        }


    } //class
} //namespace