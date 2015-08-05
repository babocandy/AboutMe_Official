using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Service.Review;
using System.Data.Entity.Core.Objects;

using System.Diagnostics;
using System.Globalization;

using AboutMe.Domain.Entity.AdminReview;

namespace AboutMe.Domain.Service.AdminReview
{
    public class AdminReviewService : IAdminReviewService
    {
        /**
         * 상품리뷰조회
         */
        public Tuple<List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>, int> ReviewPdtList(AdminReviewRouteParam p)
        {
            List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result> list = new List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>();
            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            DateTime? start_dt = null;
            DateTime? end_dt = null;

            if (!String.IsNullOrEmpty(p.SEL_DATE_FROM))
            {
                start_dt = DateTime.ParseExact(p.SEL_DATE_FROM, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(p.SEL_DATE_TO))
            {
                end_dt = DateTime.ParseExact(p.SEL_DATE_TO, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            Debug.WriteLine("--------->");
            Debug.WriteLine(start_dt);
            Debug.WriteLine(end_dt);


            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                
            Debug.WriteLine(p.PAGE);
            Debug.WriteLine(p.SEARCH_KEY);
                Debug.WriteLine(p.SEARCH_KEY);
                Debug.WriteLine(p.SEARCH_VALUE);
                Debug.WriteLine(p.MEDIA_GBN_W);
                Debug.WriteLine(p.MEDIA_GBN_M);
                Debug.WriteLine(p.IS_PHOTO_Y);
                Debug.WriteLine(p.IS_DISPLAY_N);
                Debug.WriteLine("IS_DISPLAY_Y"+p.IS_DISPLAY_Y);
                Debug.WriteLine("IS_DISPLAY_N"+p.IS_DISPLAY_N);


                list = context.SP_ADMIN_REVIEW_PRODUCT_SEL(p.PAGE, 10, p.SEARCH_KEY, p.SEARCH_VALUE, p.MEDIA_GBN_W,p.MEDIA_GBN_M, start_dt, end_dt, p.IS_PHOTO_Y, p.IS_PHOTO_N, p.IS_DISPLAY_Y, p.IS_DISPLAY_N, total).ToList();
            }

            Tuple<List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>, int> tp = new Tuple<List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>, int>(list, Convert.ToInt32(total.Value));

            return tp;

        }

        /**
         * 상품리뷰 정보
         */
        public Tuple<List<SP_ADMIN_REVIEW_PRODUCT_INFO_Result>, string, string> ReviewPdtInfo(int? idx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            List<SP_ADMIN_REVIEW_PRODUCT_INFO_Result> info = new List<SP_ADMIN_REVIEW_PRODUCT_INFO_Result>();
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                info = context.SP_ADMIN_REVIEW_PRODUCT_INFO(idx, retNum, retMsg).ToList();
            }

            return new Tuple<List<SP_ADMIN_REVIEW_PRODUCT_INFO_Result>, string, string>(info, retNum.Value.ToString(), retMsg.Value.ToString());
        }

        /**
         * 상품리뷰  수정
         */
        public Tuple<string, string> ReviewPdtUpdate(AdminReviewSaveParam p)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                context.SP_ADMIN_REVIEW_PRODUCT_UPDATE(p.IDX, p.IS_BEST, p.IS_DISPLAY, retNum, retMsg);
            }

            return new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }

        /**
         * 테마 조회
         */
        public List<SP_REVIEW_CATE_THEMA_SEL_Result> ThemaList()
        {
            List<SP_REVIEW_CATE_THEMA_SEL_Result> list = new List<SP_REVIEW_CATE_THEMA_SEL_Result>();
 
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                list = context.SP_REVIEW_CATE_THEMA_SEL().ToList();
            }

            return list;
        }

        /**
         * 테마 수정
         */
        public void ThemaUpdate(AdminReviewThemaParamToInputDB p)
        {
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                context.SP_REVIEW_CATE_THEMA_UPD(p.IDX, p.TITLE, p.IS_DISPLAY, p.TAG);
            }
        }
    }
}
