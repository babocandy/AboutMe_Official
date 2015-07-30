using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminDisplay;
using System.Data.Entity.Core.Objects;

using System.Diagnostics;

namespace AboutMe.Domain.Service.AdminDisplay
{
    public class AdminDisplayService : IAdminDisplayService
    {
       /**
        * 웹메인배너 저장
        */
        public void SaveWebMainBanner(int? idx, string url, string img){
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_SAVE(idx, "10", null, url, img, null, null, null);
            }
        }

        /**
         * 웹메인배너 삭제
         */
        public void RemoveWebMainBanner(int? idx)
        {
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_REMOVE(idx);
            }
        }

        /**
         * 웹메인배너 조회
         */
        public List<SP_ADMIN_DISPLAY_SEL_Result> GetWebMainBanner()
        {
            List<SP_ADMIN_DISPLAY_SEL_Result> list = new List<SP_ADMIN_DISPLAY_SEL_Result>();
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                list = context.SP_ADMIN_DISPLAY_SEL(null, "10", null).ToList();
            }

            return list;
        }
    }
}
