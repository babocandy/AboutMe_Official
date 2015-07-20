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
        public List<SP_REVIEW_PRODUCT_READY_SEL_Result> GetReadyList(string mid)
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
    }
}
