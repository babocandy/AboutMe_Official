﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Net;

using System.Reflection;
using System.Web.Routing;

using System.Diagnostics;

using AboutMe.Web.Admin.Models;
using AboutMe.Web.Admin.Common.Filters;
using AboutMe.Web.Admin.Common;
using AboutMe.Common.Helper;
using AboutMe.Common.Data;

using AboutMe.Domain.Service.AdminReview;
using AboutMe.Domain.Entity.AdminReview;

//using AboutMe.Domain.Entity.AdminProduct;
//using AboutMe.Domain.Service.AdminProduct;

namespace AboutMe.Web.Admin.Controllers
{
    public class AdminReviewExpController : BaseAdminController
    {

        private IAdminReviewService _service;
       // private IAdminProductService _service_p;

        public AdminReviewExpController(IAdminReviewService s )
        {
            this._service = s;
            //this._service_p = p;
        }

        // GET: AdminReviewExp
        [CustomAuthorize]
        public ActionResult Index(AdminReviewExpMasterListRouteParam p)
        {
            AdminReviewExpMasterListViewModel model = new AdminReviewExpMasterListViewModel();

            var tp = _service.ReviewExpMasterList(p);

            model.RouteParam = p;
            model.List = tp.Item1;//ReviewHelper.ExpMasterForAmdinUser(tp.Item1);
            model.Total = tp.Item2;

            return View(model);
        }


        [CustomAuthorize]
        public ActionResult Create(AdminReviewRouteParam p)
        {
            AdminReviewExpCreateViewModel model = new AdminReviewExpCreateViewModel();
            TempData["RouteData"] = p;
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminReviewExpCreateViewModel model)
        {

            AdminReviewRouteParam p = TempData["RouteData"] as AdminReviewRouteParam;
            TempData["RouteData"] = p;
            /**
             * 마스터 아이디 생성
             */
            if (ModelState.IsValid)
            {
                AdminReviewExpMasterParamToInputDB m = new AdminReviewExpMasterParamToInputDB();
                m.IS_AUTH = model.IS_AUTH;
                m.P_CODE = model.P_CODE;
                m.FROM_DATE = model.FROM_DATE;
                m.TO_DATE = model.TO_DATE;

                var midx = _service.ReviewExpMasterInsert(m);

                Debug.WriteLine("midx : " + midx);

                if (midx != null)
                {
                    HttpPostedFileBase f = Request.Files["EXCEL_FILE"];

                    if (f.ContentLength > 0)
                    {
                        //string now_dt = DateTime.Now.ToString("yyyyMMddHHmms");
                        string uploadPath = "~/Upload/ReviewExpExcel/";

                        if (!Directory.Exists(Server.MapPath(uploadPath)))
                        {
                            Directory.CreateDirectory(Server.MapPath(uploadPath));
                        }

                        string fileExtension = Path.GetExtension(f.FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            string fileLocation = Server.MapPath(uploadPath) + f.FileName;
                            if (System.IO.File.Exists(fileLocation))
                            {
                                System.IO.File.Delete(fileLocation);
                            }


                            f.SaveAs(fileLocation);

                            string excelConnectionString = string.Empty;

                            //connection String for xls file format.

                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                              fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                            /*
                            if (fileExtension == ".xls")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            //connection String for xlsx file format.
                            else if (fileExtension == ".xlsx")
                            {
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                             * */

                            DataSet ds = new DataSet();

                            using (OleDbConnection conn = new OleDbConnection(excelConnectionString))
                            {
                                conn.Open();
                                using (DataTable dtExcelSchema = conn.GetSchema("Tables"))
                                {
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    string query = "SELECT * FROM [" + sheetName + "]";
                                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                                    //DataSet ds = new DataSet();
                                    adapter.Fill(ds, "Items");
                                    if (ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {
                                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                            {
                                                //Now we can insert this data to database...

                                                Debug.WriteLine(ds.Tables[0].Rows[i][0].ToString() + "/" + ds.Tables[0].Rows[i][1].ToString());
                                                AdminReviewExpMemberParamToInputDB b = new AdminReviewExpMemberParamToInputDB();
                                                b.MASTER_IDX = midx;
                                                b.M_ID = ds.Tables[0].Rows[i][0].ToString();
                                                b.M_NAME = ds.Tables[0].Rows[i][1].ToString();
                                                _service.ReviewExpMemberInsert(b);
                                            }
                                        }
                                    }
                                }
                            }
                        } //end if

                    }

                    return RedirectToAction("Index", "AdminReviewExp", p);
                }

                TempData["ResultNum"] = "1";
                TempData["ResultMessage"] = "마스터키가 생성되지 않았습니다.";
                
                return View(model);

            }

            ModelState.AddModelError("", "필수항목들을 입력해주세요");

            return View(model);
        }

        [CustomAuthorize]
        public ActionResult Detail(int? id)
        {
            AdminReviewExpMasterDetailViewModel model = new AdminReviewExpMasterDetailViewModel();

            if (id != null)
            {
                model.IDX = id;
                model.Detail = _service.ReviewExpMasterDetail(id);
                model.Members = _service.ReviewExpMemberList(id);
            }
           
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Update()
        {
            int? idx = Convert.ToInt32( Request.Form["IDX"] );
            var isauth = Request.Form["IS_AUTH"];

            if (idx !=null && isauth != null)
            {
                _service.ReviewExpMasterUpdate(idx, isauth);
                TempData["ResultNum"] = "0";
            }

            return Redirect( Request.UrlReferrer.ToString() );
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult AddIds()
        {
            int? masterIdx = Convert.ToInt32(Request.Form["IDX"]);
            string ids = Request.Form["IDS"];

            if (masterIdx != null && ids != null)
            {
                string[] arr = ids.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    string memberId = arr[i].Trim();

                    AdminReviewExpMemberParamToInputDB p = new AdminReviewExpMemberParamToInputDB();
                    p.MASTER_IDX = masterIdx;
                    p.M_ID = memberId;
                    _service.ReviewExpMemberInsert(p);
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult DelIds()
        {
            int? masterIdx = Convert.ToInt32(Request.Form["IDX"]);
            string ids = Request.Form["IDS"];
            Debug.WriteLine("ids: " + ids);
            if ( masterIdx != null && ids != null)
            {
                string[] arr = ids.Split(',');
                Debug.WriteLine("arr: " + arr);
                for (int i = 0; i < arr.Length; i++)
                {
                    string memberId =  arr[i].Trim();

                    Debug.WriteLine(memberId);


                    _service.ReviewExpMemberDelete(memberId, masterIdx);
                }
            }

            return Redirect(Request.UrlReferrer.ToString());
        }


        [CustomAuthorize]
        public ActionResult SelProduct(AdminReviewExpFindProductRouteParam p)
        {
            AdminReviewExpFindProductViewModel model = new AdminReviewExpFindProductViewModel();

            var tp = _service.ReviewExpFindProductList(p);

            model.List = tp.Item1;
            model.Total = tp.Item2;
            model.RouteParam = p;

            return View(model);
        }
    }
}