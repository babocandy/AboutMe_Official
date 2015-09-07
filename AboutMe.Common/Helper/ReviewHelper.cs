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




        /**
         * 뷰티 체크
         */
        public static bool CheckBeauty(string pCateCode)
        {
            return pCateCode.Substring(0, 3) == CategoryCode.BEAUTY? true : false;
        }

        public static bool CheckHealth(string pCateCode)
        {
            return pCateCode.Substring(0, 3) == CategoryCode.HEALTH ? true : false;
        }
    }
}
