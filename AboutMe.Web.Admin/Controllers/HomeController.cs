using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Admin.Common;

using AboutMe.Web.Admin.Common.Filters;

namespace AboutMe.Web.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region AES256 암호화 샘플코드, md5, sha256
        public ActionResult AES256()
        {
            AES256Cipher aes = new AES256Cipher();
            //string key = AboutMe.Common.Helper.Config.GetConfigValue("AES256_KEY");
            string key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            String encode = aes.AES_encrypt("test567", key);
            String decode = aes.AES_decrypt("ITXL5+ehBoUKyWWrO8Miog==", key);
            ViewBag.encode = encode;
            ViewBag.decode = decode;

            //md5, sh256
            String md5_encode = aes.MD5Hash("test567");
            String sha256_encode = aes.SHA256Hash("test567");
            ViewBag.md5_encode = md5_encode;
            ViewBag.sha256_encode = sha256_encode;
            ViewBag.md5_sha256_enc = aes.SHA256Hash(aes.MD5Hash("test567"));
            


            return View();
        }
        #endregion


        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
        public ActionResult MailTest()
        {
            return View();
        }

        [CustomAuthorize] //어드민로그인 필요 //[CustomAuthorize(Roles = "S")] //수퍼어드민만 가능 
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


    }
}