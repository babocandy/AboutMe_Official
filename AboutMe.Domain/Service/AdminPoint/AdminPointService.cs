using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminPoint;
using System.Data.Entity.Core.Objects;

using System.Diagnostics;

namespace AboutMe.Domain.Service.AdminPoint
{
    public class AdminPointService : IAdminPointService
    {
        //관리자 목록
        public List<SP_POINT_MEMBER_SEL_Result> GetMemberList(int pageNo, int pageSize, string searchKey, string searchValue)
        {

            List<SP_POINT_MEMBER_SEL_Result> lst = new List<SP_POINT_MEMBER_SEL_Result>();
            using (AdminPointEntities context = new AdminPointEntities())
            {
                try
                {
                    lst = context.SP_POINT_MEMBER_SEL(pageNo, pageSize, searchKey, searchValue).ToList();
                }
                catch (Exception ex)
                {
                    context.Dispose();
                }
            }

            return lst;

        }

        public int GeMemberListCnt(string searchKey, string searchValue)
        {
            int ret = 0;
            using (AdminPointEntities context = new AdminPointEntities())
            {
                int? cnt = context.SP_POINT_MEMBER_CNT(searchKey, searchValue).SingleOrDefault();
                ret = cnt ?? 0;
            }

            return ret;
        }

        public Tuple<string, string> UpdateMemberPointUse(string mid, int point, string addition = null, int? orderIdx = null)
        {

            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_ADMIN_POINT_USE(mid, point, addition, orderIdx, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UpdateMemberPointSave retNum:  " + retNum.Value);
            Debug.WriteLine("UpdateMemberPointSave retMsg:  " + retMsg.Value);

            return tp;
        }

        public Tuple<string, string> UpdateMemberPointSave(string mid, int point, string addition, int? orderIdx = null, int? revieweIdx = null)
        {
            Debug.WriteLine("UpdateMemberPointSaveUpdateMemberPointSave");

            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_ADMIN_POINT_SAVE(mid, point, addition, orderIdx, revieweIdx, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UpdateMemberPointSave retNum:  " + retNum.Value);
            Debug.WriteLine("UpdateMemberPointSave retMsg:  " + retMsg.Value);

            return tp;
        }
    }
}
