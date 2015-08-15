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

            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                list = context.SP_ADMIN_REVIEW_PRODUCT_SEL(p.PAGE, 10, p.SEARCH_KEY, p.SEARCH_VALUE, p.MEDIA_GBN_W,p.MEDIA_GBN_M, start_dt, end_dt, p.IS_PHOTO_Y, p.IS_PHOTO_N, p.IS_DISPLAY_Y, p.IS_DISPLAY_N, total).ToList();
            }

            Tuple<List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>, int> tp = new Tuple<List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>, int>(list, Convert.ToInt32(total.Value));

            return tp;

        }

        /**
         * 상품리뷰 정보
         */
        public Tuple<SP_ADMIN_REVIEW_PRODUCT_INFO_Result, string, string> ReviewPdtInfo(int? idx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            SP_ADMIN_REVIEW_PRODUCT_INFO_Result info = new SP_ADMIN_REVIEW_PRODUCT_INFO_Result();
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                info = context.SP_ADMIN_REVIEW_PRODUCT_INFO(idx, retNum, retMsg).SingleOrDefault();
            }

            return new Tuple<SP_ADMIN_REVIEW_PRODUCT_INFO_Result, string, string>(info, retNum.Value.ToString(), retMsg.Value.ToString());
        }

        /**
         * 상품리뷰  수정
         */
        public Tuple<string, string> ReviewPdtUpdate(AdminReviewInputViewModel p)
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
        public List<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result> ThemaList()
        {
            List<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result> list = new List<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result>();
 
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                list = context.SP_ADMIN_REVIEW_CATE_THEMA_SEL().ToList();
            }

            return list;
        }

        /**
         * 테마 수정
         */
        public void ThemaUpdate(SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result p)
        {
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                context.SP_ADMIN_REVIEW_CATE_THEMA_UPD(p.IDX, p.TITLE, p.IS_DISPLAY, p.TAG);
            }
        }

        /**
         * 체험단 리뷰 마스터 조회
         */
        public Tuple<List<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result>, int> ReviewExpMasterList(AdminReviewExpMasterListRouteParam p)
        {
            List<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result> list = new List<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result>();

            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));


            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                list = context.SP_ADMIN_REVIEW_EXP_MASTER_SEL(p.PAGE,10, p.SEARCH_KEY, p.SEARCH_VALUE,p.FROM_DATE, p.TO_DATE, 
                    p.IS_STATE_10,p.IS_STATE_20, p.IS_STATE_30,p.IS_AUTH_Y,p.IS_AUTH_N ,total).ToList();
            }

            Tuple<List<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result>, int> tp = new Tuple<List<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result>, int>(list, Convert.ToInt32(total.Value));

            return tp;

        }

        /**
         * 체험단리뷰 마스터 저장
         */
        public int? ReviewExpMasterInsert(AdminReviewExpMasterInputViewModel p)
        {
            ObjectParameter midx = new ObjectParameter("MASTER_IDX", typeof(int));

            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                context.SP_ADMIN_REVIEW_EXP_MASTER_INS(p.P_CODE, p.FROM_DATE, p.TO_DATE, p.IS_AUTH, midx);
            }

            return Convert.ToInt32(midx.Value);

        }



        /**
         * 체험단에서 상품 찾기
         */
        public Tuple<List<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result>, int> ReviewExpFindProductList(AdminReviewExpFindProductRouteParam p)
        {
            List<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result> list = new List<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result>();
            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                list = context.SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL(p.PAGE, p.PAGE_SIZE, p.SEARCH_KEY, p.SEARCH_VALUE, total).ToList();
            }

            Tuple<List<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result>, int> tp = new Tuple<List<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result>, int>(list, Convert.ToInt32(total.Value));

            return tp;
        }


        /**
         * 마스터 상세
         */
        public SP_ADMIN_REVIEW_EXP_MASTER_DETAIL_Result ReviewExpMasterDetail(int? idx)
        {
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                return context.SP_ADMIN_REVIEW_EXP_MASTER_DETAIL(idx).SingleOrDefault();
            }

        }

        /**
         * 마스터 수정
         */
        public void ReviewExpMasterUpdate(int? idx, string isAuth)
        {
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                context.SP_ADMIN_REVIEW_EXP_MASTER_UDATE(idx, isAuth);
            }

        }

        /**
         * 마스터 관련 회원 조회
         */
        public List<SP_ADMIN_REVIEW_EXP_MEMBER_SEL_Result> ReviewExpMemberList(int? midx)
        {
            List<SP_ADMIN_REVIEW_EXP_MEMBER_SEL_Result> list = new List<SP_ADMIN_REVIEW_EXP_MEMBER_SEL_Result>();
            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                list = context.SP_ADMIN_REVIEW_EXP_MEMBER_SEL(midx).ToList();
            }

            return list;
        }

        /**
         * 체험단리뷰 회원 저장
         */
        public void ReviewExpMemberInsert(AdminReviewExpMemberParamToInputDB p)
        {

            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                context.SP_ADMIN_REVIEW_EXP_MEMBER_INS(p.MASTER_IDX, p.M_ID, p.M_NAME);
            }
        }

        /**
         * 체험단리뷰 회원 삭제
         */
        public void ReviewExpMemberDelete(string memId, int? masterIdx)
        {

            using (AdminReviewEntities context = new AdminReviewEntities())
            {
                context.SP_ADMIN_REVIEW_EXP_MEMBER_DEL(memId, masterIdx);
            }
        }

    }
}
