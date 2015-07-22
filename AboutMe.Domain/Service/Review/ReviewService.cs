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

        public SP_REVIEW_GET_PRODUCT_INFO_Result GetProductInfo(string pcode)
        {
            using (ReviewEntities context = new ReviewEntities())
            {
                return context.SP_REVIEW_GET_PRODUCT_INFO(pcode).SingleOrDefault();
            }
        }

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
    }
}
