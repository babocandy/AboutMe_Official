using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Recallbbs;
using AboutMe.Domain.Entity.Recallbbs;

namespace AboutMe.Web.Front.Controllers.Mypage
{
    [RoutePrefix("MyPage/Recall")]
    [Route("{action=index}")]
    public class RecallController : BaseFrontController
    {
         private IRecallbbsService _recallservice;

         public RecallController(IRecallbbsService _recallservice)
        {
            this._recallservice = _recallservice;
        }

        // GET: Recall
         public ActionResult Index(RECALLBBS_SEARCH param)
        {
            param.pagesize = 10;
            List<SP_TB_RECALL_BBS_SEL_Result> lst = new List<SP_TB_RECALL_BBS_SEL_Result>();
            if (_user_profile.IS_LOGIN == true)
            {
                param.reg_id = _user_profile.M_ID;
            } 
            if (param.dateType.Equals("1")){
                if (param.start_date == "" && param.end_date == ""){
                    param.start_date = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    param.end_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (param.dateType.Equals("2")){
                if (param.start_date == "" && param.end_date == "")
                {
                    param.start_date = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                    param.end_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (param.dateType.Equals("3"))
            {
                if (param.start_date == "" && param.end_date == "")
                {
                    param.start_date = DateTime.Now.AddMonths(-4).ToString("yyyy-MM-dd");
                    param.end_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (param.dateType.Equals("4"))
            {
                if (param.start_date == "" && param.end_date == "")
                {
                    param.start_date = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                    param.end_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
            } 
            
            lst = _recallservice.RecallList(param);
            
            RECALL_INDEX M = new RECALL_INDEX();
            M.SearchParam = param;
            M.BbsList = lst;
            M.BbsCount = _recallservice.RecallCount(param);
            
            return View(M);
        }

         // GET: RecallView
         public ActionResult RecallView(int IDX)
         {
             SP_TB_RECALL_BBS_VIEW_Result recallView = _recallservice.RecallView(IDX); 
             RECALL_INDEX M = new RECALL_INDEX(); 
             M.Bbsview = recallView;
            return View(M);
        }

         // GET: RecallWrite
         public ActionResult RecallWrite(TB_RECALL_BBS itemRecall, string mode)
         {
             SP_TB_RECALL_BBS_VIEW_Result recallView = null; 
             if (itemRecall.IDX > 0)
             {
                 recallView = _recallservice.RecallView(itemRecall.IDX);
                 itemRecall.GUBUN = recallView.GUBUN;
                 itemRecall.ORDER_CODE = recallView.ORDER_CODE;
                 itemRecall.ORDER_DETAIL_IDX = recallView.ORDER_DETAIL_IDX;
                 itemRecall.TITLE = recallView.TITLE;
                 itemRecall.CONTENTS = recallView.CONTENTS;
                 itemRecall.CONFIRM_CONTENTS = recallView.CONFIRM_CONTENTS;
                 if (_user_profile.IS_LOGIN == true)
                 {
                     itemRecall.ADM_ID = _user_profile.M_ID;
                 }

             }

             RECALL_INDEX M = new RECALL_INDEX();
             M.model = itemRecall;
             M.mode = mode;
             return View(M);
         }

         [HttpPost] 
         public ActionResult RecallAction(TB_RECALL_BBS itemRecall, RECALLBBS_SEARCH param , string mode)
         {
             if (itemRecall.REG_ID == null)
             {
                 itemRecall.REG_ID = "test_b1s";    //임시
             } 
             if (mode.Equals("NEW"))
             {
                 _recallservice.RecallBbsInsert(itemRecall); 
             }
             else if (mode.Equals("EDIT"))
             {
                 _recallservice.RecallBbsUpdate(itemRecall); 
             }
             
             return RedirectToAction("Index", param);
         }


    }
}