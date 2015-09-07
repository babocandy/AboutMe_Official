using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AboutMe.Domain.Entity.AdminEtc;
using AboutMe.Domain.Service.AdminEtc;
using AboutMe.Common.Data;



namespace AboutMe.Web.Mobile.Common
{
    public class AppErrorLog
    {

        /**
        private IAdminEtcErrorLogService _AdminEtcErrorLogService ;


        public AppErrorLog(AdminEtcErrorLogService _adminEtcErrorLogService)
        {
            this._AdminEtcErrorLogService = _adminEtcErrorLogService;
        }

        public AppErrorLog()
        {

        }
         * **/

        public void HandlerInsertAppErrLog(object sender, EventArgs e)
        {
            string sData = string.Empty;
            Int64 pcID = 0;

            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            //Exception error = context.Server.GetLastError();
            //HttpException exception = new HttpException();
            //int httpCode = exception.GetHttpCode();

            for (int i = 0; i < context.Request.QueryString.Count; i++)
            {
                if (i > 0)
                    sData += "&";
                sData += context.Request.QueryString.Keys[i].ToString() + "=";
                sData += context.Request.QueryString[i].ToString();
            }
            for (int i = 1; i < context.Request.Form.Count; i++)
            {
                if (i == 1)
                    sData += ",,post->";
                else
                    sData += "&";
                sData += context.Request.Form.Keys[i].ToString() + "=";
                sData += context.Request.Form[i].ToString();
            }

            /**
            if (!PageBase.PcID.Equals(string.Empty))
                pcID = Int64.Parse(PageBase.PcID);

            **/
           
            TB_APP_ERROR_LOG tb = new TB_APP_ERROR_LOG();
            tb.MODULE_ID = context.Request.Path ;
            tb.ERR_NO = 0 ;
            if (context.Error.InnerException != null)
            {
                tb.ERR_MSG = context.Error.InnerException.ToString();
            }
            else
            {
                tb.ERR_MSG = context.Error.Message.ToString();
            }
            tb.ERR_QUERY = "";
            tb.COMPUTER_NAME = context.Server.MachineName;
            tb.EMP_NAME = "";
            tb.EMP_EMAIL = "";
            tb.MEMB_ID = MemberInfo.GetMemberId();
            tb.RESULT_YN = "N";
            tb.POST_DATA = sData;
            tb.PCID = pcID;
            tb.HTTP_COOKIE = context.Request.ServerVariables["HTTP_COOKIE"];

            context.Server.ClearError(); //CPU폭증등의 문제가 발생한다면 이 라인을 활성화 해보자

            AdminEtcErrorLogService _AdminEtcErrorLogServiceObj = new AdminEtcErrorLogService();
            _AdminEtcErrorLogServiceObj.InsertAppErrlog(tb);
      

            //, context.Request.ApplicationPath
            //, context.Error.Source
            //, context.Error.TargetSite.ToString()
            //, context.Error.StackTrace
            //, context.Error.InnerException.ToString()
            //, context.Request.ServerVariables["HTTP_COOKIE"]
            //, context.Request.UserAgent);

            /**** 안내페이지로 이동
            context.Server.Transfer(AppDic.GenericErrorPage + "?msg=" + context.Error.InnerException.ToString());
            context.Response.Redirect(AppDic.GenericErrorPage+"?msg="+context.Error.InnerException.ToString());
            *****/
            //}

            //context.Server.ClearError(); CPU폭증등의 문제가 발생한다면 이 라인을 활성화 해보자 
            //context.Response.Redirect(AppDic.GenericErrorPage);

        }

    }
}