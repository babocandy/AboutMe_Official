using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AboutMe.Domain.Entity.AdminProduct;

namespace AboutMe.Web.Admin.Common
{
    public class AdminSMS
    {
        #region SMS 발송
        public void AdminSmsSend(AdminSMSModel adminSMSModel)
        {
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                    AdminProductContext.SP_ADMIN_SMS_INS(
                       adminSMSModel.SMS_FLAG
                       , adminSMSModel.SEND_TIME
                       , adminSMSModel.HANDPHONE
                       , adminSMSModel.CALLBACK_NO
                       , adminSMSModel.TITLE
                       , adminSMSModel.SEND_MSG);

            }
        }
        #endregion
    }
}