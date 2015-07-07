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
        public ActionResult Index(string SearchKey, string SearchValue, int Page = 1)
        {
            AdminPointMemberViewModel viewModel = new AdminPointMemberViewModel();

            Debug.WriteLine("SearchKey: " + SearchKey);
            Debug.WriteLine("SearchValue: " + SearchValue);

            viewModel.MemberList = _AdminPointService.GetMemberList(Page, 10, SearchKey, SearchValue);
            viewModel.PageNo = Page;
            viewModel.SearchKey = SearchKey;

            if (SearchKey == null || SearchKey.Length == 0)
            {
                SearchValue = "";
            }/**/
            viewModel.SearchValue = SearchValue;
            viewModel.TotalItem = _AdminPointService.GeMemberListCnt(SearchKey, SearchValue);

            return View(viewModel);
        }

        //public ActionResult PopupMemberPointInsert(string Mid, string Type, string Reason, string Point)
        public ActionResult PopupMemberPoint(string Mid)
        {
            AdminPointInsertViewModel model = new AdminPointInsertViewModel();
            model.Mid = Mid;

            return View(model);
        }

        [HttpPost]
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

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        public ActionResult MemberPointInsert()
        {
            return View();
        }
    }
}