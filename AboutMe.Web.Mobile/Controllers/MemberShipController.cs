using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using AboutMe.Common.Helper;

using AboutMe.Web.Mobile.Common;

using AboutMe.Domain.Service.Cart;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;



using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.Controllers
{
    public class MemberShipController : BaseMobileController
    {
        private IMemberService _MemberService;
        private ICartService _Cartservice;


        public MemberShipController(IMemberService _memberService, ICartService _cartservice)
        {
            this._MemberService = _memberService;
            this._Cartservice = _cartservice;
        }


        //모바일 사용자 로그인 폼
        public ActionResult Login(string RedirectUrl = "", string OrderList = "")
        {

            if (!string.IsNullOrEmpty(OrderList))
            {
                ViewBag.isOrderLogin = "true";
                ViewBag.OrderList = OrderList; //주문쪽상품데이터
            }
            else
            {
                ViewBag.isOrderLogin = "false";
                ViewBag.OrderList = "";
            }

            this.ViewBag.RedirectUrl = RedirectUrl;

            this.ViewBag.M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade();
            this.ViewBag.M_EMAIL = MemberInfo.GetMemberEmail();



            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + HttpContext.Request.Url.Host; //ex)https://www.aboutme.co.kr
            if (HttpContext.Request.Url.Port != 80)
                strHTTPS_DOMAIN = strHTTPS_DOMAIN + ":" + HttpContext.Request.Url.Port.ToString();

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;  //로그인시 사용됨.


            //test ---------
            this.ViewBag.Request_Url_Host = Request.Url.Host;
            this.ViewBag.Request_Url_Authority = Request.Url.Authority;
            this.ViewBag.Request_Url_Port = Request.Url.Port;
            this.ViewBag.DomainName = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);


            return View();
        }

        //사용자 로그인
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginProc(string ID = "", string PW = "", string RedirectUrl = "", string OrderList = "")
        {
            //string HTTP_DOMAIN = Config.GetConfigValue("HTTP_DOMAIN");
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority;  // ==> http://현재doamin[:port]
            UrlHelper uh = new UrlHelper(this.ControllerContext.RequestContext);  //https ->  http 로 변경 적용시

            //로그 기록 준비
            string memo = "모바일사용자-로그인";
            string comment = "모바일사용자-로그인";
            //memo = memo + "|ID:" + ID;
            //memo = memo + "|PW:" + PW;
            //memo = memo + "|RedirectUrl:" + RedirectUrl;
            //UserLog userlog = new UserLog();
            //userlog.UserLogSave(memo, comment);




            //1.넘어온 인자값 확인 
            if (ID == "" || PW == "")
            {
                //this.ViewBag.ERR_CODE = "1";
                //this.ViewBag.ERR_MSG = "로그인 실패!. 아이디 or 패스워드가 전달되지 않았습니다.";
                //return RedirectToAction("Login", "Member", new { ERR_CODE = 1, ERR_MSG = "로그인 실패! 아이디 or 패스워드가 전달되지 않았습니다." }); // 로그인 실패
                return Content("<script language='javascript' type='text/javascript'>alert('아이디 or 패스워드가 전달되지 않았습니다.');history.go(-1);</script>");
            }

            AES256Cipher objEnc = new AES256Cipher();
            //string ENC_key = "abcdefghijklmnopqrstuvwxyz123456"; // 
            //string ENC_key = Config.GetConfigValue("AES256_KEY"); //암호화에 필요한 기본키값을 가져온다.
            string M_PWD_MD5_HASH = objEnc.MD5Hash(PW);   //MD5: PW -> M_PWD_MD5_HASH
            string M_PWD_SHA256_HASH = objEnc.SHA256Hash(M_PWD_MD5_HASH);   //MD5: M_PWD_MD5_HASH -> M_PWD_SHA256_HASH


            //2.DB조회
            //List<SP_MEMBER_VIEW_Result> result_list = _MemberService.GetMemberView(ID);
            SP_MEMBER_VIEW_Result result = _MemberService.GetMemberView(ID);
            if (result == null)
            {
                memo = "모바일사용자-로그인실패-계정부재";
                comment = "모바일사용자-로그인실패-계정부재";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                UserLog userlog = new UserLog();
                userlog.UserLogSave(memo, comment);


                return Content("<script language='javascript' type='text/javascript'>alert('아이디가 존재하지 않습니다.');history.go(-1);;</script>");
            }

            if (result.M_PWD != M_PWD_SHA256_HASH)  //인코딩 비교 필요
            {
                memo = "모바일사용자-로그인실패-패스워드불일치";
                comment = "모바일사용자-로그인실패-패스워드불일치";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                UserLog userlog = new UserLog();
                userlog.UserLogSave(memo, comment);


                return Content("<script language='javascript' type='text/javascript'>alert('아이디 or 패스워드가 일치하지 않습니다.');history.go(-1);</script>");
            }
            else
            {
                this.ViewBag.ERR_CODE = "0";
                this.ViewBag.ERR_MSG = "로그인 성공!";
                //return RedirectToAction("Index", "AdminUser"); // 로그인 성공

                //사용자 세션or 쿠키 저장
                CookieSessionStore cookiesession = new CookieSessionStore();
                cookiesession.SetSecretSession("M_ID", result.M_ID);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_NAME", result.M_NAME);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_GRADE", result.M_GRADE);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_EMAIL", result.M_EMAIL);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_PHONE", result.M_PHONE);  //로그인 세션 세팅
                cookiesession.SetSecretSession("M_GBN", result.M_GBN);  //로그인 세션 세팅

                cookiesession.SetSecretSession("M_SKIN_TROUBLE_CD", result.M_SKIN_TROUBLE_CD);  //로그인 세션 세팅

                cookiesession.SetSession("M_SKIN_TROUBLE_CD_TEXT", result.M_SKIN_TROUBLE_CD);  //로그인 세션 세팅 -평문


                _Cartservice.CartMerge(result.M_ID, _user_profile.SESSION_ID, "N");  //로그인후 장바구니 합치기 : 서비스 호출  << 지젠 요청 :  HttpContext.Session.SessionID???


                memo = "모바일사용자-로그인성공";
                comment = "모바일사용자-로그인성공";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                UserLog userlog = new UserLog();
                userlog.UserLogSave(memo, comment);

                //https 제거 처리 start------------- SSL적용후 테스트 필요--------------------------

                if (RedirectUrl != "")
                {
                    if (RedirectUrl.Substring(0, 8) == "https://")
                    {
                        RedirectUrl = "http://" + RedirectUrl.Substring(8);
                    }
                    if (RedirectUrl.Substring(0, 1) == "/")
                    {
                        RedirectUrl = strHTTP_DOMAIN + RedirectUrl;
                    }
                }

                //https 제거 처리 end-------------


                if (!string.IsNullOrEmpty(OrderList))
                {
                    //개발시
                    //return RedirectToAction("InsertOrderStep1", "Order", new { OrderList = OrderList }); // 주문페이지로이동

                    //https ->  http 로 변경 적용시
                    //UrlHelper uh = new UrlHelper(this.ControllerContext.RequestContext);
                    return Redirect(uh.Action("InsertOrderStep1", "Order", null, "http")); // 주문페이지로이동
                }
                else
                {
                    if (RedirectUrl != "")
                        return Content("<script language='javascript' type='text/javascript'>location.href='" + RedirectUrl + "';</script>");  //이미 https -> http로 변경괸 상태
                }

            }


            //개발시
            //return RedirectToAction("Main", "Mypage"); // 로그인 성공

            //https ->  http 로 변경 적용시
            //UrlHelper uh = new UrlHelper(this.ControllerContext.RequestContext);
            return Redirect(uh.Action("Main", "Mypage", null, "http"));

        }

    } //class
} //namespace