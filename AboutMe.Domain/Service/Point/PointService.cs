using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.Entity.Core.Objects;

using System.Diagnostics;

using AboutMe.Domain.Entity.Point;

namespace AboutMe.Domain.Service.Point
{
    public class PointService : IPointService
    {
        /**
         * 회원 포인트 내역 조회
         * 
         * @ mid = 회원아이디
         * @ pageNo = 페이지번호
         * @ pageSize = 페이지당 글의 수
         * 
         */

        public Tuple<List<SP_POINT_MY_HISTORY_SEL_Result>, int> GetMyPointHistoryList(MyPointRouteParam p)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));
            ObjectParameter totalPoint = new ObjectParameter("TOTAL_POINT", typeof(int));

            List<SP_POINT_MY_HISTORY_SEL_Result> lst = new List<SP_POINT_MY_HISTORY_SEL_Result>();


            using (PointEntities context = new PointEntities())
            {
                lst = context.SP_POINT_MY_HISTORY_SEL(p.M_ID, p.PAGE, p.PAGE_SIZE, totalPoint, retNum, retMsg).ToList();
            }

            Tuple<List<SP_POINT_MY_HISTORY_SEL_Result>, int> tp = new Tuple<List<SP_POINT_MY_HISTORY_SEL_Result>, int>(lst, Convert.ToInt32(totalPoint.Value));

            return tp;
        }

        /**
         * 회원 포인트 내역 총수
         * 
         * @ mid = 회원아이디
         */
        public int GetMyPointHistoryListCnt(MyPointRouteParam p)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));
            

            int ret = 0;
            using (PointEntities context = new PointEntities())
            {
                int? cnt = context.SP_POINT_MY_HISTORY_CNT(p.M_ID, retNum, retMsg).SingleOrDefault();
                ret = cnt ?? 0;
            }

            return ret;
        }

        public int GetMyPointTotal(string mid)
        {
            ObjectParameter saveTotal = new ObjectParameter("GET_TOTAL_SAVE_POINT", typeof(int));

            using (PointEntities context = new PointEntities())
            {
                context.SP_POINT_SAVE_TOTAL(mid, saveTotal);
            }

            return Convert.ToInt32(saveTotal.Value);
        }
    }
}
