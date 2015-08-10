using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data.Entity.Core.Objects;

using AboutMe.Domain.Entity.Point;

namespace AboutMe.Domain.Service.Point
{
    public interface IPointService
    {
        //List<SP_POINT_MY_HISTORY_SEL_Result> GetMyPointHistoryList(string mid, int pageNo = 1, int pageSize = 10);
        //List<SP_POINT_MY_HISTORY_SEL_Result> GetMyPointHistoryList(MyPointRouteParam p);
        Tuple<List<SP_POINT_MY_HISTORY_SEL_Result>, int> GetMyPointHistoryList(MyPointRouteParam p);
        int GetMyPointHistoryListCnt(MyPointRouteParam p);
        int GetMyPointTotal(string mid);
    }
}
