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
    public class AdminMemberController : BaseAdminController
    {
        // GET: AdminMember

        private IAdminMemberService _AdminMemberService;


        public AdminMemberController(IAdminMemberService _adminMemberService)
        {
            this._AdminMemberService = _adminMemberService;
        }
        
        public ActionResult Index(string SearchCol = "",string SearchKeyword = "", string SortCol = "IDX",string SortDir ="DESC", int Page =1, int PageSize =10)
        {

            this.ViewBag.PageSize = PageSize;
            this.ViewBag.SearchCol = SearchCol;
            this.ViewBag.SearchKeyword = SearchKeyword;
            this.ViewBag.SortCol = SortCol;
            this.ViewBag.SortDir = SortDir;

            //AdminMemberService srv =  new AdminMemberService();
            int TotalRecord = 0;
            TotalRecord = _AdminMemberService.GetAdminMemberListCnt(SearchCol, SearchKeyword);
            this.ViewBag.TotalRecord = TotalRecord;
            //this.ViewBag.MaxPage = (int)Math.Ceiling((double)count / page_size); //올림
            this.ViewBag.Page = Page;


            return View(_AdminMemberService.GetAdminMemberList(SearchCol, SearchKeyword, SortCol, SortDir, Page, PageSize).ToList());
        }

        
        
        //[Bind(Exclude="Id")]Product productToCreate
        //public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        public ActionResult Create()
        {

            //Utility01 util = new Utility01();
            //Utility01.GetDropDownList<>

            var DeptListData = new List<SP_ADM_ADMIN_DEPT_SEL_Result>();

            DeptListData = _AdminMemberService.GetAdmDeptList();
            List<SelectListItem> ADM_DEPT = new List<SelectListItem>();

            ADM_DEPT = Utility01.GetDropDownList<SP_ADM_ADMIN_DEPT_SEL_Result>("DEPT_NAME", "DEPT_CODE", "001", DeptListData.ToList());
            ViewData["ADM_DEPT"] = ADM_DEPT;

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ADM_ID,ADM_PASS,ADM_NAME,ADM_DEPT,POINT")] ADMIN_MEMBER AdminMember, HttpPostedFileBase ADM_PHOTO)
        {

            if (ModelState.IsValid)
            {

                string AdmPhoto_path = AboutMe.Common.Helper.Config.GetConfigValue("PhotoBasePath");



                string PhotoFileName = "";

                //파일 업로드
                if (ADM_PHOTO != null)
                {
                    ADM_PHOTO.SaveAs(Server.MapPath(AdmPhoto_path) + ADM_PHOTO.FileName);
                    PhotoFileName = ADM_PHOTO.FileName;
                }


                _AdminMemberService.InsertAdminMemberTest(AdminMember.ADM_ID, AdminMember.ADM_NAME, PhotoFileName, AdminMember.ADM_PASS, AdminMember.ADM_DEPT, 100);
                return RedirectToAction("Index", new { SearchCol = "", SearchKeyword = "" });
            }
            else 
            {

                var DeptListData = new List<SP_ADM_ADMIN_DEPT_SEL_Result>();

                DeptListData = _AdminMemberService.GetAdmDeptList();
                List<SelectListItem> ADM_DEPT = new List<SelectListItem>();

                ADM_DEPT = Utility01.GetDropDownList<SP_ADM_ADMIN_DEPT_SEL_Result>("DEPT_NAME", "DEPT_CODE", "001", DeptListData.ToList());
                ViewData["ADM_DEPT"] = ADM_DEPT;

                return View(AdminMember);
            }

            
          
        }


        public ActionResult Create_Plain()
        {

            //Utility01 util = new Utility01();
            //Utility01.GetDropDownList<>

            var DeptListData = new List<SP_ADM_ADMIN_DEPT_SEL_Result>();
            
            DeptListData = _AdminMemberService.GetAdmDeptList();
            List<SelectListItem> ADM_DEPT = new List<SelectListItem>();

            ADM_DEPT = Utility01.GetDropDownList<SP_ADM_ADMIN_DEPT_SEL_Result>("DEPT_NAME", "DEPT_CODE", "001", DeptListData.ToList());
            ViewData["ADM_DEPT"] = ADM_DEPT;

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Plain(string ADM_ID, string ADM_NAME, HttpPostedFileBase ADM_PHOTO, string ADM_PASS, string ADM_DEPT, int POINT)
        {

            string AdmPhoto_path = AboutMe.Common.Helper.Config.GetConfigValue("PhotoBasePath");

        
            string PhotoFileName = "";

            //파일 업로드
            if (ADM_PHOTO != null)
            {
                ADM_PHOTO.SaveAs(Server.MapPath(AdmPhoto_path) + ADM_PHOTO.FileName );
                PhotoFileName = ADM_PHOTO.FileName; 
            }


            _AdminMemberService.InsertAdminMemberTest(ADM_ID, ADM_NAME, PhotoFileName, ADM_PASS, ADM_DEPT, POINT);

            return RedirectToAction("Index", new {SearchCol = "", SearchKeyword = "" });
            //Redirect("/AdminMember/Index");
            //return View(Index("" ,"", "","", 1, 10));
        }

        
    }
}