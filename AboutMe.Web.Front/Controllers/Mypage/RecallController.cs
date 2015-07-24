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
            List<SP_TB_RECALL_BBS_SEL_Result> lst = new List<SP_TB_RECALL_BBS_SEL_Result>();
            if (_user_profile.IS_LOGIN == true)
            {
                param.reg_id = _user_profile.M_ID;
            }
            lst = _recallservice.RecallList(param);
            
            RECALL_INDEX M = new RECALL_INDEX();
            M.SearchParam = param;
            M.BbsList = lst;
            
            return View(M);
        }

        // GET: Recall
        public ActionResult RecallView()
        {
            return View();
        }
    }
}