using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Magazine;
using AboutMe.Domain.Entity.Magazine;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Data;
using Newtonsoft.Json;

namespace AboutMe.Web.Admin.Controllers
{
    [CustomAuthorize]
    public class MagazineController : BaseAdminController
    {
        private IMagazineService _magazineservice;
        private string _magazineUploadPath = AboutMe.Common.Helper.Config.GetConfigValue("MagazineUploadPath");

        public MagazineController(IMagazineService _magazineservice)
        {
            this._magazineservice = _magazineservice;
        }

        public ActionResult Index(MAGAZINE_ADMIN_SEARCH SearchParam)
        {
            MAGAZINE_ADMIN_INDEX M = new MAGAZINE_ADMIN_INDEX
            {
                ListCnt = _magazineservice.MagazineAdminCount(SearchParam)
                ,
                SearchOption = SearchParam
                ,
                ListInfo = _magazineservice.MagazineAdminList(SearchParam)

            };
            return View(M);
        }

        public ActionResult New(MAGAZINE_ADMIN_SEARCH SearchParam)
        {
            SP_ADMIN_MAGAZINE_VIEW_Result info = new SP_ADMIN_MAGAZINE_VIEW_Result();

            MAGAZINE_ADMIN_EDIT M = new MAGAZINE_ADMIN_EDIT
            {
                Mode = "NEW"
                ,
                SearchOption = SearchParam
                ,
                ViewInfo = info

            };
            return View("Edit", M);
        }

        public ActionResult Edit(int IDX, MAGAZINE_ADMIN_SEARCH SearchParam)
        {
            MAGAZINE_ADMIN_EDIT M = new MAGAZINE_ADMIN_EDIT
            {
                Mode = "EDIT"
                ,
                SearchOption = SearchParam
                ,
                ViewInfo = _magazineservice.MagazineAdminView(IDX)

            };
            return View(M);
        }

        [HttpPost, ValidateInput(false)] 
        [ValidateAntiForgeryToken]
        public ActionResult EditProcess(int IDX, MAGAZINE_SAVE_PARAM SaveParam, string MODE)
        {
            string ADM_ID = AdminUserInfo.GetAdmId();
            SP_ADMIN_MAGAZINE_VIEW_Result Param = new SP_ADMIN_MAGAZINE_VIEW_Result();
            Param.TITLE = SaveParam.TITLE;
            Param.SUB_TITLE = SaveParam.SUB_TITLE;
            Param.CONTENT_GBN = SaveParam.CONTENT_GBN; // ( I:이미지  M:동영상 )
            Param.USE_YN = SaveParam.USE_YN;
            Param.MOVIE_URL = SaveParam.MOVIE_URL;
            Param.USE_YN = SaveParam.USE_YN;
            Param.DISPLAY_ORDER = 0;
            Param.IMG_PATH = "";
            Param.THUMB_IMG_PATH = "";

            string IMG_PATH_Name = "";
            string IMG_PATH_FullName = "";

            int NewIdx = 0;
            int MagazineIdx = 0;
            if (MODE == "NEW")
            {
                NewIdx = _magazineservice.MagazineAdminInsert(Param, ADM_ID);
                MagazineIdx = NewIdx;
            }
            else
            {
                MagazineIdx = IDX;
            }

            string basepath = _magazineUploadPath + Convert.ToString(MagazineIdx) + "/";

            if (!Directory.Exists(Server.MapPath(_magazineUploadPath)))
            {
                Directory.CreateDirectory(Server.MapPath(_magazineUploadPath));
            }

            if (!Directory.Exists(Server.MapPath(basepath)))
            {
                Directory.CreateDirectory(Server.MapPath(basepath));
            }

            //썸네일 이미지 파일 업로드
            if (SaveParam.THUMB_IMG_FILE != null)
            {
                ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = basepath, Tag = "T" };
                ImageResult imageResult = imageUpload.RenameUploadFile(SaveParam.THUMB_IMG_FILE);

                if (imageResult.Success)
                {
                    IMG_PATH_Name = imageResult.ImageName;
                    IMG_PATH_FullName = basepath + IMG_PATH_Name;
                    Param.THUMB_IMG_PATH = IMG_PATH_FullName;
                }
            }
            else
            {
                if (SaveParam.THUMB_IMG_PATH_DEL == "Y")
                {
                    Param.THUMB_IMG_PATH = "";
                }
                else
                {
                    Param.THUMB_IMG_PATH = SaveParam.OLD_THUMB_IMG_PATH;
                }
            }

            //이미지 파일 업로드
            if (SaveParam.IMG_FILE != null)
            {
                IMG_PATH_Name = "";
                IMG_PATH_FullName = "";

                ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = basepath, Tag = "A" };
                ImageResult imageResult = imageUpload.RenameUploadFile(SaveParam.IMG_FILE);

                if (imageResult.Success)
                {
                    IMG_PATH_Name = imageResult.ImageName;
                    IMG_PATH_FullName = basepath + IMG_PATH_Name;
                    Param.IMG_PATH = IMG_PATH_FullName;
                }
            }
            else
            {
                if (SaveParam.IMG_PATH_DEL == "Y")
                {
                    Param.IMG_PATH = "";
                }
                else
                {
                    Param.IMG_PATH = SaveParam.OLD_IMG_PATH;
                }
            }

            _magazineservice.MagazineAdminUpdate(MagazineIdx, Param, ADM_ID);

            return RedirectToAction("");
        }

    }
}