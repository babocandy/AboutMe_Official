using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminDisplay;
using System.Data.Entity.Core.Objects;

using System.Diagnostics;
using System.Globalization;



namespace AboutMe.Domain.Service.AdminDisplay
{
    public class AdminDisplayService : IAdminDisplayService
    {
        /**
         * 이미지 타입 전시물 저장
         */
        public void SaveImageTypeDisplayer(DisplayerParam param)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                //context.SP_ADMIN_DISPLAY_SAVE(idx, kind, subKind, seq, url, img, null, null, null, null, null);
                context.SP_ADMIN_DISPLAY_SAVE(param.IDX, param.KIND, param.SUB_KIND, param.SEQ, param.URL, param.IMG_NAME, null, null, null, retNum, retMsg);
            }
        }

        /**
         * 상품 타입 전시물 저장
         */
        public Tuple<string, string> SaveProductTypeDisplayer(DisplayerParam param)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_SAVE(param.IDX, param.KIND, param.SUB_KIND, param.SEQ, null, null, param.P_CODE, null, null, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            return tp;
        }

        /**
         * 링크 타입 전시물 저장
         */
        public Tuple<string, string> SaveLinkTypeDisplayer(DisplayerParam param)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_SAVE(param.IDX, param.KIND, param.SUB_KIND, param.SEQ, param.URL, null, null, param.TITLE1, param.TITLE2, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            return tp;
        }

        /**
         * 전시물 삭제
         */
        public void RemoveDisplayer(int? idx)
        {
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_REMOVE(idx);
            }
        }

        /**
         * 전시물의 이미지만 삭제
         */
        public void RemoveOnlyImageInDisplayer(int? idx)
        {
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_DISPLAY_IMAGE_REMOVE(idx);
            }
        }

        /**
         * 전시물 조회
         */
        public List<SP_ADMIN_DISPLAY_SEL_Result> GetDisplayerList(DisplayerParam param)
        {
            List<SP_ADMIN_DISPLAY_SEL_Result> list = new List<SP_ADMIN_DISPLAY_SEL_Result>();
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                list = context.SP_ADMIN_DISPLAY_SEL(param.KIND, param.SUB_KIND, param.SEQ).ToList();
            }

            return list;
        }

        /**
         * 팝업조회
         */
        public Tuple<List<SP_ADMIN_POPUP_SEL_Result>, int>  PopupSel(PopupSearchParam param)
        {
            List<SP_ADMIN_POPUP_SEL_Result> list = new List<SP_ADMIN_POPUP_SEL_Result>();
            ObjectParameter total = new ObjectParameter("TOTAL", typeof(int));
            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                list = context.SP_ADMIN_POPUP_SEL(param.PAGE, 10, param.SEARCH_KEY, param.SEARCH_VALUE, param.IS_DISPLAY, param.IS_EXPIRE, total).ToList();
            }

            Tuple<List<SP_ADMIN_POPUP_SEL_Result>, int> tp = new Tuple<List<SP_ADMIN_POPUP_SEL_Result>, int>(list, Convert.ToInt32(total.Value));
  
            return tp;

        }

        /**
         * 팝업추가
         */
        public Tuple<string, string> PopupAdd(PopupParam param)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));
            
            
            DateTime? start_dt = null;
            DateTime? end_dt = null;

            if (  !String.IsNullOrEmpty( param.DISPLAY_START ) )
            {
                start_dt = DateTime.ParseExact(param.DISPLAY_START, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                //start_dt = Convert.ToDateTime(param.DISPLAY_START);
            }

            if (!String.IsNullOrEmpty(param.DISPLAY_END))
            {
                end_dt = DateTime.ParseExact(param.DISPLAY_END, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                //end_dt =  Convert.ToDateTime(param.DISPLAY_END);
            }
            


            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_POPUP_ADD(param.MEDIA_GBN, param.TITLE, param.IS_DISPLAY, start_dt, end_dt,
                    param.POS_TOP, param.POS_LEFT, param.SIZE_WIDTH, param.SIZE_HEIGHT, param.WEB_IMG_NAME, param.WEB_LINK, param.WEB_TARGET
                    , param.MOBILE_IMG_NAME, param.MOBILE_LINK, retNum, retMsg);
            }

            return  new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }

        /**
         * 팝업 전시여부 수정
         */
        public Tuple<string, string> PopupUpdateDisplay(PopupParam param)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_POPUP_UPDATE_DISPLAY(param.IDX, param.IS_DISPLAY, retNum, retMsg);
            }

            return new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }

        /**
         * 팝업 삭제
         */
        public Tuple<string, string> PopupRemove(PopupParam param)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_POPUP_REMOVE(param.IDX, retNum, retMsg);
            }

            return new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }


        /**
         * 팝업 정보
         */
        public Tuple<SP_ADMIN_POPUP_INFO_Result, string, string> PopupInfo(int? idx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            SP_ADMIN_POPUP_INFO_Result info = new SP_ADMIN_POPUP_INFO_Result();

            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                info = context.SP_ADMIN_POPUP_INFO(idx, retNum, retMsg).SingleOrDefault();
            }

            return new Tuple<SP_ADMIN_POPUP_INFO_Result, string, string>(info, retNum.Value.ToString(), retMsg.Value.ToString());
        }




        /**
         * 팝업 수정
         */
        public Tuple< string, string> PopupUpdate(PopupParam param)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));


            DateTime? start_dt = null;
            DateTime? end_dt = null;

            if (!String.IsNullOrEmpty(param.DISPLAY_START))
            {
                start_dt = DateTime.ParseExact(param.DISPLAY_START, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            }

            if (!String.IsNullOrEmpty(param.DISPLAY_END))
            {
                end_dt = DateTime.ParseExact(param.DISPLAY_END, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            }



            using (AdminDisplayEntities context = new AdminDisplayEntities())
            {
                context.SP_ADMIN_POPUP_UPDATE(param.IDX, param.MEDIA_GBN, param.TITLE, param.IS_DISPLAY, start_dt, end_dt,
                    param.POS_TOP, param.POS_LEFT, param.SIZE_WIDTH, param.SIZE_HEIGHT, param.WEB_IMG_NAME, param.WEB_LINK, param.WEB_TARGET
                    , param.MOBILE_IMG_NAME, param.MOBILE_LINK, retNum, retMsg);
            }

            return new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
        }


    }
}
