using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Qna;
using AboutMe.Domain.Entity.Qna;

using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;

namespace AboutMe.Web.Admin.Controllers.ContentsManager
{
    [CustomAuthorize]
    public class QnaController : BaseAdminController
    {
        private IQnaService _qnaservice;
        public QnaController(IQnaService _qnaservice)
        {
            this._qnaservice = _qnaservice;
        }
        //Grid 요청
        public ActionResult QnaGridList(DataSourceRequest request, QNA_ADMIN_SEARCH searchModel)
        {
            if (searchModel == null)
            {
                searchModel = new QNA_ADMIN_SEARCH();
            }
            int total_count = 0;
            total_count = _qnaservice.QnaAdminCount(searchModel);

            List<SP_ADMIN_QNA_SEL_Result> lst = new List<SP_ADMIN_QNA_SEL_Result>();
            lst = _qnaservice.QnaAdminList(searchModel);

            var jsonData = new { Total = total_count, Data = lst };
          
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(QNA_ADMIN_SEARCH item)
        {
            if (item == null)
            {
                item = new QNA_ADMIN_SEARCH();
            }
            return View(item);
        }

        public ActionResult New(QNA_ADMIN_SEARCH searchModel)
        {

            return View("New", searchModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult QnaAction(int IDX, string ANSWER,  QNA_ADMIN_SEARCH searchModel)
        {
            _qnaservice.QnaAdminUpdate(IDX, ANSWER);
            return RedirectToAction("", searchModel);
        }

        public ActionResult Edit(int IDX, QNA_ADMIN_SEARCH searchModel)
        {
            SP_ADMIN_QNA_VIEW_Result resultModel = _qnaservice.QnaAdminView(IDX);
            QNA_ADMIN_EDIT M = new QNA_ADMIN_EDIT
            {
                QnaInfo = resultModel,
                SearchParam = searchModel
            };

            return View("Edit", M);

        }

        //회원정보 > 1:1문의내역
        public ActionResult QnaMemberList(string M_ID, int Page=1)
        {
            int PageSize = 10;

            QNA_ADMIN_MEMBER M = new QNA_ADMIN_MEMBER
            {
                Page = Page,
                PageSize = PageSize,
                M_ID = M_ID,
                QnaCount = _qnaservice.QnaAdminMemberCount(M_ID),
                QnaList =  _qnaservice.QnaAdminMemberList(M_ID, Page, PageSize)
            };
            return View(M);
        }
    }
}