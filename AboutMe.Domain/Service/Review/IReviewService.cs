using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Product;
using System.Data.Entity.Core.Objects;

using AboutMe.Domain.Entity.Review;

namespace AboutMe.Domain.Service.Review
{
    public interface IReviewService
    {
        List<SP_REVIEW_PRODUCT_READY_SEL_Result> GetMyReviewReadyList(string mid);
        SP_REVIEW_GET_PRODUCT_INFO_Result GetProductInfo(string pcode);

        Tuple<string, string> InsertMyReview(MyReviewPdtParamOnSaveToDb p);
        Tuple<string, string> UpdateMyReview(MyReviewPdtParamOnSaveToDb p);
        List<SP_REVIEW_PRODUCT_COMPLETE_SEL_Result> GetMyReviewCompleteList(string mid, int? pageNo = 1);
        int GetMyReviewCompleteCnt(string mid);
        Tuple<List<SP_REVIEW_PRODUCT_SEL_Result>, int> GetReviewProductList(int? tailIdx, string categoryCode, string sort);
        Tuple<List<SP_REVIEW_PRODUCT_IN_SHOPPING_DETAIL_Result>, int> GetReviewProductListByProductCode(string pcode, int? pageNo = 1, int? pageSize = 10);
        SP_REVIEW_PRODUCT_INFO_Result ReviewProductInfo(int? idx);
        List<SP_REVIEW_CATE_THEMA_SEL_Result> ThemaList();
        int ReadyTotal(string mid);

        Tuple<string, string> InsertMyReviewExp(MyReviewExpParamOnSaveToDb p);
        List<SP_REVIEW_MY_EXP_MASTER_SEL_Result> GetReviewExpMyMasterList(string mid);
    }
}
