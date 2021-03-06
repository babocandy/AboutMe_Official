﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AboutMe.Domain.Entity.AdminFrontMember;

namespace AboutMe.Domain.Service.AdminFrontMember
{
    public interface IAdminFrontMemberService
    {
        //관리자 > 회원------------------------------------------
        List<SP_ADMIN_FRONT_MEMBER_VIEW_Result> GetAdminFrontMemberList(string dATE_FROM, string dATE_TO, string gRADE_LIST, string gBN, string iS_RETIRE, string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE); //관리자-회원 목록 
        int GetAdminFrontMemberListCount(string dATE_FROM, string dATE_TO, string gRADE_LIST, string gBN, string iS_RETIRE, string sEARCH_COL, string sEARCH_KEYWORD); //관리자 -회원 목록 COUNT

        int SetAdminFrontMemberUpdate(string m_ID, string m_GRADE, string m_MOBILE, string m_PHONE, string m_EMAIL, string m_ZIPCODE, string m_ADDR1, string m_ADDR2, string m_ISSMS, string m_ISEMAIL, string m_ISDM, string m_SKIN_TROUBLE_CD, string m_GBN, string m_STAFF_COMAPNY, string m_STAFF_ID, string m_IS_RETIRE, string m_DEL_REASON); //관리자 -회원수정.리턴 0:에러없음 <<사용안함
        int SetAdminFrontMemberUpdate_Basic(string m_ID, string m_GRADE, string m_MOBILE, string m_PHONE, string m_EMAIL, string m_ZIPCODE, string m_ADDR1, string m_ADDR2, string m_ISSMS, string m_ISEMAIL, string m_ISDM); //관리자 -회원수정-기본정보.리턴 0:에러없음
        int SetAdminFrontMemberUpdate_PWD(string m_ID, string m_PWD); //관리자 -회원수정-암호변경.리턴 0:에러없음
        int SetAdminFrontMemberUpdate_Staff(string m_ID, string m_GBN, string m_STAFF_COMAPNY, string m_STAFF_ID); //관리자 -회원수정-임직원.리턴 0:에러없음
        int SetAdminFrontMemberUpdate_Retire(string m_ID, string m_IS_RETIRE, string m_DEL_REASON); //관리자 -회원수정-탈퇴.리턴 0:에러없음

        SP_ADMIN_FRONT_MEMBER_VIEW_Result GetAdminFrontMemberView(string m_ID);  //관리자-회원 상세 1명

        //관리자 > 임직원 신청 -----------------------------
        List<SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result> GetAdminMemberStaffRequestList(string dATE_FROM, string dATE_TO, string sTATUS, string sEARCH_COL, string sEARCH_KEYWORD, string sORT_COL, string sORT_DIR, int pAGE, int pAGESIZE); //임직원 신청목록
        int GetAdminMemberStaffRequestListCount(string dATE_FROM, string dATE_TO, string sTATUS, string sEARCH_COL, string sEARCH_KEYWORD); //임직원 신청목록 COUNT
        int SetAdminMemberStaffRequestUpdate(int iDX, string sTATUS, string pROC_COMMENT, string pROC_ADM_ID);//임직원 신청-수정
        SP_ADMIN_MEMBER_STAFF_REQUST_VIEW_Result GetAdminMemberStaffRequestView(int iDX); //임직원 신청-상세 1건

        //관리자 > 임직원 신청 연관정보-----------------------------
        List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_BASEDB_Result> GetAdminMemberStaffRequest_REF_BASEDB(string sTAFF_ID ); //임직원 신청 연관정보 : 임직원DB
        List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_MEMBER_Result> GetAdminMemberStaffRequest_REF_MEMBER(string sTAFF_ID); //임직원 신청 연관정보 : 회원TBL
        List<SP_ADMIN_MEMBER_STAFF_REQUST_REF_REQUEST_Result> GetAdminMemberStaffRequest_REF_REQUEST(string sTAFF_ID); //임직원 신청 연관정보 : 임직원신청이력

        //관리자 > 임직원 기준DB-----------------------------
        List<SP_ADMIN_MEMBER_STAFF_BASE_VIEW_Result> GetAdminMemberStaffBaseList(string dATE_FROM = "", string dATE_TO = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "", string sORT_COL = "IDX", string sORT_DIR = "DESC", int pAGE = 1, int pAGESIZE = 10); //관리자 - 임직원기준DB -목록
        int GetAdminMemberStaffBaseCount(string dATE_FROM = "", string dATE_TO = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "");//관리자-임직원기준DB  목록 COUNT
        int SetAdminMemberStaffBaseInsert(string sTAFF_COMPANY = "", string sTAFF_ID = "", string sTAFF_NAME = "", string wORK_TEMP_ID = "");//관리자 -임직원기준DB -INSERT 1건
        int SetAdminMemberStaffBaseDel(int iDX = -1); //관리자 -임직원기준DB -삭제1건 
        int SetAdminMemberStaffBaseDupCheck(string sTAFF_ID = "");//관리자 -임직원기준DB -사번 중복체크


        List<SP_ADMIN_MEMBER_STAFF_BASE_TMP_LIST_Result> GetAdminMemberStaffBaseTempList(string wORK_TEMP_ID = "");//관리자 - 임직원기준DB TEMP-목록
        int GetAdminMemberStaffBaseTempCount(string wORK_TEMP_ID = "", string aPP_RESULT = ""); //관리자-임직원기준DB TEMP-목록 COUNT
        int SetAdminMemberStaffBaseTempInsert(string sTAFF_COMPANY = "", string sTAFF_ID = "", string sTAFF_NAME = "", string wORK_TEMP_ID = "", string aDM_ID = "", string iP = "");//관리자 -임직원기준DB TEMP -INSERT 1건
        int SetAdminMemberStaffBaseTempUpdate(int iDX = -1, string aPP_RESULT = "");//관리자 -임직원기준DB TEMP -처리결과 수정 1건

         //관리자 - 휴면계정-----------------------------------------------------------
        List<SP_ADMIN_FRONT_MEMBER_VIEW_Result> GetAdminMemberSleepingList(string m_LASTVISITDATE = "", string sEARCH_COL = "", string sEARCH_KEYWORD = "", string sORT_COL = "M_LASTVISITDATE", string sORT_DIR = "DESC", int pAGE = 1, int pAGESIZE = 10);//관리자 - 휴면계정-목록
        int GetAdminMemberSleepingCount(string m_LASTVISITDATE = "", string sEARCH_COL = "", string sEARCH_KEYWORD = ""); //관리자-휴면계정-목록 COUNT

         //관리자 - 통계-회원-----------------------------------------------------------
        List<SP_ADMIN_MEMBER_REPORT_GRADE_Result> GetAdminReportMemberGradeList(); //관리자 - 통계-회원등급별
        List<SP_ADMIN_MEMBER_REPORT_MONTHLY_Result> GetAdminReportMemberMonthlyList(string yEAR = ""); //관리자 - 통계-월별- 회원가입/탈퇴


        //#####################################################################################################################
        //데이타 이행 :회원암호--오픈전 마이그레이션시 1회 필요 -----------------------------------------
        List<SP_ZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL_Result> GetZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_SEL(); //데이타 이행 :회원암호 -list 
        void SetZZ_MIGRATION_MEMBER_PWD_MD5_2_SHA256_UPD(string m_ID = "", string m_PWD_SHA256 = ""); //데이타 이행 :회원암호 -수정저장


    }
}
