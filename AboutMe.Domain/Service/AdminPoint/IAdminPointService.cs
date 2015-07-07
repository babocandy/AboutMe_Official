using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AboutMe.Domain.Entity.AdminPoint;

namespace AboutMe.Domain.Service.AdminPoint
{
    public interface IAdminPointService
    {
        List<SP_POINT_MEMBER_SEL_Result> GetMemberList(int pageNo, int pageSize, string searchKey, string searchValue);
        int GeMemberListCnt(string searchKey, string searchValue);
        Tuple<string, string> UpdateMemberPointSave(string mid, int point, string addition, int? orderIdx = null, int? revieweIdx = null);
        Tuple<string, string> UpdateMemberPointUse(string mid, int point, string addition = null, int? orderIdx = null);
    }
}
