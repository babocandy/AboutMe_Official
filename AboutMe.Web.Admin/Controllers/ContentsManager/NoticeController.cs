using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Notice;
using AboutMe.Domain.Entity.Notice;

using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;

namespace AboutMe.Web.Admin.Controllers.ContentsManager
{
    [CustomAuthorize]
    public class NoticeController : BaseAdminController
    {
        private INoticeService _noticeservice;
        public NoticeController(INoticeService _noticeservice)
        {
            this._noticeservice = _noticeservice;
        }

        public ActionResult NoticeGridList(DataSourceRequest request, NoticeSearchModel searchModel)
        {

            string SortCol = "IDX";
            string SortDir = "DESC";
            int? Page = request.Page;
            int? PageSize = request.PageSize;

            int total_count = 0;
            total_count = _noticeservice.NoticeListCount(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn);

            List<SP_TB_NOTICE_SEL_Result> lst = new List<SP_TB_NOTICE_SEL_Result>();
            lst = _noticeservice.NoticeList(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn, SortCol, SortDir, Page, PageSize);

            var jsonData = new { Total = total_count, Data = lst };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // GET: Notice
        public ActionResult Index(NoticeSearchModel item)
        {
            this.ViewBag.SearchCol = item.SearchCol;
            this.ViewBag.SearchKeyword = item.SearchKeyword;
            this.ViewBag.DisplayYn = item.DisplayYn;
            this.ViewBag.Page = item.Page;
            this.ViewBag.PageSize = item.PageSize;

            return View();
        }


        public ActionResult New(NoticeSearchModel searchModel)
        {

            var model = new TB_NOTICE();
            model.IDX = -1;
            model.TITLE = "";
            model.CONTENTS = "";
            model.DISPLAY_YN = "Y";

            ViewBag.SearchCol = searchModel.SearchCol;
            ViewBag.SearchKeyword = searchModel.SearchKeyword;
            ViewBag.DisplayYn = searchModel.DisplayYn;
            ViewBag.Page = searchModel.Page;
            ViewBag.PageSize = searchModel.PageSize;

            return View("New", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NoticeAction(TB_NOTICE items, NoticeSearchModel searchModel, string Mode)
        {
            if (ModelState.IsValid)
            {
                items.M_ID = AdminUserInfo.GetAdmId();

                if (Mode == "NEW")
                {
                    _noticeservice.NoticeInsert(items);
                }
                else if (Mode == "EDIT")
                {
                    _noticeservice.NoticeUpdate(items);
                }
                return RedirectToAction("Index", searchModel);
            }
            else
            {
                return View("New", items);
            }

        }

        public ActionResult NoticeDelete(int IDX, NoticeSearchModel searchModel)
        {
            _noticeservice.NoticeDelete(IDX);
            return RedirectToAction("Index", searchModel);
        }

        public ActionResult Edit(int IDX, NoticeSearchModel searchModel)
        {

            var model = new TB_NOTICE();

            SP_TB_NOTICE_VIEW_Result resultModel = _noticeservice.NoticeView(IDX);
            model.IDX = resultModel.IDX;
            model.TITLE = resultModel.TITLE;
            model.CONTENTS = resultModel.CONTENTS;
            model.DISPLAY_YN = resultModel.DISPLAY_YN;
            model.M_ID = resultModel.M_ID;
            model.REG_DATE = resultModel.REG_DATE;
            model.VIEW_CNT = resultModel.VIEW_CNT;

            ViewBag.SearchCol = searchModel.SearchCol;
            ViewBag.SearchKeyword = searchModel.SearchKeyword;
            ViewBag.DisplayYn = searchModel.DisplayYn;
            ViewBag.Page = searchModel.Page;
            ViewBag.PageSize = searchModel.PageSize;

            return View("New", model);

        }
    }
}