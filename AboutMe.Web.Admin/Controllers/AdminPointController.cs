using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminPoint;
using AboutMe.Domain.Entity.AdminPoint;
using System.Data.Entity.Core.Objects;
using AboutMe.Common.Data;

using System.Diagnostics;
using AboutMe.Web.Admin.Models;
using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;


namespace AboutMe.Web.Admin.Controllers
{
    public class AdminPointController : BaseAdminController
    {
        private IAdminPointService _AdminPointService;
        public AdminPointController(IAdminPointService _adminPointService)
        {
            this._AdminPointService = _adminPointService;
        }

        /**
         * 포인트 관리자 목록
         */
        // GET: AdminPoint
        [CustomAuthorize]
        public ActionResult Index(string searchKey, string searchValue, int page = 1, int pageSize = 10)
        {
            AdminPointMemberViewModel viewModel = new AdminPointMemberViewModel();

            Debug.WriteLine("Page: " + page);
            Debug.WriteLine("PageSize: " + pageSize);
            Debug.WriteLine("SearchKey: " + searchKey);
            Debug.WriteLine("SearchValue: " + searchValue);

            viewModel.MemberList = _AdminPointService.GetMemberList(page, pageSize, searchKey, searchValue);
            viewModel.PageNo = page;
            viewModel.PageSize = pageSize;
            viewModel.SearchKey = searchKey;
            
            if (searchKey == null || searchKey.Length == 0) searchValue = "";

            viewModel.SearchValue = searchValue;
            viewModel.TotalItem = _AdminPointService.GeMemberListCnt(searchKey, searchValue);

            return View(viewModel);
        }

        /**
         *  팝업-회원 포인트 적립/차감 수정  
         */
        [CustomAuthorize]
        public ActionResult MemberPointUpdate(string M_ID)
        {
            AdminPointInsertViewModel model = new AdminPointInsertViewModel();
            model.Mid = M_ID;
            model.AdminId = AdminUserInfo.GetAdmId();
            model.AdminName = AdminUserInfo.GetAdmName();
            model.MemberProfile = _AdminPointService.GetMemberProfile(M_ID);

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult MemberPointUpdate(AdminPointInsertViewModel model)
        {
            model.MemberProfile = _AdminPointService.GetMemberProfile(model.Mid);

            if (ModelState.IsValid)
            {
                if (model.Type == "1") // 관리자 적립
                {
                    Tuple<string, string> ret = _AdminPointService.UpdateMemberPointSave(model.Mid, model.Point, model.Reason, AdminUserInfo.GetAdmId(), AdminUserInfo.GetAdmName() );
                    model.ResultMessage = ret.Item2;
                    model.ResultNum = ret.Item1;

                }
                else if (model.Type == "2") // 관리자 사용
                {
                    Tuple<string, string> ret = _AdminPointService.UpdateMemberPointUse(model.Mid, model.Point, model.Reason, AdminUserInfo.GetAdmId(), AdminUserInfo.GetAdmName());
                    model.ResultMessage = ret.Item2;
                    model.ResultNum = ret.Item1;
                }

                return View(model);
            }

            ModelState.AddModelError("", "필수항목들을 입력해주세요");
            return View(model);
        }


        /**
         * 팝업 - 회원관리>포인트 내역 조회
         */
        [CustomAuthorize]
        public ActionResult MyPointHistory(string M_ID, int page = 1)
        {
            var model = new AdminMyPointHistoryViewModel();
            model.PointHistoryList = _AdminPointService.GetMyPointHistoryList(M_ID, page);
            model.Mid = M_ID;
            model.PageNo = page;
            model.TotalItem = _AdminPointService.GetMyPointHistoryListCnt(M_ID);
            return View(model);
        }


        /**
         * 샘플 - 주문 관련 테스트 페이지
         */
        [CustomAuthorize]
        public ActionResult TestPoint()
        {
            return View();
        }




        /**
         * 샘플 - 구매시 포인트 사용
         */
        [CustomAuthorize]
        public ActionResult UsePointOnOrder(string mid, int point, string orderCode)
        {
            Debug.WriteLine("mid :" + mid);
            Tuple<string, string> result = _AdminPointService.UsePointOnOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Request.Form["ORDER_CODE"]);

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        /**
         * 샘플 - 주문 전체 취소
         */
        [CustomAuthorize]
        public ActionResult CancelAllOfOrder()
        {
            /**/
            Tuple<string, string> result = _AdminPointService.CancelAllOfOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Request.Form["ORDER_CODE"]);

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        /**
         * 샘플 - 주문 부분 취소 샘플
         */
        [CustomAuthorize]
        public ActionResult CancelPartOfOrder()
        {
            /**/
            Tuple<string, string> result = _AdminPointService.CancelPartOfOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Request.Form["ORDER_CODE"], Convert.ToInt32(Request.Form["ORDER_DETAIL_IDX"]), Request.Form["P_NAME"]);

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);
           
            return RedirectToAction("TestPoint");
        }

        /**
         * 샘플 - 구매 확정후 포인트 적립
         */
        [CustomAuthorize]
        public ActionResult SavePointAfterFirmOrder()
        {

            Tuple<string, string> result = _AdminPointService.SavePointAfterFirmOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Request.Form["ORDER_CODE"], Convert.ToInt32(Request.Form["ORDER_DETAIL_IDX"]), Request.Form["P_NAME"] );

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        


        /**
         * 샘플 - 구매 확정후 취소시 사용했던 포인트 재적립
         */
        [CustomAuthorize]
        public ActionResult ResaveUsedPointOnCancelAfterFirmOrder()
        {

            Tuple<string, string> result = _AdminPointService.ResaveUsedPointOnCancelAfterFirmOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Request.Form["ORDER_CODE"], Convert.ToInt32(Request.Form["ORDER_DETAIL_IDX"]), Request.Form["P_NAME"]);

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        /**
         * 샘플 - 구매 확정후 취소시 적립했던 포인트 회수
         */
        [CustomAuthorize]
        public ActionResult GetBackSavedPointOnCancelAfterFirmOrder()
        {

            Tuple<string, string> result = _AdminPointService.GetBackSavedPointOnCancelAfterFirmOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Request.Form["ORDER_CODE"], Convert.ToInt32(Request.Form["ORDER_DETAIL_IDX"]), Request.Form["P_NAME"]);

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        
    }
}