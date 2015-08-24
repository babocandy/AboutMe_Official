using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.Winner;
using System.Data.Entity.Core.Objects;

namespace AboutMe.Domain.Service.Winner
{
    public class WinnerService : IWinnerService
    {
        #region List 
        public List<SP_TB_WINNER_SEL_Result> WinnerList(WINNER_SEARCH param)
        {
            List<SP_TB_WINNER_SEL_Result> lst = new List<SP_TB_WINNER_SEL_Result>();
            using (WinnerEntities EfContext = new WinnerEntities())
            {
                lst = EfContext.SP_TB_WINNER_SEL("","","Y","IDX", "DESC",param.Page,param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Count
        public int WinnerCount(WINNER_SEARCH param)
        {
            int? result;
            using (WinnerEntities EfContext = new WinnerEntities())
            {
                result = EfContext.SP_TB_WINNER_CNT("","","Y").DefaultIfEmpty(0).FirstOrDefault();
            }
            
            return Convert.ToInt16(result);
        }
        #endregion

       
        #region View
        public SP_TB_WINNER_VIEW_Result WinnerView(int IDX)
        {
            SP_TB_WINNER_VIEW_Result view;
            using (WinnerEntities EfContext = new WinnerEntities())
            {
                view = EfContext.SP_TB_WINNER_VIEW(IDX).FirstOrDefault();
            }
            return view;
        }
        #endregion

        #region Insert
        public int WinnerAdminInsert(TB_WINNER itemWinner)
        {
            ObjectParameter new_idx = new ObjectParameter("NEW_IDX", typeof(int));

            using (WinnerEntities EfContext = new WinnerEntities())
            {
                EfContext.SP_TB_WINNER_INS(itemWinner.TITLE, itemWinner.CONTENTS, itemWinner.DISPLAY_YN, itemWinner.M_ID, new_idx);
            }
            return Convert.ToInt16(new_idx.Value);
        }
        #endregion

        #region Admin Update
        public void WinnerAdminUpdate(int IDX, TB_WINNER itemWinner)
        {
            ObjectParameter ErrCode = new ObjectParameter("ERR_CODE", typeof(string));
            using (WinnerEntities EfContext = new WinnerEntities())
            {
                EfContext.SP_TB_WINNER_UPD(IDX, itemWinner.TITLE, itemWinner.CONTENTS, itemWinner.DISPLAY_YN, itemWinner.M_ID, ErrCode);
            }
        }
        #endregion

        #region Admin Delete
        public void WinnerAdminDelete(int IDX, string M_ID)
        {
            ObjectParameter eRR_CODE = new ObjectParameter("eRR_CODE", typeof(string));
            using (WinnerEntities EfContext = new WinnerEntities())
            {
                EfContext.SP_TB_WINNER_DEL(IDX, eRR_CODE);
            }
        }
        #endregion

        #region Admin List
        public List<SP_TB_WINNER_SEL_Result> WinnerAdminList(WINNER_ADMIN_SEARCH param)
        {
            if (!param.Page.HasValue || param.Page == 0)
                param.Page = 1;
            if (!param.PageSize.HasValue || param.PageSize == 0)
                param.PageSize = 10;

            List<SP_TB_WINNER_SEL_Result> lst = new List<SP_TB_WINNER_SEL_Result>();
            using (WinnerEntities EfContext = new WinnerEntities())
            {
                lst = EfContext.SP_TB_WINNER_SEL(param.SearchCol, param.SearchKeyword, param.DisplayYn, "IDX", "DESC", param.Page, param.PageSize).ToList();
            }

            return lst;

        }
        #endregion

        #region Admin Count
        public int WinnerAdminCount(WINNER_ADMIN_SEARCH param)
        {
            int? result = 0;
            using (WinnerEntities EfContext = new WinnerEntities())
            {
                result = EfContext.SP_TB_WINNER_CNT(param.SearchCol, param.SearchKeyword, param.DisplayYn).DefaultIfEmpty(0).FirstOrDefault();
            }
            return Convert.ToInt16(result);
        }
        #endregion

    }
}
