using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Diagnostics;
using AboutMe.Common.Data;

namespace AboutMe.Web.Admin.Controllers
{
    public class SmartEditorController : Controller
    {
        const int maxFileSize = 10;
        const string defaultUploadPath = "~/Upload/StmartEditor/";
        string uploadPath;

        //이미지 파일형식 체크
        private string[] typeFilter = { "image/jpg", "image/png", "image/gif", "image/jpeg", "image/bmp", "image/pjpeg" };

        // GET: EditorTest
        public ActionResult Sample()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult InstallEditor(SmartEditorConfig config)
        {
           // this.uploadPath = (config.uploadFolder == null) ? defaultUploadPath : config.uploadFolder;
           // Debug.WriteLine("uploadPath:" + this.uploadPath);
            return PartialView(config);
        }

        [HttpGet]
        public ActionResult PopupPhotoUploader(string uploadPath)
        {
            Debug.WriteLine("uploadPath :" + uploadPath);
            ViewBag.uploadPath = uploadPath;
            return View();
        }

        /**
         * ie 하위 버전용 업로더
         */
        [HttpPost]
        public ActionResult FileUploader()
        {
            uploadPath = Request.Form["uploadPath"];


            Debug.WriteLine("uploadPath=>" + uploadPath);

            if (uploadPath == null)
            {
                uploadPath = defaultUploadPath;
            }

            string callbackUrl = string.Format("{0}?callback_func={1}", Request.Form["callback"], Request.Form["callback_func"]);

            HttpPostedFileBase file = Request.Files["Filedata"];

            if (file != null && file.ContentLength > 0)
            {

                bool isSave = true;

                //파일 용량 체크. MB 단위
                if (file.ContentLength / (1024 * 1024) > maxFileSize)
                {
                    isSave = false;
                    callbackUrl += "&errstr=image_file_is_over";
                }


                if (!typeFilter.Contains(file.ContentType))
                {
                    isSave = false;
                    callbackUrl += "&errstr=image_file._type_is_incorrect";
                }

                //저장 실행
                if (isSave)
                {

                    Tuple<string, string, string> tt = GetFileInfoToSave(file.FileName);

                    try
                    {
                        file.SaveAs(tt.Item2);

                        callbackUrl = callbackUrl + string.Format("&bNewLine=true&sFileName={0}&sFileURL={1}", tt.Item1, tt.Item3);
                    }
                    catch (Exception)
                    {

                        callbackUrl += "&errstr=save_fail";
                    }

                }
            }
            else
            {
                callbackUrl += "&errstr=file_size_is_zero";
            }


            return Redirect(callbackUrl);
        }

        [HttpGet]
        public ActionResult Callback()
        {
            return View();
        }


        /**
         * html5 버전 업로더
         */
        [HttpPost]
        public String FileUploaderHtml5()
        {


            int length = Request.ContentLength;
            byte[] bytes = new byte[length];
            Request.InputStream.Read(bytes, 0, length);
            
            string fileName = Request.Headers["file-name"];
            int fileSize = int.Parse(Request.Headers["file-size"]);
            string fileType = Request.Headers["file-type"];

            this.uploadPath = Request.Headers["file-Upload"];
            if (this.uploadPath == null)
            {
                this.uploadPath = defaultUploadPath;
            }

            /**
             * 업로드 이미지의 유효성 체크 필요
             */
            //파일 존재 체크
            if (fileSize == 0)
            {
                return string.Format("NOTALLOW_{0}", fileName);
            }

            //파일 용량 체크. MB 단위
            if (fileSize / (1024 * 1024) > maxFileSize)
            {
                return string.Format("NOTALLOW_{0}", fileName);
            }


            if (!typeFilter.Contains(fileType))
            {
                return string.Format("NOTALLOW_{0}", fileName);
            }


            //
            Tuple<string, string, string> tt = GetFileInfoToSave(fileName);

            try
            {
                //이미지 저장 
                FileStream fileStream = new FileStream(tt.Item2, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(bytes, 0, length);
                fileStream.Close();

            }
            catch (Exception)
            {

                return string.Format("NOTALLOW_{0}", fileName);
            }

            //스마트 에디터에 맞는 리턴값 반환
            return string.Format("&bNewLine=true&sFileName={0}&sFileURL={1}", tt.Item1, tt.Item3);
        }

        /**
         * 저장하기 위한 파일에 관한 정보들. 새파일명, 로컬경로, url
         */
        Tuple<string, string, string> GetFileInfoToSave(string fileName)
        {
            //확장자
            string ext = Path.GetExtension(fileName);
            //새파일명
            string newFileName = string.Format("{0}{1}", GetUniqueKey(), ext);

            Debug.WriteLine("this.uploadPath : " + this.uploadPath);

            //업로드 디렉토리
            string filePath = Server.MapPath(this.uploadPath);

            //저장할 이미지 파일의 서버상 물리적 경로
            var saveToFileLoc = string.Format("{0}\\{1}", filePath, newFileName);

            //이미지 파일 URL 생성
            var fileUrl = string.Format("{0}://{1}{2}{3}", Request.Url.Scheme, Request.Url.Authority, Url.Content(this.uploadPath), newFileName);

            Tuple<string, string, string> tuple = new Tuple<string, string, string>(newFileName, saveToFileLoc, fileUrl);

            return tuple;
        }

        /**
         * 고유 파일명
         */
        private string GetUniqueKey()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            string RandomNumber;
            RandomNumber = rand.Next(100, 999).ToString();
            string UniqueKey = "A" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomNumber;

            return UniqueKey;
        }
    }
}