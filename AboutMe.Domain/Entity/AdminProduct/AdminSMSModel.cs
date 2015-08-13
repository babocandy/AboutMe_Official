using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.Common;

namespace AboutMe.Domain.Entity.AdminProduct
{
    #region SMS 모델 엔티티
    public class AdminSMSModel
    {
        public AdminSMSModel()
        {
            this.SMS_FLAG = "1";
            this.SEND_TIME = "";
            this.HANDPHONE = "";
            this.CALLBACK_NO = "0807407983"; //회신번호
            this.TITLE = "";
            this.SEND_MSG = "";
        }

        public string SMS_FLAG { get; set; }
        public string SEND_TIME { get; set; }
        public string HANDPHONE { get; set; }
        public string CALLBACK_NO { get; set; }
        public string TITLE { get; set; }
        public string SEND_MSG { get; set; }
    }
    #endregion
}
