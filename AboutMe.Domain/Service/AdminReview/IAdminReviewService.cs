using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Product;
using System.Data.Entity.Core.Objects;

using AboutMe.Domain.Entity.AdminReview;

namespace AboutMe.Domain.Service.AdminReview
{
    public interface IAdminReviewService
    {
        Tuple<List<SP_ADMIN_REVIEW_PRODUCT_SEL_Result>, int> ReviewPdtList(AdminReviewRouteParam p);
        Tuple<List<SP_ADMIN_REVIEW_PRODUCT_INFO_Result>, string, string> ReviewPdtInfo(int? idx);
        Tuple<string, string> ReviewPdtUpdate(AdminReviewSaveParam p);
    }
}
