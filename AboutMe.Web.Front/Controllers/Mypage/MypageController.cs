using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;

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
    public class MypageController : BaseFrontController
    {
        private IMemberService _MemberService;

        public MypageController(IMemberService _memberService)
        {
            this._MemberService = _memberService;
        }


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

        [CustomAuthorize]  //마이페이지 -탈퇴폼
        public ActionResult Retire()
        {

            return View();
        }


        [CustomAuthorize]  //마이페이지 -탈퇴처리
        public ActionResult RetireProc(string M_DEL_REASON="")
        {
            //로그 기록 준비
            string log_memo = "회원 탈퇴";
            string log_comment = "회원 탈퇴";
            UserLog userlog = new UserLog();


            string M_ID = MemberInfo.GetMemberId();
            string M_EMAIL = MemberInfo.GetMemberEmail();

            ReturnDic retDic = _MemberService.SetMemberRetire(M_ID,"Y", M_DEL_REASON);   //탈퇴처리
            if (retDic.ERR_CODE != "0") //오류
            {
                //로그 기록
                log_comment = log_comment + "실패";
                log_memo = log_memo + "실패";
                log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
                log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
                log_memo = log_memo + "|M_ID:" + M_ID;
                log_memo = log_memo + "|M_DEL_REASON:" + M_DEL_REASON;
                userlog.UserLogSave(log_memo, log_comment);

                return Content("<script language='javascript' type='text/javascript'>alert('처리중 오류 . ERR_CODE:" + retDic.ERR_CODE + "'); history.go(-1);</script>");
            }


            //로그 기록
            log_comment = log_comment + "성공";
            log_memo = log_memo + "성공";
            log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
            log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
            log_memo = log_memo + "|M_ID:" + M_ID;
            log_memo = log_memo + "|M_DEL_REASON:" + M_DEL_REASON;
            userlog.UserLogSave(log_memo, log_comment);

            //탈퇴메일 발송 --------------------------
            if (M_EMAIL != "")
            {
                string mail_skin_path = System.AppDomain.CurrentDomain.BaseDirectory + "aboutCom\\MailSkin\\"; //메일스킨 경로
                string cur_domain = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);  //도메인 ex http://www.aaa.co.kr:1234
                string skin_body = Utility01.GetTextResourceFile(mail_skin_path + "mail_memout.html");  //메일 스킨 txt Read
                skin_body = skin_body.Replace("$$DOMAIN$$", cur_domain);  //도메인
                skin_body = skin_body.Replace("$$M_ID$$", M_ID);  //아이디

                string MAIL_SUBJECT = "[AboutMe]어바웃미 회원 탈퇴가 완료 되었습니다.";
                string MAIL_BODY = skin_body;


                //메일 발송을 위한 발송정보 준비 ----------------------------------------------------
                string MAIL_SENDER_EMAIL = Config.GetConfigValue("MAIL_SENDER_EMAIL"); //noreply@cstone.co.kr
                string MAIL_SENDER_PW = Config.GetConfigValue("MAIL_SENDER_PW"); //cstonedev12
                string MAIL_SENDER_SMTP_SERVER = Config.GetConfigValue("MAIL_SENDER_SMTP_SERVER"); //smtp.gmail.com
                string MAIL_SENDER_SMTP_PORT = Config.GetConfigValue("MAIL_SENDER_SMTP_PORT"); //587
                string MAIL_SENDER_SMTP_TIMEOUT = Config.GetConfigValue("MAIL_SENDER_SMTP_TIMEOUT"); //20000

                //메일 발송
                MailSender mObj = new MailSender();
                mObj.MailSendAction(MAIL_SENDER_EMAIL, MAIL_SENDER_PW, MAIL_SENDER_SMTP_SERVER, MAIL_SENDER_SMTP_PORT, MAIL_SENDER_SMTP_TIMEOUT, M_EMAIL, MAIL_SUBJECT, MAIL_BODY);
            }

            //탈퇴로 인한 로그아웃세팅
            return Content("<script language='javascript' type='text/javascript'>alert('정상적으로 탈퇴처리 되었습니다.'); location.href='/MemberShip/Logout';</script>");



        }


        public ActionResult test()
        {
            return View();
        }
    }
}