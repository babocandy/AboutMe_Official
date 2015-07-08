﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Data;

using AboutMe.Domain.Service.AdminFrontMember;
using AboutMe.Domain.Entity.AdminFrontMember;

namespace AboutMe.Web.Admin.Controllers
{
    public class CommonChildActionController : BaseAdminController
    {

        private IAdminFrontMemberService _AdminFrontMemberService;

        public CommonChildActionController(IAdminFrontMemberService _adminFrontMemberService)
        {
            this._AdminFrontMemberService = _adminFrontMemberService;
        }


        // GET: CommonChildAction
        /**
        public ActionResult Index()
        {
            return View();
        }
         **/

        [ChildActionOnly]
        public ActionResult Paging(int TotalRecord, int RecordPerPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {

            //public PagingProps(int TotalRecord, int RecordPerPage, int pPagePerBlock, int pCurrentPage, string q_str)

            PagingProps p_value = new PagingProps(TotalRecord, RecordPerPage, PagePerBlock, CurrentPage, QueryStr);
            //p_value.max_page = (int)Math.Ceiling((double)total_cnt / page_size); //올림
            //var class_value = p_value;
            return PartialView(p_value);
        }

        //회원정보 ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildFrontMemberInfo(string M_ID="")
        {

            //회원 써머리 정보  : 초기화
            this.ViewBag.TOTAL_ORDER_CNT = 0; //총구매건수
            this.ViewBag.TOTAL_ORDER_PRICE = 0; //총 구매액
            this.ViewBag.TOTAL_COUPON_CNT = 0; //보유쿠폰
            this.ViewBag.TOTAL_QNA_CNT = 0; //1:1문의

            return View(_AdminFrontMemberService.GetAdminFrontMemberView(M_ID));
        }

    }
}
