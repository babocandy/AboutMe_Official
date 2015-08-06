using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using AboutMe.Domain.Service.Coupon;
using AboutMe.Domain.Service.Promotion;

using AboutMe.Domain.Entity.AdminCoupon;
using AboutMe.Domain.Entity.AdminPromotion;

using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.BizPromotion
{
    public interface IBizPromotion
    {
        Dictionary<string, string> GetPromotionInfoForDetialPage(string UsableDeviceGbn, string MGbn, string MGrade, string M_id, string PCode, int ResultPrice);
    }
}
