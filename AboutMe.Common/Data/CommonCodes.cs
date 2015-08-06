using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Common.Data
{
    public class BaseCode
    {

    }

    public class YNCode
    {        
        public static string YES = "Y";
        public static string NO = "N";
    }

    public class CategoryCode
    {
        public static string BEAUTY = "101";
        public static string HEALTH = "102";
        public static string SEL_SHOP = "103";

        public static string BEAUTY_DEFAULT = "101100100";
        public static string HEALTH_DEFAULT = "102100100";
        public static string SEL_SHOP_DEFAULT = "103100100";

        public static string BEAUTY_NAME = "뷰티";
        public static string HEALTH_NAME = "헬스";
        public static string SEL_SHOP_NAME = "셀렉샵";

        public static Dictionary<String, String> List = new Dictionary<string, string>(){
            { BEAUTY, BEAUTY_NAME},
            { HEALTH, HEALTH_NAME},
            { SEL_SHOP, SEL_SHOP_NAME}
        };

        public static string GetNameByCode(string code)
        {
            return code != null ? List[code] : "";
        }


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
            return code != null ? List[code] : "";
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
            return code != null ? List[code] : "";
        }

    }

    //팝업,리뷰등에서 사용
    public class MediaCode{
        
        public static string WEB = "10";
        public static string MOBILE = "20";
        public static string BOTH = "30";

        
        public static string WEB_NAME = "웹";
        public static string MOBILE_NAME = "모바일";
        public static string BOTH_NAME = "전체";

        public static Dictionary<String, String> List = new Dictionary<string, string>(){
            { BOTH, BOTH_NAME},
            { WEB, WEB_NAME},
            { MOBILE, MOBILE_NAME}
        };

        public static string GetNameByCode(string code)
        {
            return code != null ? List[code] : "";
        }
       

    }

    //리뷰에서 사용
    public class SkinTypeCode
    {
        public static string Dry = "10";
        public static string Normal = "20";
        public static string Oily = "30";

        public static string Dry_NAME = "지성";
        public static string Normal_NAME = "중성";
        public static string Oily_NAME = "지복합성";

        public static string Dry_CSS_NAME = "dry";
        public static string Normal_CSS_NAME = "normal";
        public static string Oily_CSS_NAME = "oily";

        public static Dictionary<String, String> List = new Dictionary<string, string>(){
            { Dry, Dry_NAME},
            { Normal, Normal_NAME},
            { Oily, Oily_NAME}
        };

        public static Dictionary<String, String> ListCss = new Dictionary<string, string>(){
            { Dry, Dry_CSS_NAME},
            { Normal, Normal_CSS_NAME},
            { Oily, Oily_CSS_NAME}
        };

        public static string GetNameByCode(string code)
        {

            return code != null ? List[code] : "";
        }
        public static string GetCssNameByCode(string code)
        {
            return code != null ? ListCss[code] : "";
        }
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
