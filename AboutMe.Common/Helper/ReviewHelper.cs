using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Common.Helper
{
    public class ReviewHelper
    {
        /*
         * 스킨타입값 변경-> css className
         */
        public static string ConvertSkinType(string code)
        {
            //10=건성, 20=중성, 30=지복합성

            string ret = "";

            if (code == "10")
            {
                ret = "dry";
            }
            else if (code == "20")
            {
                ret = "normal";
            }
            else if (code == "30")
            {
                ret = "oily";
            }

            return ret;

        }

        /**
         * 나이대
         */
        public static string ConvertAge(string birthday)
        {
            var d = DateTime.Parse(birthday);
            double age = DateTimeOffset.Now.Year - d.Year;
            age = Math.Floor(age / 10) * 10;

            return age + "대";
        }

        /**
         * 성별
         */
        public static string ConvertGenger(string gender)
        {
            return (gender == "1") ? "남자" : "여자";
        }

        public static bool IsBeauty(string pCateCode )
        {
            return pCateCode.Substring(0, 3) == "101" ? true : false;
        }

        public static string isBest(string pz)
        {
            return pz == "99" ? "best_choice" : "";
            
        }
    }
}
