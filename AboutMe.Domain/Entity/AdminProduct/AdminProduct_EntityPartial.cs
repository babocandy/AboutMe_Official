using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace AboutMe.Domain.Entity.AdminProduct
{
    
    /*
    class AdminProduct_EntityPartial
    {
    } */

    [MetadataType(typeof(AdminProduct_Metadata))]
    public partial class TB_PRODUCT_INFO
    {
        public string SEARCH_KEY { get; set; }
        public string SEARCH_KEYWORD { get; set; }

    }
}
