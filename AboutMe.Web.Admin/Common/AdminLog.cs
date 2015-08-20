using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AboutMe.Domain.Entity.AdminProduct;

namespace AboutMe.Web.Admin.Common
{
    public class AdminLog
    {
        #region 관리자 로그
        public void AdminLogSave(string memo, string comment)
        {
            string ip;
            string preUrl;
            string adminid;
            string userAgent;
            string urlReFerrer;
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            preUrl = HttpContext.Current.Request.Url.ToString();
            adminid = AdminUserInfo.GetAdmId();
            userAgent = HttpContext.Current.Request.UserAgent;
            urlReFerrer = HttpContext.Current.Request.UrlReferrer.ToString();

            if (string.IsNullOrEmpty(adminid))
            {
                adminid = "";
            }
            if (memo.Length > 8000) { 
                memo = memo.Substring(0, 6000);
            }
            
            if (comment.Length > 100)
            {
                comment = comment.Substring(0, 99);
            }
            if (userAgent.Length > 1000)
            {
                userAgent = userAgent.Substring(0, 990);
            }
            if (urlReFerrer.Length > 1000)
            {
                urlReFerrer = urlReFerrer.Substring(0, 990);
            }
            
            using (AdminProductEntities AdminProductContext = new AdminProductEntities())
            {
                //AdminProductContext.SP_ADMIN_LOG_INS(adminid, memo, comment, preUrl, ip);
                AdminProductContext.SP_ADMIN_LOG_INS(adminid, memo, comment, preUrl, ip, userAgent, urlReFerrer);
            }
        }
        #endregion
    }
}