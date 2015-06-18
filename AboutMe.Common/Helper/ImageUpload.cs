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

namespace AboutMe.Common.Helper
{
    public class ImageUpload
    {
        // set default size here
        public int Width { get; set; }
        public int Height { get; set; }
        public string fileType { get; set; } //파일 type:image/file

        // folder for the upload, you can put this in the web.config
        public string UploadPath = "~/Upload/"; //이미지 업로드 경로 default /Upload/
        public bool addMobileImage = false; //이미지 업로드시 모바일 업로드용으로 리사이즈추가 필요시 true
        private int fileMaxSize = 5000000; //파일 max size 5MB

        #region 고유한 이미지네임으로 변경
        public ImageResult RenameUploadFile(HttpPostedFileBase file, Int32 counter = 0)
        {
            var fileName = Path.GetFileName(file.FileName);

            string prepend = "A";
            string finalFileName = prepend + ((counter).ToString()) + "_" + fileName;
            if (System.IO.File.Exists
                (HttpContext.Current.Request.MapPath(UploadPath + finalFileName)))
            {
                //file exists => add country try again
                return RenameUploadFile(file, ++counter);
            }
            //file doesn't exist, upload item but validate first
            return UploadFile(file, finalFileName);
            
        }
        #endregion

        #region 파일 업로드
        private ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };

            var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);

            //모바일 path
            var mobile_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "mobile_"+fileName);

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
                file.SaveAs(path);
                
                if (fileType=="image")
                { 
                    Image imgOriginal = Image.FromFile(path);
                    //pass in whatever value you want
                    Image imgActual = Scale(imgOriginal);
                    imgOriginal.Dispose();
                    imgActual.Save(path);
                    imgActual.Dispose();

                    imageResult.ImageName = fileName;

                    if (addMobileImage) //모바일 이미지 추가시
                    {
                        file.SaveAs(mobile_path);
                        Image mobile_imgOriginal = Image.FromFile(mobile_path);
                        this.Width = 300; //모바일 사이즈

                        //pass in whatever value you want
                        Image mobile_imgActual = Scale(mobile_imgOriginal);
                        mobile_imgOriginal.Dispose();
                        mobile_imgActual.Save(mobile_path);
                        mobile_imgActual.Dispose();

                    }
                }

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
            if (extension=="image")
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

        #region 이미지 resize
        private Image Scale(Image imgPhoto)
        {
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
        #endregion


       private string GetUniqueKey()
       {
           return DateTime.Now.ToString().GetHashCode().ToString("x"); 
       }




    }
}
