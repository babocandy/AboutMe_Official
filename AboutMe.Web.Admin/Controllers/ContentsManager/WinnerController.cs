using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Winner;
using AboutMe.Domain.Entity.Winner;

using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;

namespace AboutMe.Web.Admin.Controllers.ContentsManager
{
    [CustomAuthorize]
    public class WinnerController : BaseAdminController
    {
        private IWinnerService _winnerservice;
        public WinnerController(IWinnerService _winnerservice)
        {
            this._winnerservice = _winnerservice;
        }
        //Grid 요청
        public ActionResult WinnerGridList(DataSourceRequest request, WINNER_ADMIN_SEARCH searchModel)
        {
            if (searchModel == null)
            {
                searchModel = new WINNER_ADMIN_SEARCH();
            }
            int total_count = 0;
            total_count = _winnerservice.WinnerAdminCount(searchModel);

            List<SP_TB_WINNER_SEL_Result> lst = new List<SP_TB_WINNER_SEL_Result>();
            lst = _winnerservice.WinnerAdminList(searchModel);

            var jsonData = new { Total = total_count, Data = lst };
          
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(WINNER_ADMIN_SEARCH item)
        {
            if (item == null)
            {
                item = new WINNER_ADMIN_SEARCH();
            }
            return View(item);
        }

        public ActionResult New(WINNER_ADMIN_SEARCH searchModel)
        {
            SP_TB_WINNER_VIEW_Result resultModel = new SP_TB_WINNER_VIEW_Result { 
                IDX = 0,
                TITLE = "",
                CONTENTS = "",
                M_ID = "",
                DISPLAY_YN="",
                VIEW_CNT = 0
            };

            WINNER_ADMIN_EDIT M = new WINNER_ADMIN_EDIT
            {
                IDX = 0,
                Mode = "NEW",
                WinnerInfo = resultModel,
                SearchParam = searchModel
            };
            return View("Edit", M);

        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult WinnerAction(int IDX, TB_WINNER UpdateData, WINNER_ADMIN_SEARCH searchModel, string MODE)
        {
            UpdateData.M_ID = AdminUserInfo.GetAdmId();
            if (MODE == "NEW")
            {
                _winnerservice.WinnerAdminInsert(UpdateData);
            }
            else if (MODE == "EDIT")
            {
                _winnerservice.WinnerAdminUpdate(IDX,UpdateData);
            }
            return RedirectToAction("", searchModel);

        }

        public ActionResult Edit(int IDX, WINNER_ADMIN_SEARCH searchModel)
        {
            SP_TB_WINNER_VIEW_Result resultModel = _winnerservice.WinnerView(IDX);
            WINNER_ADMIN_EDIT M = new WINNER_ADMIN_EDIT
            {
                IDX = IDX,
                Mode = "EDIT",
                WinnerInfo = resultModel,
                SearchParam = searchModel
            };

            return View("Edit", M);
        }

    }
}