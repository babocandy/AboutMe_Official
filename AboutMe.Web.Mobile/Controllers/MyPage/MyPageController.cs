﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AboutMe.Common.Helper;
using AboutMe.Web.Mobile.Common;

using AboutMe.Domain.Service.Member;
using AboutMe.Domain.Entity.Member;
using AboutMe.Domain.Entity.Common;

using AboutMe.Domain.Service.Order;
using AboutMe.Domain.Entity.Order;

using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

using AboutMe.Web.Mobile.Common.Filters;


namespace AboutMe.Web.Mobile.Controllers.MyPage
{
    public class MyPageController : BaseMobileController
    {
        private IMemberService _MemberService;
        private IOrderService _orderservice;

        public MyPageController(IMemberService _memberService, IOrderService _orderservice)
        {
            this._MemberService = _memberService;
            this._orderservice = _orderservice;
        }


        // GET: Mypage
        [CustomAuthorize]  //마이페이지 메인 으로 이동
        public ActionResult Index()
        {
            // return View();
            return RedirectToAction("Main", "MyPage"); //메인 으로 이동
        }

        [CustomAuthorize]  //모바일 마이페이지 메인
        public ActionResult Main()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + Request.Url.Authority; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority; //ex)http://www.aboutme.co.kr

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;


            this.ViewBag.M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade();
            this.ViewBag.M_GRADE_NAME = MemberInfo.GetMemberGradeName();
            this.ViewBag.M_SKIN_TROUBLE_CD = MemberInfo.GetMemberSkinTroubleCD(); //피부고민코드

            this.ViewBag.M_GBN = MemberInfo.GetMemberGBN();
            this.ViewBag.M_GBN_NAME = "";
            if (MemberInfo.GetMemberGBN() == "S")
                this.ViewBag.M_GBN_NAME = "[임직원]";

            SP_MYPAGE_MAIN_STATUS_Result M = _orderservice.MyPageMainInfo(_user_profile.M_ID);

            return View(M);
        }


        //--------------------------

        [CustomAuthorize]  //모바일 회원정보 수정 -폼
        public ActionResult MyModify()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + Request.Url.Authority; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority; //ex)http://www.aboutme.co.kr

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;


            string M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_ID = M_ID;

            SP_MEMBER_VIEW_Result result = _MemberService.GetMemberView(M_ID); //회원 본인 정보 가져오기
            if (result == null)
            {
                //회원정보 찾기 실패
                return Content("<script language='javascript' type='text/javascript'>alert('회원 정보를 찾을수 없습니다. 다시 로그인 해 주십시오..'); location.href='/MemberShip/Login';</script>");
            }

            string strM_MOBILE = result.M_MOBILE + "--";
            string strM_PHONE = result.M_PHONE + "--";
            string strM_EMAIL = result.M_EMAIL + "@";

            string[] arrMOBILE = strM_MOBILE.Split(new char[] { '-' });
            string[] arrPHONE = strM_PHONE.Split(new char[] { '-' });
            string[] arrEMAIL = strM_EMAIL.Split(new char[] { '@' });

            this.ViewBag.M_MOBILE_1 = arrMOBILE[0];
            this.ViewBag.M_MOBILE_2 = arrMOBILE[1];
            this.ViewBag.M_MOBILE_3 = arrMOBILE[2];

            this.ViewBag.M_PHONE_1 = arrPHONE[0];
            this.ViewBag.M_PHONE_2 = arrPHONE[1];
            this.ViewBag.M_PHONE_3 = arrPHONE[2];

            this.ViewBag.M_EMAIL_1 = arrEMAIL[0];
            this.ViewBag.M_EMAIL_2 = arrEMAIL[1];


            return View(result);

        }


        //모바일 회원정보 수정 -저장 Ajax 리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        //public ActionResult AjaxMyModifyProc()
        public ActionResult MyModifyProc()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + Request.Url.Authority; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority; //ex)http://www.aboutme.co.kr

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;

            //로그 기록 준비
            string log_memo = "모바일 회원정보 수정";
            string log_comment = "모바일 회원정보 수정";
            UserLog userlog = new UserLog();

            string M_ID = MemberInfo.GetMemberId();  //폼값 대신 세션값 사용
            //회원수정 : 입력항목 post
            string M_MOBILE = HttpContext.Request.Form["M_MOBILE"]; //
            string M_PHONE = HttpContext.Request.Form["M_PHONE"]; //
            string M_EMAIL = HttpContext.Request.Form["M_EMAIL"]; //
            string M_ZIPCODE = HttpContext.Request.Form["M_ZIPCODE"]; //
            string M_ADDR1 = HttpContext.Request.Form["M_ADDR1"]; //
            string M_ADDR2 = HttpContext.Request.Form["M_ADDR2"]; //
            string M_ISSMS = HttpContext.Request.Form["M_ISSMS"]; //
            string M_ISEMAIL = HttpContext.Request.Form["M_ISEMAIL"]; //
            string M_ISDM = HttpContext.Request.Form["M_ISDM"]; //

            if (M_ID == "")
            {
                //return Json(new { ERR_CODE = "999", ERR_MSG = "로그인 정보를 찾을수 없습니다. 다시 로그인 해 주십시오" });
                return Json(new { ERR_CODE = "999", ERR_MSG = "로그인 정보를 찾을수 없습니다. 다시 로그인 해 주십시오" });

            }

            ReturnDic retDic = _MemberService.SetMemberUpdate(M_ID, M_MOBILE, M_PHONE, M_EMAIL, M_ZIPCODE, M_ADDR1, M_ADDR2, M_ISSMS, M_ISEMAIL, M_ISDM);   //회원정보 수정
            /*
            if (retDic.ERR_CODE == "10") //오류
            {
                return Content("<script language='javascript' type='text/javascript'>alert('오류!. 회원정보를 찾을 수 없습니다. 다시로그인후 시도하십시오.'); location.href='/MemberShip/Login';</script>");
            }
            if (retDic.ERR_CODE != "0") //오류
            {
                return Content("<script language='javascript' type='text/javascript'>alert('회원정보 수정 처리 실패. ERR_CODE:"+retDic.ERR_CODE+"'); location.href='/MemberShip/Login';</script>");
            }
            */


            //로그 기록
            if (retDic.ERR_CODE == "0")
            {
                log_comment = log_comment + "성공";
                log_memo = log_memo + "성공";
            }
            else
            {
                log_comment = log_comment + "실패";
                log_memo = log_memo + "실패";
            }
            log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
            log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
            log_memo = log_memo + "|M_ID:" + M_ID;
            log_memo = log_memo + "|M_MOBILE:" + M_MOBILE;
            log_memo = log_memo + "|M_PHONE:" + M_PHONE;
            log_memo = log_memo + "|M_EMAIL:" + M_EMAIL;
            log_memo = log_memo + "|M_ZIPCODE:" + M_ZIPCODE;
            log_memo = log_memo + "|M_ADDR1:" + M_ADDR1;
            log_memo = log_memo + "|M_ADDR2:" + M_ADDR2;
            log_memo = log_memo + "|M_ISSMS:" + M_ISSMS;
            log_memo = log_memo + "|M_ISEMAIL:" + M_ISEMAIL;
            log_memo = log_memo + "|M_ISDM:" + M_ISDM;
            userlog.UserLogSave(log_memo, log_comment);


            //DB저장 후 
            //return Json(new { ERR_CODE = retDic.ERR_CODE, ERR_MSG = retDic.ERR_MSG });
            if (retDic.ERR_CODE == "0") //성공
            {
                return Content("<script language='javascript' type='text/javascript'>alert('회원정보가 수정되었습니다.'); location.href='" + strHTTP_DOMAIN + "/MyPage/Main';</script>");
            }
            else //에러
            {
                return Content("<script language='javascript' type='text/javascript'>alert('회원정보 수정 처리 실패. ERR_CODE:" + retDic.ERR_CODE + "'); history.go(-1);</script>");
            }

        }


        [CustomAuthorize]  //모바일 회원정보 비밀번호 수정 -폼
        public ActionResult Pop_PwChange()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + Request.Url.Authority; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority; //ex)http://www.aboutme.co.kr

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;


            string M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_ID = M_ID;

            return View();
        }

        //모바일 회원정보 비밀번호 수정 -저장  : Ajax리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        //public ActionResult AjaxPop_PwChangeProc()
        public ActionResult Pop_PwChangeProc()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + Request.Url.Authority; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority; //ex)http://www.aboutme.co.kr

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;

            //로그 기록 준비
            string log_memo = "모바일 회원비밀번호 변경";
            string log_comment = "모바일 회원비밀번호 변경";
            UserLog userlog = new UserLog();

            string M_ID = MemberInfo.GetMemberId();  //폼값 대신 세션값 사용
            //회원비밀번호 수정 : 입력항목 post
            string M_PWD_OLD = HttpContext.Request.Form["M_PWD_OLD"]; //
            string M_PWD_NEW = HttpContext.Request.Form["M_PWD_NEW"]; //


            if (M_ID == "")
            {
                //return Json(new { ERR_CODE = "999", ERR_MSG = "로그인 정보를 찾을수 없습니다. 다시 로그인 해 주십시오" });
                return Content("<script language='javascript' type='text/javascript'>alert('로그인 정보를 찾을수 없습니다. 다시 로그인 해 주십시오'); location.href='/MemberShip/Login'; </script>");

            }
            if (M_PWD_OLD == "" || M_PWD_NEW == "")
            {
                //return Json(new { ERR_CODE = "998", ERR_MSG = "파라메타 전달 오류" });
                return Content("<script language='javascript' type='text/javascript'>alert('로그인 정보를 찾을수 없습니다. 다시 로그인 해 주십시오'); location.href='/MemberShip/Pop_PwChange'; </script>");
            }


            AES256Cipher objEnc = new AES256Cipher();
            string M_PWD_OLD_MD5_HASH = objEnc.MD5Hash(M_PWD_OLD);
            string M_PWD_OLD_SHA256_HASH = objEnc.SHA256Hash(M_PWD_OLD_MD5_HASH);

            string M_PWD_NEW_MD5_HASH = objEnc.MD5Hash(M_PWD_NEW);
            string M_PWD_NEW_SHA256_HASH = objEnc.SHA256Hash(M_PWD_NEW_MD5_HASH);


            ReturnDic retDic = _MemberService.SetMemberPWDChange(M_ID, M_PWD_OLD_SHA256_HASH, M_PWD_NEW_SHA256_HASH);   //회원비밀번호 수정

            //로그 기록
            if (retDic.ERR_CODE == "0")
            {
                log_comment = log_comment + "성공";
                log_memo = log_memo + "성공";
            }
            else
            {
                log_comment = log_comment + "실패";
                log_memo = log_memo + "실패";
            }
            log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
            log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
            log_memo = log_memo + "|M_ID:" + M_ID;

            log_memo = log_memo + "|M_PWD_OLD:" + M_PWD_OLD;
            log_memo = log_memo + "|M_PWD_OLD_MD5_HASH:" + M_PWD_OLD_MD5_HASH;
            log_memo = log_memo + "|M_PWD_OLD_SHA256_HASH:" + M_PWD_OLD_SHA256_HASH;

            log_memo = log_memo + "|M_PWD_NEW:" + M_PWD_NEW;
            log_memo = log_memo + "|M_PWD_NEW_MD5_HASH:" + M_PWD_NEW_MD5_HASH;
            log_memo = log_memo + "|M_PWD_NEW_SHA256_HASH:" + M_PWD_NEW_SHA256_HASH;
            userlog.UserLogSave(log_memo, log_comment);


            //DB처리후 분기
            //return Json(new { ERR_CODE = retDic.ERR_CODE, ERR_MSG = retDic.ERR_MSG });
            if (retDic.ERR_CODE == "0") //성공
            {
                return Content("<script language='javascript' type='text/javascript'>alert('비밀번호가 변경되었습니다.'); location.href='" + strHTTP_DOMAIN + "/MyPage/Main';</script>");

            }
            else //실패
            {
                return Content("<script language='javascript' type='text/javascript'>alert('비밀번호변경중 오류발생. ERR_CODE:" + retDic.ERR_CODE + "');history.go(-1);</script>");

            }

        }


        [CustomAuthorize]  //모바일 회원정보 피부고민 수정 -폼
        public ActionResult Pop_SkinTrouble()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + Request.Url.Authority; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority; //ex)http://www.aboutme.co.kr

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;

            this.ViewBag.M_ID = MemberInfo.GetMemberId();
            this.ViewBag.M_NAME = MemberInfo.GetMemberName();
            this.ViewBag.M_GRADE = MemberInfo.GetMemberGrade();
            this.ViewBag.M_GRADE_NAME = MemberInfo.GetMemberGradeName();
            this.ViewBag.M_SKIN_TROUBLE_CD = MemberInfo.GetMemberSkinTroubleCD(); //피부고민코드

            return View();
        }


        //모바일 회원 피부고민  수정 -저장 Ajax 리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult AjaxSkinTroubleProc()
        {
            //로그 기록 준비
            string log_memo = "내 피부고민 수정";
            string log_comment = "내 피부고민 수정";
            UserLog userlog = new UserLog();

            string M_ID = MemberInfo.GetMemberId();  //폼값 대신 세션값 사용
            //피부고민 : 입력항목 post
            string M_SKIN_TROUBLE_CD = HttpContext.Request.Form["M_SKIN_TROUBLE_CD"]; //

            if (M_ID == "")
            {
                return Json(new { ERR_CODE = "999", ERR_MSG = "로그인 정보를 찾을수 없습니다. 다시 로그인 해 주십시오" });
            }

            ReturnDic retDic = _MemberService.SetMemberSkinTroubleUpdate(M_ID, M_SKIN_TROUBLE_CD);   //피부고민 수정
            /*
            if (retDic.ERR_CODE == "10") //오류
            {
                return Content("<script language='javascript' type='text/javascript'>alert('오류!. 회원정보를 찾을 수 없습니다. 다시로그인후 시도하십시오.'); location.href='/MemberShip/Login';</script>");
            }
            */
            //로그 기록
            if (retDic.ERR_CODE == "0")
            {
                CookieSessionStore cookiesession = new CookieSessionStore();
                cookiesession.SetSecretSession("M_SKIN_TROUBLE_CD", M_SKIN_TROUBLE_CD);  //피부고민 세션 세팅
                cookiesession.SetSession("M_SKIN_TROUBLE_CD_TEXT", M_SKIN_TROUBLE_CD);  //피부고민 세션 세팅 -평문

                log_comment = log_comment + "성공";
                log_memo = log_memo + "성공";
            }
            else
            {
                log_comment = log_comment + "실패";
                log_memo = log_memo + "실패";
            }
            log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
            log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
            log_memo = log_memo + "|M_ID:" + M_ID;
            log_memo = log_memo + "|M_SKIN_TROUBLE_CD:" + M_SKIN_TROUBLE_CD;
            userlog.UserLogSave(log_memo, log_comment);


            return Json(new { ERR_CODE = retDic.ERR_CODE, ERR_MSG = retDic.ERR_MSG });

        }


        // 임직원신청 팝업 폼
        [CustomAuthorize]
        public ActionResult StaffRequest()
        {
            string strHTTPS_DOMAIN = Config.GetConfigValue("HTTPS_PROTOCOL") + Request.Url.Authority; //ex)https://www.aboutme.co.kr
            string strHTTP_DOMAIN = "http://" + Request.Url.Authority; //ex)http://www.aboutme.co.kr

            this.ViewBag.HTTPS_DOMAIN = strHTTPS_DOMAIN;
            this.ViewBag.HTTP_DOMAIN = strHTTP_DOMAIN;


            string M_ID = MemberInfo.GetMemberId();
            string M_NAME = MemberInfo.GetMemberName();
            string M_GBN = MemberInfo.GetMemberGBN();

            this.ViewBag.M_ID = M_ID;
            this.ViewBag.M_NAME = M_NAME;
            this.ViewBag.M_GBN = M_GBN;

            if (M_GBN == "S")
            {
                return Content("<script language='javascript' type='text/javascript'>alert('이미 임직원 이십니다.'); self.close();</script>");
            }
            return View();
        }

        // 임직원신청 저장 :Ajax 리턴
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        //public ActionResult AjaxStaffRequestProc()
        public ActionResult StaffRequestProc()
        {
            //로그 기록 준비
            string log_memo = "임직원신청";
            string log_comment = "임직원신청";
            UserLog userlog = new UserLog();

            string M_ID = MemberInfo.GetMemberId();  //폼값 대신 세션값 사용
            string M_NAME = MemberInfo.GetMemberName(); //폼값 대신 세션값 사용
            string M_GRADE = MemberInfo.GetMemberGrade(); //폼값 대신 세션값 사용
            //임직원신청 : 입력항목 post
            string STAFF_COMPANY = HttpContext.Request.Form["STAFF_COMPANY"]; //
            string STAFF_ID = HttpContext.Request.Form["STAFF_ID"]; //
            string STAFF_NAME = HttpContext.Request.Form["STAFF_NAME"]; //

            if (M_ID == "")
            {
                //return Json(new { ERR_CODE = "999", ERR_MSG = "로그인 정보를 찾을수 없습니다. 다시 로그인 해 주십시오" });
                return Content("<script language='javascript' type='text/javascript'>alert('이미 임직원 이십니다.'); self.close();</script>");

            }

            if (MemberInfo.GetMemberGBN() == "S") //이미 임직원임
            {
                log_comment = log_comment + "실패-이미 임직원";
                log_memo = log_memo + "실패-이미 임직원";
                log_memo = log_memo + "|M_ID:" + M_ID;
                log_memo = log_memo + "|M_NAME:" + M_NAME;
                log_memo = log_memo + "|M_GRADE:" + M_GRADE;
                log_memo = log_memo + "|STAFF_COMPANY:" + STAFF_COMPANY;
                log_memo = log_memo + "|STAFF_ID:" + STAFF_ID;
                log_memo = log_memo + "|STAFF_NAME:" + STAFF_NAME;
                userlog.UserLogSave(log_memo, log_comment);

                //return Json(new { ERR_CODE = "998", ERR_MSG = "이미 임직원이십니다. 필요시 다시로그인해 주십시오." });
                return Content("<script language='javascript' type='text/javascript'>alert('이미 임직원 이십니다.필요시 다시로그인해 주십시오.'); self.close();</script>");

            }

            ReturnDic retDic = _MemberService.SetMemberStaffRequestInert(M_ID, M_NAME, M_GRADE, STAFF_COMPANY, STAFF_ID, STAFF_NAME);   //임직원신청 등록
            /*
            if (retDic.ERR_CODE == "10") //오류
            {
                return Content("<script language='javascript' type='text/javascript'>alert('오류!. 회원정보를 찾을 수 없습니다. 다시로그인후 시도하십시오.'); location.href='/MemberShip/Login';</script>");
            }
            */
            //로그 기록
            if (retDic.ERR_CODE == "0")
            {
                log_comment = log_comment + "성공";
                log_memo = log_memo + "성공";
            }
            else
            {
                log_comment = log_comment + "실패";
                log_memo = log_memo + "실패";
            }
            log_memo = log_memo + "|retDic.ERR_CODE:" + retDic.ERR_CODE;
            log_memo = log_memo + "|retDic.ERR_MSG:" + retDic.ERR_MSG;
            log_memo = log_memo + "|M_ID:" + M_ID;
            log_memo = log_memo + "|M_NAME:" + M_NAME;
            log_memo = log_memo + "|M_GRADE:" + M_GRADE;
            log_memo = log_memo + "|STAFF_COMPANY:" + STAFF_COMPANY;
            log_memo = log_memo + "|STAFF_ID:" + STAFF_ID;
            log_memo = log_memo + "|STAFF_NAME:" + STAFF_NAME;
            userlog.UserLogSave(log_memo, log_comment);

            //return Json(new { ERR_CODE = retDic.ERR_CODE, ERR_MSG = retDic.ERR_MSG });
            if (retDic.ERR_CODE == "0")
            {
                return Content("<script language='javascript' type='text/javascript'>alert('임직원 신청이 접수 되었습니다.'); self.close(); </script>");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('임직원 신청중 오류발생.ERR_CODE:" + retDic.ERR_CODE + "'); history.go(-1); </script>");
            }

        }


    } //class
}//namespace