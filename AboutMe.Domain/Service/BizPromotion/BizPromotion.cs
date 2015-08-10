using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AboutMe.Domain.Service.Coupon;
using AboutMe.Domain.Service.Promotion;

using AboutMe.Domain.Entity.AdminCoupon;
using AboutMe.Domain.Entity.AdminPromotion;

namespace AboutMe.Domain.Service.BizPromotion
{
    public class BizPromotion : IBizPromotion
    {
        public Dictionary<string,string> GetPromotionInfoForDetialPage(string UsableDeviceGbn, string MGbn , string MGrade , string M_id ,string PCode , int ResultPrice )
        {
            //string[] return_info = null ;

            Dictionary<string, string> dict = new Dictionary<string, string>();
            
            //[로그인 안했을때]
            dict.Add("NoLogin_Coupon_RateOrMoney", ""); // 로긴 안했을때 - 쿠폰 할인방법 - 정률:R / 정액 :M
            dict.Add("NoLogin_Coupon_Discount_Rate", "0");// 로긴 안했을때 - 쿠폰 할인율(정률할인시)
            dict.Add("NoLogin_Coupon_Discount_Money", "0");// 로긴 안했을때 - 쿠폰 할인액(정액할인시)
            dict.Add("NoLogin_Point", "0");// 로긴 안했을때 - 포인트 적립액

            //로그인 했을때
            dict.Add("Login_Promotion_00", ""); //로긴시 - 임직원할인 유무 -- 있으면 '00'
            dict.Add("Login_Promotion_03", "");  //로긴시 - 등급할인 유무 -- 있으면 등급 B=브론즈,S=실버,G=골드,V=VIP , 없으면 공백
            dict.Add("Login_Promotion_00_result_price", "0"); //로긴시 - 임직원할인가 적용가 
            dict.Add("Login_Promotion_03_result_price", "0"); //로긴시 - 임직원할인가에서 등급할인까지 적용된 가격 
            dict.Add("Login_Point", "0"); //로긴시 - 포인트


            dict.Add("Login_Coupon_RateOrMoney", ""); //로긴시 - 쿠폰 할인방법 - 정률:R / 정액 :M
            dict.Add("Login_Coupon_Discount_Rate", "0"); // 로긴시 - 쿠폰 할인율(정률할인시)
            dict.Add("Login_Coupon_Discount_Money", "0"); // 로긴시 - 쿠폰 할인액(정액할인시)
            dict.Add("Login_Coupon_Idx_Coupon_Number", "0");// 로긴시 - 쿠폰 번호
            dict.Add("Login_Coupon_Name",""); //로긴시 = 쿠폰명 
 
            dict.Add("Login_Coupon_USE_STATUS", "0");// 사용상태 1=발행(다운로드가능) , 5=다운로드완료(사용가능) , 10=사용완료
            dict.Add("Login_Coupon_COUPON_NUM_CHECK_TF", ""); //번호인증해야하는 쿠폰이면 = Y ,아니면 =N

            CouponService CpnService  = new CouponService();
            PromotionService PmoService = new PromotionService();


            int PointSavingRate = 0;
            int PointSaving = 0 ;
            int MinusPrice = 0;

            if(M_id == "") //============================================================================================로긴 안했으면
            {
                //[1]쿠폰정보
            

                List<SP_COUPON_TOP1_BY_PRD_SEL_NO_LOGIN_SEL_Result> lst1  = CpnService.GetCouponTop1_ByNoLogin_ByPrd(UsableDeviceGbn, PCode);
                if (lst1.Count > 0)
                {
                    dict["NoLogin_Coupon_RateOrMoney"] = lst1[0].RATE_OR_MONEY.ToString();
                    dict["NoLogin_Coupon_Discount_Rate"] = lst1[0].COUPON_DISCOUNT_RATE.ToString();
                    dict["NoLogin_Coupon_Discount_Money"] = lst1[0].COUPON_DISCOUNT_MONEY.ToString();
                   
                }

                //[2]포인트정보 - 비 로그인시에는 일반, 브론즈회원으로.. 
                MGbn = "A" ;
                MGrade = "S" ;
                List<SP_PROMOTION_POINT_SAVE_RATE_BY_MEMGRADE_SEL_Result> lst2 = PmoService.GetPointSaveRateByMemGrade(MGbn, MGrade);
                

                if(lst2.Count > 0)
                {
                    PointSavingRate= lst2[0].SAVE_RATE.Value;

                    //PointSaving = ((ResultPrice * PointSavingRate) / 100);
                    PointSaving = (int)Math.Ceiling((double)((ResultPrice * PointSavingRate) / 100));
                    dict["NoLogin_Point"] = PointSaving.ToString();
                }
            }

            if (M_id != "") //===========================================================================================로긴 했으면
            {
                //[1] 임직원가 , VIP 할인가 
                List<SP_PROMOTION_TOTAL_BY_PRODUCT_DUMMY_ALL_SEL_Result> lst1 = PmoService.GetPromotionTotalInfo_ByProduct(PCode, MGbn, MGrade);


                //임직원 전용할인 존재 여부 활인
                var qry = from WD in lst1
                          where WD.PMO_TOTAL_CATEGORY == "00"
                          select WD;
                
                if(qry.Count() > 0) //임직원 전용할인 이 있으면 최종가를 그렇게 적용
                {
                   
                     dict["Login_Promotion_00"] = "00";
                     var itm = qry.FirstOrDefault();

                     if (itm.PMO_TOTAL_RATE_OR_MONEY == "R")
                     {
                         MinusPrice = (int)Math.Ceiling((double)((ResultPrice * itm.PMO_TOTAL_DISCOUNT_RATE) / 100));
                         ResultPrice = ResultPrice - MinusPrice;
                      }

                      if (itm.PMO_TOTAL_RATE_OR_MONEY == "M")
                      {
                          ResultPrice = ResultPrice - itm.PMO_TOTAL_DISCOUNT_MONEY.Value;
                      }

                      dict["Login_Promotion_00_result_price"] = ResultPrice.ToString();
                   
                }

                //등급 할인 존재 여부 확인
                qry = from WD in lst1
                          where WD.PMO_TOTAL_CATEGORY == "03"
                          select WD;


                if (qry.Count() > 0) //등급 할인 이 있으면 최종가를 그렇게 적용
                {
                    var itm = qry.FirstOrDefault();

                    if (itm.M_GRADE == "V") //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!일단은 VIP대상 등급할인만 보여주기로함 !!!!!!!!!!!!!!!!!!!!!!!!!
                    {

                        if (itm.PMO_TOTAL_CATEGORY == "03") //등급할인 이 있으면 최종가를 그렇게 적용
                        {
                            dict["Login_Promotion_03"] = itm.M_GRADE;

                            if (itm.PMO_TOTAL_RATE_OR_MONEY == "R")
                            {
                                MinusPrice = (int)Math.Ceiling((double)((ResultPrice * itm.PMO_TOTAL_DISCOUNT_RATE) / 100));
                                ResultPrice = ResultPrice - MinusPrice;
                            }

                            if (itm.PMO_TOTAL_RATE_OR_MONEY == "M")
                            {
                                ResultPrice = ResultPrice - itm.PMO_TOTAL_DISCOUNT_MONEY.Value;
                            }
                            dict["Login_Promotion_03_result_price"] = ResultPrice.ToString();
                        }
                    }
                }


               
                  
                //[2]포인트 적용율 계산
                List<SP_PROMOTION_POINT_SAVE_RATE_BY_MEMGRADE_SEL_Result> lst2 = PmoService.GetPointSaveRateByMemGrade(MGbn, MGrade);
                  
                if (lst2.Count > 0)
                {
                    PointSavingRate = lst2[0].SAVE_RATE.Value;

                    //PointSaving = ((ResultPrice * PointSavingRate) / 100);
                    PointSaving = (int)Math.Ceiling((double)((ResultPrice * PointSavingRate) / 100));
                    dict["Login_Point"] = PointSaving.ToString();
                }


                //[3]쿠폰정보
                List<SP_COUPON_TOP_1_BY_MEMBER_BY_PRD_SEL_Result> lst3 =  CpnService.GetCouponTop1_ByMem_ByPrd(UsableDeviceGbn, PCode, M_id);
                if(lst3.Count > 0 )
                {
                    dict["Login_Coupon_Name"] = lst3[0].COUPON_NAME.ToString();
                    dict["Login_Coupon_RateOrMoney"] = lst3[0].RATE_OR_MONEY.ToString();
                    dict["Login_Coupon_Discount_Rate"] = lst3[0].COUPON_DISCOUNT_RATE.ToString();
                    dict["Login_Coupon_Discount_Money"] = lst3[0].COUPON_DISCOUNT_MONEY.ToString();
                    dict["Login_Coupon_Idx_Coupon_Number"] = lst3[0].IDX_COUPON_NUMBER.ToString();
                    dict["Login_Coupon_USE_STATUS"] = lst3[0].USE_STATUS.ToString();
                    dict["Login_Coupon_COUPON_NUM_CHECK_TF"] = lst3[0].COUPON_NUM_CHECK_TF.ToString(); //Y=번호인증필요한 쿠폰

                }
             
                
            }
            
           

            return dict ;
        }
    }
}
