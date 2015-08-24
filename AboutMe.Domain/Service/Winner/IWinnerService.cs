using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Winner;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Winner
{
    public interface IWinnerService
    {
        //User
        List<SP_TB_WINNER_SEL_Result> WinnerList(WINNER_SEARCH param);
        int WinnerCount(WINNER_SEARCH param);
        SP_TB_WINNER_VIEW_Result WinnerView(int IDX);

        //Admin
        int WinnerAdminInsert(TB_WINNER itemWinner);
        void WinnerAdminUpdate(int IDX, TB_WINNER itemWinner);
        void WinnerAdminDelete(int IDX, string M_ID);
        List<SP_TB_WINNER_SEL_Result> WinnerAdminList(WINNER_ADMIN_SEARCH param);
        int WinnerAdminCount(WINNER_ADMIN_SEARCH param);

    }
}
