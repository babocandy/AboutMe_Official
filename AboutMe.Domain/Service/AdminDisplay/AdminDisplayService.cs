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
                context.SP_ADMIN_DISPLAY_SAVE_WEB_MAIN_BANNER(idx, url, img);
            }
        }

        /**
         * 웹메인배너 삭제
         */
        public void RemoveWebMainBanner(int? idx)
        {
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_REMOVE_WEB_MAIN_BANNER(idx);
            }
        }

        /**
         * 웹메인배너 조회
         */
        public List<SP_ADMIN_DISPLAY_WEB_MAIN_BANNER_SEL_Result> GetWebMainBanner()
        {
            List<SP_ADMIN_DISPLAY_WEB_MAIN_BANNER_SEL_Result> list = new List<SP_ADMIN_DISPLAY_WEB_MAIN_BANNER_SEL_Result>();
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                list = context.SP_ADMIN_DISPLAY_WEB_MAIN_BANNER_SEL().ToList();
            }

            return list;
        }
    }
}
