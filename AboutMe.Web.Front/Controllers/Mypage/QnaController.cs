using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Domain.Service.Qna;
using AboutMe.Domain.Entity.Qna;

namespace AboutMe.Web.Front.Controllers.Mypage
{

    [RoutePrefix("MyPage/Qna")]
    [Route("{action=index}")]
    public class QnaController : BaseFrontController
    {
        private IQnaService _qnaservice;

        public QnaController(IQnaService _qnaservice)
        {
            this._qnaservice = _qnaservice;
        }


        // GET: Qna
        public ActionResult Index(QNA_SEARCH param)
        {
            if (_user_profile.IS_LOGIN == true)
            {
                param.reg_id = _user_profile.M_ID;
            } 
            List<SP_TB_QNA_SEL_Result> lst = new List<SP_TB_QNA_SEL_Result>(); 
            lst = _qnaservice.QnaList(param);

            QNA_INDEX M = new QNA_INDEX();
            M.SearchParam = param;
            M.QnaList = lst;
            M.QnaCount = _qnaservice.QnaCount(param);
            return View(M);
        }

        public ActionResult QnaDelete(int IDX)
        {
            QNA_SEARCH param = new QNA_SEARCH(); 
            
            _qnaservice.QnaDelete(IDX); 
            return RedirectToAction("Index", param);
        }

        public ActionResult QnaWrite(int IDX , string mode)
        {

            SP_TB_QNA_VIEW_Result view = new SP_TB_QNA_VIEW_Result();
            if (IDX > 0) { 
                view =_qnaservice.QnaView(IDX);
            }
            QNA_INDEX M = new QNA_INDEX();
            M.mode = mode;
            M.QnaView = view;
            return View(M);
        }
         [HttpPost]
        public ActionResult QnaAction(TB_QNA itemQna, QNA_SEARCH param, string mode)
         {
             if (itemQna.M_ID == null)
             {
                 itemQna.M_ID = "test_b1s";    //임시
             } 
             if (mode.Equals("NEW"))
             {
                 _qnaservice.QnaInsert(itemQna);
             }
             else if (mode.Equals("EDIT"))
             {
                 _qnaservice.QnaUpdate(itemQna); 
             }
             
             return RedirectToAction("Index", param);
         } 

    }
}