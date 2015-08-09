using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using AboutMe.Common.Data;

using System.Diagnostics;

namespace AboutMe.Common.Helper
{
    public class ImagePlainUpload
    {

        public ImagePlainUpload()
        {
            UploadPath = "~/Upload/Temp/"; //이미지 업로드 경로 default /Upload/
            addMobileImage = false; //이미지 업로드시 모바일 업로드용으로 리사이즈추가 필요시 true
            fileType = "image";  //파일 type:image/file
            fileMaxSize = 5000000; //파일 max size 5MB
            Width = 500; //이미지 기본 사이즈
        }

        // set default size here
        public int Width { get; set; }
        public int Height { get; set; }
        public string UploadPath { get; set; }
        public bool addMobileImage { get; set; }
        public string fileType { get; set; }
        private int fileMaxSize { get; set; }


        //고유한 이미지네임으로 변경
        public ImageResult RenameUploadFile(HttpPostedFileBase file, Int32 counter = 0)
        {
            var fileName = Path.GetFileName(file.FileName);

            // 기존소스 주석처리됨 (같은파일 있으면 리네임)
            //string prepend = "A";
            //string finalFileName = prepend + ((counter).ToString()) + "_" + fileName;
            //if (System.IO.File.Exists
            //    (HttpContext.Current.Request.MapPath(UploadPath + finalFileName)))
            //{
            //    //file exists => add country try again
            //    return RenameUploadFile(file, ++counter);
            //}
           

            //파일명 유니크하게 리네임
            string finalFileName = GetUniqueKey() + Path.GetExtension(file.FileName);

            //file doesn't exist, upload item but validate first
            return UploadFile(file, finalFileName);

        }
       

        #region 파일 업로드
        private ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };

            var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);

            //썸네일이미지 resize path
            /*
            var resize_type_500_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R500_" + fileName);
            var resize_type_270_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R270_" + fileName);
            var resize_type_120_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R120_" + fileName);
            var resize_type_64_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R64_" + fileName);
             * */

            string extension = Path.GetExtension(file.FileName);

            if (file.ContentLength > fileMaxSize)
            {
                imageResult.Success = false;
                imageResult.ErrorMessage = "파일 용량 초과";
                return imageResult;
            }


            //make sure the file is valid
            if (!ValidateExtension(extension, fileType))
            {
                imageResult.Success = false;
                imageResult.ErrorMessage = "Invalid Extension";
                return imageResult;
            }

            try
            {
                Debug.WriteLine("fileName: " + fileName);
                file.SaveAs(path);
                imageResult.ImageName = fileName;

                return imageResult;
            }
            catch (Exception ex)
            {
                // you might NOT want to show the exception error for the user
                // this is generally logging or testing

                imageResult.Success = false;
                imageResult.ErrorMessage = ex.Message;
                return imageResult;
            }
        }
        #endregion

        #region 파일이 이미지 타입인지 체크
        private bool ValidateExtension(string extension, string fileType)
        {
            extension = extension.ToLower();
            if (extension == "image")
            {
                switch (extension)
                {
                    case ".jpg":
                        return true;
                    case ".png":
                        return true;
                    case ".gif":
                        return true;
                    case ".jpeg":
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                switch (extension)
                {
                    case ".jpg":
                        return true;
                    case ".png":
                        return true;
                    case ".gif":
                        return true;
                    case ".jpeg":
                        return true;
                    case ".doc":
                        return true;
                    case ".docx":
                        return true;
                    case ".pdf":
                        return true;
                    case ".txt":
                        return true;
                    case ".xls":
                        return true;
                    case ".xlsx":
                        return true;
                    case ".hwp":
                        return true;
                    case ".ppt":
                        return true;
                    case ".pptx":
                        return true;
                    default:
                        return false;
                }

            }

        }
        #endregion


        #region 고유키값 생성
        private string GetUniqueKey()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            string RandomNumber;
            RandomNumber = rand.Next(100, 999).ToString();
            string UniqueKey = "A" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + RandomNumber;

            return UniqueKey;
        }
        #endregion


    }
}
