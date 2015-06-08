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

        #region AES256 암호화 샘플코드
        public ActionResult AES256()
        {
            AES256Cipher aes = new AES256Cipher();
            //string key = AboutMe.Common.Helper.Config.GetConfigValue("AES256_KEY");
            string key = Config.GetConfigValue("AES256_KEY");
            String encode = aes.AES_encrypt("test567", key);
            String decode = aes.AES_decrypt("ITXL5+ehBoUKyWWrO8Miog==", key);
            ViewBag.encode = encode;
            ViewBag.decode = decode;
            return View();
        }
        #endregion

    }
}