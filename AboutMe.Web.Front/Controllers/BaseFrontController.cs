//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;
using AboutMe.Domain.Entity.Common;

//using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers
{
    public class BaseFrontController : Controller
    {

        public string DetectingDevice()
        {
            string str =  AboutMe.Common.Helper.DetectDeviceUtil.GetUserDeviceType();
            return str;
        }

        protected USER_PROFILE _user_profile = new USER_PROFILE
        {
            IS_LOGIN = MemberInfo.IsMemberLogin()
            ,
            M_ID = MemberInfo.GetMemberId()
            ,
            M_NAME = MemberInfo.GetMemberName()
            ,
            SESSION_ID = System.Web.HttpContext.Current.Session.SessionID
            ,
            M_GRADE = MemberInfo.GetMemberGrade()
            ,
            M_GRADE_NAME = MemberInfo.GetMemberGradeName()
            ,
            M_GBN = MemberInfo.GetMemberGBN()                    //회원 구분  S:임직원 , A or기타:일반회원 varchar(1)
            ,
            M_EMAIL = MemberInfo.GetMemberEmail()
            ,
            M_M_SKIN_TROUBLE_CD = MemberInfo.GetMemberSkinTroubleCD()  //회원 피부트러블 코드  char(9)
            ,
            NOMEMBER_ORDER_CODE = MemberInfo.GetNomemberOrderCode()
        };

        /**
         * 상품 이미지 폴더
         */
        public string _img_path_product{
            get
            {
                return AboutMe.Common.Helper.Config.GetConfigValue("ProductPhotoPath");
            }
        }

        /**
         * 리뷰 이미지 폴더
         */
        public string _img_path_review
        {
            get
            {
                return AboutMe.Common.Helper.Config.GetConfigValue("ReviewPhotoPath");
            }
        }

        /**
         * 전시 이미지 폴더
         */
        public string _img_path_display
        {
            get
            {
                return AboutMe.Common.Helper.Config.GetConfigValue("DisplayPath");
            }
        }

       protected override void Initialize(System.Web.Routing.RequestContext requestContext)
       {
          
          base.Initialize(requestContext);


          /////////////////////////////////////////////////////////////////////////// device 디바이스 감지 

          string ThisHost = Request.Url.Host.ToString();
          string ThisDevice = this.DetectingDevice();
          CookieSessionStore Sobj = new CookieSessionStore();

          if (ThisDevice != "d" && ThisDevice != "t") //데스크탑이나 태블릿이 아니라면 강제로 모바일사이트로 이동.
          {
              if (Request["forced"] == null || Request["forced"].ToString() == "" )
              {
                  if (Sobj.GetSession("ForcedDevice") == null || Sobj.GetSession("ForcedDevice") == "")
                  {
                      if (ThisHost != "localhost") //개발툴에서 디버깅 중일때는 이동 안시킴
                      {
                          Response.Redirect(AboutMe.Common.Helper.Config.GetConfigValue("MobileUrl"));
                          //RedirectToAction("Index", AboutMe.Common.Helper.Config.GetConfigValue("MobileUrl"));
                      }
                  }
              }
              else if (Request["forced"] != null && Request["forced"] != "") //forced=Y로 입력해서 들어왔으면 세션에 입력 //forced=Y로 입력해서 들어왔으면 세션에 입력 
              {
                  
                  Sobj.SetSession("ForcedDevice", "Y");
              }

          }


      
         // MyInitialzie()
        

       }

      
    }
}