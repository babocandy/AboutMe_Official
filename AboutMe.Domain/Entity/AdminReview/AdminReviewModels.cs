﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutMe.Domain.Entity.AdminReview
{
    public class AdminReviewRouteParam
    {
        private int? _page;

        public int? PAGE
        {
            get
            {
                return _page ?? 1;
            }
            set
            {
                _page = value;
            }
        }

        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }
        
        public string MEDIA_GBN { get;set; }
        public string MEDIA_GBN_W { get; set; }
        public string MEDIA_GBN_M { get; set; }

        public string SEL_DATE_FROM { get;set; }
        public string SEL_DATE_TO { get;set; }

        public string IS_PHOTO { get;set; }
        public string IS_PHOTO_Y { get; set; }
        public string IS_PHOTO_N { get; set; }

        public string IS_DISPLAY { get; set; }
        public string IS_DISPLAY_Y { get; set; }
        public string IS_DISPLAY_N { get; set; }
    }

    public class AdminReviewSaveParam{
        public int? IDX { get; set; }
        //public string COMMENT { get; set; }
        public string IS_DISPLAY{get;set;}
        public string IS_BEST{get;set;}
    }

    public class AdminReviewThemaParamToInputDB
    {
        public int? IDX { get; set; }
        public string TITLE { get; set; }
        public string IS_DISPLAY { get; set; }
        public string TAG { get; set; }
    }


    /**
     * 체험단리뷰 마스터 목록,수정,저장시
     */
   
    public class AdminReviewExpMasterParamToInputDB
    {
        public string P_CODE { get; set; }
        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string IS_AUTH { get; set; }
    }

    public class AdminReviewExpMemberParamToInputDB
    {
        public int? MASTER_IDX { get; set; }
        public string M_ID { get; set; }
        public string M_NAME { get; set; }
    }

    public class AdminReviewExpMasterListRouteParam
    {
        private int? _page;
        
        public int? PAGE
        {
            get
            {
                return _page ?? 1;
            }
            set
            {
                _page = value;
            }
        }

        public string SEARCH_KEY { get; set; }
        public string SEARCH_VALUE { get; set; }

        public string FROM_DATE { get; set; }
        public string TO_DATE { get; set; }

        public string IS_STATE_10 { get; set; }
        public string IS_STATE_20 { get; set; }
        public string IS_STATE_30 { get; set; }

        public string IS_AUTH_Y { get; set; }
        public string IS_AUTH_N { get; set; }
    }
}
