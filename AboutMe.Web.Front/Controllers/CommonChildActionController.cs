using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Data;

namespace AboutMe.Web.Front.Controllers
{
    public class CommonChildActionController : Controller
    {
        // GET: CommonChildAction
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Paging(int TotalRecord, int RecordPage, int PagePerBlock, int CurrentPage, string QueryStr)
        {

            //public PagingProps(int TotalRecord, int RecordPerPage, int pPagePerBlock, int pCurrentPage, string q_str)

            PagingProps p_value = new PagingProps(TotalRecord, RecordPage, PagePerBlock, CurrentPage, QueryStr);
            //p_value.max_page = (int)Math.Ceiling((double)total_cnt / page_size); //올림
            //var class_value = p_value;
            return PartialView(p_value);
        }
    }
}