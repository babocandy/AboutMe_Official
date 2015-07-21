using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using AboutMe.Common.Helper;

using System.Reflection;
using System.IO;


namespace AboutMe.Web.Test.Controllers
{
    public class TestJSHController : Controller
    {
        // GET: TestJSH : Mailsend
        public ActionResult Index()
        {

            return View();
        }
        [ValidateInput(false)]
        public ActionResult SendMailGo(string receiver = "", string subject = "[AboutMe]No Subject", string body = "")
        {

            //메일 발송을 위한 발송정보 준비 ----------------------------------------------------
            string MAIL_SENDER_EMAIL = Config.GetConfigValue("MAIL_SENDER_EMAIL"); //noreply@cstone.co.kr
            string MAIL_SENDER_PW = Config.GetConfigValue("MAIL_SENDER_PW"); //cstonedev12
            string MAIL_SENDER_SMTP_SERVER = Config.GetConfigValue("MAIL_SENDER_SMTP_SERVER"); //smtp.gmail.com
            string MAIL_SENDER_SMTP_PORT = Config.GetConfigValue("MAIL_SENDER_SMTP_PORT"); //587
            string MAIL_SENDER_SMTP_TIMEOUT = Config.GetConfigValue("MAIL_SENDER_SMTP_TIMEOUT"); //20000

            //메일 발송
            MailSender mObj = new MailSender();
            mObj.MailSendAction(MAIL_SENDER_EMAIL, MAIL_SENDER_PW, MAIL_SENDER_SMTP_SERVER, MAIL_SENDER_SMTP_PORT, MAIL_SENDER_SMTP_TIMEOUT, receiver, subject, body);


            ViewBag.mail_err_no = mObj.err_no;
            ViewBag.mail_err_msg = mObj.err_msg;

            ViewBag.mail_sender = mObj.sender;
            ViewBag.mail_receiver = mObj.receiver;
            ViewBag.mail_subject = mObj.subject;
            ViewBag.mail_body = mObj.body;


            return View();
        }

        public ActionResult SendMailGoUsinMailSkin(string receiver = "", string subject = "[AboutMe]No Subject")
        {
            string base_path = "D:\\CStone\\삼양어바웃미\\VisualStudio\\AboutMe_Official\\AboutMe.Web.Test\\Views\\TestJSH\\";
            string skin_body = GetTextResourceFile(base_path + "mail_skin01.html");
            skin_body = skin_body.Replace("$$NAME$$", "홍길동");
            skin_body = skin_body.Replace("$$SUBJECT$$", "테스트 제목 ");


            MailSender mObj = new MailSender();
            //mObj.MailSendAction(receiver, subject, body);
            mObj.MailSendAction(receiver, subject, skin_body);

            ViewBag.mail_err_no = mObj.err_no;
            ViewBag.mail_err_msg = mObj.err_msg;

            ViewBag.mail_sender = mObj.sender;
            ViewBag.mail_receiver = mObj.receiver;
            ViewBag.mail_subject = mObj.subject;
            ViewBag.mail_body = mObj.body;


            return View();
        }

        public string GetTextResourceFile(string resourceName)
        {
            return System.IO.File.ReadAllText(resourceName);
            /*
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
             */
        }

        //---COOKIE & SESSION TEST ----------------------------------------------------------------------------------------
        // GET: 
        
        public ActionResult CookieSessionIndex()
        {

            CookieSessionStore cookiesession = new CookieSessionStore();

            this.ViewBag.COOKIE_id = cookiesession.GetSecretCookie("id");
            this.ViewBag.COOKIE_email = cookiesession.GetSecretCookie("email");

            this.ViewBag.SESSION_id = cookiesession.GetSecretSession("id");
            this.ViewBag.SESSION_email = cookiesession.GetSecretSession("email");


            return View();
        }
        
        public ActionResult SET_COOKIE_SESSION(string id = "", string email = "")
        {

            CookieSessionStore cookiesession = new CookieSessionStore();
            cookiesession.SetSecretCookie("id", id);
            cookiesession.SetSecretCookie("email", email);

            cookiesession.SetSecretSession("id", id);
            cookiesession.SetSecretSession("email", email);

            
            return RedirectToAction("CookieSessionIndex", "TestJSH");
            //return View();
        }

        public ActionResult CLEAR_COOKIES()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            cookiesession.ClearCookies();

            return RedirectToAction("CookieSessionIndex", "TestJSH");

        }

        public ActionResult CLEAR_SESSION()
        {
            CookieSessionStore cookiesession = new CookieSessionStore();
            cookiesession.ClearSession();

            return RedirectToAction("CookieSessionIndex", "TestJSH");

        }


    }
}