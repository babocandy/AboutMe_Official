using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminEtc;


namespace AboutMe.Domain.Service.AdminEtc
{
    public class AdminEtcErrorLogService : IAdminEtcErrorLogService
    {

        //어플리케이션에서 에러 발생시 에러로그 insert 
        public void InsertAppErrlog(TB_APP_ERROR_LOG tb)
        {
            try
            {
                using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
                {
                    AdmEtcContext.SP_APP_ERRORLOG_INS(tb.MODULE_ID, tb.ERR_NO, tb.ERR_MSG, tb.ERR_QUERY, tb.COMPUTER_NAME, tb.EMP_NAME, tb.EMP_EMAIL, tb.MEMB_ID, tb.RESULT_YN, tb.POST_DATA, tb.PCID, tb.HTTP_COOKIE);
                }
            }
            catch (Exception ex)
            {

            }
        }



        //에러리스트
        public List<SP_APP_LOG_DETAIL_SEL_Result> GetAppErrorList(string SearchCol, string SearchKeyword, int Page, int PageSize)
        {

            List<SP_APP_LOG_DETAIL_SEL_Result> lst = new List<SP_APP_LOG_DETAIL_SEL_Result>();
            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
                /**try {**/
                lst = AdmEtcContext.SP_APP_ERROR_LOG_SEL(Page, PageSize, SearchCol, SearchKeyword).ToList();
                /** }catch()
                 {
                       AdmEtcContext.Dispose();
                 }**/
            }

            return lst;

        }

        //쿠폰마스터  row수 가져오기 
        public int GetAppErrorListCnt(string SearchCol, string SearchKeyword)
        {

            List<SP_ADMIN_COMMON_CNT_Result> lst = new List<SP_ADMIN_COMMON_CNT_Result>();
            int list_cnt = 0;

            using (AdminEtcEntities AdmEtcContext = new AdminEtcEntities())
            {
             
                /**try {**/
                lst = AdmEtcContext.SP_APP_ERROR_LOG_CNT(SearchCol, SearchKeyword).ToList();
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
