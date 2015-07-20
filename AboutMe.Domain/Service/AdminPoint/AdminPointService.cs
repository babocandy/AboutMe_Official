using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Entity.AdminPoint;
using System.Data.Entity.Core.Objects;

using System.Diagnostics;

namespace AboutMe.Domain.Service.AdminPoint
{
    public class AdminPointService : IAdminPointService
    {
        /**
         * 관리자에서 포인트관리 > 회원 목록
         * 
         * @pageNo = 페이지번호
         * @pageSize = 페이지당 글의 사이즈
         * @searchKey = 검색키
         * @searchValue = 검색값
         * 
         */
        public List<SP_POINT_MEMBER_SEL_Result> GetMemberList(int pageNo=1, int pageSize = 10, string searchKey=null, string searchValue=null)
        {

            List<SP_POINT_MEMBER_SEL_Result> lst = new List<SP_POINT_MEMBER_SEL_Result>();
            using (AdminPointEntities context = new AdminPointEntities())
            {
                lst = context.SP_POINT_MEMBER_SEL(pageNo, pageSize, searchKey, searchValue).ToList();
            }

            return lst;

        }

        /**
         * 회원개별정보
         * 
         * @ mid= 회원아이디
         */
        public SP_POINT_MEMBER_PROFILE_Result GetMemberProfile(string mid)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            SP_POINT_MEMBER_PROFILE_Result result = new SP_POINT_MEMBER_PROFILE_Result();
            using (AdminPointEntities context = new AdminPointEntities())
            {
                result = context.SP_POINT_MEMBER_PROFILE(mid, retNum, retMsg).FirstOrDefault();
            }

            return result;

        }

        /**
         * 회원총수
         * 
         * @ searchKey = 검색키
         * @ searchValue = 검색값
         * 
         */
        public int GeMemberListCnt(string searchKey, string searchValue)
        {
            int ret = 0;
            using (AdminPointEntities context = new AdminPointEntities())
            {
                int? cnt = context.SP_POINT_MEMBER_CNT(searchKey, searchValue).SingleOrDefault();
                ret = cnt ?? 0;
            }

            return ret;
        }

        /**
         * 관리자에 의한 포인트 차감 변경 
         * 
         * @ mid = 회원아이디
         * @ point = 차감포인트
         * @ addition = 추가설명
         * @ adminId = 관리자 아이디
         * @ adminName = 관리자 이름
         * @ orderCode = 주문코드
         * 
         */
        public Tuple<string, string> UpdateMemberPointUse(string mid, int point, string addition = null, string adminId = null, string adminName = null, string orderCode = null)
        {

            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_ADMIN_POINT_USE(mid, point, addition, adminId, adminName, orderCode, retNum, retMsg);
            }
            
            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UpdateMemberPointUse retNum:  " + retNum.Value);
            Debug.WriteLine("UpdateMemberPointUse retMsg:  " + retMsg.Value);

            return tp;
        }


        /**
         * 관리자에 의한 포인트 적립 변경 
         * 
         * @ mid = 회원아이디
         * @ point = 적립포인트
         * @ addition = 추가설명
         * @ adminId = 관리자 아이디
         * @ adminName = 관리자 이름
         * @ orderCode = 주문코드
         * @ reviewIdx = 리뷰번호
         * 
         */
        public Tuple<string, string> UpdateMemberPointSave(string mid, int point, string addition = null, string adminId=null, string adminName=null ,string orderCode = null, int? revieweIdx = null)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_ADMIN_POINT_SAVE(mid, point, addition, adminId, adminName, orderCode, revieweIdx, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UpdateMemberPointSave retNum:  " + retNum.Value);
            Debug.WriteLine("UpdateMemberPointSave retMsg:  " + retMsg.Value);

            return tp;
        }


        /**
         * 회원 포인트 내역 조회
         * 
         * @ mid = 회원아이디
         * @ pageNo = 페이지번호
         * @ pageSize = 페이지당 글의 수
         * 
         */
        public List<SP_ADMIN_POINT_HISTORY_SEL_Result> GetMyPointHistoryList(string mid, int pageNo = 1, int pageSize = 10)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            List<SP_ADMIN_POINT_HISTORY_SEL_Result> lst = new List<SP_ADMIN_POINT_HISTORY_SEL_Result>();

            using (AdminPointEntities context = new AdminPointEntities())
            {
                lst = context.SP_ADMIN_POINT_HISTORY_SEL(mid,pageNo,pageSize,retNum,retMsg).ToList();
            }

            return lst;
        }

        /**
         * 회원 포인트 내역 총수
         * 
         * @ mid = 회원아이디
         */
        public int GetMyPointHistoryListCnt(string mid)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            int ret = 0;
            using (AdminPointEntities context = new AdminPointEntities())
            {
                int? cnt = context.SP_ADMIN_POINT_HISTORY_CNT(mid, retNum, retMsg).SingleOrDefault();
                ret = cnt ?? 0;
            }

            return ret;
        }


        /**
         * 구매할때 포인트 사용
         * 
         * @ mid = 회원아이디
         * @ point = 사용(=차감) 포인트
         * @ orderCode = 주문코드
         * 
         */
        public Tuple<string, string> UsePointOnOrder(string mid, int point, string orderCode)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_USE_ON_ORDER(mid, point, orderCode, retNum, retMsg);

            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("UsePointOnOrder retNum:  " + retNum.Value);
            Debug.WriteLine("UsePointOnOrder retMsg:  " + retMsg.Value);

            return tp;
        }

        /**
         * 주문 전체 취소
         * 
         * @mid=회원아이디
         * @point=반환(=적립) 포인트
         * @orderCode = 주문코드
         * 
         */
        public Tuple<string, string> CancelAllOfOrder(string mid, int point, string orderCode)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_CANCEL_ALL_ORDER(mid, point, orderCode, retNum, retMsg);

            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("CancelAllOfOrder retNum:  " + retNum.Value);
            Debug.WriteLine("CancelAllOfOrder retMsg:  " + retMsg.Value);

            return tp;
        }

        /**
         * 주문 부분 취소
         * 
         * @mid=회원아이디
         * @point=반환(=적립) 포인트
         * @orderCode = 주문코드
         * @orderDetailIndx = 주문상세일련번호
         * @productName = 상품명
         * 
         */
        public Tuple<string, string> CancelPartOfOrder(string mid, int point, string orderCode, int? orderDetailIndx, string productName)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_CANCEL_PART_ORDER(mid, point, orderCode, orderDetailIndx, productName, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("CancelParrOfOrder retNum:  " + retNum.Value);
            Debug.WriteLine("CancelParrOfOrder retMsg:  " + retMsg.Value);

            return tp;
        }


        /**
         * 구매확정후 포인트 적립
         * 
         * @ mid = 회원아이디
         * @ point = 적립포인트
         * @ orderCode = 주문코드
         * @orderDetailIndx = 주문상세일련번호
         * @productName = 상품명
         * 
         */
        public Tuple<string, string> SavePointAfterFirmOrder(string mid, int? point, string orderCode, int? orderDetailIndx, string productName)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_SAVE_AFTER_FIRM_ORDER(mid, point, orderCode, orderDetailIndx,productName, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("SavePointOnOrder retNum:  " + retNum.Value);
            Debug.WriteLine("SavePointOnOrder retMsg:  " + retMsg.Value);

            return tp;
        }



        /**
         * 구매확정후 취소시 사용했던 포인트 재적립
         * 
         * @ mid = 회원아이디
         * @ point = 적립포인트
         * @ orderCode = 주문코드
         * @orderDetailIndx = 주문상세일련번호
         * @productName = 상품명
         * 
         */
        public Tuple<string, string> ResaveUsedPointOnCancelAfterFirmOrder(string mid, int? point, string orderCode, int? orderDetailIndx, string productName)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_RESAVE_USED_POINT_ON_CANCEL_AFTER_FIRM_ORDER(mid, point, orderCode, orderDetailIndx, productName, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("SavePointOnOrder retNum:  " + retNum.Value);
            Debug.WriteLine("SavePointOnOrder retMsg:  " + retMsg.Value);

            return tp;
        }


        /**
         * 구매확정후 취소시 적립했던 포인트 회수
         * 
         * @ mid = 회원아이디
         * @ point = 적립포인트
         * @ orderCode = 주문코드
         * @orderDetailIndx = 주문상세일련번호
         * @productName = 상품명
         * 
         */
        public Tuple<string, string> GetBackSavedPointOnCancelAfterFirmOrder(string mid, int? point, string orderCode, int? orderDetailIndx, string productName)
        {
            ObjectParameter retNum = new ObjectParameter("RET_NUM", typeof(string));
            ObjectParameter retMsg = new ObjectParameter("RET_MESSAGE", typeof(string));

            using (AdminPointEntities context = new AdminPointEntities())
            {
                context.SP_POINT_GET_BACK_SAVED_POINT_ON_CANCEL_AFTER_FIRM_ORDER(mid, point, orderCode, orderDetailIndx, productName, retNum, retMsg);
            }

            Tuple<string, string> tp = new Tuple<string, string>(retNum.Value.ToString(), retMsg.Value.ToString());
            Debug.WriteLine("GetBackSavedPointOnCancelAfterFirmOrder retNum:  " + retNum.Value);
            Debug.WriteLine("GetBackSavedPointOnCancelAfterFirmOrder retMsg:  " + retMsg.Value);

            return tp;
        }

    }
}
