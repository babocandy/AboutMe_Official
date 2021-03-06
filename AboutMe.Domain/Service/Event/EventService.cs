﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Event;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Event
{
    public class EventService : IEventService
    {
        #region Admin List
        public List<SP_ADMIN_EVENT_SEL_Result> EventAdminList(EVENT_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_ADMIN_EVENT_SEL_Result> lst = new List<SP_ADMIN_EVENT_SEL_Result>();
            using (EventEntities EfContext = new EventEntities())
            {
                lst = EfContext.SP_ADMIN_EVENT_SEL(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword, param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public int EventAdminCount(EVENT_ADMIN_SEARCH param)
        {
            SP_ADMIN_EVENT_CNT_Result result = new SP_ADMIN_EVENT_CNT_Result();
            using (EventEntities EfContext = new EventEntities())
            {
                result = EfContext.SP_ADMIN_EVENT_CNT(param.FromDate, param.ToDate, param.IngGbn, param.UseYn, param.SearchCol, param.SearchKeyword).FirstOrDefault();
            }
            return result.COUNT;
        }
        #endregion

        #region Admin View
        public SP_ADMIN_EVENT_VIEW_Result EventAdminView(int IDX)
        {
            SP_ADMIN_EVENT_VIEW_Result result = new SP_ADMIN_EVENT_VIEW_Result();
            using (EventEntities EfContext = new EventEntities())
            {
                result = EfContext.SP_ADMIN_EVENT_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Insert
        public int EventAdminInsert(SP_ADMIN_EVENT_VIEW_Result param)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));
            using (EventEntities EfContext = new EventEntities())
            {
                EfContext.SP_ADMIN_EVENT_INS(param.EVENT_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.EVENT_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.USE_YN, param.ADM_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Update
        public void EventAdminUpdate(int IDX, SP_ADMIN_EVENT_VIEW_Result param)
        {
            using (EventEntities EfContext = new EventEntities())
            {
                EfContext.SP_ADMIN_EVENT_UPD(IDX, param.EVENT_NAME, param.FROM_DATE, param.FROM_TIME, param.TO_DATE, param.TO_TIME, param.EVENT_GBN, param.WEB_CONTENTS, param.MOBILE_FILE, param.WEB_BANNER, param.MOBILE_BANNER, param.USE_YN, param.ADM_ID);
            }
        }
        #endregion

        #region Admin Main View
        public SP_ADMIN_EVENT_MAIN_VIEW_Result EventAdminMainView()
        {
            SP_ADMIN_EVENT_MAIN_VIEW_Result result = new SP_ADMIN_EVENT_MAIN_VIEW_Result();
            using (EventEntities EfContext = new EventEntities())
            {
                result = EfContext.SP_ADMIN_EVENT_MAIN_VIEW().FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region Admin Main 진행중인 이벤트 목록
        public List<SP_ADMIN_EVENT_MAIN_LIST_Result> EventAdminMainIngList()
        {
            List<SP_ADMIN_EVENT_MAIN_LIST_Result> lst = new List<SP_ADMIN_EVENT_MAIN_LIST_Result>();
            using (EventEntities EfContext = new EventEntities())
            {
                lst = EfContext.SP_ADMIN_EVENT_MAIN_LIST().ToList();
            }
            return lst;
        }
        #endregion

        #region Admin Main 진행중인 이벤트 목록 순서변경
        public void EventAdminMainListOrderUpdate(string GBN, int IDX, int ORDER)
        {
            using (EventEntities EfContext = new EventEntities())
            {
                EfContext.SP_ADMIN_EVENT_MAIN_LIST_ORDER_UPD(GBN, IDX, ORDER);
            }
        }
        #endregion

        #region Admin Main 이미지/URL 업데이트
        public void EventAdminMainImageUpdate(string BANNER_GBN, string IMG, string URL, string ADM_ID, string TITLE = "", string DESC = "")
        {
            using (EventEntities EfContext = new EventEntities())
            {
                EfContext.SP_ADMIN_EVENT_MAIN_UPD(BANNER_GBN, IMG, URL, TITLE, DESC, ADM_ID);
            }
        }
        #endregion

        #region Admin Main 이미지 Delete
        public void EventAdminMainImageDelete(string BANNER_GBN, string ADM_ID)
        {
            using (EventEntities EfContext = new EventEntities())
            {
                EfContext.SP_ADMIN_EVENT_MAIN_DEL(BANNER_GBN, ADM_ID);
            }
        }
        #endregion

        #region User Main View
        public SP_EVENT_MAIN_VIEW_Result EventMainView()
        {
            SP_EVENT_MAIN_VIEW_Result result = new SP_EVENT_MAIN_VIEW_Result();
            using (EventEntities EfContext = new EventEntities())
            {
                result = EfContext.SP_EVENT_MAIN_VIEW().FirstOrDefault();
            }
            return result;
        }
        #endregion

        #region User Main 진행중인 이벤트 목록
        public List<SP_EVENT_ING_LIST_Result> EventMainIngList()
        {
            List<SP_EVENT_ING_LIST_Result> lst = new List<SP_EVENT_ING_LIST_Result>();
            using (EventEntities EfContext = new EventEntities())
            {
                lst = EfContext.SP_EVENT_ING_LIST().ToList();
            }
            return lst;
        }
        #endregion

        #region User Main 종료 이벤트 목록
        public List<SP_EVENT_END_LIST_Result> EventMainEndList()
        {
            List<SP_EVENT_END_LIST_Result> lst = new List<SP_EVENT_END_LIST_Result>();
            using (EventEntities EfContext = new EventEntities())
            {
                lst = EfContext.SP_EVENT_END_LIST().ToList();
            }
            return lst;
        }
        #endregion


        #region User Main View
        public SP_EVENT_VIEW_Result EventView(int IDX)
        {
            SP_EVENT_VIEW_Result result = new SP_EVENT_VIEW_Result();
            using (EventEntities EfContext = new EventEntities())
            {
                result = EfContext.SP_EVENT_VIEW(IDX).FirstOrDefault();
            }
            return result;
        }
        #endregion
    }
}
