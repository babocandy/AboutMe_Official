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


//회원관련 ctl --jsh
namespace AboutMe.Web.Front.Controllers
{
    public class MemberController : BaseFrontController
    {

        private IMemberService _MemberService;


        public MemberController(IMemberService _memberService)
        {
            this._MemberService = _memberService;
        }



        // GET: Member
        public ActionResult Index()
        {
            return View();
        }



        //사용자 로그인 폼
        public ActionResult Login(string RedirectUrl = "")
        {
            this.ViewBag.RedirectUrl = RedirectUrl;


            this.ViewBag.M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade();
            this.ViewBag.M_EMAIL = MemberInfo.GetMemberEmail();


            return View();
        }

        //사용자 로그인
        public ActionResult LoginProc(string ID = "", string PW = "", string RedirectUrl = "")
        {
            //로그 기록
            string memo = "사용자-로그인";
            string comment = "사용자-로그인";
            memo = memo + "|ID:" + ID;
            memo = memo + "|PW:" + PW;
            memo = memo + "|RedirectUrl:" + RedirectUrl;
            //FrontLog frontlog = new FrontLog();
            //frontlog.FrontLogSave(memo, comment);



            //1.넘어온 인자값 확인 
            if (ID == "" || PW == "")
            {
                //this.ViewBag.ERR_CODE = "1";
                //this.ViewBag.ERR_MSG = "로그인 실패!. 아이디 or 패스워드가 전달되지 않았습니다.";
                //return RedirectToAction("Login", "Member", new { ERR_CODE = 1, ERR_MSG = "로그인 실패! 아이디 or 패스워드가 전달되지 않았습니다." }); // 로그인 실패
                return Content("<script language='javascript' type='text/javascript'>alert('아이디 or 패스워드가 전달되지 않았습니다.');history.go(-1);</script>");
            }

            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            //string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            string M_PWD_MD5_HASH = objEnc.MD5Hash(PW);   //MD5: PW -> M_PWD_MD5_HASH
            string M_PWD_SHA256_HASH = objEnc.SHA256Hash(M_PWD_MD5_HASH);   //MD5: M_PWD_MD5_HASH -> M_PWD_SHA256_HASH


            //2.DB조회
            //List<SP_MEMBER_VIEW_Result> result_list = _MemberService.GetMemberView(ID);
            SP_MEMBER_VIEW_Result result = _MemberService.GetMemberView(ID);
            if (result == null)
            {
                memo = "사용자-로그인실패-계정부재";
                comment = "사용자-로그인실패-계정부재";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                //FrontLog frontlog = new FrontLog();
                //frontlog.FrontLogSave(memo, comment);

                return Content("<script language='javascript' type='text/javascript'>alert('아이디가 존재하지 않습니다.');history.go(-1);;</script>");
            }

            if (result.M_PWD != M_PWD_SHA256_HASH)  //인코딩 비교 필요
            {
                memo = "사용자-로그인실패-패스워드불일치";
                comment = "사용자-로그인실패-패스워드불일치";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                //FrontLog frontlog = new FrontLog();
                //frontlog.FrontLogSave(memo, comment);

                return Content("<script language='javascript' type='text/javascript'>alert('아이디 or 패스워드가 일치하지 않습니다.');history.go(-1);</script>");
            }
            else
            {
                this.ViewBag.ERR_CODE = "0";
                this.ViewBag.ERR_MSG = "로그인 성공!";
                //return RedirectToAction("Index", "AdminUser"); // 로그인 성공

                //관리자 세션or 쿠키 저장
                CookieSessionStore cookiesession = new CookieSessionStore();
                cookiesession.SetSecretSession("M_ID", result.M_ID);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_NAME", result.M_NAME);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_GRADE", result.M_GRADE);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_EMAIL", result.M_EMAIL);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_PHONE", result.M_PHONE);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_SKIN_TROUBLE_CD", result.M_SKIN_TROUBLE_CD);  //로그인 세션 세팅

                cookiesession.SetSession("M_SKIN_TROUBLE_CD_TEXT", result.M_SKIN_TROUBLE_CD);  //로그인 세션 세팅 -평문

                


                memo = "사용자-로그인성공";
                comment = "사용자-로그인성공";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                //FrontLog frontlog = new FrontLog();
                //frontlog.FrontLogSave(memo, comment);

                if (RedirectUrl != "")
                    return Content("<script language='javascript' type='text/javascript'>location.href='" + RedirectUrl + "';</script>");


            }


            return RedirectToAction("Main", "Mypage"); // 로그인 성공
            //return View();        }
        }


        //사용자 로그아웃 처리
        public ActionResult Logout()
        {
            string LOGOUT_M_ID = MemberInfo.GetMemberId();  //

            CookieSessionStore cookiesession = new CookieSessionStore();
            cookiesession.SetSecretSession("M_ID", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_NAME", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_NAME_TXT", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_GRADE", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_EMAIL", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_PHONE", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_SKIN_TROUBLE_CD", "");  //로그인 세션 세팅

            cookiesession.SetSession("M_SKIN_TROUBLE_CD_TEXT", "");  //로그인 세션 세팅 -평문

            cookiesession.ClearSession(); //session Abandon


    

            //로그 기록
            string memo = "사용자-로그아웃";
            string comment = "사용자-로그아웃";
            memo = memo + "|M_ID:" + LOGOUT_M_ID;
            //FrontLog frontlog = new FrontLog();
            //frontlog.FrontLogSave(memo, comment);


            //return RedirectToAction("Login", "Member"); // 로그인 페이지로 이동

            return Content("<script language='javascript' type='text/javascript'>alert('로그아웃 되었습니다.'); location.href='/Member/Login';</script>");
        }




    }
}