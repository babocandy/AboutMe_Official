using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Recallbbs;
using AboutMe.Domain.Entity.Recallbbs;
using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.Controllers.Mypage
{
    [RoutePrefix("MyPage/Recall")]
    [Route("{action=index}")]
    [NoMemberAuthorize]
    public class RecallController : BaseMobileController
    {
         private IRecallbbsService _recallservice;

         public RecallController(IRecallbbsService _recallservice)
        {
            this._recallservice = _recallservice;
        }

        // GET: Recall
         public ActionResult Index(RECALLBBS_SEARCH param)
        {
            if (_user_profile.IS_LOGIN)
            {
                param.order_code = "";
                param.reg_id = _user_profile.M_ID;
            }
            else
            {
                param.order_code = _user_profile.NOMEMBER_ORDER_CODE;
                param.reg_id = "";
            }

            param.pagesize = 10;
            List<SP_TB_RECALL_BBS_SEL_Result> lst = new List<SP_TB_RECALL_BBS_SEL_Result>();
            if (_user_profile.IS_LOGIN == true)
            {
                
            } 
            
            lst = _recallservice.RecallList(param);
            
            RECALL_INDEX M = new RECALL_INDEX();
            M.SearchParam = param;
            M.BbsList = lst;
            M.BbsCount = _recallservice.RecallCount(param);
            
            return View(M);
        }

         // GET: RecallView
         public ActionResult RecallView(int IDX, RECALLBBS_SEARCH param)
         {

             string ORDER_CODE = _user_profile.NOMEMBER_ORDER_CODE;
             string M_ID = _user_profile.M_ID;

             if (_user_profile.IS_LOGIN)
             {
                 ORDER_CODE = "";
             }
             else
             {
                 M_ID = "";
             }

             SP_TB_RECALL_BBS_VIEW_Result recallView = _recallservice.RecallView(IDX, M_ID, ORDER_CODE);
             RECALL_VIEW M = new RECALL_VIEW {
                 SearchParam = param,
                 Bbsview = recallView,
                 Mode = ""
             }; 
             
            return View(M);
        }

         // 수정
         public ActionResult RecallEdit(int IDX, RECALLBBS_SEARCH param)
         {
             string ORDER_CODE = _user_profile.NOMEMBER_ORDER_CODE;
             string M_ID = _user_profile.M_ID;

             if (_user_profile.IS_LOGIN)
             {
                 ORDER_CODE = "";
             }
             else
             {
                 M_ID = "";
             }

             SP_TB_RECALL_BBS_VIEW_Result recallView = _recallservice.RecallView(IDX, M_ID, ORDER_CODE);


             RECALL_VIEW M = new RECALL_VIEW
             {
                 SearchParam = param,
                 Bbsview = recallView,
                 Mode = "EDIT"
             };
             return View("RecallWrite", M);
         }

         // 작성
         public ActionResult RecallWrite(string order_code, string order_detail_idx, RECALLBBS_SEARCH param)
         {
             string ORDER_CODE = _user_profile.NOMEMBER_ORDER_CODE;
             string M_ID = _user_profile.M_ID;

             if (_user_profile.IS_LOGIN)
             {
                 ORDER_CODE = "";
             }
             else
             {
                 M_ID = "";
             }

             SP_TB_RECALL_BBS_VIEW_Result recallView = new SP_TB_RECALL_BBS_VIEW_Result();
             if (!string.IsNullOrEmpty(ORDER_CODE))
             {
                 recallView.ORDER_CODE = ORDER_CODE;
             }
             else
             {
                 recallView.ORDER_CODE = order_code;
             }
             recallView.REG_ID = M_ID;
             recallView.ORDER_DETAIL_IDX = Convert.ToInt32(order_detail_idx);

             RECALL_VIEW M = new RECALL_VIEW
             {
                 SearchParam = param,
                 Bbsview = recallView,
                 Mode = "NEW"
             };
             return View("RecallWrite", M);
         }

         [HttpPost] 
         public ActionResult RecallAction(TB_RECALL_BBS itemRecall, RECALLBBS_SEARCH param , string mode)
         {
             string ORDER_CODE = _user_profile.NOMEMBER_ORDER_CODE;
             string M_ID = _user_profile.M_ID;

             if (_user_profile.IS_LOGIN)
             {
                 ORDER_CODE = "";
             }
             else
             {
                 M_ID = "";
             }

             itemRecall.REG_ID = M_ID;

             if (!string.IsNullOrEmpty(ORDER_CODE))
             {
                 itemRecall.ORDER_CODE = ORDER_CODE;
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