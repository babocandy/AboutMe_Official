/**
 * 
 *  Admin Revew. Experience
 *  writer      : dykim 
 *  create date : 2015.08.15
 * 
 */
using System;
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
    [RoutePrefix("AdminContents/ReviewExp")]
    [Route("{action=Index}")]
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
            model.List = tp.Item1;
            model.Total = tp.Item2;

            return View(model);
        }


        [CustomAuthorize]
        public ActionResult Create(AdminReviewExpMasterListRouteParam p)
        {
            AdminReviewExpMasterInputViewModel model = new AdminReviewExpMasterInputViewModel();
            model.RouteParam = p;
            
            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminReviewExpMasterInputViewModel model, AdminReviewExpMasterListRouteParam p)
        {
            model.RouteParam = p;

            /**
             * 마스터 아이디 생성
             */
            if (ModelState.IsValid)
            {
                var midx = _service.ReviewExpMasterInsert(model);


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


                            /** */
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
        [Route("Detail/{id:int}")]
        public ActionResult Detail(int? id, AdminReviewExpMasterListRouteParam p)
        {
            AdminReviewExpMasterDetailViewModel model = new AdminReviewExpMasterDetailViewModel();
            model.RouteParam = p;

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
        public ActionResult Update(AdminReviewExpMasterListRouteParam p)
        {
            int? idx = Convert.ToInt32( Request.Form["IDX"] );
            var isauth = Request.Form["IS_AUTH"];

            if (idx !=null && isauth != null)
            {
                _service.ReviewExpMasterUpdate(idx, isauth);
                TempData["ResultNum"] = "0";
            }

            return RedirectToAction("Index", "AdminReviewExp", p);
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult AddIds()
        {
            int? masterIdx = Convert.ToInt32(Request.Form["IDX"]);
            string ids = Request.Form["IDS"];

            var msg = "";
            if (masterIdx != null && ids != null)
            {
                string[] arr = ids.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    string memberId = arr[i].Trim();

                    AdminReviewExpMemberParamToInputDB p = new AdminReviewExpMemberParamToInputDB();
                    p.MASTER_IDX = masterIdx;
                    p.M_ID = memberId;
                    
                    var tp = _service.ReviewExpMemberInsert(p);

                    if (tp.Item1 != "0")
                    {
                        msg += tp.Item2 + "<br />";
                    }
                }
            }

            TempData["msg"] = msg;

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult DelIds()
        {
            int? masterIdx = Convert.ToInt32(Request.Form["IDX"]);
            string ids = Request.Form["IDS"];
           // Debug.WriteLine("ids: " + ids);

            var msg = "";
            if ( masterIdx != null && ids != null)
            {
                string[] arr = ids.Split(',');
                Debug.WriteLine("arr: " + arr);
                for (int i = 0; i < arr.Length; i++)
                {
                    string memberId =  arr[i].Trim();

                    Debug.WriteLine(memberId);


                    var tp = _service.ReviewExpMemberDelete(memberId, masterIdx);

                    if (tp.Item1 != "0")
                    {
                        msg += tp.Item2 + "<br />";
                    }
                }
            }

            TempData["msg"] = msg;

            return Redirect(Request.UrlReferrer.ToString());
        }

        /**
         * 체험단 리뷰를 위한 상품선택
         */
        [CustomAuthorize]
        public ActionResult FindProduct(AdminReviewExpFindProductRouteParam p)
        {
            AdminReviewExpFindProductViewModel model = new AdminReviewExpFindProductViewModel();

            var tp = _service.ReviewExpFindProductList(p);

            model.List = tp.Item1;
            model.Total = tp.Item2;
            model.RouteParam = p;

            return View(model);
        }


        /**
         * 체험단 관련 리뷰글들 조회
         */
        [CustomAuthorize]
        public ActionResult ArticleList(AdminReviewExpArticleRouteParam p)
        {
            AdminReviewExpArticleListViewModel model = new AdminReviewExpArticleListViewModel();

            if (p.EXP_MASTER_IDX == null)
            {
                TempData["ResultNum"] = "1";
                TempData["ResultMessage"] = "조회시 체험단 고유아이디가 필요합니다.";
                return   RedirectToAction("Index", "AdminReviewExp", p);
            }

            var tp = _service.ReviewExpArticleList(p);

            if (tp.Item3 != "0")
            {
                TempData["ResultNum"] = tp.Item3;
                TempData["ResultMessage"] = tp.Item4;
                return RedirectToAction("Index", "AdminReviewExp", p);
            }

            model.RouteParam = p;
            model.Total = tp.Item2;
            model.List = tp.Item1;
            model.MasterDetail = _service.ReviewExpMasterDetail(p.EXP_MASTER_IDX);



            return View(model);
        }

        /**
         * 체험단 관련 리뷰글들 수정
         */
        [CustomAuthorize]
        [Route("ArticleUpdate/{id:int}")]
        public ActionResult ArticleUpdate(int? id, AdminReviewExpArticleRouteParam p)
        {
            SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result model = new SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result();
            model.RouteParam = p;

            var detail = _service.ReviewExpArticleDetail(id);

            model.IDX = detail.IDX;
            model.EXP_MASTER_IDX = detail.EXP_MASTER_IDX;
            model.P_NAME = detail.P_NAME;
            model.M_ID = detail.M_ID;
            model.TITLE = detail.TITLE;
            model.TAG = detail.TAG;
            model.VIEW_CNT = detail.VIEW_CNT;
            model.IS_DISPLAY = detail.IS_DISPLAY;
            //model.COMMENT = detail.COMMENT;
            model.COMMENT_HTML = detail.COMMENT;

            return View(model);
        }

        [HttpPost]
        [CustomAuthorize]
        [ValidateAntiForgeryToken]
        [Route("ArticleUpdate/{id:int}")]
        public ActionResult ArticleUpdate(SP_ADMIN_REVIEW_EXP_ARTICLE_DETAIL_Result model, AdminReviewExpArticleRouteParam p)
        {
            model.RouteParam = p;

            /**
             * 마스터 아이디 생성
             */
            if (ModelState.IsValid)
            {
                _service.ReviewExpArticleUpdate(model);

                TempData["ResultNum"] = "0";
                return View(model);
            }
            TempData["ResultNum"] = "1";
            TempData["ResultMessage"] = "에러가 발생했습니다.";
            return View(model);
        }
    }
}