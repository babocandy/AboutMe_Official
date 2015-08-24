using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Data;

using AboutMe.Domain.Service.AdminFrontMember;
using AboutMe.Domain.Entity.AdminFrontMember;
using AboutMe.Domain.Service.AdminOrder;
using AboutMe.Domain.Entity.AdminOrder;

namespace AboutMe.Web.Admin.Controllers
{
    public class CommonChildActionController : BaseAdminController
    {

        private IAdminFrontMemberService _AdminFrontMemberService;
        private IAdminOrderService _adminorderservice;

        public CommonChildActionController(IAdminFrontMemberService _adminFrontMemberService, IAdminOrderService _adminorderservice)
        {
            this._AdminFrontMemberService = _adminFrontMemberService;
            this._adminorderservice = _adminorderservice;
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
            SP_ADMIN_ORDER_MEMBER_STATUS_Result orderResult = _adminorderservice.OrderMemberStatus(M_ID);
            //회원 써머리 정보  : 초기화
            this.ViewBag.TOTAL_ORDER_CNT = orderResult.ORDER_CNT; //총구매건수
            this.ViewBag.TOTAL_ORDER_PRICE = orderResult.ORDER_PRICE; //총 구매액
            this.ViewBag.TOTAL_COUPON_CNT = 0; //보유쿠폰
            this.ViewBag.TOTAL_QNA_CNT = orderResult.QNA_CNT; //1:1문의

            return View(_AdminFrontMemberService.GetAdminFrontMemberView(M_ID));
        }

        //임직원신청 연관정보 : 임직원DB- ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildStaffRequest_REF_BASEDB(string STAFF_ID = "")
        {
            this.ViewBag.STAFF_ID = STAFF_ID;
            return View(_AdminFrontMemberService.GetAdminMemberStaffRequest_REF_BASEDB(STAFF_ID));
        }

        //임직원신청 연관정보 : 회원TBL- ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildStaffRequest_REF_MEMBER(string STAFF_ID = "")
        {
            this.ViewBag.STAFF_ID = STAFF_ID;
            return View(_AdminFrontMemberService.GetAdminMemberStaffRequest_REF_MEMBER(STAFF_ID));
        }

        //임직원신청 연관정보 : 임직원신청이력- ChildView제공을 위한 Ctl
        [ChildActionOnly]
        public ActionResult ChildStaffRequest_REF_REQUEST(string STAFF_ID = "")
        {
            this.ViewBag.STAFF_ID = STAFF_ID;
            return View(_AdminFrontMemberService.GetAdminMemberStaffRequest_REF_REQUEST(STAFF_ID));
        }



    }//class
} //namespace
