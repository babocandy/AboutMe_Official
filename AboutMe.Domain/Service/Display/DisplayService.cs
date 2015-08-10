using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.Core.Objects;

using System.Diagnostics;

using AboutMe.Domain.Entity.Display;

namespace AboutMe.Domain.Service.Display
{
    public class DisplayService : IDisplayService
    {
        public List<SP_DISPLAY_SEL_Result> GetListDisplay(string kind, string subKind = null, int? seq = null)
        {
            using(DisplayEntities context = new DisplayEntities()){
                return context.SP_DISPLAY_SEL(kind, subKind, seq).ToList();
            }
            
        }

        public List<SP_POPUP_WEB_SEL_Result> GetListPopupWeb()
        {
            using (DisplayEntities context = new DisplayEntities())
            {
                return context.SP_POPUP_WEB_SEL().ToList();
            }
        }

        public SP_POPUP_DETAIL_Result GePopupDetail(int? idx)
        {
            using (DisplayEntities context = new DisplayEntities())
            {
                return context.SP_POPUP_DETAIL(idx).SingleOrDefault();
            }
        }
    }
}
