using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Common.Data
{
    public class CommonCode
    {        
        public static string YES = "Y";
        public static string NO = "N";
    }

    public class MemberCode
    {
        public static string BRONZE = "B";
        public static string SILVER = "S";
        public static string GOLD = "G";
        public static string VIP = "V";

        public static string BRONZE_NAME = "브론즈";
        public static string SILVER_NAME = "실버";
        public static string GOLD_NAME = "골드";
        public static string VIP_NAME = "VIP";


        public static Dictionary<String, String> List = new Dictionary<string, string>(){
            { BRONZE, BRONZE_NAME},
            { SILVER, SILVER_NAME},
            { GOLD, GOLD_NAME}, 
            { VIP, VIP_NAME}
        };

        public static string GetNameByCode(string code)
        {
            return List[code];
        }


    }

    public class MemberGbnCode
    {
        public static string GENERAL = "A";
        public static string STAFF = "S";

        public static string GENERAL_NAME = "일반회원";
        public static string STAFF_NAME = "임직원";

        public static Dictionary<String, String> List = new Dictionary<string, string>(){
            { GENERAL, GENERAL_NAME},
            { STAFF, STAFF_NAME}
        };

        public static string GetNameByCode(string code)
        {
            return List[code];
        }

    }

    //팝업,리뷰등에서 사용
    public class MediaCode{
        public static string ALL = "10";
        public static string WEB = "20";
        public static string MOBILE = "30";

        public static string ALL_NAME = "전체";
        public static string WEB_NAME = "웹";
        public static string MOBILE_NAME = "모바일";

        public static Dictionary<String, String> List = new Dictionary<string, string>(){
            { ALL, ALL_NAME},
            { WEB, WEB_NAME},
            { MOBILE, MOBILE_NAME}
        };

        public static string GetNameByCode(string code)
        {
            return List[code];
        }
       

    }

    //리뷰에서 사용
    public class SkinTypeCode
    {
        public static string Dry = "10";
        public static string Normal = "20";
        public static string Oily = "30";
    }

    //전시물 코드
    public class DisplayerCode
    {
        public static string WEB_MAIN_BANNER = "10";
        public static string WEB_MAIN_MIDDLE_BANNER = "20";
        public static string WEB_MAIN_PRODUCT_DISPLAY = "30";


        public static string MOBILE_MAIN_BANNER = "40";
        public static string MOBILE_MAIN_TALK = "50";
        public static string MOBILE_MAIN_BEST = "60";

        public static string CART_WEB = "70";
        public static string CART_MOBILE = "80";
        public static string CART_PRODUCT_DISPLAY = "90";

        public static string GBN_BANNER = "100";
        public static string GBN_LINK = "110";

        public static string PDT_DETAIL_WEB = "120";
        public static string PDT_DETAIL_MOBILE = "130";

        public static string SUB_KIND_10 = "10";
        public static string SUB_KIND_20 = "20";
    }
}
