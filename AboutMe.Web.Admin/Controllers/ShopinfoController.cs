using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Shopinfo;
using AboutMe.Domain.Entity.Shopinfo;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Data;
using Newtonsoft.Json;

namespace AboutMe.Web.Admin.Controllers
{
    [CustomAuthorize]
    public class ShopinfoController : BaseAdminController
    {
        private IShopinfoService _shopinfoservice;

        public ShopinfoController(IShopinfoService _shopinfoservice)
        {
            this._shopinfoservice = _shopinfoservice;
        }

        public ActionResult Index(SHOPINFO_ADMIN_SEARCH SearchParam)
        {
            SHOPINFO_ADMIN_INDEX M = new SHOPINFO_ADMIN_INDEX
            {
                ListCnt = _shopinfoservice.ShopinfoAdminCount(SearchParam)
                ,
                SearchOption = SearchParam
                ,
                ListInfo = _shopinfoservice.ShopinfoAdminList(SearchParam)

            };
            return View(M);
        }

        public ActionResult New(SHOPINFO_ADMIN_SEARCH SearchParam)
        {
            SP_ADMIN_SHOPINFO_VIEW_Result info = new SP_ADMIN_SHOPINFO_VIEW_Result();

            SHOPINFO_ADMIN_EDIT M = new SHOPINFO_ADMIN_EDIT
            {
                Mode = "NEW"
                ,
                SearchOption = SearchParam
                ,
                ViewInfo = info

            };
            return View("Edit", M);
        }

        public ActionResult Edit(int IDX, SHOPINFO_ADMIN_SEARCH SearchParam)
        {
            SHOPINFO_ADMIN_EDIT M = new SHOPINFO_ADMIN_EDIT
            {
                Mode = "EDIT"
                ,
                SearchOption = SearchParam
                ,
                ViewInfo = _shopinfoservice.ShopinfoAdminView(IDX)

            };
            return View(M);
        }

        [HttpPost, ValidateInput(false)] 
        [ValidateAntiForgeryToken]
        public ActionResult EditProcess(int IDX, SHOPINFO_SAVE_PARAM SaveParam, string MODE)
        {
            string ADM_ID = AdminUserInfo.GetAdmId();
            SP_ADMIN_SHOPINFO_VIEW_Result Param = new SP_ADMIN_SHOPINFO_VIEW_Result();
            Param.SHOP_NAME = SaveParam.SHOP_NAME;
            Param.TEL_NUM = SaveParam.TEL_NUM;
            Param.POST = SaveParam.POST;
            Param.ADDR1 = SaveParam.ADDR1;
            Param.ADDR2 = SaveParam.ADDR2;
            Param.LOCATION_INFO = SaveParam.LOCATION_INFO;
            Param.USE_YN = SaveParam.USE_YN;
            Param.DISPLAY_ORDER = 0;
            Param.LATITUDE = SaveParam.LATITUDE;
            Param.LONGITUDE = SaveParam.LONGITUDE;

            if (MODE == "NEW")
            {
                _shopinfoservice.ShopinfoAdminInsert(Param, ADM_ID);
            }
            else
            {
                _shopinfoservice.ShopinfoAdminUpdate(IDX, Param, ADM_ID);
            }

            return RedirectToAction("");
        }

    }
}