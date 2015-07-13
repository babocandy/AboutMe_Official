using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AboutMe.Domain.Entity.AdminPromotion
{
    [MetadataType(typeof(TB_PROMOTION_BY_TOTAL_Metadata))]
    public partial  class TB_PROMOTION_BY_TOTAL
    {
    }

    [MetadataType(typeof(TB_PROMOTION_BY_PRODUCT_Metadata))]
    public partial class TB_PROMOTION_BY_PRODUCT
    {
    }

     [MetadataType(typeof(TB_PROMOTION_BY_PRODUCT_PRICE_Meatadata))]
    public partial class TB_PROMOTION_BY_PRODUCT_PRICE
    {
    }
}
