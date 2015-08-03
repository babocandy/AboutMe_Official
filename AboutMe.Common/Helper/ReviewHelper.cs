using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Review;

namespace AboutMe.Common.Helper
{
    public class ReviewHelper
    {

        public static List<ReviewProductInfo> GetDataForDisplay(List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> r){

            List<ReviewProductInfo> list = new List<ReviewProductInfo>();

            foreach (var item in r)
            {
                var d = new ReviewProductInfo();

                d.IDX = item.IDX;
                d.M_ID = item.M_ID;
                d.M_NAME = item.M_NAME;
                d.M_SEX = ConvertGenger(item.M_SEX);
                d.M_BIRTHDAY = ConvertAge(item.M_BIRTHDAY);
                
                d.P_CODE = item.P_CODE;
                d.P_MAIN_IMG = item.P_MAIN_IMG;
                d.P_SUB_TITLE = item.P_SUB_TITLE;
                d.P_NAME = item.P_NAME;
                d.C_CATE_CODE = item.C_CATE_CODE;


                d.SKIN_TYPE = ConvertSkinType(item.SKIN_TYPE);
                d.COMMENT = item.COMMENT;
                d.ADD_IMAGE = item.ADD_IMAGE;

                d.IS_BEAUTY = IsBeauty(item.CATE_GBN);
                d.IS_BEST = item.IS_BEST == "Y" ? true : false;
                d.IS_PHOTO = item.IS_PHOTO == "Y" ? true : false;

                d.INS_DATE = item.INS_DATE.Value.ToString("yyyy.MM.dd");

                list.Add(d);
            }
            return list;
        }

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

        public static bool IsBest(string pz)
        {
            return pz == "99" ? true : false;
            
        }
    }
}
