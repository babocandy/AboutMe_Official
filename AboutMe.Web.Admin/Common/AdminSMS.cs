using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AboutMe.Domain.Entity.AdminProduct;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Web.Admin.Common
{
    public class AdminSMS
    {
        #region SMS 발송
        public int AdminSmsSend(AdminSMSModel adminSMSModel)
        {
            int ResultValue = -5;
            string logtran = ""; // 로그
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                ObjectParameter objOutParam = new ObjectParameter("INTRESULT", typeof(Int32));//sp의 output parameter변수명을 동일하게 사용한다.
                int val = AdminProductContext.SP_ADMIN_SMS_INS(
                       adminSMSModel.SMS_FLAG
                       , adminSMSModel.SEND_TIME
                       , adminSMSModel.HANDPHONE
                       , adminSMSModel.CALLBACK_NO
                       , adminSMSModel.TITLE
                       , adminSMSModel.SEND_MSG
                       , objOutParam);
                ResultValue = (int)objOutParam.Value;

                #region SMS 로그
                string ip;
                string adminid;
                string userAgent;
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                adminid = AdminUserInfo.GetAdmId();
                userAgent = HttpContext.Current.Request.UserAgent;
                
                logtran = "ResultValue:" + ResultValue;
                logtran += "|SMS_FLAG:" + adminSMSModel.SMS_FLAG;
                logtran += "|SEND_TIME:" + adminSMSModel.SEND_TIME;
                logtran += "|HANDPHONE:" + adminSMSModel.HANDPHONE;
                logtran += "|CALLBACK_NO:" + adminSMSModel.CALLBACK_NO;
                logtran += "|TITLE:" + adminSMSModel.TITLE;
                logtran += "|SEND_MSG:" + adminSMSModel.SEND_MSG;
                if (string.IsNullOrEmpty(adminid))
                {
                    adminid = "";
                }
                if (logtran.Length > 8000)
                {
                    logtran = logtran.Substring(0, 7890);
                    var test = logtran.Length;
                }
                if (userAgent.Length > 1000)
                {
                    userAgent = userAgent.Substring(0, 990);
                }
               
                AdminProductContext.SP_ADMIN_LOG_INS(adminid, logtran, "관리자SMS발송로그", "", ip, userAgent, "");
                #endregion
            }
            return ResultValue;
        }
        #endregion
    }
}