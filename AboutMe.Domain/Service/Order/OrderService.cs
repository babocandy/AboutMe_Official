﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Order;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Order
{
    public class OrderService : IOrderService
    {
        #region Insert
        public Int32 InsertOrderStep1(string M_ID, string SESSION_ID, string P_CODE_LIST, string P_COUNT_LIST)
        {
            SP_ORDER_STEP2_Result result;
            using (OrderEntities EfContext = new OrderEntities())
            {
                result = EfContext.SP_ORDER_STEP1(M_ID, SESSION_ID, P_CODE_LIST, P_COUNT_LIST).FirstOrDefault();
            }
            return result.ORDER_IDX;
        }
        #endregion
    }
}
