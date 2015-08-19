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
            //addMobileImage = false; //이미지 업로드시 모바일 업로드용으로 리사이즈추가 필요시 true
            fileType = "image";  //파일 type:image/file
            fileMaxSize = 5000000; //파일 max size 5MB
            //Width = 500; //이미지 기본 사이즈
            IsThumbNail = false;
        }

        // set default size here
        //public int Width { get; set; }
        //public int Height { get; set; }
        public string UploadPath { get; set; }
        //public bool addMobileImage { get; set; }
        public string fileType { get; set; }
        private int fileMaxSize { get; set; }
        public int DefaultWidth { get; set; }
        public int DefaultHeight { get; set; }
        public bool IsThumbNail { get; set; }

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

        public void SaveImageByFileName(string UploadPath, string fileName)
        {
            try
            {
                //썸네일
                var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath),  fileName);

                var path_small = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R100_" + fileName);
                var path_middle = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R308_" + fileName);
                var path_big = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R500_" + fileName);

                //원본 뜨기
                Image imgOriginal = Image.FromFile(path);

                //썸네일 이미지 생성
                Image imgActual = Scale(imgOriginal, 500);
                imgActual.Save(path_big);
                imgActual.Dispose();

                imgActual = Scale(imgOriginal, 308);
                imgActual.Save(path_middle);
                imgActual.Dispose();

                imgOriginal.Dispose();

            }
            catch (Exception ex)
            {
            //
             }
        }

 
        private ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            //리턴 결과 객체
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };

            //오리지널 패스
            var path = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), fileName);

            //이미지 확장자
            string extension = Path.GetExtension(file.FileName);

            //용량체크
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


            //썸네일
            var path_small = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R100_" + fileName);
            var path_middle = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R308_" + fileName);
            var path_big = Path.Combine(HttpContext.Current.Request.MapPath(UploadPath), "R500_" + fileName);

            try
            {


                byte[] imageData = new byte[file.ContentLength];
                file.InputStream.Read(imageData, 0, file.ContentLength);

                MemoryStream ms = new MemoryStream(imageData);
                Image originalImage = Image.FromStream(ms);

                if (originalImage.PropertyIdList.Contains(0x0112))
                {
                    int rotationValue = originalImage.GetPropertyItem(0x0112).Value[0];

                    Debug.WriteLine("rotationValue " + rotationValue);
                    switch (rotationValue)
                    {
                        case 1: // landscape, do nothing
                            break;

                        case 8: // rotated 90 right
                            // de-rotate:
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                            break;

                        case 3: // bottoms up
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                            break;

                        case 6: // rotated 90 left
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                            break;
                    }
                }


                //file.SaveAs(path);
                originalImage.Save(path);

                if (IsThumbNail)
                {
                    //원본 뜨기
                    Image imgOriginal = Image.FromFile(path);

                    //썸네일 이미지 생성
                    Image imgActual = Scale(imgOriginal, 500);
                    imgActual.Save(path_big);
                    imgActual.Dispose();

                    imgActual = Scale(imgOriginal, 308);
                    imgActual.Save(path_middle);
                    imgActual.Dispose();

                    imgOriginal.Dispose();
                }


                //원본제거
                originalImage.Dispose();

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



        private Image Scale(Image imgPhoto, int Width = 0, int Height = 0)
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
