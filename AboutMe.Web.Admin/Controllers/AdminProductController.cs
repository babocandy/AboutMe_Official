using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.AdminProduct;
using AboutMe.Domain.Entity.AdminProduct;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminProductController : BaseAdminController
    {

        // GET: AdminProduct
        private IAdminProductService _AdminProductService;
        public AdminProductController(IAdminProductService _adminProductService)
        {
            this._AdminProductService = _adminProductService;
        }

        public ActionResult Index()
        {
            return View(_AdminProductService.GetAdminCategoryOneList().ToList());
        }

        // GET: AdminProduct/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminProduct/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string DEPTH1_NAME)
        {

            //System.Data.Entity.Core.Objects.ObjectParameter intResult;
            //int intResult = 0;
            try
            {
                // TODO: Add insert logic here

                //;
                int i = _AdminProductService.InsertAdminCategoryOne(DEPTH1_NAME);

                //return RedirectToAction("Index");
                //Redirect("/AdminMember/Index");
                //return View(Index("" ,"", "","", 1, 10));
                ViewBag.resultVal = i;
                return RedirectToAction("Index", new { SearchCol = ViewBag.resultVal});
            }
           catch
            {
                return View();
            }
        }

        // GET: AdminProduct/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminProduct/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminProduct/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminProduct/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
