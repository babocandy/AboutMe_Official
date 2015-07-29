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
         * 제품 정보 
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
        public Tuple<string, string>  InsertMyReview(string mid, int? orderDetailIdx, string pCode, string skinType, string comment, string addImage  ){
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (ReviewEntities context = new ReviewEntities())
            {
                context.SP_REVIEW_PRODUCT_INS(mid, orderDetailIdx, pCode, skinType, comment, addImage, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UpdateMemberPointSave retNum:  " + retNum.Value);
            Debug.WriteLine("UpdateMemberPointSave retMsg:  " + retMsg.Value);

            return tp;
        }

        /**
         * 마이리뷰 상품. 작성완료 목록 조회
         */
        public List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> GetMyReviewCompleteList(string mid, int? pageNo=1, int? pageSize=10)
        {
            List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> lst = new List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>();

            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_COMPLETE_SEL(mid, pageNo, pageSize, retNum, retMsg).ToList();
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
        public Tuple<List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>, int> GetReviewProductList(int? tailIdx, string categoryCode, string sort)
        {
            List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> lst = new List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>();

            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_SEL(tailIdx, categoryCode, sort, total).ToList();
            }

            Tuple<List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>, int> tp = new Tuple<List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>, int>(lst,   Convert.ToInt32(total.Value) );

            return tp;
        }

        /**
         * 상품코드별 상품리뷰 조회
         */
        public Tuple<List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>, int> GetReviewProductListByProductCode(string pcode, int? pageNo = 1, int? pageSize = 10)
        {
            List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> lst = new List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>();

            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));

            using (ReviewEntities context = new ReviewEntities())
            {
                lst = context.SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL(pcode, pageNo, pageSize, total).ToList();
            }

            Tuple<List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>, int> tp = new Tuple<List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result>, int>(lst, Convert.ToInt32(total.Value));

            return tp;
        }
    }
}
