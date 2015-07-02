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

//관리자 -회원 관리 ctl --jsh

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminFrontMemberController : BaseAdminController
    {
        private IAdminFrontMemberService _AdminFrontMemberService;

        public AdminFrontMemberController(IAdminFrontMemberService _adminFrontMemberService)
        {
            this._AdminFrontMemberService = _adminFrontMemberService;
        }



        //관리자 - 회원 목록
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Index(string DATE_FROM = "", string DATE_TO = "", string GRADE_LIST = "", string GBN = "", string IS_RETIRE = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "M_CREDATE", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
        {
            //return View();
            this.ViewBag.DATE_FROM = DATE_FROM;
            this.ViewBag.DATE_TO = DATE_TO;
            this.ViewBag.GRADE_LIST = GRADE_LIST;
            this.ViewBag.GBN = GBN;
            this.ViewBag.IS_RETIRE = IS_RETIRE;

            this.ViewBag.SEARCH_COL = SEARCH_COL;
            this.ViewBag.SEARCH_KEYWORD = SEARCH_KEYWORD;
            this.ViewBag.SORT_COL = SORT_COL;
            this.ViewBag.SORT_DIR = SORT_DIR;
            this.ViewBag.PAGE = PAGE;
            this.ViewBag.PAGESIZE = PAGESIZE;

            int TotalRecord = 0;
            //TotalRecord = 999;
            TotalRecord = _AdminFrontMemberService.GetAdminFrontMemberListCount(DATE_FROM, DATE_TO, GRADE_LIST, GBN, IS_RETIRE, SEARCH_COL, SEARCH_KEYWORD);

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림

            return View(_AdminFrontMemberService.GetAdminFrontMemberList(DATE_FROM, DATE_TO, GRADE_LIST, GBN, IS_RETIRE, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, PAGE, PAGESIZE).ToList());
        }
    
    
    
    
    }
}