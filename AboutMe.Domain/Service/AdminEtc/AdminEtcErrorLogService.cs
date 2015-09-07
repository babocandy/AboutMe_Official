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
    }
}
