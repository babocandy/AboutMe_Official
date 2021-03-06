﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AboutMe.Domain.Entity.AdminPoint;

namespace AboutMe.Web.Admin.Models
{
    public class AdminPointMemberViewModel
    {
        public IList<SP_POINT_MEMBER_SEL_Result> MemberList { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string SearchKey { get; set; }
        public string SearchValue { get; set; }
        public int TotalItem { get; set; }
    }

    public class AdminPointInsertViewModel
    {
        [Required(ErrorMessage = "필수사항입니다")]
        public string Mid { get; set; }
        
        [Required(ErrorMessage = "필수사항입니다")]
        public string Type { get; set; }

        [StringLength(150, ErrorMessage = "메모는 150자 이상을 초과할 수 없습니다.")]
        [Required(ErrorMessage = "필수사항입니다")]
        public string Reason { get; set; }
        
        [Required(ErrorMessage = "필수사항입니다")]
        public int Point { get; set; }

        public string AdminId { get; set; }
        public string AdminName { get; set; }
        public SP_POINT_MEMBER_PROFILE_Result MemberProfile { get; set; }

        public string ResultNum { get; set; }
        public string ResultMessage { get; set; }

    }

    public class AdminMyPointHistoryViewModel
    {
        public IList<SP_ADMIN_POINT_HISTORY_SEL_Result> PointHistoryList { get; set; }
        public string Mid { get; set; }
        public int PageNo { get; set; }
        public int TotalItem { get; set; }
    }
}