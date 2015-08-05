using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Recallbbs;
using AboutMe.Domain.Entity.Recallbbs;

using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;

namespace AboutMe.Web.Admin.Controllers.Order
{
    [CustomAuthorize]
    public class RecallController : BaseAdminController
    {
        private IRecallbbsService _recallservice;
        public RecallController(IRecallbbsService _recallservice)
        {
            this._recallservice = _recallservice;
        }
        //Grid 요청
        public ActionResult RecallGridList(DataSourceRequest request, RECALL_ADMIN_SEARCH searchModel)
        {
            if (searchModel == null)
            {
                searchModel = new RECALL_ADMIN_SEARCH();
            }
            int total_count = 0;
            total_count = _recallservice.RecallAdminCount(searchModel);

            List<SP_ADMIN_RECALL_BBS_SEL_Result> lst = new List<SP_ADMIN_RECALL_BBS_SEL_Result>();
            lst = _recallservice.RecallAdminList(searchModel);

            var jsonData = new { Total = total_count, Data = lst };
          
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(RECALL_ADMIN_SEARCH item)
        {
            if (item == null)
            {
                item = new RECALL_ADMIN_SEARCH();
            }
            return View(item);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecallAction(int IDX, string CONFIRM_CONTENTS, RECALL_ADMIN_SEARCH searchModel)
        {
            string ADM_ID = AdminUserInfo.GetAdmId();
            _recallservice.RecallAdminUpdate(IDX, ADM_ID, CONFIRM_CONTENTS);
            return RedirectToAction("", searchModel);
        }

        public ActionResult Edit(int IDX, RECALL_ADMIN_SEARCH searchModel)
        {
            SP_ADMIN_RECALL_BBS_VIEW_Result resultModel = _recallservice.QnaAdminView(IDX);
            RECALL_ADMIN_EDIT M = new RECALL_ADMIN_EDIT
            {
                RecallInfo = resultModel,
                SearchParam = searchModel
            };

            return View("Edit", M);

        }
    }
}