﻿//using System;
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

       protected override void Initialize(System.Web.Routing.RequestContext requestContext)
       {
          
         
          base.Initialize(requestContext);
      
         // MyInitialzie()
        

       }

      
    }
}