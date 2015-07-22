using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using AboutMe.Common.Helper;

using AboutMe.Web.Front.Common;

using AboutMe.Domain.Service.Cart;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;



using AboutMe.Web.Front.Common.Filters;


namespace AboutMe.Web.Front.Controllers
{
    public class MemberShipController : BaseFrontController
    {
        private IMemberService _MemberService;
        private ICartService _Cartservice;


        public MemberShipController(IMemberService _memberService, ICartService _cartservice)
        {
            this._MemberService = _memberService;
            this._Cartservice = _cartservice;
        }


        //사용자 로그인 폼
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

            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + HttpContext.Request.Url.Host; //ex)https://www.aboutme.co.kr
            if ( HttpContext.Request.Url.Port !=80)
                strHTTPS_DOMAIN =strHTTPS_DOMAIN + ":" + HttpContext.Request.Url.Port.ToString();


            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;


            this.ViewBag.RedirectUrl = RedirectUrl;

            this.ViewBag.M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade();
            this.ViewBag.M_EMAIL = MemberInfo.GetMemberEmail();

            //test ---------
            this.ViewBag.Request_Url_Host = Request.Url.Host;
            this.ViewBag.Request_Url_Authority = Request.Url.Authority;
            this.ViewBag.Request_Url_Port = Request.Url.Port;
            this.ViewBag.DomainName = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority);

            return View();
        }

        //사용자 로그인
        public ActionResult LoginProc(string ID = "", string PW = "", string RedirectUrl = "", string OrderList = "")
        {
            //string HTTP_DOMAIN = Config.GetConfigValue("HTTP_DOMAIN");
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority;  // ==> http://현재doamin[:port]

            UrlHelper uh = new UrlHelper(this.ControllerContext.RequestContext);

            //로그 기록
            string memo = "사용자-로그인";
            string comment = "사용자-로그인";
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
                memo = "사용자-로그인실패-계정부재";
                comment = "사용자-로그인실패-계정부재";
                memo = memo + "|ID:" + ID;
                memo = memo + "|PW:" + PW;
                memo = memo + "|RedirectUrl:" + RedirectUrl;
                UserLog userlog = new UserLog();
                userlog.UserLogSave(memo, comment);


                return Content("<script language='javascript' type='text/javascript'>alert('아이디가 존재하지 않습니다.');history.go(-1);;</script>");
            }

            if (result.M_PWD != M_PWD_SHA256_HASH)  //인코딩 비교 필요
            {
                memo = "사용자-로그인실패-패스워드불일치";
                comment = "사용자-로그인실패-패스워드불일치";
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

                //관리자 세션or 쿠키 저장
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
                

                memo = "사용자-로그인성공";
                comment = "사용자-로그인성공";
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

        //비회원 주문
        [HttpPost]
        public ActionResult GoOrder(string OrderList)
        {
            return RedirectToAction("InsertOrderStep1", "Order", new { OrderList = OrderList }); // 주문페이지로이동
        }

        //사용자 로그아웃 처리
        public ActionResult Logout()
        {
            string LOGOUT_M_ID = MemberInfo.GetMemberId();  //

            CookieSessionStore cookiesession = new CookieSessionStore();
            cookiesession.SetSecretSession("M_ID", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_NAME", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_NAME_TXT", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_GRADE", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_EMAIL", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_PHONE", "");  //로그인 세션 세팅
            cookiesession.SetSecretSession("M_SKIN_TROUBLE_CD", "");  //로그인 세션 세팅

            cookiesession.SetSession("M_SKIN_TROUBLE_CD_TEXT", "");  //로그인 세션 세팅 -평문

            cookiesession.ClearSession(); //session Abandon


    

            //로그 기록
            string memo = "사용자-로그아웃";
            string comment = "사용자-로그아웃";
            memo = memo + "|M_ID:" + LOGOUT_M_ID;
            UserLog userlog = new UserLog();
            userlog.UserLogSave(memo, comment);



            //return RedirectToAction("Login", "Member"); // 로그인 페이지로 이동

            return Content("<script language='javascript' type='text/javascript'>alert('로그아웃 되었습니다.'); location.href='/MemberShip/Login';</script>");
        }



        //사용자 회원가입- Step1 -실명인증
        public ActionResult JoinStep1()
        {
            return View();
        }
        //사용자 회원가입- Step1 -실명인증 결과 
        public ActionResult RealNameResult(string WORK_TMP_ID = "", string M_JOIN_MODE="")
        {
            if (WORK_TMP_ID == null || WORK_TMP_ID == "" || M_JOIN_MODE == "" || M_JOIN_MODE == null)
                return Content("<script language='javascript' type='text/javascript'>alert('실명인증 정보가 전달되지 않았습니다.'); location.href='/MemberShip/JoinStep1';</script>");

            //실명인증 정상여부 판단.<<<<<<<<<<<<<<<<<<<<<<

            //핸드폰 인증 :M_JOIN_MODE == hp
            string mem_id = HttpContext.Request.Form["mem_id"]; //회원사ID
            string tx_seqno = HttpContext.Request.Form["tx_seqno"];  //서비스거래번호
            string result_cd = HttpContext.Request.Form["result_cd"];  //결과코드 "000"정상
            string result_msg = HttpContext.Request.Form["result_msg"]; 
            string cert_dt_tm = HttpContext.Request.Form["cert_dt_tm"]; 
            string di = HttpContext.Request.Form["di"];   //DI
            string ci = HttpContext.Request.Form["ci"];   //CI
            string name = HttpContext.Request.Form["name"]; //실명
            string birthday = HttpContext.Request.Form["birthday"]; //생일 (YYYYMMDD)
            string gender = HttpContext.Request.Form["gender"]; //성별 0:여자, 1:남자
            string nation = HttpContext.Request.Form["nation"]; //내외국인 1:내국인 ,2 외국인   <<<<<<<<< HP vs  ipin과 차이 주의
            string tel_com_cd = HttpContext.Request.Form["tel_com_cd"]; //통신사 코드 : 01:SKT,02:KTF, 03:LGU+ , 04:알뜰폰SKT ,x
            string tel_no = HttpContext.Request.Form["tel_no"]; //햔드폰번호 "-"없이



            //핸드폰 인증 :M_JOIN_MODE == ipin
            string encPsnlInfo = HttpContext.Request.Form["encPsnlInfo"]; 
            string virtualno = HttpContext.Request.Form["virtualno"];  //가상주민번호 (13)
            string dupinfo = HttpContext.Request.Form["dupinfo"];   //DI  (64) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            string realname = HttpContext.Request.Form["realname"];  //실명 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            string cprequestnumber = HttpContext.Request.Form["cprequestnumber"];  //사이트 세션정보 (17)
            string age = HttpContext.Request.Form["age"]; //연령대 (1)  0:~9세, 1:~12세 ,2:~14세, 3:~15세, 4: ~18세, 5: ~19세, 6:~20세, 7:20세~
            string sex = HttpContext.Request.Form["sex"]; //0 :여자, 1:남자 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            string nationalinfo = HttpContext.Request.Form["nationalinfo"];  //0:내국인, 1:외국인  <<<<<<<<< HP vs  ipin과 차이 주의
            string birthdate = HttpContext.Request.Form["birthdate"];  //생일(8) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            string coinfo1 = HttpContext.Request.Form["coinfo1"];  //CI (88) <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            string coinfo2 = HttpContext.Request.Form["coinfo2"];  //연계정보 -ciupdate 가 짝수일때 넘어옴?? (88)
            string ciupdate = HttpContext.Request.Form["ciupdate"];  //CI 갱신횟수 (1) -고정값"1"
            string cpcode = HttpContext.Request.Form["cpcode"]; 
            string authinfo = HttpContext.Request.Form["authinfo"]; //PIN발급수단 (0:공인인증서,1:카드,2:휴대폰,3:대면,5:..7)

            //DB임시 저장 필요<<<<<<<<<<<<<<<<<<<<<<
            //임시DB저장


            //로그 기록
            string memo = "실명인증";
            string comment = "실명인증";
            memo = memo + "|WORK_TMP_ID:" + WORK_TMP_ID;
            memo = memo + "|M_JOIN_MODE:" + M_JOIN_MODE;
            
            memo = memo + "||휴대폰인증리턴===========:" ;
            memo = memo + "|mem_id:" + mem_id;
            memo = memo + "|tx_seqno:" + tx_seqno;
            memo = memo + "|result_cd:" + result_cd;
            memo = memo + "|result_msg:" + result_msg;
            memo = memo + "|cert_dt_tm:" + cert_dt_tm;
            memo = memo + "|di:" + di;
            memo = memo + "|ci:" + ci;
            memo = memo + "|name:" + name;
            memo = memo + "|birthday:" + birthday;
            memo = memo + "|gender:" + gender;
            memo = memo + "|nation:" + nation;
            memo = memo + "|tel_com_cd:" + tel_com_cd;
            memo = memo + "|tel_no:" + tel_no;

            memo = memo + "||IPIN인증리턴=============:";
            memo = memo + "|encPsnlInfo:" + encPsnlInfo;
            memo = memo + "|virtualno:" + virtualno;
            memo = memo + "|dupinfo:" + dupinfo;
            memo = memo + "|realname:" + realname;
            memo = memo + "|cprequestnumber:" + cprequestnumber;
            memo = memo + "|age:" + age;
            memo = memo + "|sex:" + sex;
            memo = memo + "|nationalinfo:" + nationalinfo;
            memo = memo + "|birthdate:" + birthdate;
            memo = memo + "|coinfo1:" + coinfo1;
            memo = memo + "|coinfo2:" + coinfo2;
            memo = memo + "|ciupdate:" + ciupdate;
            memo = memo + "|cpcode:" + cpcode;
            memo = memo + "|authinfo:" + authinfo;

            UserLog userlog = new UserLog();
            userlog.UserLogSave(memo, comment);

            //실명인증 로그등록
            string argM_JOIN_TYPE = "";
            string argRESULT_CODE = "";
            string argTRAN_NO = "";
            string argM_NAME = "";
            string argdi = "";
            string argci = "";
            string argM_BIRTHDAY = "";
            string argM_SEX = "";
            string argnation = "";
            string argRETURN_MSG_ALL = "";
            string argIP = HttpContext.Request.ServerVariables["REMOTE_ADDR"];

            argM_JOIN_TYPE = M_JOIN_MODE;
            argRETURN_MSG_ALL = memo;
            if (argM_JOIN_TYPE == "hp" || argM_JOIN_TYPE == "HP")
            {
                argRESULT_CODE = result_cd;
                argTRAN_NO = tx_seqno;
                argM_NAME = name;
                argdi = di;
                argci = ci;
                if (birthday.Length==8)
                    argM_BIRTHDAY = birthday.Substring(0, 4) + "-" + birthday.Substring(4, 2) + "-" + birthday.Substring(6, 2);
                else
                    argM_BIRTHDAY = birthday;

                argM_SEX = gender;
                if (gender == "1") //0:여자 ,1:남자
                    argM_SEX = "1"; //남자
                else
                    argM_SEX = "2"; //여자 =>2
                argnation = nation;
            }
            if (argM_JOIN_TYPE == "ipin" || argM_JOIN_TYPE == "IPIN")
            {
                argRESULT_CODE = "";
                argTRAN_NO = cprequestnumber;
                argM_NAME = realname;
                argdi = dupinfo;
                argci = coinfo1;
                if (birthdate.Length == 8)
                    argM_BIRTHDAY = birthdate.Substring(0, 4) + "-" + birthdate.Substring(4, 2) + "-" + birthdate.Substring(6, 2);
                else
                    argM_BIRTHDAY = birthdate;
                argM_SEX = sex;
                if (sex == "1") //0:여자 ,1:남자
                    argM_SEX = "1"; //남자
                else
                    argM_SEX = "2"; //여자 =>2
                argnation = nationalinfo;
            }


            //실명인증 로그 DB저장 -> Step3에서  WORK_TMP_ID로 1시간내 사용
            _MemberService.SetRealNameLogInsert(WORK_TMP_ID, argM_JOIN_TYPE, argRESULT_CODE, argTRAN_NO, argM_NAME, argdi, argci, argM_BIRTHDAY, argM_SEX, argnation, argRETURN_MSG_ALL, argIP);


            //DI기준으로 이미 가입한 적이 있는지?
            ReturnDic retDic = _MemberService.GetMemberFindDI(argdi);
            if (retDic.ERR_CODE =="10") //이미 가입한 실명인증회원임            
            {
                return Content("<script language='javascript' type='text/javascript'>alert('"+ retDic.ERR_MSG +"'); location.href='/MemberShip/Login';</script>");
            }

            if (retDic.ERR_CODE =="20") //DI 파라메타 전달오류        
            {
                return Content("<script language='javascript' type='text/javascript'>alert('"+ retDic.ERR_MSG +"'); location.href='/MemberShip/JoinStep1';</script>");
            }

            if (retDic.ERR_CODE != "0") //DI 기타오류        
            {
                return Content("<script language='javascript' type='text/javascript'>alert('" + retDic.ERR_MSG + "'); history.go(-1);</script>");
            }



            //성공: 다음 스텝 이동<<<<<<<<<<<<<<<<<<<<<<
            return RedirectToAction("JoinStep2", "MemberShip", new { WORK_TMP_ID = WORK_TMP_ID }); // 실명인증 성공 -> Go Step2
        }
        

        //사용자 회원가입- Step2 -약관동의
        public ActionResult JoinStep2(string WORK_TMP_ID="")
        {
            //if (WORK_TMP_ID == null || WORK_TMP_ID == "")
            //    return Content("<script language='javascript' type='text/javascript'>alert('실명인증 정보가 전달되지 않았습니다.'); location.href='/MemberShip/JoinStep1';</script>");

            this.ViewBag.WORK_TMP_ID = WORK_TMP_ID;
            return View();
        }

        //사용자 회원가입- Step3 -회원정보 입력
        public ActionResult JoinStep3(string WORK_TMP_ID = "", string M_AGREE = "N", string M_AGREE2 = "Y")
        {
            if (WORK_TMP_ID == null || WORK_TMP_ID == "")
                return Content("<script language='javascript' type='text/javascript'>alert('실명인증 정보가 전달되지 않았습니다.'); location.href='/MemberShip/JoinStep1';</script>");

            SP_MEMBER_REALNAME_LOG_VIEW_Result row =  _MemberService.GetRealNameLogWorkTmp(WORK_TMP_ID);
            if(row ==null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('실명인증 정보가 전달되지 않았습니다2.'); location.href='/MemberShip/JoinStep1';</script>");
            }
            this.ViewBag.WORK_TMP_ID = WORK_TMP_ID;
            this.ViewBag.M_AGREE = M_AGREE;
            this.ViewBag.M_AGREE2 = M_AGREE2;

            this.ViewBag.M_JOIN_MODE = row.M_JOIN_TYPE;
            this.ViewBag.M_DI = row.di;
            this.ViewBag.M_NAME = row.M_NAME;
            this.ViewBag.M_SEX = row.M_SEX;
            this.ViewBag.M_BIRTHDAY = row.M_BIRTHDAY;

            return View();
        }

        //사용자 회원가입- Step3-1 --ID중복체크
        public ActionResult AjaxID_DUPCheck(string M_ID = "")
        {
            string strERR_CODE = "0";
            string strERR_MSG = "";
            if (M_ID == "")
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('회원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                strERR_CODE = "1";
                strERR_MSG = "회원 아이디가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = strERR_CODE, ERR_MSG = strERR_MSG });
            }

            //DB저장: 회원 가입시 ID중복체크
            ReturnDic rObj = _MemberService.GetMemberID_Dup_Check(M_ID);

            return Json(new { ERR_CODE = rObj.ERR_CODE, ERR_MSG =rObj.ERR_MSG });
           

        }

        //사용자 회원 신규가입 -저장: ajax > JSON리턴
        public ActionResult AjaxJOIN_Register(string M_ID = "")
        {
            //로그 기록 준비
            string log_memo = "회원 신규가입";
            string log_comment = "회원 신규가입";
            UserLog userlog = new UserLog();


            string strERR_CODE = "0";
            string strERR_MSG = "";
            ReturnDic ObjretDIC = new ReturnDic();
            //return View();
            if (M_ID == "")
            {
                log_comment = log_comment + "실패 : param오류";
                log_memo = log_memo + "실패 : param오류";
                log_memo = log_memo + "|M_ID:" + M_ID;
                userlog.UserLogSave(log_memo, log_comment);

                //return Content("<script language='javascript' type='text/javascript'>alert('회원아이디가 전달되지 않았습니다.');history.go(-1);</script>");
                strERR_CODE = "1";
                strERR_MSG = "회원 아이디가 전달되지 않았습니다.";
                return Json(new { ERR_CODE = strERR_CODE, ERR_MSG = strERR_MSG });
            }

            //회원가입 : 입력항목 post
            string M_NAME = HttpContext.Request.Form["M_NAME"]; //
            string M_PWD = HttpContext.Request.Form["M_PWD"]; //
            string M_GRADE = "B"; // 기본등급:브론즈
            string M_SEX = HttpContext.Request.Form["M_SEX"]; //
            string M_BIRTHDAY = HttpContext.Request.Form["M_BIRTHDAY"]; //
            string M_MOBILE = HttpContext.Request.Form["M_MOBILE"]; //
            string M_PHONE = HttpContext.Request.Form["M_PHONE"]; //
            string M_EMAIL = HttpContext.Request.Form["M_EMAIL"]; //
            string M_ZIPCODE = HttpContext.Request.Form["M_ZIPCODE"]; //
            string M_ADDR1 = HttpContext.Request.Form["M_ADDR1"]; //
            string M_ADDR2 = HttpContext.Request.Form["M_ADDR2"]; //
            string M_ISSMS = HttpContext.Request.Form["M_ISSMS"]; //
            string M_ISEMAIL = HttpContext.Request.Form["M_ISEMAIL"]; //
            string M_ISDM = HttpContext.Request.Form["M_ISDM"]; //

            string M_JOIN_MODE = HttpContext.Request.Form["M_JOIN_MODE"]; //
            string M_DI = HttpContext.Request.Form["M_DI"]; //
            string M_AGREE = HttpContext.Request.Form["M_AGREE"]; //
            string M_AGREE2 = HttpContext.Request.Form["M_AGREE2"]; //
            string M_SKIN_TROUBLE_CD =""; //  HttpContext.Request.Form["M_SKIN_TROUBLE_CD"]; // 가입시- 피부트러블 항목 없음

            //임직원 정보 -- 가입시 기본:일반회원
            string M_GBN = "A"; //가입시 기본:일반회원  HttpContext.Request.Form["M_GBN"]; //
            string M_STAFF_COMPANY = ""; //가입시 기본:일반회원
            string M_STAFF_ID = ""; //가입시 기본:일반회원

            AES256Cipher objEnc = new AES256Cipher();
            string M_PWD_MD5_HASH = objEnc.MD5Hash(M_PWD);   //MD5: ADM_PWD -> ADM_PWD_MD5_HASH
            string M_PWD_SHA256_HASH = objEnc.SHA256Hash(M_PWD_MD5_HASH);   //MD5: ADM_PWD_MD5_HASH -> ADM_PWD_SHA256_HASH


            //이미 사용된 DI인지 최종 체크
            //DI기준으로 이미 가입한 적이 있는지?
            ReturnDic retDic = _MemberService.GetMemberFindDI(M_DI);
            if (retDic.ERR_CODE != "0") //DI 기타오류      10:이미 가입한 DI , 20 :Param Err  
            {
                //로그 기록
                log_comment = log_comment + "실패:DI중복오류";
                log_memo = log_memo + "실패:DI중복오류";
                log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
                log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
                log_memo = log_memo + "|M_ID:" + M_ID;
                log_memo = log_memo + "|M_NAME:" + M_NAME;
                log_memo = log_memo + "|M_PWD:" + M_PWD;
                log_memo = log_memo + "|M_PWD_MD5_HASH:" + M_PWD_MD5_HASH;
                log_memo = log_memo + "|M_PWD_SHA256_HASH:" + M_PWD_SHA256_HASH;
                log_memo = log_memo + "|M_GRADE:" + M_GRADE;
                log_memo = log_memo + "|M_SEX:" + M_SEX;
                log_memo = log_memo + "|M_BIRTHDAY:" + M_BIRTHDAY;
                log_memo = log_memo + "|M_PHONE:" + M_PHONE;
                log_memo = log_memo + "|M_EMAIL:" + M_EMAIL;
                log_memo = log_memo + "|M_ZIPCODE:" + M_ZIPCODE;
                log_memo = log_memo + "|M_ADDR1:" + M_ADDR1;
                log_memo = log_memo + "|M_ADDR2:" + M_ADDR2;
                log_memo = log_memo + "|M_ISSMS:" + M_ISSMS;
                log_memo = log_memo + "|M_ISEMAIL:" + M_ISEMAIL;
                log_memo = log_memo + "|M_ISDM:" + M_ISDM;

                log_memo = log_memo + "|M_JOIN_MODE:" + M_JOIN_MODE;
                log_memo = log_memo + "|M_DI:" + M_DI;
                log_memo = log_memo + "|M_AGREE:" + M_AGREE;
                log_memo = log_memo + "|M_AGREE2:" + M_AGREE2;
                log_memo = log_memo + "|M_SKIN_TROUBLE_CD:" + M_SKIN_TROUBLE_CD;

                log_memo = log_memo + "|M_GBN:" + M_GBN;
                log_memo = log_memo + "|M_STAFF_COMPANY:" + M_STAFF_COMPANY;
                log_memo = log_memo + "|M_STAFF_ID:" + M_STAFF_ID;
                userlog.UserLogSave(log_memo, log_comment);

                //return Content("<script language='javascript' type='text/javascript'>alert('" + retDic.ERR_MSG + "'); history.go(-1);</script>");
                return Json(new { ERR_CODE = retDic.ERR_CODE, ERR_MSG = retDic.ERR_MSG });
            }



            //DB저장: 회원 신규가입 -----------------------
            ObjretDIC = _MemberService.SetMemberRegister(M_ID, M_NAME, M_PWD_SHA256_HASH, M_GRADE, M_SEX, M_BIRTHDAY, M_MOBILE, M_PHONE, M_EMAIL, M_ZIPCODE, M_ADDR1, M_ADDR2, M_ISSMS, M_ISEMAIL, M_ISDM
            , M_JOIN_MODE, M_DI, M_AGREE, M_AGREE2, M_SKIN_TROUBLE_CD
            , M_GBN, M_STAFF_COMPANY, M_STAFF_ID);
            if (ObjretDIC.ERR_CODE != "0")
            {
                strERR_CODE = ObjretDIC.ERR_CODE;
                strERR_MSG = "==DB저장중 오류발생!==";
                strERR_MSG = strERR_MSG + "\\n ERR_CODE:" + strERR_CODE;
                if (strERR_CODE == "10")
                    strERR_MSG = strERR_MSG + "\\n 이미 존재하는 계정입니다.";
            }

            //로그 기록
            if (strERR_CODE == "0")
            {
                log_comment = log_comment + "성공";
                log_memo = log_memo + "성공";
            }
            else
            {
                log_comment = log_comment + "실패";
                log_memo = log_memo + "실패";

            }
            log_memo = log_memo + "|strERR_CODE:" + strERR_CODE;
            log_memo = log_memo + "|strERR_MSG:" + strERR_MSG;
            log_memo = log_memo + "|M_ID:" + M_ID;
            log_memo = log_memo + "|M_NAME:" + M_NAME;
            log_memo = log_memo + "|M_PWD:" + M_PWD;
            log_memo = log_memo + "|M_PWD_MD5_HASH:" + M_PWD_MD5_HASH;
            log_memo = log_memo + "|M_PWD_SHA256_HASH:" + M_PWD_SHA256_HASH;
            log_memo = log_memo + "|M_GRADE:" + M_GRADE;
            log_memo = log_memo + "|M_SEX:" + M_SEX;
            log_memo = log_memo + "|M_BIRTHDAY:" + M_BIRTHDAY;
            log_memo = log_memo + "|M_PHONE:" + M_PHONE;
            log_memo = log_memo + "|M_EMAIL:" + M_EMAIL;
            log_memo = log_memo + "|M_ZIPCODE:" + M_ZIPCODE;
            log_memo = log_memo + "|M_ADDR1:" + M_ADDR1;
            log_memo = log_memo + "|M_ADDR2:" + M_ADDR2;
            log_memo = log_memo + "|M_ISSMS:" + M_ISSMS;
            log_memo = log_memo + "|M_ISEMAIL:" + M_ISEMAIL;
            log_memo = log_memo + "|M_ISDM:" + M_ISDM;

            log_memo = log_memo + "|M_JOIN_MODE:" + M_JOIN_MODE;
            log_memo = log_memo + "|M_DI:" + M_DI;
            log_memo = log_memo + "|M_AGREE:" + M_AGREE;
            log_memo = log_memo + "|M_AGREE2:" + M_AGREE2;
            log_memo = log_memo + "|M_SKIN_TROUBLE_CD:" + M_SKIN_TROUBLE_CD;

            log_memo = log_memo + "|M_GBN:" + M_GBN;
            log_memo = log_memo + "|M_STAFF_COMPANY:" + M_STAFF_COMPANY;
            log_memo = log_memo + "|M_STAFF_ID:" + M_STAFF_ID;
            userlog.UserLogSave(log_memo, log_comment);

            //신규회원가입 축하메일 발송 --------------------------
            string MAIL_SUBJECT="[AboutMe]회원가입 축하메일";
            string MAIL_BODY="";
            MAIL_BODY = MAIL_BODY + "<html><body>";
            MAIL_BODY =MAIL_BODY +"<br>====AboutMe 회원가입을 축하합니다=========<br>";
            MAIL_BODY =MAIL_BODY +"<br>아이디:"+M_ID;
            MAIL_BODY =MAIL_BODY +"<br>이름:"+M_NAME;
            MAIL_BODY =MAIL_BODY +"</body></html>";

            //메일 발송을 위한 발송정보 준비 ----------------------------------------------------
            string MAIL_SENDER_EMAIL = Config.GetConfigValue("MAIL_SENDER_EMAIL"); //noreply@cstone.co.kr
            string MAIL_SENDER_PW = Config.GetConfigValue("MAIL_SENDER_PW"); //cstonedev12
            string MAIL_SENDER_SMTP_SERVER = Config.GetConfigValue("MAIL_SENDER_SMTP_SERVER"); //smtp.gmail.com
            string MAIL_SENDER_SMTP_PORT = Config.GetConfigValue("MAIL_SENDER_SMTP_PORT"); //587
            string MAIL_SENDER_SMTP_TIMEOUT = Config.GetConfigValue("MAIL_SENDER_SMTP_TIMEOUT"); //20000

            //메일 발송
            MailSender mObj = new MailSender();
            mObj.MailSendAction(MAIL_SENDER_EMAIL, MAIL_SENDER_PW, MAIL_SENDER_SMTP_SERVER, MAIL_SENDER_SMTP_PORT, MAIL_SENDER_SMTP_TIMEOUT, M_EMAIL, MAIL_SUBJECT, MAIL_BODY);

            //ViewBag.mail_err_no = mObj.err_no;
            //ViewBag.mail_err_msg = mObj.err_msg;

            return Json(new { ERR_CODE = strERR_CODE, ERR_MSG = strERR_MSG });

        }

        //사용자 회원가입- Step4 -가입완료 : DB에서 확인 필요
        public ActionResult JoinStep4(string M_ID = "")
        {
            if (M_ID == null || M_ID == "")
            {
                return Content("<script language='javascript' type='text/javascript'>alert('회원계정이 전달되지 않았습니다.'); location.href='/MemberShip/JoinStep1';</script>");

            }

            //DB에서 확인
            SP_MEMBER_VIEW_Result result = _MemberService.GetMemberView(M_ID);
            if (result == null)
            {
                return Content("<script language='javascript' type='text/javascript'>alert('회원가입후 DB조회에 실패하였습니다.'); location.href='/MemberShip/JoinStep1';</script>");
            }

            this.ViewBag.M_ID = result.M_ID;
            this.ViewBag.M_NAME = result.M_NAME;
            this.ViewBag.M_EMAIL = result.M_EMAIL;
            this.ViewBag.M_CREDATE = result.M_CREDATE.ToString("yyyy.MM.dd");

            return View();
        }


        //아이디 찾기 팝업 폼
        public ActionResult IdSearch()
        {
            return View();
        }

        //아이디찾기 팝업:결과
        public ActionResult IdSearch_End(string M_NAME = "", string M_EMAIL = "", string M_MOBILE = "")
        {
            //로그 기록 준비
            string log_memo = "아이디찾기";
            string log_comment = "아이디찾기";
            UserLog userlog = new UserLog();


            if (M_NAME == "" || M_EMAIL == "" || M_MOBILE=="")
            {
                log_comment = log_comment + "실패 : param오류";
                log_memo = log_memo + "실패 : param오류";
                log_memo = log_memo + "|inputM_NAME:" + M_NAME;
                log_memo = log_memo + "|inputM_EMAIL:" + M_EMAIL;
                log_memo = log_memo + "|inputM_MOBILE:" + M_MOBILE;
                userlog.UserLogSave(log_memo, log_comment);

                return Content("<script language='javascript' type='text/javascript'>alert('이름,이메일,핸드폰 값 모두가 전달되어야 합니다.'); location.href='/MemberShip/IdSearch';</script>");
            }

            //아이디 찾기 - DB처리
            ReturnDic retDic = _MemberService.GetMemberFindID(M_NAME, M_EMAIL, M_MOBILE);
            if (retDic.ERR_CODE != "0") //아이디찾기 오류        
            {
                log_comment = log_comment + "실패 : DB찾기 오류";
                log_memo = log_memo + "실패: DB찾기 오류";
                log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
                log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
                log_memo = log_memo + "|retDic.ETC1:" + retDic.ETC1;
                log_memo = log_memo + "|retDic.ETC2:" + retDic.ETC2;
                log_memo = log_memo + "|retDic.ETC3:" + retDic.ETC3;
                log_memo = log_memo + "|inputM_NAME:" + M_NAME;
                log_memo = log_memo + "|inputM_EMAIL:" + M_EMAIL;
                log_memo = log_memo + "|inputM_MOBILE:" + M_MOBILE;
                userlog.UserLogSave(log_memo, log_comment);

                return Content("<script language='javascript' type='text/javascript'>alert('" + retDic.ERR_MSG + "'); location.href='/MemberShip/IdSearch';</script>");
            }

            this.ViewBag.M_ID = retDic.ETC1; //찾아진 ID
            this.ViewBag.M_NAME = retDic.ETC2; //찾아진 이름
            this.ViewBag.M_CREDATE = retDic.ETC3; //찾아진 가입일

            //로그 기록
            log_comment = log_comment + "성공";
            log_memo = log_memo + "성공";
            log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
            log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
            log_memo = log_memo + "|retDic.ETC1:" + retDic.ETC1;
            log_memo = log_memo + "|retDic.ETC2:" + retDic.ETC2;
            log_memo = log_memo + "|retDic.ETC3:" + retDic.ETC3;
            log_memo = log_memo + "|inputM_NAME:" + M_NAME;
            log_memo = log_memo + "|inputM_EMAIL:" + M_EMAIL;
            log_memo = log_memo + "|inputM_MOBILE:" + M_MOBILE;
            userlog.UserLogSave(log_memo, log_comment);



            return View();
        }

        //비밀번호 찾기 팝업 폼
        public ActionResult PwSearch()
        {
            return View();
        }

        //비밀번호 팝업:결과
        public ActionResult PwSearch_End(string M_ID = "", string M_NAME = "", string M_EMAIL = "", string M_MOBILE = "")
        {
            //로그 기록 준비
            string log_memo = "비밀번호 찾기";
            string log_comment = "비밀번호 찾기";
            UserLog userlog = new UserLog();

            if (M_ID == "" || M_NAME == "" || M_EMAIL == "" || M_MOBILE == "")
            {
                log_comment = log_comment + "실패 : param오류";
                log_memo = log_memo + "실패 : param오류";
                log_memo = log_memo + "|inputM_ID:" + M_ID;
                log_memo = log_memo + "|inputM_NAME:" + M_NAME;
                log_memo = log_memo + "|inputM_EMAIL:" + M_EMAIL;
                log_memo = log_memo + "|inputM_MOBILE:" + M_MOBILE;
                userlog.UserLogSave(log_memo, log_comment);

                return Content("<script language='javascript' type='text/javascript'>alert('이름,이메일,핸드폰 값 모두가 전달되어야 합니다.'); location.href='/MemberShip/PwSearch';</script>");
            }

            //신규비밀번호 랜덤생성
            string strNEW_PWD ="";
            string strNEW_PWD_MD5 ="";
            string strNEW_PWD_SHA256_HASH = "";

            Random r = new Random();
            int randomNumber = r.Next(100000, 90000000); //alph(1)+ 6~8 자리
            //string strNOW_TIME = @DateTime.Now.ToString("yyyyMMdd_HHmmss");
            //string strWORK_TMP_ID = @strNOW_TIME +  @randomNumber.ToString();

            strNEW_PWD = Utility01.GetRandomAlpha(1) + Utility01.GetRandomAlphanumeric(7);  //랜덤암호 신규생성 :문자로 시작
            AES256Cipher objEnc = new AES256Cipher();
            strNEW_PWD_MD5 = objEnc.MD5Hash(strNEW_PWD);   //MD5 
            strNEW_PWD_SHA256_HASH = objEnc.SHA256Hash(strNEW_PWD_MD5);   //MD5->HA256_HASH

           

            //비밀번호 찾기 - DB처리
            ReturnDic retDic = _MemberService.GetMemberFindPWD(M_ID, M_NAME, M_EMAIL, M_MOBILE, strNEW_PWD_SHA256_HASH);
            if (retDic.ERR_CODE != "0") //비밀번호디찾기 오류        
            {
                log_comment = log_comment + "실패 : DB찾기 오류";
                log_memo = log_memo + "실패: DB찾기 오류";
                log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
                log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
                log_memo = log_memo + "|retDic.ETC1:" + retDic.ETC1;
                log_memo = log_memo + "|retDic.ETC2:" + retDic.ETC2;
                log_memo = log_memo + "|retDic.ETC3:" + retDic.ETC3;
                log_memo = log_memo + "|inputM_ID:" + M_ID;
                log_memo = log_memo + "|inputM_NAME:" + M_NAME;
                log_memo = log_memo + "|inputM_EMAIL:" + M_EMAIL;
                log_memo = log_memo + "|inputM_MOBILE:" + M_MOBILE;
                log_memo = log_memo + "|strNEW_PWD:" + strNEW_PWD;
                userlog.UserLogSave(log_memo, log_comment); 
                
                return Content("<script language='javascript' type='text/javascript'>alert('" + retDic.ERR_MSG + "'); location.href='/MemberShip/PwSearch';</script>");
            }

            this.ViewBag.M_NAME = retDic.ETC1; //찾아진 이름
            this.ViewBag.M_EMAIL = retDic.ETC2; //찾아진 EMAIL
            this.ViewBag.M_PWD_NEW = strNEW_PWD; //새로 세팅된 비밀번호


            //로그 기록
            log_comment = log_comment + "성공";
            log_memo = log_memo + "성공";
            log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
            log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
            log_memo = log_memo + "|retDic.ETC1:" + retDic.ETC1;
            log_memo = log_memo + "|retDic.ETC2:" + retDic.ETC2;
            log_memo = log_memo + "|retDic.ETC3:" + retDic.ETC3;
            log_memo = log_memo + "|inputM_ID:" + M_ID;
            log_memo = log_memo + "|inputM_NAME:" + M_NAME;
            log_memo = log_memo + "|inputM_EMAIL:" + M_EMAIL;
            log_memo = log_memo + "|inputM_MOBILE:" + M_MOBILE;
            log_memo = log_memo + "|strNEW_PWD:" + strNEW_PWD;
            userlog.UserLogSave(log_memo, log_comment);


            //새로 세팅된 비밀번호 메일로 전송 ----------------------
            string MAIL_SUBJECT = "[AboutMe]비밀번호 신규 세팅";
            string MAIL_BODY = "";
            MAIL_BODY = MAIL_BODY + "<html><body>";
            MAIL_BODY = MAIL_BODY + "<br>====AboutMe 비밀번호가 새롭게 변경되었습니다.=========<br>";
            //MAIL_BODY = MAIL_BODY + "<br>아이디:" + M_ID;
            MAIL_BODY = MAIL_BODY + "<br>신규비밀번호:" + strNEW_PWD;
            MAIL_BODY = MAIL_BODY + "</body></html>";

            //비밀번호 찾기: 메일 발송을 위한 발송정보 준비 ----------------------------------------------------
            string MAIL_SENDER_EMAIL = Config.GetConfigValue("MAIL_SENDER_EMAIL"); //noreply@cstone.co.kr
            string MAIL_SENDER_PW = Config.GetConfigValue("MAIL_SENDER_PW"); //cstonedev12
            string MAIL_SENDER_SMTP_SERVER = Config.GetConfigValue("MAIL_SENDER_SMTP_SERVER"); //smtp.gmail.com
            string MAIL_SENDER_SMTP_PORT = Config.GetConfigValue("MAIL_SENDER_SMTP_PORT"); //587
            string MAIL_SENDER_SMTP_TIMEOUT = Config.GetConfigValue("MAIL_SENDER_SMTP_TIMEOUT"); //20000

            //비밀번호 찾기: 메일 발송
            MailSender mObj = new MailSender();
            mObj.MailSendAction(MAIL_SENDER_EMAIL, MAIL_SENDER_PW, MAIL_SENDER_SMTP_SERVER, MAIL_SENDER_SMTP_PORT, MAIL_SENDER_SMTP_TIMEOUT, M_EMAIL, MAIL_SUBJECT, MAIL_BODY);

            //ViewBag.mail_err_no = mObj.err_no;
            //ViewBag.mail_err_msg = mObj.err_msg;




            return View();
        }

    } //class
}//namespace