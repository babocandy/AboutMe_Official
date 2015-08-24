using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using AboutMe.Common.Helper;
using AboutMe.Web.Admin.Common;

using AboutMe.Domain.Service.AdminFrontMember;
using AboutMe.Domain.Entity.AdminFrontMember;
using AboutMe.Domain.Service.AdminOrder;
using AboutMe.Domain.Entity.AdminOrder;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Admin.Common.Filters;
using System.Data.OleDb;
using System.Data;

//관리자 -회원 관리 ctl --jsh

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminFrontMemberController : BaseAdminController
    {
        private IAdminFrontMemberService _AdminFrontMemberService;
        private IAdminOrderService _adminorderservice;

        public AdminFrontMemberController(IAdminFrontMemberService _adminFrontMemberService, IAdminOrderService _adminorderservice)
        {
            this._AdminFrontMemberService = _adminFrontMemberService;
            this._adminorderservice = _adminorderservice;
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

        //관리자 - 회원 상세
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Edit(string SEL_M_ID = "", string SEL_DATE_FROM = "", string SEL_DATE_TO = "", string SEL_GRADE_LIST = "", string SEL_GBN = "", string SEL_IS_RETIRE = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "M_CREDATE", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
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
            if (SEL_M_ID=="")
                return Content("<script language='javascript' type='text/javascript'>alert('회원원아이디가 전달되지 않았습니다.');history.go(-1);</script>");


            //회원 써머리 정보  : 초기화
            SP_ADMIN_ORDER_MEMBER_STATUS_Result orderResult = _adminorderservice.OrderMemberStatus(SEL_M_ID);
            this.ViewBag.TOTAL_ORDER_CNT = orderResult.ORDER_CNT; //총구매건수
            this.ViewBag.TOTAL_ORDER_PRICE = orderResult.ORDER_PRICE; //총 구매액
            this.ViewBag.TOTAL_COUPON_CNT = 0; //보유쿠폰
            this.ViewBag.TOTAL_QNA_CNT = orderResult.QNA_CNT; //1:1문의

            return View(_AdminFrontMemberService.GetAdminFrontMemberView(SEL_M_ID));
        }

        //관리자 - 회원  수정 -기본정보: ajax > JSON리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult EditOK_Basic(string M_ID = "", string M_GRADE = "", string M_MOBILE = "--", string M_PHONE = "--", string M_EMAIL = "@", string M_ZIPCODE = "", string M_ADDR1 = "", string M_ADDR2 = "", string M_ISSMS = "", string M_ISEMAIL = "", string M_ISDM = "")
        {
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";
            //return View();
            if (M_ID == "")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('회원원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 1;
                strERR_MSG = "회원 아이디가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }

            //DB저장: 회원  수정 -기본정보-----------------------
            nERR_CODE_SP = _AdminFrontMemberService.SetAdminFrontMemberUpdate_Basic(M_ID, M_GRADE, M_MOBILE, M_PHONE, M_EMAIL, M_ZIPCODE, M_ADDR1, M_ADDR2, M_ISSMS, M_ISEMAIL, M_ISDM);
            if (nERR_CODE_SP != 0)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\nDB에서 회원정보를 찾을수 없습니다.";
                if (nERR_CODE_SP == 11)
                    strERR_MSG = strERR_MSG + "\nEMAIL이 중복됩니다.";
            }


            //로그 기록
            string memo = "관리자-회원정보수정-기본정보";
            string comment= "관리자-회원정보수정-기본정보" ;
            memo = memo + "|nERR_CODE:" + nERR_CODE.ToString();
            memo = memo + "|strERR_MSG:" + strERR_MSG;
            memo = memo + "|M_ID:" + M_ID;
            memo = memo + "|M_GRADE:" + M_GRADE;
            memo = memo + "|M_PHONE:" + M_PHONE;
            memo = memo + "|M_EMAIL:" + M_EMAIL;
            memo = memo + "|M_ZIPCODE:" + M_ZIPCODE;
            memo = memo + "|M_ADDR1:" + M_ADDR1;
            memo = memo + "|M_ADDR2:" + M_ADDR1;
            memo = memo + "|M_ISSMS:" + M_ISSMS;
            memo = memo + "|M_ISEMAIL:" + M_ISEMAIL;
            memo = memo + "|M_ISDM:" + M_ISDM;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);

            //결과 리턴
            return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG }); 

        }

        //관리자 - 회원  수정 -암호변경: ajax > JSON리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult EditOK_PWD(string M_ID = "", string M_PWD="")
        {
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";
            //return View();
            if (M_ID == "")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('회원원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 1;
                strERR_MSG = "회원 아이디가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }
            if (M_PWD == "")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('회원원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 2;
                strERR_MSG = "암호가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }

            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            //string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            string M_PWD_MD5_HASH = objEnc.MD5Hash(M_PWD);   //MD5: ADM_PWD -> ADM_PWD_MD5_HASH
            string M_PWD_SHA256_HASH = objEnc.SHA256Hash(M_PWD_MD5_HASH);   //MD5: ADM_PWD_MD5_HASH -> ADM_PWD_SHA256_HASH


            //DB저장: 회원  수정 -암호변경-----------------------
            nERR_CODE_SP = _AdminFrontMemberService.SetAdminFrontMemberUpdate_PWD(M_ID, M_PWD_SHA256_HASH);
            if (nERR_CODE_SP != 0)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\nDB에서 회원정보를 찾을수 없습니다..";
            }

            //로그 기록
            string memo = "관리자-회원정보수정-암호변경";
            string comment = "관리자-회원정보수정-암호변경";
            memo = memo + "|nERR_CODE:" + nERR_CODE.ToString();
            memo = memo + "|strERR_MSG:" + strERR_MSG;
            memo = memo + "|M_ID:" + M_ID;
            memo = memo + "|M_PWD:" + M_PWD;
            memo = memo + "|M_PWD_MD5_HASH:" + M_PWD_MD5_HASH;
            memo = memo + "|M_PWD_SHA256_HASH:" + M_PWD_SHA256_HASH;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);

            //결과 리턴
            return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });

        }

        //관리자 - 회원  수정 -임직원: ajax > JSON리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult EditOK_Staff(string M_ID = "", string M_GBN = "", string M_STAFF_COMPANY = "", string M_STAFF_ID = "")
        {
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";
            //return View();
            if (M_ID == "")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('회원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 1;
                strERR_MSG = "회원 아이디가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }


            //DB저장: 회원  수정 -임직원-----------------------
            nERR_CODE_SP = _AdminFrontMemberService.SetAdminFrontMemberUpdate_Staff(M_ID, M_GBN, M_STAFF_COMPANY, M_STAFF_ID);
            if (nERR_CODE_SP != 0)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\nDB에서 회원정보를 찾을수 없습니다..";
            }

            //로그 기록
            string memo = "관리자-회원정보수정-임직원";
            string comment = "관리자-회원정보수정-임직원";
            memo = memo + "|nERR_CODE:" + nERR_CODE.ToString();
            memo = memo + "|strERR_MSG:" + strERR_MSG;
            memo = memo + "|M_ID:" + M_ID;
            memo = memo + "|M_GBN:" + M_GBN;
            memo = memo + "|M_STAFF_COMPANY:" + M_STAFF_COMPANY;
            memo = memo + "|M_STAFF_ID:" + M_STAFF_ID;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);

            return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });

        }

        //관리자 - 회원  수정 -탈퇴: ajax > JSON리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult EditOK_Retire(string M_ID = "", string M_DEL_REASON = "")
        {
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";
            //return View();
            if (M_ID == "")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('회원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 1;
                strERR_MSG = "회원 아이디가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }

            string strIS_RETIRE = "Y";

            //DB저장: 회원  수정 -탈퇴-----------------------
            nERR_CODE_SP = _AdminFrontMemberService.SetAdminFrontMemberUpdate_Retire(M_ID, strIS_RETIRE, M_DEL_REASON);
            if (nERR_CODE_SP != 0)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\nDB에서 회원정보를 찾을수 없습니다..";
            }

            //로그 기록
            string memo = "관리자-회원정보수정-탈퇴";
            string comment = "관리자-회원정보수정-탈퇴";
            memo = memo + "|nERR_CODE:" + nERR_CODE.ToString();
            memo = memo + "|strERR_MSG:" + strERR_MSG;
            memo = memo + "|strIS_RETIRE:" + strIS_RETIRE;
            memo = memo + "|M_DEL_REASON:" + M_DEL_REASON;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);



            return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });

        }

        //관리자 - 회원 상세 팝업
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult PopMemberInfo(string M_ID = "")
        {
            if (M_ID == "")
                return Content("<script language='javascript' type='text/javascript'>alert('회원원아이디가 전달되지 않았습니다.');self.close();</script>");

            this.ViewBag.M_ID = M_ID;
            return View();
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

            int nDOWN_ROW_CNT = 0;
            var Data = _AdminFrontMemberService.GetAdminFrontMemberList(SEL_DATE_FROM, SEL_DATE_TO, SEL_GRADE_LIST, SEL_GBN, SEL_IS_RETIRE, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, 1, 10000000).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {

                foreach (var result in Data)
                {
                    products.Rows.Add(result.M_ID, result.M_NAME, result.M_GBN, result.M_STAFF_COMPANY, result.M_STAFF_ID, result.M_GRADE, result.M_EMAIL, result.M_MOBILE, result.M_PHONE, result.M_POINT
                                       , result.M_SEX, result.M_BIRTHDAY, result.M_ZIPCODE, result.M_ADDR1, result.M_ADDR2
                                       , result.M_ISSMS, result.M_ISEMAIL, result.M_ISDM, result.M_AGREE, result.M_AGREE2
                                       , result.M_CREDATE, result.M_LASTVISITDATE, result.M_IS_RETIRE, result.M_DEL_DATE, result.M_DEL_POINT, result.M_DEL_REASON
                        );

                    nDOWN_ROW_CNT++;
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

            //로그 기록
            string memo = "관리자-회원정보-엑셀다운";
            string comment = "관리자-회원정보-엑셀다운";
            memo = memo + "|nDOWN_ROW_CNT:" + nDOWN_ROW_CNT.ToString();
            memo = memo + "|SEL_DATE_FROM:" + SEL_DATE_FROM;
            memo = memo + "|SEL_DATE_TO:" + SEL_DATE_TO;
            memo = memo + "|SEL_GRADE_LIST:" + SEL_GRADE_LIST;
            memo = memo + "|SEL_GBN:" + SEL_GBN;
            memo = memo + "|SEL_IS_RETIRE:" + SEL_IS_RETIRE;
            memo = memo + "|SEARCH_COL:" + SEARCH_COL;
            memo = memo + "|SEARCH_KEYWORD:" + SEARCH_KEYWORD;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);


            return Content(sw.ToString(), "application/ms-excel");

        }

        //-임직원 신청  =============================================================================================================================
        //관리자 - 임직원 신청 - 목록
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffRequestIndex(string SEL_DATE_FROM = "", string SEL_DATE_TO = "", string SEL_STATUS = "",  string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "IDX", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
        {
            //return View();

            this.ViewBag.SEL_DATE_FROM = SEL_DATE_FROM;
            this.ViewBag.SEL_DATE_TO = SEL_DATE_TO;
            this.ViewBag.SEL_STATUS = SEL_STATUS;

            this.ViewBag.SEARCH_COL = SEARCH_COL;
            this.ViewBag.SEARCH_KEYWORD = SEARCH_KEYWORD;
            this.ViewBag.SORT_COL = SORT_COL;
            this.ViewBag.SORT_DIR = SORT_DIR;
            this.ViewBag.PAGE = PAGE;
            this.ViewBag.PAGESIZE = PAGESIZE;

            int TotalRecord = 0;
            //TotalRecord = 999;
            TotalRecord = _AdminFrontMemberService.GetAdminMemberStaffRequestListCount(SEL_DATE_FROM, SEL_DATE_TO, SEL_STATUS, SEARCH_COL, SEARCH_KEYWORD);

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림

            return View(_AdminFrontMemberService.GetAdminMemberStaffRequestList(SEL_DATE_FROM, SEL_DATE_TO, SEL_STATUS, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, PAGE, PAGESIZE).ToList());
        }

        //관리자 - 임직원 신청 - 상세
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffRequestEdit(int SEL_IDX = -1, string SEL_DATE_FROM = "", string SEL_DATE_TO = "", string SEL_STATUS = "",  string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "IDX", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
        {
            //return View();

            this.ViewBag.SEL_IDX = SEL_IDX;

            this.ViewBag.SEL_DATE_FROM = SEL_DATE_FROM;
            this.ViewBag.SEL_DATE_TO = SEL_DATE_TO;
            this.ViewBag.SEL_STATUS = SEL_STATUS;

            this.ViewBag.SEARCH_COL = SEARCH_COL;
            this.ViewBag.SEARCH_KEYWORD = SEARCH_KEYWORD;
            this.ViewBag.SORT_COL = SORT_COL;
            this.ViewBag.SORT_DIR = SORT_DIR;
            this.ViewBag.PAGE = PAGE;
            this.ViewBag.PAGESIZE = PAGESIZE;
            if (SEL_IDX<1)
                return Content("<script language='javascript' type='text/javascript'>alert('문서번호가 전달되지 않았습니다.');history.go(-1);</script>");


            return View(_AdminFrontMemberService.GetAdminMemberStaffRequestView(SEL_IDX));
        }

        //관리자 - 임직원 신청- 수정 : ajax > JSON리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffRequestEditOK(int IDX = -1, string STATUS = "", string PROC_COMMENT = "")
        {
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";

            string PROC_ADM_ID = AdminUserInfo.GetAdmId();  //처리자 계정 (관리자 로그인계정)

            //return View();
            if (IDX<1)
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('문서번호가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 1;
                strERR_MSG = "문서번호가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }


            //DB저장: 임직원 신청  수정 -----------------------
            nERR_CODE_SP = _AdminFrontMemberService.SetAdminMemberStaffRequestUpdate(IDX, STATUS, PROC_COMMENT, PROC_ADM_ID);
            if (nERR_CODE_SP != 0)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\nDB에서 문서를 찾을수 없습니다..";
            }

            //로그 기록
            string memo = "관리자-임직원신청수정";
            string comment = "관리자-임직원신청수정";
            memo = memo + "|nERR_CODE:" + nERR_CODE.ToString();
            memo = memo + "|strERR_MSG:" + strERR_MSG;
            memo = memo + "|IDX:" + IDX.ToString();
            memo = memo + "|STATUS:" + STATUS;
            memo = memo + "|PROC_COMMENT:" + PROC_COMMENT;
            memo = memo + "|PROC_ADM_ID:" + PROC_ADM_ID;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);

            return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });

        }

        //-임직원 기준DB  =============================================================================================================================
        //관리자 - 임직원 기준DB - 목록
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffBaseIndex(string SEL_DATE_FROM = "", string SEL_DATE_TO = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "IDX", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
        {
            //return View();

            this.ViewBag.SEL_DATE_FROM = SEL_DATE_FROM;
            this.ViewBag.SEL_DATE_TO = SEL_DATE_TO;

            this.ViewBag.SEARCH_COL = SEARCH_COL;
            this.ViewBag.SEARCH_KEYWORD = SEARCH_KEYWORD;
            this.ViewBag.SORT_COL = SORT_COL;
            this.ViewBag.SORT_DIR = SORT_DIR;
            this.ViewBag.PAGE = PAGE;
            this.ViewBag.PAGESIZE = PAGESIZE;

            int TotalRecord = 0;
            //TotalRecord = 999;
            TotalRecord = _AdminFrontMemberService.GetAdminMemberStaffBaseCount(SEL_DATE_FROM, SEL_DATE_TO, SEARCH_COL, SEARCH_KEYWORD);

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림

            return View(_AdminFrontMemberService.GetAdminMemberStaffBaseList(SEL_DATE_FROM, SEL_DATE_TO, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, PAGE, PAGESIZE).ToList());
        }
        
        //관리자 - 임직원 기준DB - 엑셀
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffBaseExcel(string SEL_DATE_FROM = "", string SEL_DATE_TO = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "IDX", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 100000)
        {

            var products = new System.Data.DataTable("test");
            //헤더 구성
            products.Columns.Add("회사", typeof(string));
            products.Columns.Add("사번", typeof(string));
            products.Columns.Add("이름", typeof(string));
            products.Columns.Add("등록일", typeof(string));
            
            int nDOWN_ROW_CNT = 0;
            var Data = _AdminFrontMemberService.GetAdminMemberStaffBaseList(SEL_DATE_FROM, SEL_DATE_TO, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, 1, 100000).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {

                foreach (var result in Data)
                {
                    products.Rows.Add(result.STAFF_COMPANY, result.STAFF_ID, result.STAFF_NAME, result.INS_DATE);

                    nDOWN_ROW_CNT++;
                } //for
            } //if

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=StaffBase.xls");
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

            //로그 기록
            string memo = "관리자-임직원 기준DB-엑셀다운";
            string comment = "관리자-임직원 기준DB-엑셀다운";
            memo = memo + "|nDOWN_ROW_CNT:" + nDOWN_ROW_CNT.ToString();
            memo = memo + "|SEL_DATE_FROM:" + SEL_DATE_FROM;
            memo = memo + "|SEL_DATE_TO:" + SEL_DATE_TO;
            memo = memo + "|SEARCH_COL:" + SEARCH_COL;
            memo = memo + "|SEARCH_KEYWORD:" + SEARCH_KEYWORD;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);


            return Content(sw.ToString(), "application/ms-excel");

        }


        //관리자 - 임직원 기준DB - 삭제 :ajax > JSON리턴
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult AjaxStaffBaseDeleteProc(int IDX=-1)
        {
            //return View();
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";

            //string PROC_ADM_ID = AdminUserInfo.GetAdmId();  //처리자 계정 (관리자 로그인계정)

            //return View();
            if (IDX < 1)
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('문서번호가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 1;
                strERR_MSG = "문서번호가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }


            //DB저장: 임직원 기준DB - 삭제 -----------------------
            nERR_CODE_SP = _AdminFrontMemberService.SetAdminMemberStaffBaseDel(IDX);
            if (nERR_CODE_SP != 0)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\nDB에서 문서를 찾을수 없습니다..";
            }

            //로그 기록
            string memo = "관리자-임직원기준DB - 삭제";
            string comment = "관리자-임직원기준DB - 삭제";
            memo = memo + "|nERR_CODE:" + nERR_CODE.ToString();
            memo = memo + "|strERR_MSG:" + strERR_MSG;
            memo = memo + "|IDX:" + IDX.ToString();

            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);

            return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
        }


        //관리자 - 임직원 기준DB - 등록 폼
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffBaseInsert()
        {
            return View();
        }

        //관리자 - 임직원 기준DB - 등록(개별1건) :ajax > JSON리턴
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult AjaxStaffBaseInsertProc(string STAFF_ID = "", string STAFF_NAME = "", string STAFF_COMPANY = "")
        {
            //return View();
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";

            //string PROC_ADM_ID = AdminUserInfo.GetAdmId();  //처리자 계정 (관리자 로그인계정)

            //return View();
            if (STAFF_ID=="")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('문서번호가 전달되지 않았습니다.');history.go(-1);</script>");
                nERR_CODE = 1;
                strERR_MSG = "사번이 전달되지 않았습니다.";
                return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
            }

            string strNOW_TIME = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string strWORK_TEMP_ID = "E"+strNOW_TIME + "_" + Utility01.GetRandomAlphanumeric(4);
            //DB저장: 임직원 기준DB - 등록(개별1건) -----------------------
            nERR_CODE_SP = _AdminFrontMemberService.SetAdminMemberStaffBaseInsert(STAFF_COMPANY, STAFF_ID, STAFF_NAME,strWORK_TEMP_ID);
            if (nERR_CODE_SP != 0)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\nDB에서 문서를 찾을수 없습니다..";
            }
            if (nERR_CODE_SP == 10)
            {
                nERR_CODE = nERR_CODE_SP;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n 사번:" + STAFF_ID;
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\n 이미 존재하는 사번입니다.";
            }
            //로그 기록
            string memo = "관리자-임직원기준DB - 개별등록";
            string comment = "관리자-임직원기준DB - 개별등록";
            memo = memo + "|nERR_CODE:" + nERR_CODE.ToString();
            memo = memo + "|strERR_MSG:" + strERR_MSG;
            memo = memo + "|STAFF_ID:" + STAFF_ID;
            memo = memo + "|STAFF_NAME:" + STAFF_NAME;
            memo = memo + "|STAFF_COMPANY:" + STAFF_COMPANY;
            memo = memo + "|strWORK_TEMP_ID:" + strWORK_TEMP_ID;

            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);

            return Json(new { ERR_CODE = nERR_CODE.ToString(), ERR_MSG = strERR_MSG });
        }


        //관리자 - 임직원 기준DB - Excel등록(파일 업로드 & DB저장) :ajax > JSON리턴
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffBaseExcelUplodBatch()
        {
            DataSet ds = new DataSet();
            int OK_CNT = 0;

            if (Request.Files["FILE1"].ContentLength < 1)
            {
                return Content("엑셀 첨부파일 오류1.");
            }

            string fileExtension = System.IO.Path.GetExtension(Request.Files["FILE1"].FileName);

            if (fileExtension.ToLower() == ".xls" || fileExtension.ToLower() == ".xlsx")
                ;
            else
            {
                return Content("엑셀 첨부파일 오류2.<br>엑셀파일이 아님!");

            }

  
            //string fileLocation = Server.MapPath("~/Upload/Staff/") + System.IO.Path.GetFileName(Request.Files["FILE1"].FileName);
            //string fileLocation = Config.GetConfigValue("StaffBaseDB") +System.IO.Path.GetFileName(Request.Files["FILE1"].FileName);  // 보안상의 이유- 제외
            //string fileLocation = Server.MapPath("~"+Config.GetConfigValue("StaffBaseDB")) + System.IO.Path.GetFileName(Request.Files["FILE1"].FileName);  // 보안상의 이유- 제외
            string fileLocation = Server.MapPath("~/App_Data/Staff/") + System.IO.Path.GetFileName(Request.Files["FILE1"].FileName);
            
            if (System.IO.File.Exists(fileLocation))
            {

                System.IO.File.Delete(fileLocation);
            }
            Request.Files["FILE1"].SaveAs(fileLocation);
            string excelConnectionString = string.Empty;
            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //connection String for xls file format.
            if (fileExtension.ToLower() == ".xls")
            {
                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            //connection String for xlsx file format.
            else if (fileExtension.ToLower() == ".xlsx")
            {
                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            //Create Connection to Excel work book and add oledb namespace
            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
            excelConnection.Open();
            DataTable dt = new DataTable();

            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                return null;
            }

            String[] excelSheets = new String[dt.Rows.Count];
            int t = 0;
            //excel data saves in temp file here.
            foreach (DataRow row in dt.Rows)
            {
                excelSheets[t] = row["TABLE_NAME"].ToString();
                t++;
            }
            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


            string query = string.Format("Select * from [{0}]", excelSheets[0]);
            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
            {
                dataAdapter.Fill(ds);
            }


            string strNOW_TIME = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string strWORK_TEMP_ID = "B" + strNOW_TIME + "_" + Utility01.GetRandomAlphanumeric(4);
            string strADM_ID = AdminUserInfo.GetAdmId(); 
            string strIP = "";
            int nERR_CODE_SP;

            //Step1. 임시테이블 저장
            //string conn = "data source=0.0.0.0;initial catalog=AboutMe;user id=uuuu;password=1111;";
            //SqlConnection con = new SqlConnection(conn);
            //con.Open();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][0].ToString() != "")
                {
                    //string query = "Insert into TB_MEMBER_STAFF_BASE_TMP " +
                    //    " (STAFF_COMAPNY,STAFF_ID,STAFF_NAME,INS_DATE,WORK_TEMP_ID,APP_RESULT,ADM_ID,IP) " +
                    //    " Values " +
                    //    " ('" + ds.Tables[0].Rows[i][0].ToString() + "','" + ds.Tables[0].Rows[i][1].ToString() + "','" + ds.Tables[0].Rows[i][2].ToString() + "',getdate(),'" + strWORK_TEMP_ID + "','','" + strADM_ID + "','" + strIP + "')";
                    //SqlCommand cmd = new SqlCommand(query, con);
                    //cmd.ExecuteNonQuery();
                    string strSTAFF_COMPANY = ds.Tables[0].Rows[i][0].ToString() ;
                    string strSTAFF_ID=ds.Tables[0].Rows[i][1].ToString() ;
                    string strSTAFF_NAME = ds.Tables[0].Rows[i][2].ToString();

                    nERR_CODE_SP = _AdminFrontMemberService.SetAdminMemberStaffBaseTempInsert(strSTAFF_COMPANY, strSTAFF_ID, strSTAFF_NAME,strWORK_TEMP_ID,strADM_ID,strIP); //임시테이블 저장
                    OK_CNT++;
                }
            } //for
            //con.Close();



            //Step2. 임시테이블 검증 및 임직원테이블 등록
            List<SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST_Result> tmpList = _AdminFrontMemberService.GetAdminMemberStaffBaseTempList(strWORK_TEMP_ID);
            for(int i=0; i<tmpList.Count;i++)
            {
                nERR_CODE_SP = _AdminFrontMemberService.SetAdminMemberStaffBaseInsert(tmpList[i].STAFF_COMPANY,tmpList[i].STAFF_ID,tmpList[i].STAFF_NAME,tmpList[i].WORK_TEMP_ID); //임직원 DB - 1건 INSERT
                if(nERR_CODE_SP==0) //성공
                    _AdminFrontMemberService.SetAdminMemberStaffBaseTempUpdate(tmpList[i].IDX,"추가");
                else if(nERR_CODE_SP==10) //중복오류
                    _AdminFrontMemberService.SetAdminMemberStaffBaseTempUpdate(tmpList[i].IDX,"중복오류");
                else  //기타오류
                    _AdminFrontMemberService.SetAdminMemberStaffBaseTempUpdate(tmpList[i].IDX,"기타오류");


            }

            //업로드 엑셀파일 삭제 : 사용중 오류 발생
            excelConnection.Close();
            //FileInfo file = new FileInfo(fileLocation);
            //file.Delete();
            if (System.IO.File.Exists(fileLocation))
            {

                System.IO.File.Delete(fileLocation);
            }

            //return View();
            //return View(_AdminFrontMemberService.GetAdminMemberStaffBaseTempList(strWORK_TEMP_ID)); //결과 리스트
            return RedirectToAction("StaffBaseExcelUploadResult", "AdminFrontMember", new { WORK_TEMP_ID = strWORK_TEMP_ID }); // 결과 리스트


        }

        //관리자 - 임직원 기준DB - Excel등록결과
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffBaseExcelUploadResult(string WORK_TEMP_ID = "XXXXXXXX")
        {
            this.ViewBag.WORK_TEMP_ID = WORK_TEMP_ID;
            return View(_AdminFrontMemberService.GetAdminMemberStaffBaseTempList(WORK_TEMP_ID)); //결과 리스트
        }

        //-휴면계정  =============================================================================================================================
        //휴면계정-목록
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult SleepingIndex(string M_LASTVISITDATE = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "M_LASTVISITDATE", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
        {
            //return View();
            if (M_LASTVISITDATE == "")
                M_LASTVISITDATE = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");  //기본 1년전 로그인

            this.ViewBag.M_LASTVISITDATE = M_LASTVISITDATE;

            this.ViewBag.SEARCH_COL = SEARCH_COL;
            this.ViewBag.SEARCH_KEYWORD = SEARCH_KEYWORD;
            this.ViewBag.SORT_COL = SORT_COL;
            this.ViewBag.SORT_DIR = SORT_DIR;
            this.ViewBag.PAGE = PAGE;
            this.ViewBag.PAGESIZE = PAGESIZE;


            int TotalRecord = 0;
            //TotalRecord = 999;
            TotalRecord = _AdminFrontMemberService.GetAdminMemberSleepingCount(M_LASTVISITDATE, SEARCH_COL, SEARCH_KEYWORD);

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림

            return View(_AdminFrontMemberService.GetAdminMemberSleepingList(M_LASTVISITDATE, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, PAGE, PAGESIZE).ToList());
        }

        //휴면계정-엑셀
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult SleepingExcel(string M_LASTVISITDATE = "", string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORT_COL = "M_LASTVISITDATE", string SORT_DIR = "DESC", int PAGE = 1, int PAGESIZE = 100000)
        {
            //return View();
            if (M_LASTVISITDATE == "")
                M_LASTVISITDATE = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");  //기본 1년전 로그인


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

            int nDOWN_ROW_CNT = 0;
            var Data = _AdminFrontMemberService.GetAdminMemberSleepingList(M_LASTVISITDATE, SEARCH_COL, SEARCH_KEYWORD, SORT_COL, SORT_DIR, 1, 100000).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {

                foreach (var result in Data)
                {
                    products.Rows.Add(result.M_ID, result.M_NAME, result.M_GBN, result.M_STAFF_COMPANY, result.M_STAFF_ID, result.M_GRADE, result.M_EMAIL, result.M_MOBILE, result.M_PHONE, result.M_POINT
                                       , result.M_SEX, result.M_BIRTHDAY, result.M_ZIPCODE, result.M_ADDR1, result.M_ADDR2
                                       , result.M_ISSMS, result.M_ISEMAIL, result.M_ISDM, result.M_AGREE, result.M_AGREE2
                                       , result.M_CREDATE, result.M_LASTVISITDATE, result.M_IS_RETIRE, result.M_DEL_DATE, result.M_DEL_POINT, result.M_DEL_REASON
                        );

                    nDOWN_ROW_CNT++;
                } //for
            } //if

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=SleepingMember.xls");
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

            //로그 기록
            string memo = "관리자-회원정보-엑셀다운-휴면계정";
            string comment = "관리자-회원정보-엑셀다운-휴면계정";
            memo = memo + "|nDOWN_ROW_CNT:" + nDOWN_ROW_CNT.ToString();
            memo = memo + "|M_LASTVISITDATE:" + M_LASTVISITDATE;
            memo = memo + "|SEARCH_COL:" + SEARCH_COL;
            memo = memo + "|SEARCH_KEYWORD:" + SEARCH_KEYWORD;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);


            return Content(sw.ToString(), "application/ms-excel");

        }


        //#####################################################################################################################################
        //-//데이타 이행 :회원암호 -list  --오픈전 마이그레이션시 1회 필요 =============================================================================================================================
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL()
        {
            //return View();

            AES256Cipher objEnc = new AES256Cipher();

            List<SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL_Result> result = _AdminFrontMemberService.GetZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL();

            int TotalRecord = result.Count;

            int ACTION_CNT = 0;

            this.ViewBag.START = DateTime.Now;

            for (int i = 0; i < result.Count;i++ )
            {
                string M_ID = result[i].M_ID;
                string M_PWD = result[i].M_PWD;
                string ZZ_OLD_PWD_MD5 = result[i].ZZ_OLD_PWD_MD5;

                //string M_PWD_MD5_HASH = objEnc.MD5Hash(M_PWD);  
                string M_PWD_SHA256_HASH = objEnc.SHA256Hash(ZZ_OLD_PWD_MD5);   //MD5-> SHA256_HASH

                if(M_PWD.Length<1 && ZZ_OLD_PWD_MD5.Length>20)
                {
                    _AdminFrontMemberService.SetZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_UPD(M_ID, M_PWD_SHA256_HASH);
                    ACTION_CNT++;

                }

            } //for

            this.ViewBag.TotalRecord = TotalRecord; //대상건수
            this.ViewBag.ACTION_CNT = ACTION_CNT;  //처리건수
            this.ViewBag.END = DateTime.Now;

            return View();
        }      
    
    } //clsss
}// namespace