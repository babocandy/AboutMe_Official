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
            this.ViewBag.TOTAL_ORDER_CNT = 0; //총구매건수
            this.ViewBag.TOTAL_ORDER_PRICE = 0; //총 구매액
            this.ViewBag.TOTAL_COUPON_CNT = 0; //보유쿠폰
            this.ViewBag.TOTAL_QNA_CNT = 0; //1:1문의

            return View(_AdminFrontMemberService.GetAdminFrontMemberView(SEL_M_ID));
        }

        //관리자 - 회원  수정 -기본정보: ajax > JSON리턴
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
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + nERR_CODE_SP.ToString();
                if (nERR_CODE_SP == 10)
                    strERR_MSG = strERR_MSG + "\\nDB에서 회원정보를 찾을수 없습니다..";
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
            if (SEL_IDX == null || SEL_IDX<1)
                return Content("<script language='javascript' type='text/javascript'>alert('문서번호가 전달되지 않았습니다.');history.go(-1);</script>");


            return View(_AdminFrontMemberService.GetAdminMemberStaffRequestView(SEL_IDX));
        }

        //관리자 - 임직원 신청- 수정 : ajax > JSON리턴
        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult StaffRequestEditOK(int IDX = -1, string STATUS = "", string PROC_COMMENT = "", string PROC_ADM_ID = "")
        {
            int nERR_CODE = 0;
            int nERR_CODE_SP = 0;
            string strERR_MSG = "";
            //return View();
            if (IDX == null || IDX<1)
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

      
    
    }
}