using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;

using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Cart;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;



using AboutMe.Web.Front.Common.Filters;


namespace AboutMe.Web.Front.Controllers
{
    public class MemberShipController : BaseFrontController
    {
        private IMemberService _MemberService;
        private ICartService _Cartservice;


        public MemberShipController(IMemberService _memberService, ICartService _cartservice)
        {
            this._MemberService = _memberService;
            this._Cartservice = _cartservice;
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
            //memo = memo + "|ID:" + ID;
            //memo = memo + "|PW:" + PW;
            //memo = memo + "|RedirectUrl:" + RedirectUrl;
            //UserLog userlog = new UserLog();
            //userlog.UserLogSave(memo, comment);




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
                UserLog userlog = new UserLog();
                userlog.UserLogSave(memo, comment);


                return Content("<script language='javascript' type='text/javascript'>alert('아이디가 존재하지 않습니다.');history.go(-1);;</script>");
            }

            if (result.M_PWD != M_PWD_SHA256_HASH)  //인코딩 비교 필요
            {
                memo = "사용자-로그인실패-패스워드불일치";
                comment = "사용자-로그인실패-패스워드불일치";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                UserLog userlog = new UserLog();
                userlog.UserLogSave(memo, comment);


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
                cookiesession.SetSecretSession("M_GBN", result.M_GBN);  //로그인 세션 세팅

                cookiesession.SetSecretSession("M_SKIN_TROUBLE_CD", result.M_SKIN_TROUBLE_CD);  //로그인 세션 세팅

                cookiesession.SetSession("M_SKIN_TROUBLE_CD_TEXT", result.M_SKIN_TROUBLE_CD);  //로그인 세션 세팅 -평문


                _Cartservice.CartMerge(result.M_ID, _user_profile.SESSION_ID, "Y");  //로그인후 장바구니 합치기 : 서비스 호출  << 지젠 요청 :  HttpContext.Session.SessionID???
                

                memo = "사용자-로그인성공";
                comment = "사용자-로그인성공";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                UserLog userlog = new UserLog();
                userlog.UserLogSave(memo, comment);


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
            UserLog userlog = new UserLog();
            userlog.UserLogSave(memo, comment);



            //return RedirectToAction("Login", "Member"); // 로그인 페이지로 이동

            return Content("<script language='javascript' type='text/javascript'>alert('로그아웃 되었습니다.'); location.href='/MemberShip/Login';</script>");
        }



        //사용자 회원가입- Step1 -실명인증
        public ActionResult JoinStep1()
        {
            return View();
        }
        //사용자 회원가입- Step1 -실명인증 결과 
        public ActionResult RealNameResult(string WORK_TMP_ID = "", string M_JOIN_MODE="")
        {
            if (WORK_TMP_ID == null || WORK_TMP_ID == "" || M_JOIN_MODE == "" || M_JOIN_MODE == null)
                return Content("<script language='javascript' type='text/javascript'>alert('실명인증 정보가 전달되지 않았습니다.'); location.href='/MemberShip/JoinStep1';</script>");

            //실명인증 정상여부 판단.<<<<<<<<<<<<<<<<<<<<<<

            //DB임시 저장 필요<<<<<<<<<<<<<<<<<<<<<<

            //다음 스텝 이동<<<<<<<<<<<<<<<<<<<<<<
            return RedirectToAction("JoinStep2", "MemberShip", new { WORK_TMP_ID = WORK_TMP_ID }); // 실명인증 성공 -> Go Step2
        }
        

        //사용자 회원가입- Step2 -약관동의
        public ActionResult JoinStep2(string WORK_TMP_ID="")
        {
            if (WORK_TMP_ID == null || WORK_TMP_ID == "")
                return Content("<script language='javascript' type='text/javascript'>alert('실명인증 정보가 전달되지 않았습니다.'); location.href='/MemberShip/JoinStep1';</script>");

            this.ViewBag.WORK_TMP_ID = WORK_TMP_ID;
            return View();
        }

        //사용자 회원가입- Step3 -회원정보 입력
        public ActionResult JoinStep3(string WORK_TMP_ID = "")
        {
            if (WORK_TMP_ID == null || WORK_TMP_ID == "")
                return Content("<script language='javascript' type='text/javascript'>alert('실명인증 정보가 전달되지 않았습니다.'); location.href='/MemberShip/JoinStep1';</script>");

            this.ViewBag.WORK_TMP_ID = WORK_TMP_ID;
            return View();
        }

        //사용자 회원가입- Step3-1 --ID중복체크
        public ActionResult AjaxID_DUPCheck(string M_ID = "")
        {
            string strERR_CODE = "0";
            string strERR_MSG = "";
            if (M_ID == "")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('회원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                strERR_CODE = "1";
                strERR_MSG = "회원 아이디가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = strERR_CODE, ERR_MSG = strERR_MSG });
            }

            //DB저장: 회원 가입시 ID중복체크
            ReturnDic rObj = _MemberService.GetMemberID_Dup_Check(M_ID);

            return Json(new { ERR_CODE = rObj.ERR_CODE, ERR_MSG =rObj.ERR_MSG });
           

        }

        

    } //class
}//namespace