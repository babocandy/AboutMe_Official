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

        public string OLD_MAIN_IMG { get; set; }
        public string OLD_OTHER_IMG1 { get; set; }
        public string OLD_OTHER_IMG2 { get; set; }
        public string OLD_OTHER_IMG3 { get; set; }
        public string OLD_OTHER_IMG4 { get; set; }
        public string OLD_OTHER_IMG5 { get; set; }

    }
}
