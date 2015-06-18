using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Common.Data
{
    public class ImageResult
    {
        public bool Success { get; set; }           //이미지 업로드 true/false
        public string ImageName { get; set; }       //리네임 이미지네임값
        public string ErrorMessage { get; set; }    //에러메시지 
    }
}
