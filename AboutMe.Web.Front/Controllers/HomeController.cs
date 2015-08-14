using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Script.Serialization;

namespace AboutMe.Web.Front.Controllers 
{

        public class JsonpResult : JsonResult
        {
               object data = null;
               private const string CALLBACK_QUERYSTRING = "callback";
               private const string CALLBACK_CONTENTTYPE = "application/x-javascript";
               
              public JsonpResult()
                {
                }
 
                public JsonpResult(object data)
                {
                    this.data = data;
                }

                public override void ExecuteResult(ControllerContext controllerContext)
                {
                    if (controllerContext != null)
                    {
                        HttpResponseBase Response = controllerContext.HttpContext.Response;
                        HttpRequestBase Request = controllerContext.HttpContext.Request;

                        string callbackfunction = Request["callback"];

                        if (string.IsNullOrEmpty(callbackfunction))
                        {
                            throw new Exception("Callback function name must be provided in the request!");
                        }
                        Response.ContentType = "application/x-javascript";
                        if (data != null)
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            Response.Write(string.Format("{0}({1});", callbackfunction, serializer.Serialize(data)));
                            
                        }
                    }
                }
        }

//-------------------------------
    /*
    public class JsonpResult : JsonResult
    {
        private const string CALLBACK_QUERYSTRING = "callback";
        private const string CALLBACK_CONTENTTYPE = "application/x-javascript";

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            if (controllerContext != null)
            {
                var request = controllerContext.HttpContext.Request;
                object callback = request[CALLBACK_QUERYSTRING];
                if (callback == null)
                {
                    controllerContext.RouteData.Values.TryGetValue(CALLBACK_QUERYSTRING, out callback);
                }

                var hasCallback = !string.IsNullOrWhiteSpace(callback == null ? "" : callback as string);

                if (hasCallback)
                {
                    SetContentTypeIfEmpty();
                    var response = controllerContext.HttpContext.Response;

                    response.Write(callback);
                    response.Write("(");
                    base.ExecuteResult(controllerContext);
                    response.Write(")");
                }
                else
                {
                    base.ExecuteResult(controllerContext);
                }
            }
        }

        private void SetContentTypeIfEmpty()
        {
            if (string.IsNullOrWhiteSpace(base.ContentType))
            {
                base.ContentType = CALLBACK_CONTENTTYPE;
            }
        }
    }

    */

//-------------------------------
    public class HomeController : BaseFrontController
    {
        public ActionResult Index()  //웹루트 메인 페이지  
        {
            return View();
        }

        public ActionResult Test01()
        {
            string s = DetectingDevice();
            ViewBag.device = s;
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //---------------------------------- test -----------------------
        public ActionResult TestJsonpForm()
        {
            return View();
        }

        public JsonpResult AjaxTestJsonpRet()
        {

            string strM_ID = HttpContext.Request.Form["M_ID"]; //

            //JsonpResult objJsonp;
            //objJsonp = new JsonpResult(Json(new { ERR_CODE = "E000", ERR_MSG = "test msg", M_ID =strM_ID}));
            //return objJsonp;

           // JsonpResult objJsonp = new JsonpResult(Json(new { ERR_CODE = "E000", ERR_MSG = "test msg", M_ID = strM_ID }));
           // objJsonp.ExecuteResult(this.ControllerContext);
           //return objJsonp;

            //return "fCAll_BACK({ERR_CODE:\"000\",ERR_MSG:\"\",M_ID:\"aaa\"})";

            //return new JsonpResult();
            JsonpResult obj = new JsonpResult(Json(new { ERR_CODE = "E000", ERR_MSG = "test msg", M_ID = strM_ID }));
            return obj;


        }

    
    }

}