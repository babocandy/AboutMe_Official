using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminDisplay;

namespace AboutMe.Common.Helper
{
    public class PopupHelper
    {
        public static List<PopupInfo> GetDataForDisplay(List<SP_ADMIN_POPUP_SEL_Result> r)
        {

            List<PopupInfo> list = new List<PopupInfo>();

            foreach (var item in r)
            {
                var d = new PopupInfo();

                d.IDX = item.IDX;

                if (item.MEDIA_GBN == "10")
                {
                    d.MEDIA_GBN = "전체";
                }
                else if (item.MEDIA_GBN == "20")
                {
                    d.MEDIA_GBN = "웹";
                }
                else if (item.MEDIA_GBN == "30")
                {
                    d.MEDIA_GBN = "모바일";
                }

                d.TITLE = item.TITLE;

                d.IS_DISPLAY = item.IS_DISPLAY =="Y" ? true : false;
                d.DISPLAY_START = item.DISPLAY_START != null ? item.DISPLAY_START.Value.ToString("yyyy-MM-dd HH:mm") : "";
                d.DISPLAY_END = item.DISPLAY_END != null ? item.DISPLAY_END.Value.ToString("yyyy-MM-dd HH:mm") : "";
                d.WEB_IMG = item.WEB_IMG;
                d.WEB_LINK = item.WEB_LINK;
                d.MOBILE_IMG = item.MOBILE_IMG;
                d.MOBILE_LINK = item.MOBILE_LINK;
                d.INS_DATE = item.INS_DATE != null ? item.INS_DATE.Value.ToString("yyyy.MM.dd") : "";

                list.Add(d);
            }
            return list;
        }
    }
}
