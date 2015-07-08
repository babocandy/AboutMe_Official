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


namespace AboutMe.Web.Admin.Controllers
{
    public class AdminPointController : BaseAdminController
    {
        // GET: AdminProduct
        private IAdminPointService _AdminPointService;
        public AdminPointController(IAdminPointService _adminPointService)
        {
            this._AdminPointService = _adminPointService;
        }

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

        [CustomAuthorize]
        public ActionResult PopupMemberPoint(string M_ID)
        {
            AdminPointInsertViewModel model = new AdminPointInsertViewModel();
            model.Mid = M_ID;

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult PopupMemberPoint(AdminPointInsertViewModel model)
        {
            Debug.WriteLine("ModelState.IsValid - " + ModelState.IsValid);
            Debug.WriteLine("Type - " + model.Mid);
            Debug.WriteLine("Type - " + model.Type);
            Debug.WriteLine("Reason - " + model.Reason);
            Debug.WriteLine("Point - " + model.Point);

            if (ModelState.IsValid)
            {
                if (model.Type == "1") // 관리자 적립
                {
                    Tuple<string, string> ret = _AdminPointService.UpdateMemberPointSave(model.Mid, model.Point, model.Reason);
                    model.ResultMessage = ret.Item2;
                    model.ResultNum = ret.Item1;

                }
                else if (model.Type == "2") // 관리자 사용
                {
                    Tuple<string, string> ret = _AdminPointService.UpdateMemberPointUse(model.Mid, model.Point, model.Reason);
                    model.ResultMessage = ret.Item2;
                    model.ResultNum = ret.Item1;
                }

                return View(model);
            }

            ModelState.AddModelError("", "필수항목들을 입력해주세요");
            return View(model);
        }

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

        [CustomAuthorize]
        public ActionResult TestPoint()
        {
            return View();
        }

        [CustomAuthorize]
        public ActionResult SavePointOnOrder()
        {

            Tuple<string,string> result = _AdminPointService.SavePointOnOrder(Request.Form["M_ID"], Convert.ToInt32( Request.Form["AMOUNT"]), Convert.ToInt32(Request.Form["ORDER_IDX"]));

            Debug.WriteLine("에러번호 : "+result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        [CustomAuthorize]
        public ActionResult UsePointOnOrder()
        {

            Tuple<string, string> result = _AdminPointService.UsePointOnOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Convert.ToInt32(Request.Form["ORDER_IDX"]));

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        [CustomAuthorize]
        public ActionResult CancelAllOfOrder()
        {
            /**/
            Tuple<string, string> result = _AdminPointService.CancelAllOfOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Convert.ToInt32(Request.Form["ORDER_IDX"]));

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);

            return RedirectToAction("TestPoint");
        }

        [CustomAuthorize]
        public ActionResult CancelPartOfOrder()
        {
            /**/
            Tuple<string, string> result = _AdminPointService.CancelPartOfOrder(Request.Form["M_ID"], Convert.ToInt32(Request.Form["POINT"]), Convert.ToInt32(Request.Form["ORDER_IDX"]));

            Debug.WriteLine("에러번호 : " + result.Item1);
            Debug.WriteLine("에러메세지 : " + result.Item2);
           
            return RedirectToAction("TestPoint");
        }

        
    }
}