using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminEtc;
using AboutMe.Domain.Entity.AdminEtc;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminUserController : BaseAdminController
    {
        private IAdminUserService _AdminUserService;


        public AdminUserController(IAdminUserService _adminUserService)
        {
            this._AdminUserService = _adminUserService;
        }
        

        // GET: AdminUser 관리자관리-목록 /AdminUser/Index/
        public ActionResult Index(string SearchCol = "", string SearchKeyword = "", string SortCol = "ADM_INS_DATE", string SortDir = "DESC", int Page = 1, int PageSize = 10)
        {
            //return View();
            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;

            //AdminUserService srv = new AdminUserService();
            int TotalRecord = 0;
            TotalRecord = _AdminUserService.GetAdminMemberListCnt(SearchCol, SearchKeyword);
            //TotalRecord = srv.GetAdminMemberListCnt(SearchCol, SearchKeyword);

            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;


            return View(_AdminUserService.GetAdminMemberList(SearchCol, SearchKeyword, SortCol, SortDir, Page, PageSize).ToList());
            //return View(srv.GetAdminMemberList(SearchCol, SearchKeyword, SortCol, SortDir, Page, PageSize).ToList());
        }
    }
}