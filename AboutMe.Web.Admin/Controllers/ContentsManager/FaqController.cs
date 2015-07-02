using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Faq;
using AboutMe.Domain.Entity.Faq;

using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace AboutMe.Web.Admin.Controllers.ContentsManager
{
    
     public class FaqSearchModel
    {
         private string searchcol;
         private string searchkeyword;
         private string displayyn;
         private int? page;
         private int? pagesize;

         public string SearchCol
         {
            get {
                return this.searchcol ?? "";
            }
            set {
                this.searchcol = value;
            }
        }


         public string SearchKeyword
         {
             get
             {
                 return this.searchkeyword ?? "";
             }
             set
             {
                 this.searchkeyword = value;
             }
         }

         public string DisplayYn
         {
             get
             {
                 return this.displayyn ?? "A";
             }
             set
             {
                 this.displayyn = value;
             }
         }

         public int? Page
         {
             get
             {
                 return this.page ?? 1;
             }
             set
             {
                 this.page = value;
             }
         }

         public int? PageSize
         {
             get
             {
                 return this.pagesize ?? 10;
             }
             set
             {
                 this.pagesize = value;
             }
         }
    }

    public class FaqController : Controller
    {
       

        private IFaqService _faqservice;


        public FaqController(IFaqService _faqservice)
        {
            this._faqservice = _faqservice;
        }

        // GET: Faq
        
        public ActionResult FaqGridList(DataSourceRequest request, FaqSearchModel searchModel)
        {
           
            string SortCol = "IDX";
            string SortDir = "DESC";
            int? Page = request.Page;
            int? PageSize = request.PageSize;

            int total_count = 0;
            total_count = _faqservice.FaqListCount(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn);
            
            List<SP_TB_FAQ_SEL_Result> lst = new List<SP_TB_FAQ_SEL_Result>();
            lst = _faqservice.FaqList(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn, SortCol, SortDir, Page, PageSize);

            //DataSourceResult result = new DataSourceResult();
            //result = lst.ToDataSourceResult(request);
            //result.Total = total_count;
            var jsonData = new { Total = total_count, Data = lst };
          
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /*
        public ActionResult FaqList(string SearchCol = "", string SearchKeyword = "", string DisplayYn = "A", string SortCol = "IDX", string SortDir = "DESC", int? Page = 1, int? PageSize = 10)
        {
            int total_count = 0;
            total_count = _faqservice.FaqListCount(SearchCol, SearchKeyword, DisplayYn);

            List<SP_TB_FAQ_SEL_Result> lst = new List<SP_TB_FAQ_SEL_Result>();
            lst = _faqservice.FaqList(SearchCol, SearchKeyword, DisplayYn, SortCol, SortDir, Page, PageSize);
            var jsonData = new { count = total_count, result = lst };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        */
        public ActionResult Index(FaqSearchModel item)
        {
            this.ViewBag.SearchCol = item.SearchCol;
            this.ViewBag.SearchKeyword = item.SearchKeyword;
            this.ViewBag.DisplayYn = item.DisplayYn;
            this.ViewBag.Page = item.Page;
            this.ViewBag.PageSize = item.PageSize;

            return View();
        }

        public ActionResult New(FaqSearchModel searchModel)
        {

            var model = new TB_FAQ();
            model.IDX = -1;
            model.TITLE = "";
            model.QUESTION = "";
            model.ANSWER = "";
            model.DISPLAY_YN = "Y";

            ViewBag.SearchCol = searchModel.SearchCol;
            ViewBag.SearchKeyword = searchModel.SearchKeyword;
            ViewBag.DisplayYn = searchModel.DisplayYn;
            ViewBag.Page = searchModel.Page;
            ViewBag.PageSize = searchModel.PageSize;

            return View("New", model);

        }

        [HttpPost]
        public ActionResult FaqAction(TB_FAQ itemFaq, FaqSearchModel searchModel, string Mode)
        {
            if (ModelState.IsValid)
            {
                //ToBe: 관리자계정 
                itemFaq.M_ID = "bckang";

                if (Mode == "NEW")
                {
                    _faqservice.FaqInsert(itemFaq);
                }
                else if (Mode == "EDIT")
                {
                    _faqservice.FaqUpdate(itemFaq);
                }
                return RedirectToAction("Index", searchModel);
            }
            else
            {
                return View("New",itemFaq);
            }
            
        }

        public ActionResult FaqDelete(int IDX, FaqSearchModel searchModel)
        {
            _faqservice.FaqDelete(IDX);
            return RedirectToAction("Index",searchModel);            
        }

        public ActionResult Edit(int IDX, FaqSearchModel searchModel)
        {

            var model = new TB_FAQ();
            
            SP_TB_FAQ_VIEW_Result resultModel =  _faqservice.FaqView(IDX);
            model.IDX = resultModel.IDX;
            model.TITLE = resultModel.TITLE;
            model.QUESTION = resultModel.QUESTION;
            model.ANSWER = resultModel.ANSWER;
            
            model.CATEGORY = resultModel.CATEGORY;
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