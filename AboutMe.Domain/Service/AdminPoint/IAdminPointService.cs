﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AboutMe.Domain.Entity.AdminPoint;

namespace AboutMe.Domain.Service.AdminPoint
{
    public interface IAdminPointService
    {
        List<SP_POINT_MEMBER_SEL_Result> GetMemberList(int pageNo = 1, int pageSize = 10, string searchKey = null, string searchValue = null);
        int GeMemberListCnt(string searchKey, string searchValue);

        List<SP_ADMIN_POINT_HISTORY_SEL_Result> GetMyPointHistoryList(string mid, int pageNo = 1, int pageSize = 10);
        int GetMyPointHistoryListCnt(string mid);

        SP_POINT_MEMBER_PROFILE_Result GetMemberProfile(string mid);

        Tuple<string, string> UpdateMemberPointSave(string mid, int point, string addition = null, string adminId = null, string adminName = null, string orderCode = null, int? revieweIdx = null);
        Tuple<string, string> UpdateMemberPointUse(string mid, int point, string addition = null, string adminId = null, string adminName = null, string orderCode = null);

        
        Tuple<string, string> UsePointOnOrder(string mid, int point, string orderCode);
        Tuple<string, string> CancelAllOfOrder(string mid, int point, string orderCode);
        Tuple<string, string> CancelPartOfOrder(string mid, int point, string orderCode, int? orderDetailIndx, string productName);

        Tuple<string, string> SavePointAfterFirmOrder(string mid, int? point, string orderCode, int? orderDetailIndx, string productName);
        Tuple<string, string> ResaveUsedPointOnCancelAfterFirmOrder(string mid, int? point, string orderCode, int? orderDetailIndx, string productName);
        Tuple<string, string> GetBackSavedPointOnCancelAfterFirmOrder(string mid, int? point, string orderCode, int? orderDetailIndx, string productName);
    }
}
