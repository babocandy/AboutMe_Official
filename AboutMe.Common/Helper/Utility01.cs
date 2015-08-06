using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;  //for Random 

using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Configuration;

using System.Web.Mvc;

using System.Data.Entity;
using System.Data.Entity.Core.Objects;

using System.Web;
using System.Collections.Specialized;


namespace AboutMe.Common.Helper
{
    public class Utility01
    {

        public static List<SelectListItem> GetDropDownList<T>(string text, string value, string selected, List<T> result) where T : class
        {
            

                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "-Please select-", Value = string.Empty });
                //IQueryable<T> result = Db.Repository<T>();

                //IQueryable<T> result = DataValues;
               // var lisData = (from items in result
                var lisData = (from items in result.AsQueryable()  // AsQueryable 추가 ssw
                               select items).AsEnumerable().Select(m => new SelectListItem
                               {
                                   Text = (string)m.GetType().GetProperty(text).GetValue(m),
                                   Value = (string)m.GetType().GetProperty(value).GetValue(m),
                                   Selected = (selected != "") ? ((string)
                                     m.GetType().GetProperty(value).GetValue(m) == 
                                     selected ? true : false) : false,
                               }).ToList();
                list.AddRange(lisData);
                return list;
          }


        //문자 숫자 랜덤  :혼동소지의 char는 제거 ,O,o,0 ,I,i,1,l,L
        public static string GetRandomAlphanumeric(int length=4)
        {
            const string alphanumericCharacters =
                "ABCDEFGHJKMNPQRSTUVWXYZ" +
                "abcdefghjkmnpqrstuvwxyz" +
                "23456789";
            return GetRandomAny(length, alphanumericCharacters);
        }
        //문자 랜덤 :혼동소지의 char는 제거 ,O,o,0 ,I,i,1,l,L
        public static string GetRandomAlpha(int length = 4)
        {
            const string alphanumericCharacters =
                "ABCDEFGHJKMNPQRSTUVWXYZ" +
                "abcdefghjkmnpqrstuvwxyz";
            return GetRandomAny(length, alphanumericCharacters);
        }
        //숫자 랜덤 
        public static string GetRandomNumberString(int length = 4)
        {
            const string alphanumericCharacters = "0123456789";
            return GetRandomAny(length, alphanumericCharacters);
        }
        //랜덤 팩토리
        public static string GetRandomAny(int length = 4, IEnumerable<char> characterSet=null )
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }


        //서버의 로컬파일 text파일 가져오기 ex)text,html,메일스킨,... 읽기
        //사용예
        //    string cur_domain = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);  //현재 도메인 구하기. ex http://www.aaa.co.kr:1234  
        //    string mail_skin_path = System.AppDomain.CurrentDomain.BaseDirectory + "aboutCom\\MailSkin\\" ;  //메일스킨이 존재하는 폴더(웹루트이하로 가정)
        //    string skin_body =Utility01.GetTextResourceFile(mail_skin_path + "mail_join.html");  //메일스킨 파일명
        //    skin_body = skin_body.Replace("$$DOMAIN$$", cur_domain);  //도메인 변경
        public static string GetTextResourceFile(string resourceName)
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

        /**
         * 객체를 uri 쿼리 형태로 변환
         */
        public static string ConvertObject2UriQuery(object o)
        {

            Type t = o.GetType();
            NameValueCollection nvc = new NameValueCollection();
            foreach (var p in t.GetProperties())
            {
                var name = p.Name;
                var tmp = p.GetValue(o, null);
                var value = tmp != null ? tmp.ToString() : "";
                nvc.Add(name, value);
            }

            var result = ConstructQueryString(nvc);
            return result;
        }

        private static string ConstructQueryString(NameValueCollection Params)
        {
            List<string> items = new List<string>();
            foreach (string name in Params)
                items.Add(String.Concat(name, "=", HttpUtility.UrlEncode(Params[name])));
            return string.Join("&", items.ToArray());
        }


    } //class
}//namespace
