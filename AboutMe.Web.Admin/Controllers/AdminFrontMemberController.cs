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
        public ActionResult Index(string SEL_DATE_FROM = "", string SEL_DATE_TO = "", string SEL_GRADE_LIST = "", string SEL_GBN = "", string SEL_IS_RETIRE = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "M_CREDATE", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
        {
            //return View();

            this.ViewBag.SEL_DATE_FROM = SEL_DATE_FROM;
            this.ViewBag.SEL_DATE_TO = SEL_DATE_TO;
            this.ViewBag.SEL_GRADE_LIST = SEL_GRADE_LIST;
            this.ViewBag.SEL_GBN = SEL_GBN;
            this.ViewBag.SEL_IS_RETIRE = SEL_IS_RETIRE;

            this.ViewBag.SEARCH_COL = SEARCH_COL;
            this.ViewBag.SEARCH_KEYWORD = SEARCH_KEYWORD;
            this.ViewBag.SORT_COL = SORT_COL;
            this.ViewBag.SORT_DIR = SORT_DIR;
            this.ViewBag.PAGE = PAGE;
            this.ViewBag.PAGESIZE = PAGESIZE;

            int TotalRecord = 0;
            //TotalRecord = 999;
            TotalRecord = _AdminFrontMemberService.GetAdminFrontMemberListCount(SEL_DATE_FROM, SEL_DATE_TO, SEL_GRADE_LIST, SEL_GBN, SEL_IS_RETIRE, SEARCH_COL, SEARCH_KEYWORD);

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림

            return View(_AdminFrontMemberService.GetAdminFrontMemberList(SEL_DATE_FROM, SEL_DATE_TO, SEL_GRADE_LIST, SEL_GBN, SEL_IS_RETIRE, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, PAGE, PAGESIZE).ToList());
        }


        // 관리자 - 회원 목록엑셀  /AdminUser/Excel/
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Excel(string SEL_DATE_FROM = "", string SEL_DATE_TO = "", string SEL_GRADE_LIST = "", string SEL_GBN = "", string SEL_IS_RETIRE = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "M_CREDATE", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10000000)
        {

            var products = new System.Data.DataTable("test");
            //헤더 구성
            products.Columns.Add("계정", typeof(string));
            products.Columns.Add("이름", typeof(string));
            products.Columns.Add("임직원구분", typeof(string));
            products.Columns.Add("임직원_회사", typeof(string));
            products.Columns.Add("임직원_사번", typeof(string));
            products.Columns.Add("등급", typeof(string));
            products.Columns.Add("EMAIL", typeof(string));
            products.Columns.Add("핸드폰", typeof(string));
            products.Columns.Add("전화", typeof(string));
            products.Columns.Add("현재포인트", typeof(string));

            products.Columns.Add("성별", typeof(string));
            products.Columns.Add("생일", typeof(string));
            products.Columns.Add("우편번호", typeof(string));
            products.Columns.Add("주소1", typeof(string));
            products.Columns.Add("주소2", typeof(string));

            products.Columns.Add("SMS수신", typeof(string));
            products.Columns.Add("EMAIL수신", typeof(string));
            products.Columns.Add("DM수신", typeof(string));
            products.Columns.Add("개인정보제3자제공에동의", typeof(string));
            products.Columns.Add("고유식별정보수집및이용동의", typeof(string));

            products.Columns.Add("가입일", typeof(string));
            products.Columns.Add("최종방문일", typeof(string));
            products.Columns.Add("탈퇴여부", typeof(string));
            products.Columns.Add("탈퇴일", typeof(string));
            products.Columns.Add("탈퇴시잔여포인트", typeof(string));
            products.Columns.Add("탈퇴사유", typeof(string));

            var Data = _AdminFrontMemberService.GetAdminFrontMemberList(SEL_DATE_FROM, SEL_DATE_TO, SEL_GRADE_LIST, SEL_GBN, SEL_IS_RETIRE, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, 1, 10000000).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {

                foreach (var result in Data)
                {
                    products.Rows.Add(result.M_ID, result.M_NAME, result.M_GBN, result.M_STAFF_COMAPNY, result.M_STAFF_ID, result.M_GRADE, result.M_EMAIL, result.M_MOBILE, result.M_PHONE, result.M_POINT
                                       , result.M_SEX, result.M_BIRTHDAY, result.M_ZIPCODE, result.M_ADDR1, result.M_ADDR2
                                       , result.M_ISSMS, result.M_ISEMAIL, result.M_ISDM, result.M_AGREE, result.M_AGREE2
                                       , result.M_CREDATE, result.M_LASTVISITDATE, result.M_IS_RETIRE, result.M_DEL_DATE, result.M_DEL_POINT, result.M_DEL_REASON
                        );
                } //for
            } //if

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=FrontMember.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "euc-kr";  //UTF-8 ,euc-kr
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("euc-kr");

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();

            //return View("MyView");

            return Content(sw.ToString(), "application/ms-excel");

        }
    
    
    
    }
}