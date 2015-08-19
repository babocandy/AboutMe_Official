﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using AboutMe.Domain.Service.Faq;
using AboutMe.Domain.Entity.Faq;
using AboutMe.Domain.Service.Notice;
using AboutMe.Domain.Entity.Notice;
using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.Controllers
{
    public class CustomerController : BaseFrontController
    {
        private IFaqService _faqservice;
        private INoticeService _noticeservice;


        public CustomerController(IFaqService _faqservice, INoticeService _noticeservice)
        {
            this._faqservice = _faqservice;
            this._noticeservice = _noticeservice;
        }

        //고객센터메인
        public ActionResult Index(FaqSearchModel search)
        {
            string SortCol = "IDX";
            string SortDir = "DESC";
            string DisplayYn = "Y";
            int? PageSize = 5;
            int? Page = search.Page;
            search.PageSize = PageSize;

            int total_count = 0;
            total_count = _faqservice.FaqListCount(search.SearchCol, search.SearchKeyword, DisplayYn);
            List<SP_TB_FAQ_SEL_Result> lst = new List<SP_TB_FAQ_SEL_Result>();
            lst = _faqservice.FaqList(search.SearchCol, search.SearchKeyword, DisplayYn, SortCol, SortDir, Page, PageSize);

            FaqUserModel FaqModel = new FaqUserModel
            {
                searchOption = search,
                faqCnt = total_count,
                faqList = lst
            };
            
            NoticeSearchModel searchModel = new NoticeSearchModel { 
                DisplayYn = "Y"
                , Page = 1
                , PageSize = 3
                , SearchCol = ""
                , SearchKeyword = ""
            };

            int total_notice = 0;
            total_notice = _noticeservice.NoticeListCount(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn);

            List<SP_TB_NOTICE_SEL_Result> notice_list = new List<SP_TB_NOTICE_SEL_Result>();
            notice_list = _noticeservice.NoticeList(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn, SortCol, SortDir, searchModel.Page, searchModel.PageSize);

            NoticeUserModel NoticeModel = new NoticeUserModel
            {
                searchOption = searchModel,
                NoticeCnt = total_notice,
                NoticeList = notice_list
            };

            CustomerMainModel M = new CustomerMainModel
            {
                Faq = FaqModel
                , Notice = NoticeModel
            };

            return View(M);
        }

        //Faq
        public ActionResult Faq(FaqSearchModel search)
        {
            string SortCol = "IDX";
            string SortDir = "DESC";
            string DisplayYn = "Y";
            int? PageSize = 10;
            int? Page = search.Page;
            
            search.PageSize = PageSize;
            
            int total_count = 0;
            total_count = _faqservice.FaqListCount(search.SearchCol, search.SearchKeyword, DisplayYn);
            List<SP_TB_FAQ_SEL_Result> lst = new List<SP_TB_FAQ_SEL_Result>();
            lst = _faqservice.FaqList(search.SearchCol, search.SearchKeyword, DisplayYn, SortCol, SortDir, Page, PageSize);

            FaqUserModel M = new FaqUserModel
            {
                searchOption = search,
                faqCnt = total_count,
                faqList = lst
            };

            return View(M);
        }

        //공지사항
        public ActionResult Notice(NoticeSearchModel searchModel)
        {
            string SortCol = "IDX";
            string SortDir = "DESC";
            int? Page = searchModel.Page;
            int? PageSize = 5;
            searchModel.PageSize = PageSize;
            searchModel.DisplayYn = "Y";

            int total_count = 0;
            total_count = _noticeservice.NoticeListCount(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn);

            List<SP_TB_NOTICE_SEL_Result> lst = new List<SP_TB_NOTICE_SEL_Result>();
            lst = _noticeservice.NoticeList(searchModel.SearchCol, searchModel.SearchKeyword, searchModel.DisplayYn, SortCol, SortDir, Page, PageSize);

            NoticeUserModel M = new NoticeUserModel
            {
                searchOption = searchModel,
                NoticeCnt = total_count,
                NoticeList = lst
            };
            return View(M);
        }


        //공지사항 상세
        public ActionResult NoticeView(int IDX, NoticeSearchModel searchModel)
        {
            var model = new TB_NOTICE();

            SP_TB_NOTICE_VIEW_Result resultModel = _noticeservice.NoticeView(IDX);
            model.IDX = resultModel.IDX;
            model.TITLE = resultModel.TITLE;
            model.CONTENTS = resultModel.CONTENTS;
            model.DISPLAY_YN = resultModel.DISPLAY_YN;
            model.M_ID = resultModel.M_ID;
            model.REG_DATE = resultModel.REG_DATE;
            model.VIEW_CNT = resultModel.VIEW_CNT;
            NoticeUserView M = new NoticeUserView
            {
                IDX = IDX,
                searchOption = searchModel,
                Notice = model
            };
            return View(M);
        }

        //회원혜택
        public ActionResult Benefit()
        {
            return View();
        }

        //매장안내
        public ActionResult Store()
        {
            return View();
        }
    }
}