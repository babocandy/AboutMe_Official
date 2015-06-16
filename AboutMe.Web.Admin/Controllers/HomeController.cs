using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;

namespace AboutMe.Web.Admin.Controllers
{
    public class HomeController : Controller
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

    }
}