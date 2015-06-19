using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminEtc;

namespace AboutMe.Domain.Service.AdminEtc
{
    public class AdminUserService : IAdminUserService
    {
        public List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> GetAdminMemberList(string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE)
        {

            List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result> lst = new List<SP_ADMIN_ADMIN_MEMBER_VIEW_Result>();
            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                //lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();
                lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD, sORT_COL, sORT_DIR, pAGE, pAGESIZE).ToList();

                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        public int GetAdminMemberListCnt(string sEARCH_COL, string sEARCH_KEYWORD)
        {

            List<SP_ADMIN_COMMON_CNT_Result> lst = new List<SP_ADMIN_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                lst = AdmEtcContext.SP_ADMIN_ADMIN_MEMBER_CNT(sEARCH_COL, sEARCH_KEYWORD).ToList();
                if (lst != null && lst.Count > 0)
                    list_cnt = lst[0].COUNT;
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return list_cnt;

        }

    }
}
