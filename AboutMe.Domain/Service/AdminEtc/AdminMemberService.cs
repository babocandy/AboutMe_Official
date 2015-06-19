using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminEtc;

namespace AboutMe.Domain.Service.AdminEtc 
{
    public class AdminMemberService : IAdminMemberService
    {

        public List<SP_ADM_ADMIN_MEMBER_SEL_Result> GetAdminMemberList(string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE)
      {

          List<SP_ADM_ADMIN_MEMBER_SEL_Result> lst = new List<SP_ADM_ADMIN_MEMBER_SEL_Result>();
          using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
          {
              /**try {**/
                lst = AdmEtcContext.SP_ADM_ADMIN_MEMBER_SEL(sEARCH_COL, sEARCH_KEYWORD,sORT_COL,sORT_DIR,pAGE,pAGESIZE).ToList();
             /** }catch()
              {
                    AdmEtcContext.Dispose();
              }**/
           }
          
          return lst;

      }

      public int GetAdminMemberListCnt(string sEARCH_COL, string sEARCH_KEYWORD)
      {

          List<SP_ADM_ADMIN_MEMBER_CNT> lst = new List<SP_ADM_ADMIN_MEMBER_CNT>();
          int list_cnt = 0;

          using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
          {
              /**try {**/
              lst = AdmEtcContext.SP_ADM_ADMIN_MEMBER_CNT(sEARCH_COL, sEARCH_KEYWORD).ToList();
              if (lst != null && lst.Count > 0)
                  list_cnt = lst[0].COUNT;
              /** }catch()
               {
                     AdmEtcContext.Dispose();
               }**/
          }

          return list_cnt;

      }

      public List<SP_ADM_ADMIN_DEPT_SEL_Result> GetAdmDeptList()
      {

          List<SP_ADM_ADMIN_DEPT_SEL_Result> lst = new List<SP_ADM_ADMIN_DEPT_SEL_Result>();
          using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
          {
              /**try {**/
              lst = AdmEtcContext.SP_ADM_ADMIN_DEPT_SEL().ToList();
              /** }catch()
               {
                     AdmEtcContext.Dispose();
               }**/
          }

          return lst;

      }


      public void InsertAdminMemberTest(string AdmId, string AdmName,string AdmPhoto , string AdmPass, string AdmDept, int Point)
      {
          using(AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
          {
             // AdmEtcContext.SP_ADM_ADMIN_MEMBER_INS(AdmId, AdmPass, AdmName,AdmPhoto, AdmDept, Point);
          }
      }


      public List<SP_DUMMY_ADMIN_SEL_Result> GetDummyList()
      {

          List<SP_DUMMY_ADMIN_SEL_Result> lst = new List<SP_DUMMY_ADMIN_SEL_Result>();
          using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
          {
              /**try {**/
              lst = AdmEtcContext.SP_DUMMY_ADMIN_SEL().ToList();
              /** }catch()
               {
                     AdmEtcContext.Dispose();
               }**/
          }

          return lst;

     
     }

  }       

}
