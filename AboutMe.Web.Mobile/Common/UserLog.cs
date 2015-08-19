using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AboutMe.Domain.Entity.Product;

namespace AboutMe.Web.Mobile.Common
{
    public class UserLog
    {
        #region 사용자 로그
        public void UserLogSave(string memo, string comment)
        {
            string ip;
            string preUrl;
            string MemberId;
            string userAgent;
            string urlReFerrer;
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            preUrl = HttpContext.Current.Request.Url.ToString();
            MemberId = MemberInfo.GetMemberId();
            //userAgent = HttpContext.Current.Request.UserAgent;
            //urlReFerrer = HttpContext.Current.Request.UrlReferrer.ToString();

            if (string.IsNullOrEmpty(MemberId))
            {
                MemberId = "";
            }
            if (memo.Length > 8000)
            {
                memo = memo.Substring(0, 6000);
            }
            if (comment.Length > 100)
            {
                comment = comment.Substring(0, 99);
            }
            //if (userAgent.Length > 1000)
            //{
            //    userAgent = userAgent.Substring(0, 990);
            //}
            //if (urlReFerrer.Length > 1000)
            //{
            //    urlReFerrer = urlReFerrer.Substring(0, 990);
            //}

            using (ProductEntities ProductContext = new ProductEntities())
            {
                //AdminProductContext.SP_ADMIN_LOG_INS(adminid, memo, comment, preUrl, ip);
                ProductContext.SP_USER_LOG_INS(MemberId, memo, comment, preUrl, ip);
            }
        }
        #endregion
    }
}