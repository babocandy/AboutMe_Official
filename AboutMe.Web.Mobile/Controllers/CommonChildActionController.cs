﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Data;

using AboutMe.Common.Helper;
using AboutMe.Web.Mobile.Common;


namespace AboutMe.Web.Mobile.Controllers
{
    public class CommonChildActionController : BaseMobileController
    {
        // GET: CommonChildAction
        public ActionResult Index()
        {
            return View();
        }

        //모바일 레이아웃메뉴 - 회원로그인 상황에따른 ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildLayoutMenu_Util()
        {
            this.ViewBag.IS_LOGIN = MemberInfo.IsMemberLogin();  //로그인 여부  T/F
            this.ViewBag.M_ID = MemberInfo.GetMemberId();  //로그인 계정
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GBN = MemberInfo.GetMemberGBN();  //임직원 구분 A/S

            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade(); //등급
            this.ViewBag.M_GRADE_NAME = MemberInfo.GetMemberGradeName(); //등급명

            // this.ViewBag.RedirectUrl_HEADER = Server.UrlEncode(HttpContext.Request.Url.Authority);
            this.ViewBag.RedirectUrl_HEADER = ""; //기본:메인페이지
            string strRedirectUrl_HEADER = HttpContext.Request.Url.OriginalString;
            if (strRedirectUrl_HEADER.Length > 1)
            {
                string strRedirectUrl_HEADER_LOWER = strRedirectUrl_HEADER.ToLower(); //thanswk qusghks
                if (!strRedirectUrl_HEADER_LOWER.Contains("/membership"))  // 로그인/회원가입.. =>메인페이지로
                    this.ViewBag.RedirectUrl_HEADER = Server.UrlEncode(strRedirectUrl_HEADER);  // &를 포함한 경우를 대비
                if (strRedirectUrl_HEADER_LOWER.Contains("/order"))  //주문서 작성중 에서 로그인  ->Cart 로 이동 :지젠협의 OK
                    this.ViewBag.RedirectUrl_HEADER = "/Cart";
            }



            return View();
        }


        //모바일 푸터메뉴 - 회원로그인 상황에따른 ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildFooterMenu()
        {
            this.ViewBag.IS_LOGIN = MemberInfo.IsMemberLogin();  //로그인 여부  T/F
            this.ViewBag.M_ID = MemberInfo.GetMemberId();  //로그인 계정
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GBN = MemberInfo.GetMemberGBN();  //임직원 구분 A/S

            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade(); //등급
            this.ViewBag.M_GRADE_NAME = MemberInfo.GetMemberGradeName(); //등급명

            this.ViewBag.PC_VERSION_URL = Config.GetConfigValue("PC_VERSION_URL"); 

            return View();
        }


        [ChildActionOnly]
        public ActionResult Paging(int TotalRecord, int RecordPerPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {
            PagingProps p_value = new PagingProps(TotalRecord, RecordPerPage, PagePerBlock, CurrentPage, QueryStr);
            return PartialView(p_value);
        }

    }
}