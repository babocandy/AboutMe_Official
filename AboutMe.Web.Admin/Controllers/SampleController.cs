using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Web.UI;

using AboutMe.Common.Helper;
using AboutMe.Domain.Service.Sample;
using AboutMe.Domain.Entity.Sample;

using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Data;
using Newtonsoft.Json;

namespace AboutMe.Web.Admin.Controllers
{
    [CustomAuthorize]
    public class SampleController : BaseAdminController
    {
        private ISampleService _sampleservice;
        private string _eventUploadPath = AboutMe.Common.Helper.Config.GetConfigValue("SampleUploadPath");

        public SampleController(ISampleService _sampleservice)
        {
            this._sampleservice = _sampleservice;
        }
        
        public ActionResult Index(SAMPLE_ADMIN_SEARCH SearchParam)
        {
            SAMPLE_ADMIN_INDEX M = new SAMPLE_ADMIN_INDEX
            {
                ListCnt = _sampleservice.SampleAdminCount(SearchParam)
                ,
                SearchOption = SearchParam
                ,
                ListInfo = _sampleservice.SampleAdminList(SearchParam)

            };
            return View(M);
        }

        public ActionResult New(SAMPLE_ADMIN_SEARCH SearchParam)
        {
            SP_ADMIN_SAMPLE_VIEW_Result eventinfo = new SP_ADMIN_SAMPLE_VIEW_Result();

            SAMPLE_ADMIN_EDIT M = new SAMPLE_ADMIN_EDIT
            {
                Mode = "NEW"
                ,
                SearchOption = SearchParam
                ,
                SampleInfo = eventinfo

            };
            return View("Edit", M);
        }

        public ActionResult Edit(int IDX, SAMPLE_ADMIN_SEARCH SearchParam)
        {
            SAMPLE_ADMIN_EDIT M = new SAMPLE_ADMIN_EDIT
            {
                Mode = "EDIT"
                ,
                SearchOption = SearchParam
                ,
                SampleInfo = _sampleservice.SampleAdminView(IDX)

            };
            return View(M);
        }
        
        [HttpPost, ValidateInput(false)] 
        [ValidateAntiForgeryToken]
        public ActionResult EditProcess(int IDX, SAMPLE_SAVE_PARAM SaveParam, string MODE)
        {
            SP_ADMIN_SAMPLE_VIEW_Result Param = new SP_ADMIN_SAMPLE_VIEW_Result();
            Param.SAMPLE_NAME = SaveParam.SAMPLE_NAME;
            Param.FROM_DATE = SaveParam.FROM_DATE;
            Param.FROM_TIME = SaveParam.FROM_TIME;
            Param.TO_DATE = SaveParam.TO_DATE;
            Param.TO_TIME = SaveParam.TO_TIME;
            Param.SAMPLE_GBN = "A"; //A:전체 , W(웹), M (모바일)
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
            int SampleIdx = 0;
            if (MODE == "NEW")
            {
                NewIdx = _sampleservice.SampleAdminInsert(Param);
                SampleIdx = NewIdx;
            }
            else
            {
                SampleIdx = IDX;
            }

            string basepath = _eventUploadPath + Convert.ToString(SampleIdx) + "/";

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
                ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = basepath };
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
                ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = basepath };
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
                ImagePlainUpload imageUpload = new ImagePlainUpload { UploadPath = basepath };
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

            _sampleservice.SampleAdminUpdate(SampleIdx, Param);

            return RedirectToAction("");
        }

        public ActionResult MemberList(int IDX, int Page=1, int PageSize=10)
        {
            if (Page == 0)
            {
                Page = 1;
            }
            if (PageSize == 0)
            {
                PageSize = 10;
            }
            SAMPLE_MEMBER_INDEX M = new SAMPLE_MEMBER_INDEX
            {
                IDX = IDX
                ,
                ListCnt = _sampleservice.SampleAdminMemberCount(IDX)
                ,
                Page = Page
                , 
                PageSize = PageSize
                ,
                SampleInfo = _sampleservice.SampleAdminView(IDX)
                ,
                ListInfo = _sampleservice.SampleAdminMemberList(IDX, Page, PageSize)

            };
            return View(M);
        }

        public ActionResult ExcelDownload(int IDX)
        {

            var products = new System.Data.DataTable("test");
            //헤더 구성
            products.Columns.Add("No", typeof(string));
            products.Columns.Add("ID", typeof(string));
            products.Columns.Add("성명", typeof(string));
            products.Columns.Add("등급", typeof(string));
            products.Columns.Add("이메일", typeof(string));
            products.Columns.Add("핸드폰", typeof(string));
            products.Columns.Add("주소", typeof(string));

            int nDOWN_ROW_CNT = 0;
            var Data = _sampleservice.SampleAdminMemberList(IDX, 1, 10000000).ToList(); //데이타 가져오기
            if (Data != null && Data.Any())
            {

                foreach (var result in Data)
                {
                    string grade = "";
                    if (result.M_GRADE == "B")
                    {
                        grade = "브론즈";
                    }
                    else if (result.M_GRADE == "G")
                    {
                        grade = "골드";
                    }
                    else if (result.M_GRADE == "S")
                    {
                        grade = "실버";
                    }
                    else if (result.M_GRADE == "V")
                    {
                        grade = "VIP";
                    }

                    products.Rows.Add(result.IDX, result.M_ID, result.M_NAME, grade, result.M_EMAIL, result.M_MOBILE, result.M_ADDR);

                    nDOWN_ROW_CNT++;
                } //for
            } //if

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=FrontMember.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "euc-kr";  //UTF-8 ,euc-kr
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("euc-kr");

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            return Content(sw.ToString(), "application/ms-excel");

        }
    }

}