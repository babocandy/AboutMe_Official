using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace AboutMe.Common.Helper
{
    public class CookieSessionStore
    {
        //---------------------------- cookie-----------------------------------
        public void SetSecretCookie(string key = "", string value = "", int expire_days = 99999)
        {
            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.

            this.SetCookie(key, objEnc.AES_encrypt(value,ENC_key), expire_days);
        }
        public string GetSecretCookie(string key = "")
        {
            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            return objEnc.AES_decrypt(this.GetCookie(key), ENC_key);
        }

        //public static void SetCookie(string key="", string value="", TimeSpan expires)
        public void SetCookie(string key = "", string value = "", int expire_days = 99999)
        {
            HttpCookie encodedCookie = new HttpCookie(key, value);

            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var cookieOld = HttpContext.Current.Request.Cookies[key];
                //cookieOld.Expires = DateTime.Now.Add(expires);
                if (expire_days != 99999)
                {
                   // cookieOld.Expires.AddDays(expire_days);
                    cookieOld.Expires = DateTime.UtcNow.AddDays(30);
                }

                cookieOld.Value = encodedCookie.Value;
                HttpContext.Current.Response.Cookies.Add(cookieOld);
            }
            else
            {
                //encodedCookie.Expires = DateTime.Now.Add(expires);
                if (expire_days != 99999)
                {
                    // encodedCookie.Expires.AddDays(expire_days);
                    encodedCookie.Expires = DateTime.UtcNow.AddDays(30);
                }

                HttpContext.Current.Response.Cookies.Add(encodedCookie);
            }
        }
        public string GetCookie(string key = "")
        {
            string value = ""; // string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
            {
                // For security purpose, we need to encrypt the value.
                HttpCookie decodedCookie = cookie;
                value = decodedCookie.Value;
            }
            return value;
        }

        public void ClearCookies()  //쿠키 클리어시 세션도 사라지는 문제있음
        {
            string[] myCookies = HttpContext.Current.Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                HttpContext.Current.Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
        }

        //---------------------------- session ---------------------------------------
        public void SetSecretSession(string key = "", string value = "")
        {
            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.

            this.SetSession(key, objEnc.AES_encrypt(value, ENC_key));
        }
        public string GetSecretSession(string key = "")
        {
            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            return objEnc.AES_decrypt(this.GetSession(key), ENC_key);
        }


        public void SetSession(string key = "", string value = "")
        {
            HttpContext.Current.Session[key] = value;
        }
        public string GetSession(string key = "")
        {
            string value = ""; // string.Empty;
            value = (string)HttpContext.Current.Session[key];
            return value;
        }

        public void ClearSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Abandon();
        }
       

    }
}
