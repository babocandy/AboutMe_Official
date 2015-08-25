using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.OrderStat;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.OrderStat
{
    public class OrderStatService : IOrderStatService
    {

        #region 
        public List<SP_ADMIN_STAT_ORDER_DAY_Result> OrderStatDayList(string FROM_DATE, string TO_DATE, bool PAT_GUBUN1, bool PAT_GUBUN2, bool MEMBER_GBN1, bool MEMBER_GBN2, bool MEMBER_GBN3)
        {
            List<SP_ADMIN_STAT_ORDER_DAY_Result> lst = new List<SP_ADMIN_STAT_ORDER_DAY_Result>();
            using (OrderStatEntities EfContext = new OrderStatEntities())
            {
                lst = EfContext.SP_ADMIN_STAT_ORDER_DAY(FROM_DATE, TO_DATE, PAT_GUBUN1, PAT_GUBUN2, MEMBER_GBN1, MEMBER_GBN2, MEMBER_GBN3).ToList();
            }

            return lst;
        }
        #endregion

        #region
        public List<SP_ADMIN_STAT_ORDER_CATEGORY_Result> OrderStatCateList(string FROM_DATE, string TO_DATE, bool PAT_GUBUN1, bool PAT_GUBUN2, bool MEMBER_GBN1, bool MEMBER_GBN2, bool MEMBER_GBN3, string cateCode)
        {
            List<SP_ADMIN_STAT_ORDER_CATEGORY_Result> lst = new List<SP_ADMIN_STAT_ORDER_CATEGORY_Result>();
            using (OrderStatEntities EfContext = new OrderStatEntities())
            {
                lst = EfContext.SP_ADMIN_STAT_ORDER_CATEGORY(FROM_DATE, TO_DATE, PAT_GUBUN1, PAT_GUBUN2, MEMBER_GBN1, MEMBER_GBN2, MEMBER_GBN3, cateCode).ToList();
            }

            return lst;
        }
        #endregion

    }
}
