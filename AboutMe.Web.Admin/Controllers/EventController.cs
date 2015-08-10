using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Event;
using AboutMe.Domain.Entity.Event;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Data;
using Newtonsoft.Json;

namespace AboutMe.Web.Admin.Controllers
{
    [CustomAuthorize]
    public class EventController : BaseAdminController
    {
        private IEventService _eventservice;
        private string _eventUploadPath = AboutMe.Common.Helper.Config.GetConfigValue("EventUploadPath");

        public EventController(IEventService _eventservice)
        {
            this._eventservice = _eventservice;
        }

        public ActionResult Main()
        {
            EVENT_ADMIN_MAIN_INDEX M = new EVENT_ADMIN_MAIN_INDEX
            {
                MainInfo = _eventservice.EventAdminMainView(),
                ListInfo = _eventservice.EventAdminMainIngList()
            };
            return View(M);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult MainUpdate(EVENT_MAIN_SAVE_PARAM saveParam)
        {
            //BANNER_GBN  배너위치구분 (메인배너1~5 : M1~M5 / 우측배너1 : R1 / 중간배너1~4 : B1~B4 [BOTTOM] )
            string ADM_ID = AdminUserInfo.GetAdmId();
            string IMG = "";
            string basepath = _eventUploadPath + "/Main/";

            if (!Directory.Exists(Server.MapPath(basepath)))
            {
                Directory.CreateDirectory(Server.MapPath(basepath));
            }

            //이미지 파일 업로드
            if (saveParam.IMAGE_FILE != null)
            {
                ImageUpload imageUpload = new ImageUpload { UploadPath = basepath, addMobileImage = false };
                ImageResult imageResult = imageUpload.RenameUploadFile(saveParam.IMAGE_FILE);

                if (imageResult.Success)
                {
                    IMG = basepath + imageResult.ImageName;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(saveParam.OLD_IMAGE_FILE))
                {
                    IMG = saveParam.OLD_IMAGE_FILE;
                }
            }

            _eventservice.EventAdminMainImageUpdate(saveParam.BANNER_GBN, IMG, saveParam.URL, ADM_ID);
            
            return RedirectToAction("Main");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MainDelete(string BANNER_GBN)
        {
            //BANNER_GBN  배너위치구분 (메인배너1~5 : M1~M5 / 우측배너1 : R1 / 중간배너1~4 : B1~B4 [BOTTOM] )
            string ADM_ID = AdminUserInfo.GetAdmId();
            _eventservice.EventAdminMainImageDelete(BANNER_GBN, ADM_ID);

            return RedirectToAction("Main");
        }

        public ActionResult Index(EVENT_ADMIN_SEARCH SearchParam)
        {
            EVENT_ADMIN_INDEX M = new EVENT_ADMIN_INDEX
            {
                ListCnt = _eventservice.EventAdminCount(SearchParam)
                ,
                SearchOption = SearchParam
                ,
                ListInfo = _eventservice.EventAdminList(SearchParam)

            };
            return View(M);
        }

        public ActionResult New(EVENT_ADMIN_SEARCH SearchParam)
        {
            SP_ADMIN_EVENT_VIEW_Result eventinfo = new SP_ADMIN_EVENT_VIEW_Result();

            EVENT_ADMIN_EDIT M = new EVENT_ADMIN_EDIT
            {
                Mode = "NEW"
                ,
                SearchOption = SearchParam
                ,
                EventInfo = eventinfo

            };
            return View("Edit", M);
        }

        public ActionResult Edit(int IDX, EVENT_ADMIN_SEARCH SearchParam)
        {
            EVENT_ADMIN_EDIT M = new EVENT_ADMIN_EDIT
            {
                Mode = "EDIT"
                ,
                SearchOption = SearchParam
                ,
                EventInfo = _eventservice.EventAdminView(IDX)

            };
            return View(M);
        }

        [HttpPost, ValidateInput(false)] 
        [ValidateAntiForgeryToken]
        public ActionResult EditProcess(int IDX, EVENT_SAVE_PARAM SaveParam, string MODE)
        {
            SP_ADMIN_EVENT_VIEW_Result Param = new SP_ADMIN_EVENT_VIEW_Result();
            Param.EVENT_NAME = SaveParam.EVENT_NAME;
            Param.FROM_DATE = SaveParam.FROM_DATE;
            Param.FROM_TIME = SaveParam.FROM_TIME;
            Param.TO_DATE = SaveParam.TO_DATE;
            Param.TO_TIME = SaveParam.TO_TIME;
            Param.EVENT_GBN = "A"; //A:전체 , W(웹), M (모바일)
            Param.WEB_CONTENTS = SaveParam.WEB_CONTENTS;
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
            int EventIdx = 0;
            if (MODE == "NEW")
            {
                NewIdx = _eventservice.EventAdminInsert(Param);
                EventIdx = NewIdx;
            }
            else
            {
                EventIdx = IDX;
            }

            string basepath = _eventUploadPath + Convert.ToString(EventIdx) + "/";

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
                    Param.MOBILE_FILE = SaveParam.OLD_MOBILE_FILE;
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
                    Param.MOBILE_BANNER = SaveParam.OLD_MOBILE_BANNER;
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
                    Param.WEB_BANNER = SaveParam.OLD_WEB_BANNER;
                }
            }

            _eventservice.EventAdminUpdate(EventIdx, Param);

            return RedirectToAction("");
        }

        //메인 전시순서 일괄 변경
        [HttpPost]
        public ActionResult MainOrderUpdate(string data)
        {
            List<EVENT_MAIN_ORDER_PARAM> DataList = JsonConvert.DeserializeObject<List<EVENT_MAIN_ORDER_PARAM>>(data);

            foreach (EVENT_MAIN_ORDER_PARAM item in DataList)
            {
                _eventservice.EventAdminMainListOrderUpdate(item.GBN, item.IDX, item.ORDER);
            }

            var jsonData = new { result = "true" };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

    }
}