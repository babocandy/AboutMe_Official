using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Admin.Common;

using AboutMe.Domain.Service.AdminEtc;
using AboutMe.Domain.Entity.AdminEtc;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Admin.Common.Filters;

//관리자 관리 ctl --jsh
namespace AboutMe.Web.Admin.Controllers
{
    public class AdminUserController : BaseAdminController
    {
        private IAdminUserService _AdminUserService;


        public AdminUserController(IAdminUserService _adminUserService)
        {
            this._AdminUserService = _adminUserService;
        }

       
        //관리자 로그인 폼
        public ActionResult Login( string RedirectUrl = "")
        {
            //세션 확인용
            /*
            CookieSessionStore cookiesession = new CookieSessionStore();
            this.ViewBag.ADM_ID = cookiesession.GetSecretSession("ADM_ID");
            this.ViewBag.ADM_NAME = cookiesession.GetSecretSession("ADM_NAME");
            this.ViewBag.ADM_GRADE = cookiesession.GetSecretSession("ADM_GRADE");
            this.ViewBag.ADM_EMAIL = cookiesession.GetSecretSession("ADM_EMAIL");
            this.ViewBag.ADM_PHONE = cookiesession.GetSecretSession("ADM_PHONE");
            */


            this.ViewBag.RedirectUrl = RedirectUrl;


            this.ViewBag.ADM_ID = AdminUserInfo.GetAdmId();
            this.ViewBag.ADM_NAME = AdminUserInfo.GetAdmName();
            this.ViewBag.ADM_GRADE = AdminUserInfo.GetAdmGrade();
            this.ViewBag.ADM_EMAIL = AdminUserInfo.GetAdmEmail();
            this.ViewBag.ADM_PHONE = AdminUserInfo.GetAdmPhone();


            return View();
        }



        //관리자 로그인 처리
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginProc(string ID = "", string PW = "", string RedirectUrl = "")
        {
            //로그 기록
            string memo = "관리자-로그인";
            string comment = "관리자-로그인";
            memo = memo + "|ID:" + ID;
            memo = memo + "|PW:" + PW;
            memo = memo + "|RedirectUrl:" + RedirectUrl;
            AdminLog adminlog = new AdminLog();
            //adminlog.AdminLogSave(memo, comment);



            //1.넘어온 인자값 확인 
            if (ID == "" || PW == "")
            {
                //this.ViewBag.ERR_CODE = "1";
                //this.ViewBag.ERR_MSG = "로그인 실패!. 아이디 or 패스워드가 전달되지 않았습니다.";
                //return RedirectToAction("Login", "AdminUser", new { ERR_CODE = 1, ERR_MSG = "로그인 실패! 아이디 or 패스워드가 전달되지 않았습니다." }); // 로그인 실패
                return Content("<script language='javascript' type='text/javascript'>alert('아이디 or 패스워드가 전달되지 않았습니다.');history.go(-1);</script>");
            }

            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            //string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            string ADM_PWD_MD5_HASH = objEnc.MD5Hash(PW);   //MD5: PW -> ADM_PWD_MD5_HASH
            string ADM_PWD_SHA256_HASH = objEnc.SHA256Hash(ADM_PWD_MD5_HASH);   //MD5: ADM_PWD_MD5_HASH -> ADM_PWD_SHA256_HASH


            //2.DB조회
            List<SP_ADMIN_ADMIN_LOGIN_Result> result_list = _AdminUserService.GetAdminLoginList(ID).ToList();
            if(result_list.Count <1)
            {
                memo = "관리자-로그인실패-계정부재";
                comment = "관리자-로그인실패-계정부재";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                //AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(memo, comment);

                return Content("<script language='javascript' type='text/javascript'>alert('아이디가 존재하지 않습니다.');history.go(-1);;</script>");
            }

            if (result_list[0].ADM_PWD != ADM_PWD_SHA256_HASH)  //인코딩 비교 필요
            {
                memo = "관리자-로그인실패-패스워드불일치";
                comment = "관리자-로그인실패-패스워드불일치";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                //AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(memo, comment);

                return Content("<script language='javascript' type='text/javascript'>alert('아이디 or 패스워드가 일치하지 않습니다.');history.go(-1);</script>");
            }
            else
            {
                this.ViewBag.ERR_CODE = "0";
                this.ViewBag.ERR_MSG = "로그인 성공!";
                //return RedirectToAction("Index", "AdminUser"); // 로그인 성공

                //관리자 세션or 쿠키 저장
                CookieSessionStore cookiesession = new CookieSessionStore();
                cookiesession.SetSecretSession("ADM_ID", result_list[0].ADM_ID);  //로그인 세션 세팅
                cookiesession.SetSecretSession("ADM_NAME", result_list[0].ADM_NAME);  //로그인 세션 세팅
                cookiesession.SetSecretSession("ADM_GRADE", result_list[0].ADM_GRADE);  //로그인 세션 세팅
                cookiesession.SetSecretSession("ADM_EMAIL", result_list[0].ADM_EMAIL);  //로그인 세션 세팅
                cookiesession.SetSecretSession("ADM_PHONE", result_list[0].ADM_PHONE);  //로그인 세션 세팅

                cookiesession.SetSession("ADM_NAME_TXT", result_list[0].ADM_NAME);  //로그인 세션 세팅 <--평문 이름
                cookiesession.SetSession("ADM_GRADE_TXT", result_list[0].ADM_GRADE);  //로그인 세션 세팅 <--평문 등급


                memo = "관리자-로그인성공";
                comment = "관리자-로그인성공";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                //AdminLog adminlog = new AdminLog();
                adminlog.AdminLogSave(memo, comment); 

                if (RedirectUrl!="")
                    return Content("<script language='javascript' type='text/javascript'>location.href='" + RedirectUrl + "';</script>");


                //세션 확인용-ssw
                /*
                this.ViewBag.ADM_ID = AdminUserInfo.GetAdmId();
                this.ViewBag.ADM_NAME = AdminUserInfo.GetAdmName();
                this.ViewBag.ADM_GRADE = AdminUserInfo.GetAdmGrade();
                this.ViewBag.ADM_EMAIL = AdminUserInfo.GetAdmEmail();
                this.ViewBag.ADM_PHONE = AdminUserInfo.GetAdmPhone();
                */
            }

            
            return RedirectToAction("Index", "Order"); // 로그인 성공
            //return View();
        }

        //관리자 로그아웃 처리
        public ActionResult Logout()
        {
            string LOGOUT_ADM_ID = AdminUserInfo.GetAdmId();

            CookieSessionStore cookiesession = new CookieSessionStore();
            cookiesession.SetSecretSession("ADM_ID", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("ADM_NAME", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("ADM_NAME_TXT", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("ADM_GRADE", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("ADM_EMAIL", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("ADM_PHONE", "");  //로그인 세션 세팅

            cookiesession.ClearSession(); //session Abandon

            //확인용 -jsh
            /*
            AdminInfo adminInfo = new AdminInfo();
            this.ViewBag.ADM_ID = adminInfo.getADM_ID();
            this.ViewBag.ADM_NAME = adminInfo.getADM_NAME();
            this.ViewBag.ADM_GRADE = adminInfo.getADM_GRADE();
            this.ViewBag.ADM_EMAIL = adminInfo.getADM_EMAIL();
            this.ViewBag.ADM_PHONE = adminInfo.getADM_PHONE();
            */
            //세션 확인용-ssw
            this.ViewBag.ADM_ID = AdminUserInfo.GetAdmId();
            this.ViewBag.ADM_NAME = AdminUserInfo.GetAdmName();
            this.ViewBag.ADM_GRADE = AdminUserInfo.GetAdmGrade();
            this.ViewBag.ADM_EMAIL = AdminUserInfo.GetAdmEmail();
            this.ViewBag.ADM_PHONE = AdminUserInfo.GetAdmPhone();


            //로그 기록
            string memo = "관리자-로그아웃";
            string comment = "관리자-로그아웃";
            memo = memo + "|ADM_ID:" + LOGOUT_ADM_ID;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);


            return RedirectToAction("Login", "AdminUser"); // 로그인 페이지로 이동
            //return View();
        }

        // GET: AdminUser 관리자관리-목록 /AdminUser/Index/
        [CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Index(string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORTCOL = "ADM_INS_DATE", string SORTDIR = "DESC", int PAGE = 1, int PAGESIZE = 10)
        {
            //return View();
            this.ViewBag.PAGESIZE = PAGESIZE;
            this.ViewBag.SEARCH_COL = SEARCH_COL;
            this.ViewBag.SEARCH_KEYWORD = SEARCH_KEYWORD;
            this.ViewBag.SORTCOL = SORTCOL;
            this.ViewBag.SORTDIR = SORTDIR;

            //AdminUserService srv = new AdminUserService();
            int TotalRecord = 0;
            TotalRecord = _AdminUserService.GetAdminMemberListCnt(SEARCH_COL, SEARCH_KEYWORD);
            //TotalRecord = srv.GetAdminMemberListCnt(SearchCol, SearchKeyword);

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = PAGE;


            return View(_AdminUserService.GetAdminMemberList(SEARCH_COL, SEARCH_KEYWORD, SORTCOL, SORTDIR, PAGE, PAGESIZE).ToList());
            //return View(srv.GetAdminMemberList(SearchCol, SearchKeyword, SortCol, SortDir, Page, PageSize).ToList());
        }

        // GET: AdminUser 관리자관리-등록폼 /AdminUser/Inert/
        [CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Insert()
        {
            return View();
         }
        // GET: AdminUser 관리자관리-등록저장 /AdminUser/InserOK/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult InsertOK(string ADM_ID = "", string ADM_NAME = "", string ADM_PWD = "", string ADM_GRADE = "A", string ADM_EMAIL = "", string ADM_PHONE = "", string ADM_USE_YN = "N")
        {

            int iERR_CODE = 0;
            string ERR_MSG = "";

            if (ADM_ID == "")
            {
                ERR_MSG = "==저장중 오류발생!==";
                ERR_MSG = ERR_MSG + "\\n ERR_CODE:" + iERR_CODE.ToString();
                ERR_MSG = ERR_MSG + "\\n 계정이 공란입니다..";
                return Content("<script language='javascript' type='text/javascript'>alert('" + ERR_MSG + "');history.go(-1);</script>");
            }


            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            //string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            string ADM_PWD_MD5_HASH = objEnc.MD5Hash(ADM_PWD);   //MD5: ADM_PWD -> ADM_PWD_MD5_HASH
            string ADM_PWD_SHA256_HASH = objEnc.SHA256Hash(ADM_PWD_MD5_HASH);   //MD5: ADM_PWD_MD5_HASH -> ADM_PWD_SHA256_HASH

            //DB저장-----------------------
            iERR_CODE = _AdminUserService.SetAdminMemberInsert(ADM_ID, ADM_NAME, ADM_PWD_SHA256_HASH, ADM_GRADE, ADM_EMAIL, ADM_PHONE, ADM_USE_YN);
            if (iERR_CODE != 0)
            {

                ERR_MSG = "==저장중 오류발생!==";
                ERR_MSG = ERR_MSG + "\\n ERR_CODE:" + iERR_CODE.ToString();
                if (iERR_CODE == 10)
                    ERR_MSG = ERR_MSG + "\\n이미 사용중인 계정입니다.";
                return Content("<script language='javascript' type='text/javascript'>alert('"+ERR_MSG+"');history.go(-1);</script>");


            }

            //로그 기록
            string memo = "관리자-관리자등록";
            string comment = "관리자-관리자등록";
            memo = memo + "|ADM_ID:" + ADM_ID;
            memo = memo + "|ADM_NAME:" + ADM_NAME;
            memo = memo + "|ADM_PWD:" + ADM_PWD;
            memo = memo + "|ADM_GRADE:" + ADM_GRADE;
            memo = memo + "|ADM_EMAIL:" + ADM_EMAIL;
            memo = memo + "|ADM_PHONE:" + ADM_PHONE;
            memo = memo + "|ADM_USE_YN:" + ADM_USE_YN;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);


            //정상 등록저장
            return RedirectToAction("Index", "AdminUser"); // 로그인 페이지로 이동
        }

        // GET: AdminUser 관리자관리-수정폼 /AdminUser/Edit/
        [CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Edit(string SEL_ADM_ID = "")
        {
            string ERR_MSG = "";
            if (SEL_ADM_ID == "")
            {
                ERR_MSG = "==오류발생!==";
                ERR_MSG = ERR_MSG + "\\n 계정이 전달되지 않았습니다.";
                return Content("<script language='javascript' type='text/javascript'>alert('" + ERR_MSG + "');history.go(-1);</script>");
            }

            //return View(_AdminUserService.GetAdminMemberView(SEL_ADM_ID)); //1건
 
            SP_ADMIN_ADMIN_MEMBER_VIEW_Result ret = _AdminUserService.GetAdminMemberView(SEL_ADM_ID);
            return View(ret);
        }

        // GET: AdminUser 관리자관리-수정저장 /AdminUser/EditOK/
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult EditOK(string ADM_ID = "", string ADM_NAME = "", string ADM_PWD = "", string ADM_GRADE = "A", string ADM_EMAIL = "", string ADM_PHONE = "", string ADM_USE_YN = "N")
        {

            int iERR_CODE = 0;
            string ERR_MSG = "";

            if (ADM_ID == "")
            {
                ERR_MSG = "==저장중 오류발생!==";
                ERR_MSG = ERR_MSG + "\\n ERR_CODE:" + iERR_CODE.ToString();
                ERR_MSG = ERR_MSG + "\\n 계정이 공란입니다..";
                return Content("<script language='javascript' type='text/javascript'>alert('" + ERR_MSG + "');history.go(-1);</script>");
            }


            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            //string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            string ADM_PWD_MD5_HASH = "";
            string ADM_PWD_SHA256_HASH = "";

            if (ADM_PWD != "") //암호가 변경되었음
            {
                ADM_PWD_MD5_HASH = objEnc.MD5Hash(ADM_PWD);   //MD5: ADM_PWD -> ADM_PWD_MD5_HASH
                ADM_PWD_SHA256_HASH = objEnc.SHA256Hash(ADM_PWD_MD5_HASH);   //MD5: ADM_PWD_MD5_HASH -> ADM_PWD_SHA256_HASH

            }
            else
            {
                ADM_PWD_SHA256_HASH = "";  //암호 변경 없음
            }

            //DB저장-----------------------
            iERR_CODE = _AdminUserService.SetAdminMemberUpdate(ADM_ID, ADM_NAME, ADM_PWD_SHA256_HASH, ADM_GRADE, ADM_EMAIL, ADM_PHONE, ADM_USE_YN);
            if (iERR_CODE != 0)
            {

                ERR_MSG = "==저장중 오류발생!==";
                ERR_MSG = ERR_MSG + "\\n ERR_CODE:" + iERR_CODE.ToString();


                return Content("<script language='javascript' type='text/javascript'>alert('" + ERR_MSG + "');history.go(-1);</script>");


            }


            //로그 기록
            string memo = "관리자-관리자수정";
            string comment = "관리자-관리자수정";
            memo = memo + "|ADM_ID:" + ADM_ID;
            memo = memo + "|ADM_NAME:" + ADM_NAME;
            memo = memo + "|ADM_PWD:" + ADM_PWD;
            memo = memo + "|ADM_GRADE:" + ADM_GRADE;
            memo = memo + "|ADM_EMAIL:" + ADM_EMAIL;
            memo = memo + "|ADM_PHONE:" + ADM_PHONE;
            memo = memo + "|ADM_USE_YN:" + ADM_USE_YN;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);

            //정상 수정저장
            return RedirectToAction("Index", "AdminUser"); // 목록 페이지로 이동
        }


        // GET: AdminUser 관리자관리-목록엑셀  /AdminUser/Excel/
        [CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Excel(string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORTCOL = "ADM_INS_DATE", string SORTDIR = "DESC", int PAGE = 1, int PAGESIZE = 10000000)
        {

            var products = new System.Data.DataTable("test");
            //헤더 구성
            products.Columns.Add("ID", typeof(string));
            products.Columns.Add("Name", typeof(string));
            products.Columns.Add("Grade", typeof(string));
            products.Columns.Add("Email", typeof(string));
            products.Columns.Add("Phone", typeof(string));
            products.Columns.Add("UseYN", typeof(string));

            var Data = _AdminUserService.GetAdminMemberList(SEARCH_COL, SEARCH_KEYWORD, SORTCOL, SORTDIR, 1, 10000000).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {

                 foreach (var result in Data)
                 {
                    products.Rows.Add(result.ADM_ID ,result.ADM_NAME ,result.ADM_GRADE ,result.ADM_EMAIL ,result.ADM_PHONE ,result.ADM_USE_YN );
                 } //for
            } //if

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=AdminUser2.xls");
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
            string memo = "관리자-관리자엑셀다운";
            string comment = "관리자-관리자엑셀다운";
            memo = memo + "|SEARCH_COL:" + SEARCH_COL;
            memo = memo + "|SEARCH_KEYWORD:" + SEARCH_KEYWORD;
            AdminLog adminlog = new AdminLog();
            adminlog.AdminLogSave(memo, comment);


            return Content(sw.ToString(), "application/ms-excel");

        }
        // GET: AdminUser 관리자관리-목록엑셀  /AdminUser/Excel2/  
        [CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult Excel2(string SEARCH_COL = "", string SEARCH_KEYWORD = "", string SORTCOL = "ADM_INS_DATE", string SORTDIR = "DESC", int PAGE = 1, int PAGESIZE = 10000000)
        {

             StringBuilder sb = new StringBuilder();
            //static file name, can be changes as per requirement
            string sFileName = "AdminUser.xls";
            //Bind data list from edmx
            var Data = _AdminUserService.GetAdminMemberList(SEARCH_COL, SEARCH_KEYWORD, SORTCOL, SORTDIR, 1, 10000000).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {
                sb.Append("<table style='1px solid black; font-size:12px;'>");
                sb.Append("<tr>");
                sb.Append("<td style='width:120px;'><b>ID</b></td>");
                sb.Append("<td style='width:300px;'><b>Name</b></td>");
                sb.Append("<td style='width:120px;'><b>Grade</b></td>");
                sb.Append("<td style='width:300px;'><b>Email</b></td>");
                sb.Append("<td style='width:300px;'><b>Phone</b></td>");
                sb.Append("<td style='width:300px;'><b>UseYN</b></td>");
                sb.Append("</tr>");

                foreach (var result in Data)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + result.ADM_ID + "</td>");
                    sb.Append("<td>" + result.ADM_NAME + "</td>");
                    sb.Append("<td>" + result.ADM_GRADE + "</td>");
                    sb.Append("<td>" + result.ADM_EMAIL + "</td>");
                    sb.Append("<td>" + result.ADM_PHONE + "</td>");
                    sb.Append("<td>" + result.ADM_USE_YN + "</td>");
                    sb.Append("</tr>");
                }
            }
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
            this.Response.ContentType = "application/vnd.ms-excel";

            Response.Charset = "euc-kr";  //UTF-8 ,euc-kr
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("euc-kr");

            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            byte[] buffer = System.Text.Encoding.Default.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel");

        }



    }
}