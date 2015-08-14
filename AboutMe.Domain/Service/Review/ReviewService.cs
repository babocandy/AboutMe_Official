using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Service.Review;
using System.Data.Entity.Core.Objects;

using System.Diagnostics;

using AboutMe.Domain.Entity.Review;

namespace AboutMe.Domain.Service.Review
{
    public class ReviewService : IReviewService
    {
        /**
         * 마이리뷰 상품. 작성 가능 
         */
        public List<SP_REVIEW_PRODUCT_READY_SEL_Result> GetMyReviewReadyList(string mid)
        {
            List<SP_REVIEW_PRODUCT_READY_SEL_Result> lst = new List<SP_REVIEW_PRODUCT_READY_SEL_Result>();

            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_READY_SEL(mid, retNum, retMsg).ToList();
            }

            return lst;
       }

        /**
         * 
         * 단일 상품 정보 
         */
        public SP_REVIEW_GET_PRODUCT_INFO_Result GetProductInfo(string pcode)
        {
            using (ReviewEntities context = new ReviewEntities())
            {
                return context.SP_REVIEW_GET_PRODUCT_INFO(pcode).SingleOrDefault();
            }
        }

        /**
         * 마이리뷰 상품. 작성완료  
         */
        public Tuple<string, string> InsertMyReview(MyReviewProductInputViewModel p)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (ReviewEntities context = new ReviewEntities())
            {
                context.SP_REVIEW_PRODUCT_INS(p.M_ID, p.ORDER_DETAIL_IDX, p.P_CODE, p.SKIN_TYPE, p.COMMENT, p.ADD_IMAGE, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UpdateMemberPointSave retNum:  " + retNum.Value);
            Debug.WriteLine("UpdateMemberPointSave retMsg:  " + retMsg.Value);

            return tp;
        }

        /**
         * 마이리뷰  수정
         */
        public Tuple<string, string> UpdateMyReview(MyReviewProductInputViewModel p)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (ReviewEntities context = new ReviewEntities())
            {
                context.SP_REVIEW_PRODUCT_UPD(p.IDX, p.COMMENT, retNum, retMsg);
            }

            return new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }

        /**
         * 마이리뷰 상품. 작성완료 목록 조회
         */
        public List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> GetMyReviewCompleteList(string mid, int? pageNo=1)
        {
            List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> lst = new List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>();

            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_COMPLETE_SEL(mid, pageNo, 10, retNum, retMsg).ToList();
            }

            return lst;
        }

        /**
         * 마이리뷰 상품. 작성완료 목록 총수
         */
        public int GetMyReviewCompleteCnt(string mid)
        {
            int ret = 0;
            using (ReviewEntities context = new ReviewEntities())
            {
                int? cnt = context.SP_REVIEW_PRODUCT_COMPLETE_CNT(mid).SingleOrDefault() ;
                ret = cnt ?? 0;
            }

            return ret;
        }

        /*
         * 상품 리뷰 목록 조회
         */
        public Tuple<List<SP_REVIEW_PRODUCT_SEL_Result>, int> GetReviewProductList(int? tailIdx, string categoryCode, string sort)
        {
            List<SP_REVIEW_PRODUCT_SEL_Result> lst = new List<SP_REVIEW_PRODUCT_SEL_Result>();

            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_SEL(tailIdx, categoryCode, sort, total).ToList();
            }

            Tuple<List<SP_REVIEW_PRODUCT_SEL_Result>, int> tp = new Tuple<List<SP_REVIEW_PRODUCT_SEL_Result>, int>(lst, Convert.ToInt32(total.Value));

            return tp;
        }

        /**
         * 상품코드별 상품리뷰 조회 - 상품상세에서 사용
         */
        public Tuple<List<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result>, int> GetReviewProductListByProductCode(ReviewListJsonParamInShopping p)
        {
            List<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result> lst = new List<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result>();

            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL(p.P_CODE, p.PAGE_NO, p.PAGE_SIZE, total).ToList();
            }

            Tuple<List<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result>, int> tp = new Tuple<List<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result>, int>(lst, Convert.ToInt32(total.Value));

            return tp;
        }

        /**
         * 
         * 상품리뷰 상세 
         */
        public SP_REVIEW_PRODUCT_INFO_Result GetReviewProductDetail(int? idx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            SP_REVIEW_PRODUCT_INFO_Result info = new SP_REVIEW_PRODUCT_INFO_Result();

            using (ReviewEntities context = new ReviewEntities())
            {
                info = context.SP_REVIEW_PRODUCT_INFO(idx, retNum, retMsg).SingleOrDefault();
            }
            return info;
        }

        /**
         * 테마 카테고리 조회
         */
        public List<SP_REVIEW_CATE_THEMA_SEL_Result> ThemaList()
        {
            List<SP_REVIEW_CATE_THEMA_SEL_Result> lst = new List<SP_REVIEW_CATE_THEMA_SEL_Result>();

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_CATE_THEMA_SEL().ToList();
            }

            return lst;
        }

        /**
         * 마이페이지. 상품리뷰 작성준비 총수
         */
        public int ReadyTotal(string mid)
        {
            using (ReviewEntities context = new ReviewEntities())
            {
                return context.SP_REVIEW_PRODUCT_READY_CNT(mid).SingleOrDefault() ?? 0;
            }
        }

        /**
         *  체험단 마스터 조회
         */
        public List<SP_REVIEW_MY_EXP_MASTER_SEL_Result> GetReviewExpMyMasterList(string mid)
        {
            List<SP_REVIEW_MY_EXP_MASTER_SEL_Result> lst = new List<SP_REVIEW_MY_EXP_MASTER_SEL_Result>();
            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_MY_EXP_MASTER_SEL(mid).ToList();
            }
            return lst;
        }


        /**
         *  체험단  나의 리뷰 저장
         */
        public Tuple<string, string> InsertMyReviewExp(MyReviewExpInputViewModel p)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (ReviewEntities context = new ReviewEntities())
            {
                context.SP_REVIEW_MY_EXP_INS(p.M_ID, p.EXP_MASTER_IDX, p.P_CODE, p.TITLE, p.SKIN_TYPE, p.SUB_IMG_1, p.SUB_IMG_2, p.SUB_IMG_3, p.TAG, p.COMMENT, retNum, retMsg);
            }

            return new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }


        /**
         *  체험단  나의 리뷰 상세
         */
        public SP_REVIEW_MY_EXP_DETAIL_Result GetMyReviewExpDetail(int? idx)
        {
            using (ReviewEntities context = new ReviewEntities())
            {
                return context.SP_REVIEW_MY_EXP_DETAIL(idx).SingleOrDefault();
            }

        }


        /**
         *  체험단 나의 리뷰 상세
         */
        public Tuple<string, string> UpdateMyReviewExp(MyReviewExpInputViewModel p)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));
            using (ReviewEntities context = new ReviewEntities())
            {
                context.SP_REVIEW_MY_EXP_UPD(p.ARTICLE_IDX, p.M_ID, p.EXP_MASTER_IDX, p.P_CODE, p.TITLE, p.SKIN_TYPE, p.SUB_IMG_1, p.SUB_IMG_2, p.SUB_IMG_3, p.TAG, p.COMMENT, retNum, retMsg);
            }
             return new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }


        /**
         *  체험단리뷰 전체 조회
         */
        public Tuple<List<SP_REVIEW_EXP_SEL_Result>, int> GetReviewExpList(int? tailIdx, string categoryCode, string sort)
        {
            List<SP_REVIEW_EXP_SEL_Result> lst = new List<SP_REVIEW_EXP_SEL_Result>();

            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_EXP_SEL(tailIdx, categoryCode, sort, total).ToList();
            }

            Tuple<List<SP_REVIEW_EXP_SEL_Result>, int> tp = new Tuple<List<SP_REVIEW_EXP_SEL_Result>, int>(lst, Convert.ToInt32(total.Value));

            return tp;
        }

        /**
         *  체험단리뷰 상세 + 기획전 이벤트
         */
        public SP_REVIEW_EXP_DETAIL_Result GetReviewExpDetail(ReviewExpDetailParam p)
        {
            using (ReviewEntities context = new ReviewEntities())
            {
                return context.SP_REVIEW_EXP_DETAIL(  p.ARTICLE_IDX    , p.P_CODE).SingleOrDefault();
            }
        }

        /**
         * 상품코드별 체험단리뷰 조회 - 상품상세에서 사용
         */
        public Tuple<List<SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result>, int> GetReviewExpByProductCode(ReviewListJsonParamInShopping p)
        {
            List<SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result> lst = new List<SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result>();

            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_EXP_IN_SHOPPING_DETAIL(p.P_CODE, p.PAGE_NO, p.PAGE_SIZE, total).ToList();
            }

            Tuple<List<SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result>, int> tp = new Tuple<List<SP_REVIEW_EXP_IN_SHOPPING_DETAIL_Result>, int>(lst, Convert.ToInt32(total.Value));

            return tp;
        }


        /*
         * 모바일 상품리뷰 조회
         */
        public Tuple<List<SP_REVIEW_PRODUCT_MOBILE_SEL_Result>, int, int> GetReviewProductListMobile(ReviewListMobileUrlParam p)
        {
            List<SP_REVIEW_PRODUCT_MOBILE_SEL_Result> lst = new List<SP_REVIEW_PRODUCT_MOBILE_SEL_Result>();

            ObjectParameter total_page = new ObjectParameter("TOTAL_PAGE", typeof(int));
            ObjectParameter total_item = new ObjectParameter("TOTAL_ITEM", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_MOBILE_SEL(p.PAGE,p.PAGE_SIZE,p.CATE_CODE, p.SORT, total_page, total_item).ToList();
            }

            Tuple<List<SP_REVIEW_PRODUCT_MOBILE_SEL_Result>, int, int> tp = new Tuple<List<SP_REVIEW_PRODUCT_MOBILE_SEL_Result>, int, int>(lst, Convert.ToInt32( total_page.Value) , Convert.ToInt32( total_item.Value));
            return tp;

        }

        /*
         * 모바일 체험된리뷰 조회
         */
        public Tuple<List<SP_REVIEW_EXP_MOBILE_SEL_Result>, int, int> GetReviewExpListMobile(ReviewListMobileUrlParam p)
        {
            List<SP_REVIEW_EXP_MOBILE_SEL_Result> lst = new List<SP_REVIEW_EXP_MOBILE_SEL_Result>();

            ObjectParameter total_page = new ObjectParameter("TOTAL_PAGE", typeof(int));
            ObjectParameter total_item = new ObjectParameter("TOTAL_ITEM", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_EXP_MOBILE_SEL(p.PAGE, p.PAGE_SIZE, p.CATE_CODE, p.SORT, total_page, total_item).ToList();
            }

            Tuple<List<SP_REVIEW_EXP_MOBILE_SEL_Result>, int, int> tp = new Tuple<List<SP_REVIEW_EXP_MOBILE_SEL_Result>, int, int>(lst, Convert.ToInt32(total_page.Value), Convert.ToInt32(total_item.Value));
            return tp;

        }

    }
}
