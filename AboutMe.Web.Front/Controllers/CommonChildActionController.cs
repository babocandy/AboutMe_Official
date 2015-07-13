using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Data;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;



namespace AboutMe.Web.Front.Controllers
{
    public class CommonChildActionController : BaseFrontController
    {


        // GET: CommonChildAction
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Paging(int TotalRecord, int RecordPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {

            //public PagingProps(int TotalRecord, int RecordPerPage, int pPagePerBlock, int pCurrentPage, string q_str)

            PagingProps p_value = new PagingProps(TotalRecord, RecordPage, PagePerBlock, CurrentPage, QueryStr);
            //p_value.max_page = (int)Math.Ceiling((double)total_cnt / page_size); //올림
            //var class_value = p_value;
            return PartialView(p_value);
        }


        //프론트 상단 우측 - 회원로그인 상황에따른 ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildRightTopMenu_Util()
        {
            this.ViewBag.IS_LOGIN = MemberInfo.IsMemberLogin();  //로그인 여부  T/F
            this.ViewBag.M_ID = MemberInfo.GetMemberId();  //로그인 계정

            return View();
        }


    }
}