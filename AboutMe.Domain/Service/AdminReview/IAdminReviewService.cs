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
        Tuple<SP_ADMIN_REVIEW_PRODUCT_INFO_Result, string, string> ReviewPdtInfo(int? idx);
        Tuple<string, string> ReviewPdtUpdate(AdminReviewInputViewModel p);

        List<SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result> ThemaList();
        void ThemaUpdate(SP_ADMIN_REVIEW_CATE_THEMA_SEL_Result p);

        Tuple<List<SP_ADMIN_REVIEW_EXP_MASTER_SEL_Result>, int> ReviewExpMasterList(AdminReviewExpMasterListRouteParam p);
        int? ReviewExpMasterInsert(AdminReviewExpMasterInputViewModel p);
        //void ReviewExpMemberInsert(AdminReviewExpMemberParamToInputDB p);
        Tuple<string, string> ReviewExpMemberInsert(AdminReviewExpMemberParamToInputDB p);

        Tuple<List<SP_ADMIN_REVIEW_EXP_FIND_PRODUCT_SEL_Result>, int> ReviewExpFindProductList(AdminReviewExpFindProductRouteParam p);

        SP_ADMIN_REVIEW_EXP_MASTER_DETAIL_Result ReviewExpMasterDetail(int? idx);

        //void ReviewExpMemberDelete(string memId, int? masterIdx);
        Tuple<string, string> ReviewExpMemberDelete(string memId, int? masterIdx);
        List<SP_ADMIN_REVIEW_EXP_MEMBER_SEL_Result> ReviewExpMemberList(int? midx);
        void ReviewExpMasterUpdate(int? idx, string isAuth);

        Tuple<List<SP_ADMIN_REVIEW_EXP_ARTICLE_SEL_Result>, int, string, string> ReviewExpArticleList(AdminReviewExpArticleRouteParam p);
        SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result ReviewExpArticleDetail(int? idx);
        void ReviewExpArticleUpdate(SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result model);
    }
}
