using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Specialized;

using AboutMe.Domain.Entity.Review;
using AboutMe.Domain.Entity.AdminReview;
using AboutMe.Common.Data;

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

                d.IS_BEAUTY = item.CATE_GBN == "101" ? true : false;
                d.IS_BEST = item.IS_BEST == CommonCode.YES ? true : false;
                d.IS_PHOTO = item.IS_PHOTO == CommonCode.YES ? true : false;

                d.INS_DATE = item.INS_DATE.Value.ToString("yyyy.MM.dd");

                list.Add(d);
            }
            return list;
        }

        /**
         * 관리자 사용자 화면용 - 목록
         */
        public static List<AdminReviewUserModel> GetDataForAdminUser(List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result> r)
        {

            List<AdminReviewUserModel> list = new List<AdminReviewUserModel>();

            foreach (var item in r)
            {
                var d = new AdminReviewUserModel();
    
                d.IDX = item.IDX;
                d.M_ID = item.M_ID;
                d.P_CODE = item.P_CODE;
                d.P_NAME = item.P_NAME;
                d.C_CATE_CODE = item.C_CATE_CODE;
                d.CATE_GBN = item.CATE_GBN == "101" ? "뷰티" : "헬스";
                d.COMMENT = item.COMMENT;
                d.IS_PHOTO = item.IS_PHOTO;
                d.INS_DATE = item.INS_DATE.Value.ToShortDateString();
                d.IS_BEST = item.IS_BEST == "Y" ? "O" : "";

                d.P_MAIN_IMG = item.P_MAIN_IMG;
                d.VIEW_CNT = item.VIEW_CNT ?? 0;
                d.M_GRADE = MemberCode.GetNameByCode( item.M_GRADE );
                d.MEDIA_GBN = item.MEDIA_GBN != null ? MediaCode.GetNameByCode(item.MEDIA_GBN) : "";
                d.ADD_IMAGE = item.ADD_IMAGE;
                d.IS_DISPLAY = item.IS_DISPLAY == "Y" ? "전시" : "비전시";

                list.Add(d);
            }
            return list;
        }

        /**
         * 관리자 사용자 화면용 - 수정
         */
        public static List<AdminReviewUserModel> GetDataForAdminUserSave(List<SP_ADMIN_REVIEW_PRODUCT_INFO_Result> r)
        {

            List<AdminReviewUserModel> list = new List<AdminReviewUserModel>();

            foreach (var item in r)
            {
                var d = new AdminReviewUserModel();

                d.IDX = item.IDX;
                d.M_ID = item.M_ID;
                d.P_CODE = item.P_CODE;
                d.P_NAME = item.P_NAME;
                d.C_CATE_CODE = item.C_CATE_CODE;
                d.CATE_GBN = item.CATE_GBN;
                d.COMMENT = item.COMMENT;
                d.IS_PHOTO = item.IS_PHOTO;
                d.INS_DATE = item.INS_DATE.ToShortDateString();
                d.IS_BEST = item.IS_BEST;
                d.P_MAIN_IMG = item.P_MAIN_IMG;
                d.VIEW_CNT = item.VIEW_CNT ?? 0;
                d.M_GRADE = item.M_GRADE;
                d.MEDIA_GBN = item.MEDIA_GBN != null ? MediaCode.GetNameByCode(item.MEDIA_GBN) : "";
                d.ADD_IMAGE = item.ADD_IMAGE;
                d.IS_DISPLAY = item.IS_DISPLAY;
                
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

            if (code == SkinTypeCode.Dry)
            {
                ret = "dry";
            }
            else if (code == SkinTypeCode.Normal)
            {
                ret = "normal";
            }
            else if (code == SkinTypeCode.Oily)
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

        /**
         * 뷰티 체크
         */
        public static bool CheckBeauty(string pCateCode)
        {
            return pCateCode.Substring(0, 3) == "101" ? true : false;
        }



    }
}
