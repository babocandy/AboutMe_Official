﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AboutMe.Domain.Entity.Display;
using AboutMe.Domain.Service.Product;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Front.Models
{
    public class BaseDisplayerViewModel
    {
        public IList<SP_DISPLAY_SEL_Result> List { get; set; }
        public List<SP_PRODUCT_DETAIL_VIEW_Result> PdtList { get; set; }
    }

}