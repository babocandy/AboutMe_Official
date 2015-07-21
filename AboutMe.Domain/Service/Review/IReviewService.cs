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
        List<SP_REVIEW_PRODUCT_READY_SEL_Result> GetReadyList(string mid);
        SP_REVIEW_GET_PRODUCT_INFO_Result GetProductInfo(string pcode);

        Tuple<string, string> InsertMyReview(string mid, int? orderDetailIdx, string pCode, string skinType, string comment, string addImage);
    }
}
