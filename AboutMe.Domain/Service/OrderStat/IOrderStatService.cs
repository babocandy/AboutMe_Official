using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.OrderStat;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.OrderStat
{
    public interface IOrderStatService
    {
        List<SP_ADMIN_STAT_ORDER_DAY_Result> OrderStatDayList(string FROM_DATE, string TO_DATE, bool PAT_GUBUN1, bool PAT_GUBUN2, bool MEMBER_GBN1, bool MEMBER_GBN2, bool MEMBER_GBN3);

        List<SP_ADMIN_STAT_ORDER_CATEGORY_Result> OrderStatCateList(string FROM_DATE, string TO_DATE, bool PAT_GUBUN1, bool PAT_GUBUN2, bool MEMBER_GBN1, bool MEMBER_GBN2, bool MEMBER_GBN3, string cateCode);
    }
}
