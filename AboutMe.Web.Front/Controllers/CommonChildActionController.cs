﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Data;

using AboutMe.Common.Helper;
using AboutMe.Web.Front.Common;



namespace AboutMe.Web.Front.Controllers
{
    public class CommonChildActionController : BaseFrontController
    {


        // GET: CommonChildAction
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Paging(int TotalRecord, int RecordPerPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {

            //public PagingProps(int TotalRecord, int RecordPerPage, int pPagePerBlock, int pCurrentPage, string q_str)

            PagingProps p_value = new PagingProps(TotalRecord, RecordPerPage, PagePerBlock, CurrentPage, QueryStr);
            //p_value.max_page = (int)Math.Ceiling((double)total_cnt / page_size); //올림
            //var class_value = p_value;
            return PartialView(p_value);
        }


        //프론트 상단 우측 - 회원로그인 상황에따른 ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildRightTopMenu_Util()
        {
            this.ViewBag.IS_LOGIN = MemberInfo.IsMemberLogin();  //로그인 여부  T/F
            this.ViewBag.M_ID = MemberInfo.GetMemberId();  //로그인 계정
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();

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

        #region HSW

        #region 상품상세의 상품리뷰 페이지
        [ChildActionOnly]
        public ActionResult ShoppingDetailReviewInfo(string P_CODE)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion

        #region 상품상세의 right 프로모션
        [ChildActionOnly]
        public ActionResult ShoppingDetailRightPromotion(string P_CODE)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion

        #region 상품상세의 right 세트상품
        [ChildActionOnly]
        public ActionResult ShoppingDetailRightSetGoods(string P_CODE)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion

        #region 상품상세의 left 이벤트
        [ChildActionOnly]
        public ActionResult ShoppingDetailLeftEvent(string P_CODE)
        {
            this.ViewBag.P_CODE = P_CODE; //상품코드
            return View();
        }
        #endregion

        
        #endregion
    }
}