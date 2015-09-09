using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AboutMe.Domain.Entity.AdminEtc;
using AboutMe.Domain.Service.AdminEtc;
using AboutMe.Common.Data;
using AboutMe.Common.Helper;



namespace AboutMe.Common.ErrorHandler
{
    public class AppErrorLog
    {

        // 콘트롤러에서 에러가 발생했을 때 에러로그 인서트 
        public void HandlerInsertAppErrLogForController(Exception ex)
        {

            try
            {
                string sData = string.Empty;
                Int64 pcID = 0;


                HttpContext context = HttpContext.Current;


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



                //회원아이디 가져오기 
                CookieSessionStore cookiesession = new CookieSessionStore();
                string M_ID = cookiesession.GetSecretSession("M_ID");

                TB_APP_ERROR_LOG tb = new TB_APP_ERROR_LOG();
                tb.MODULE_ID = context.Request.Path;
                tb.ERR_NO = 0;

                if (ex.InnerException != null)
                {
                    tb.ERR_MSG = ex.InnerException.ToString();
                    //tb.ERR_MSG = context.Error.Message.ToString();
                }
                tb.ERR_QUERY = "";
                tb.COMPUTER_NAME = context.Server.MachineName;
                tb.EMP_NAME = "";
                tb.EMP_EMAIL = AboutMe.Common.Helper.Utility01.GetUserIPAddress(); ;
                tb.MEMB_ID = M_ID;
                tb.RESULT_YN = "N";
                tb.POST_DATA = sData;
                tb.PCID = pcID;
                tb.HTTP_COOKIE = context.Request.ServerVariables["HTTP_COOKIE"];

                context.Server.ClearError(); //CPU폭증등의 문제가 발생한다면 이 라인을 활성화 해보자

                AdminEtcErrorLogService _AdminEtcErrorLogServiceObj = new AdminEtcErrorLogService();
                _AdminEtcErrorLogServiceObj.InsertAppErrlog(tb);
              
            }
            catch (Exception ex02)
            {

               

            }
            


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