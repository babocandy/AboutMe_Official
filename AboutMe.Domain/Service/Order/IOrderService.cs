using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Order;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Order
{
    public interface IOrderService
    {
        Int32 InsertOrderStep1(string M_ID, string SESSION_ID, string P_CODE_LIST, string P_COUNT_LIST);
    }
}
