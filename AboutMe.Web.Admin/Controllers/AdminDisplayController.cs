using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;
using AboutMe.Web.Admin.Models;
using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Domain.Entity.AdminDisplay;
using AboutMe.Domain.Service.AdminDisplay;
using AboutMe.Common.Helper;
using AboutMe.Common.Data;


namespace AboutMe.Web.Admin.Controllers
{
    public class AdminDisplayController : BaseAdminController
    {
        private IAdminDisplayService _Service;
        public AdminDisplayController(IAdminDisplayService service)
        {
            this._Service = service;
        }

        [CustomAuthorize]
        public ActionResult Index()
        {
            AdminDisplayWebMainViewModel model = new AdminDisplayWebMainViewModel();

            model.WebMainBannerList =  _Service.GetWebMainBanner();

            return View(model);
        }

        [HttpPost]        
        public ActionResult SaveWebMainBanner(WebMainBannerParam param)
        {

            string imgName = param.IMG_NAME;

            if (param.IMG_FILE != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = _img_path_display, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(param.IMG_FILE);

                if (imageResult.Success)
                {
                    imgName = imageResult.ImageName;
                }
            }

            if(  !String.IsNullOrEmpty( param.URL ) || !String.IsNullOrEmpty(imgName) )
            {
                _Service.SaveWebMainBanner(param.IDX, param.URL, imgName);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveWebMainBanner(WebMainBannerParam param)
        {
            if (param.IDX != null )
            {
                _Service.RemoveWebMainBanner(param.IDX);
            }

            return RedirectToAction("Index");
        }

        public ActionResult MobileMain()
        {
            return View();
        }

        public ActionResult CartBanner()
        {
            return View();
        }

        public ActionResult GBNBanner()
        {
            return View();
        }

        public ActionResult ProductDetailBanner()
        {
            return View();
        }

        public ActionResult Popup()
        {
            return View();
        }
    }
}