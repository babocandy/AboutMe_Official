using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Exhibit;
using AboutMe.Domain.Entity.Exhibit;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Data;
using Newtonsoft.Json;

namespace AboutMe.Web.Admin.Controllers
{
    [CustomAuthorize]
    public class ExhibitController : BaseAdminController
    {
        private IExhibitService _exhibitservice;
        private string _eventUploadPath = AboutMe.Common.Helper.Config.GetConfigValue("ExhibitUploadPath");

        public ExhibitController(IExhibitService _exhibitservice)
        {
            this._exhibitservice = _exhibitservice;
        }
        
        public ActionResult Index(EXHIBIT_ADMIN_SEARCH SearchParam)
        {
            EXHIBIT_ADMIN_INDEX M = new EXHIBIT_ADMIN_INDEX
            {
                ListCnt = _exhibitservice.ExhibitAdminCount(SearchParam)
                ,
                SearchOption = SearchParam
                ,
                ListInfo = _exhibitservice.ExhibitAdminList(SearchParam)

            };
            return View(M);
        }

        public ActionResult New(EXHIBIT_ADMIN_SEARCH SearchParam)
        {
            SP_ADMIN_EXHIBIT_VIEW_Result eventinfo = new SP_ADMIN_EXHIBIT_VIEW_Result();

            EXHIBIT_ADMIN_EDIT M = new EXHIBIT_ADMIN_EDIT
            {
                Mode = "NEW"
                ,
                SearchOption = SearchParam
                ,
                ExhibitInfo = eventinfo

            };
            return View("Edit", M);
        }

        public ActionResult Edit(int IDX, EXHIBIT_ADMIN_SEARCH SearchParam)
        {
            EXHIBIT_ADMIN_EDIT M = new EXHIBIT_ADMIN_EDIT
            {
                Mode = "EDIT"
                ,
                SearchOption = SearchParam
                ,
                ExhibitInfo = _exhibitservice.ExhibitAdminView(IDX)

            };
            return View(M);
        }

        public ActionResult ProductEdit(int IDX, EXHIBIT_ADMIN_SEARCH SearchParam)
        {
            EXHIBIT_PRODUCT_EDIT M = new EXHIBIT_PRODUCT_EDIT
            {
                IDX = IDX
                ,
                SearchOption = SearchParam
                ,
                ExhibitInfo = _exhibitservice.ExhibitAdminView(IDX)
                ,
                TabList = _exhibitservice.ExhibitAdminTabList(IDX)
            };
            return View(M);
        }

        [HttpPost, ValidateInput(false)] 
        [ValidateAntiForgeryToken]
        public ActionResult EditProcess(int IDX, EXHIBIT_SAVE_PARAM SaveParam, string MODE)
        {
            SP_ADMIN_EXHIBIT_VIEW_Result Param = new SP_ADMIN_EXHIBIT_VIEW_Result();
            Param.EXHIBIT_NAME = SaveParam.EXHIBIT_NAME;
            Param.FROM_DATE = SaveParam.FROM_DATE;
            Param.FROM_TIME = SaveParam.FROM_TIME;
            Param.TO_DATE = SaveParam.TO_DATE;
            Param.TO_TIME = SaveParam.TO_TIME;
            Param.EXHIBIT_GBN = "A"; //A:전체 , W(웹), M (모바일)
            Param.WEB_CONTENTS = SaveParam.WEB_CONTENTS;
            Param.WEB_URL = SaveParam.WEB_URL;
            Param.MOBILE_URL = SaveParam.MOBILE_URL;
            Param.USE_YN = SaveParam.USE_YN;
            Param.ADM_ID  = AdminUserInfo.GetAdmId();
            Param.MOBILE_FILE = "";
            Param.WEB_BANNER = "";
            Param.MOBILE_BANNER = "";

            string MOBILE_FILE_Name = "";
            string WEB_BANNER_Name = "";
            string MOBILE_BANNER_Name = "";

            string MOBILE_FILE_FullName = "";
            string WEB_BANNER_FullName = "";
            string MOBILE_BANNER_FullName = "";

            int NewIdx = 0;
            int ExhibitIdx = 0;
            if (MODE == "NEW")
            {
                NewIdx = _exhibitservice.ExhibitAdminInsert(Param);
                ExhibitIdx = NewIdx;
            }
            else
            {
                ExhibitIdx = IDX;
            }

            string basepath = _eventUploadPath + Convert.ToString(ExhibitIdx) + "/";

            if (!Directory.Exists(Server.MapPath(_eventUploadPath)))
            {
                Directory.CreateDirectory(Server.MapPath(_eventUploadPath));
            }

            if (!Directory.Exists(Server.MapPath(basepath)))
            {
                Directory.CreateDirectory(Server.MapPath(basepath));
            }

            //모바일 이미지 파일 업로드
            if (SaveParam.MOBILE_FILE != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = basepath, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(SaveParam.MOBILE_FILE);

                if (imageResult.Success)
                {
                    MOBILE_FILE_Name = imageResult.ImageName;
                    MOBILE_FILE_FullName = basepath + MOBILE_FILE_Name;
                    Param.MOBILE_FILE = MOBILE_FILE_FullName;
                }
            }
            else
            {
                if (SaveParam.MOBILE_FILE_DEL == "Y")
                {
                    Param.MOBILE_FILE = "";
                }
                else
                {
                    Param.MOBILE_FILE = (string.IsNullOrEmpty(SaveParam.OLD_MOBILE_FILE) ? "" : SaveParam.OLD_MOBILE_FILE );
                }
            }

            //모바일배너 파일 업로드
            if (SaveParam.MOBILE_BANNER != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = basepath, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(SaveParam.MOBILE_BANNER);

                if (imageResult.Success)
                {
                    MOBILE_BANNER_Name = imageResult.ImageName;
                    MOBILE_BANNER_FullName = basepath + MOBILE_BANNER_Name;
                    Param.MOBILE_BANNER = MOBILE_BANNER_FullName;
                }
            }
            else
            {
                if (SaveParam.MOBILE_BANNER_DEL == "Y")
                {
                    Param.MOBILE_BANNER = "";
                }
                else
                {
                    Param.MOBILE_BANNER = (string.IsNullOrEmpty(SaveParam.OLD_MOBILE_BANNER) ? "" : SaveParam.OLD_MOBILE_BANNER); 
                }
            }

            //웹배너 업로드
            if (SaveParam.WEB_BANNER != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = basepath, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(SaveParam.WEB_BANNER);

                if (imageResult.Success)
                {
                    WEB_BANNER_Name = imageResult.ImageName;
                    WEB_BANNER_FullName = basepath + WEB_BANNER_Name;
                    Param.WEB_BANNER = WEB_BANNER_FullName;
                }
            }
            else
            {
                if (SaveParam.WEB_BANNER_DEL == "Y")
                {
                    Param.WEB_BANNER = "";
                }
                else
                {
                    Param.WEB_BANNER = (string.IsNullOrEmpty(SaveParam.OLD_WEB_BANNER) ? "" : SaveParam.OLD_WEB_BANNER); 
                }
            }

            _exhibitservice.ExhibitAdminUpdate(ExhibitIdx, Param);

            return RedirectToAction("");
        }

        //탭(분류) 삭제
        [HttpPost]
        public ActionResult ChkTabDelete(int IDX, string data)
        {
            List<int> DataList = JsonConvert.DeserializeObject<List<int>>(data);
            string REG_ID = AdminUserInfo.GetAdmId();
            foreach (int tabidx in DataList)
            {
                
            }

            var jsonData = new { result = "true" };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //탭(분류) 추가
        [HttpPost]
        public ActionResult ChkTabInsert(int IDX, string TAB_NAME, int DISPLAY_ORDER)
        {
            string ADM_ID = AdminUserInfo.GetAdmId();
            _exhibitservice.ExhibitAdminTabInsert(IDX, TAB_NAME, DISPLAY_ORDER, ADM_ID);
            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        
        //탭(분류) 수정
        [HttpPost]
        public ActionResult ChkTabUpdate(int IDX, string TAB_NAME, int DISPLAY_ORDER)
        {
            string ADM_ID = AdminUserInfo.GetAdmId();
            _exhibitservice.ExhibitAdminTabUpdate(IDX, TAB_NAME, DISPLAY_ORDER, ADM_ID);
            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    
        //탭(분류) 전시순서 일괄 변경
        [HttpPost]
        public ActionResult MultiTabOrderUpdate(string data)
        {
            List<TAB_ORDER_UPDATE> DataList = JsonConvert.DeserializeObject<List<TAB_ORDER_UPDATE>>(data);
            string ADM_ID = AdminUserInfo.GetAdmId();
            foreach (TAB_ORDER_UPDATE item in DataList)
            {
                _exhibitservice.ExhibitAdminTabOrderUpdate(item.TAB_IDX, item.DISPLAY_ORDER, ADM_ID);
            }

            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        //탭의 전시상품 목록
        public ActionResult TabProductList(int TAB_IDX)
        {
            List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_LIST_Result> lst = new List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_LIST_Result>();
            lst = _exhibitservice.ExhibitAdminTabProductList(TAB_IDX);
            return PartialView(lst);
        }

        //전시상품 검색
        public ActionResult ProductSearch(string SEARCH_COL, string SEARCH_KEYWORD )
        {
            List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_SEARCH_Result> lst = new List<SP_ADMIN_EXHIBIT_TAB_PRODUCT_SEARCH_Result>(); 
            lst = _exhibitservice.ExhibitAdminTabProductSearch(SEARCH_COL, SEARCH_KEYWORD);
            return PartialView(lst);
        }

    }

}