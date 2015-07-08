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
        public List<SP_POINT_MEMBER_SEL_Result> GetMemberList(int pageNo=1, int pageSize = 10, string searchKey=null, string searchValue=null)
        {

            List<SP_POINT_MEMBER_SEL_Result> lst = new List<SP_POINT_MEMBER_SEL_Result>();
            using (AdminPointEntities context = new AdminPointEntities())
            {
                lst = context.SP_POINT_MEMBER_SEL(pageNo, pageSize, searchKey, searchValue).ToList();
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
            Debug.WriteLine("UpdateMemberPointUse retNum:  " + retNum.Value);
            Debug.WriteLine("UpdateMemberPointUse retMsg:  " + retMsg.Value);

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

        public List<SP_ADMIN_POINT_HISTORY_SEL_Result> GetMyPointHistoryList(string mid, int pageNo = 1, int pageSize = 10)
        {
            List<SP_ADMIN_POINT_HISTORY_SEL_Result> lst = new List<SP_ADMIN_POINT_HISTORY_SEL_Result>();

            using (AdminPointEntities context = new AdminPointEntities())
            {
                lst = context.SP_ADMIN_POINT_HISTORY_SEL(mid,pageNo,pageSize).ToList();
            }

            return lst;
        }

        public int GetMyPointHistoryListCnt(string mid)
        {
            int ret = 0;
            using (AdminPointEntities context = new AdminPointEntities())
            {
                int? cnt = context.SP_ADMIN_POINT_HISTORY_CNT(mid).SingleOrDefault();
                ret = cnt ?? 0;
            }

            return ret;
        }

        public Tuple<string, string> SavePointOnOrder(string mid, int amount, int orderIdx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_SAVE_ON_ORDER(mid, amount, orderIdx, retNum, retMsg);

            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("SavePointOnOrder retNum:  " + retNum.Value);
            Debug.WriteLine("SavePointOnOrder retMsg:  " + retMsg.Value);

            return tp;
        }


        public Tuple<string, string> UsePointOnOrder(string mid, int point, int orderIdx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_USE_ON_ORDER(mid, point, orderIdx, retNum, retMsg);

            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UsePointOnOrder retNum:  " + retNum.Value);
            Debug.WriteLine("UsePointOnOrder retMsg:  " + retMsg.Value);

            return tp;
        }

        public Tuple<string, string> CancelAllOfOrder(string mid, int point, int orderIdx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_CANCEL_ALL_ORDER(mid, point, orderIdx, retNum, retMsg);

            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("CancelAllOfOrder retNum:  " + retNum.Value);
            Debug.WriteLine("CancelAllOfOrder retMsg:  " + retMsg.Value);

            return tp;
        }

        public Tuple<string, string> CancelPartOfOrder(string mid, int point, int orderIdx)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_CANCEL_PART_ORDER(mid, point, orderIdx, retNum, retMsg);

            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("CancelParrOfOrder retNum:  " + retNum.Value);
            Debug.WriteLine("CancelParrOfOrder retMsg:  " + retMsg.Value);

            return tp;
        }

    }
}
