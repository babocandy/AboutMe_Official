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
        #region 기본 초기값
        public ImageUpload()
        {
            UploadPath = "~/Upload/Temp/"; //이미지 업로드 경로 default /Upload/
            addMobileImage = false; //이미지 업로드시 모바일 업로드용으로 리사이즈추가 필요시 true
            fileType = "image";  //파일 type:image/file
            fileMaxSize = 10000000; //파일 max size 5MB
            Width = 500; //이미지 기본 사이즈
        }
        // set default size here
        public int Width { get; set; }
        public int Height { get; set; }
        public string UploadPath { get; set; }
        public bool addMobileImage { get; set; }
        public string fileType { get; set; }
        private int fileMaxSize { get; set; }

       #endregion

        #region 고유한 이미지네임으로 변경
        public ImageResult RenameUploadFile(HttpPostedFileBase file, Int32 counter = 0)
        {
            var fileName = Path.GetFileName(file.FileName);

            #region 기존소스 주석처리됨 (같은파일 있으면 리네임)
            //string prepend = "A";
            //string finalFileName = prepend + ((counter).ToString()) + "_" + fileName;
            //if (System.IO.File.Exists
            //    (HttpContext.Current.Request.MapPath(UploadPath + finalFileName)))
            //{
            //    //file exists => add country try again
            //    return RenameUploadFile(file, ++counter);
            //}
            #endregion

            //파일명 유니크하게 리네임
            string finalFileName = GetUniqueKey() + Path.GetExtension(file.FileName);

            //file doesn't exist, upload item but validate first
            return UploadFile(file, finalFileName);
            
        }
        #endregion

        #region 파일 업로드
        private ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };

            var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);

            //썸네일이미지 resize path
            var resize_type_500_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R500_"+fileName);
            var resize_type_270_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R270_" + fileName);
            var resize_type_120_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R120_" + fileName);
            var resize_type_64_path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R64_" + fileName);

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
                        //500size
                        file.SaveAs(resize_type_500_path);
                        Image r500_imgOriginal = Image.FromFile(resize_type_500_path);
                        this.Width = 500; //500 썸네일 사이즈

                        Image r500_imgActual = Scale(r500_imgOriginal);
                        r500_imgOriginal.Dispose();
                        r500_imgActual.Save(resize_type_500_path);
                        r500_imgActual.Dispose();

                        //270size
                        file.SaveAs(resize_type_270_path);
                        Image r270_imgOriginal = Image.FromFile(resize_type_270_path);
                        this.Width = 270; //270 썸네일 사이즈

                        Image r270_imgActual = Scale(r270_imgOriginal);
                        r270_imgOriginal.Dispose();
                        r270_imgActual.Save(resize_type_270_path);
                        r270_imgActual.Dispose();


                        //120size
                        file.SaveAs(resize_type_120_path);
                        Image r120_imgOriginal = Image.FromFile(resize_type_120_path);
                        this.Width = 120; //120 썸네일 사이즈

                        Image r120_imgActual = Scale(r120_imgOriginal);
                        r120_imgOriginal.Dispose();
                        r120_imgActual.Save(resize_type_120_path);
                        r120_imgActual.Dispose();


                        //64size
                        file.SaveAs(resize_type_64_path);
                        Image r64_imgOriginal = Image.FromFile(resize_type_64_path);
                        this.Width = 64; //64 썸네일 사이즈

                        Image r64_imgActual = Scale(r64_imgOriginal);
                        r64_imgOriginal.Dispose();
                        r64_imgActual.Save(resize_type_64_path);
                        r64_imgActual.Dispose();
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
                //destWidth = sourceWidth;
                //destHeight = sourceHeight;
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

        #region 고유키값 생성
        private string GetUniqueKey()
       {
           Random rand = new Random((int)DateTime.Now.Ticks);
           string RandomNumber;
           RandomNumber = rand.Next(100, 999).ToString();
           string UniqueKey = "A"+DateTime.Now.ToString("yyyyMMddHHmmssfff")+"_"+ RandomNumber;

           return UniqueKey;
       }
        #endregion


    }
}
