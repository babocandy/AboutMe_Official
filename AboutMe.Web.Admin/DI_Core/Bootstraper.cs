﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

using AboutMe.Web.Admin.Common.Filters;

namespace AboutMe.Web.Admin.DI_Core
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<AboutMe.Domain.Service.AdminEtc.IAdminMemberService, AboutMe.Domain.Service.AdminEtc.AdminMemberService>();   //샘플용임
            container.RegisterType<AboutMe.Domain.Service.AdminProduct.IAdminProductService, AboutMe.Domain.Service.AdminProduct.AdminProductService>();
            //container.RegisterType<IController, StoreController>("Store");

            container.RegisterType<AboutMe.Domain.Service.AdminEtc.IAdminUserService, AboutMe.Domain.Service.AdminEtc.AdminUserService>(); //관리자관리 -jsh 6/?
            container.RegisterType<AboutMe.Domain.Service.AdminFrontMember.IAdminFrontMemberService, AboutMe.Domain.Service.AdminFrontMember.AdminFrontMemberService>(); //관리자-회원관리 -jsh 7/2
            container.RegisterType<AboutMe.Domain.Service.Faq.IFaqService, AboutMe.Domain.Service.Faq.FaqService>(); //Faq
            container.RegisterType<AboutMe.Domain.Service.Notice.INoticeService, AboutMe.Domain.Service.Notice.NoticeService>(); //Notice
            container.RegisterType<AboutMe.Domain.Service.Qna.IQnaService, AboutMe.Domain.Service.Qna.QnaService>(); //1:1문의
            container.RegisterType<AboutMe.Domain.Service.Event.IEventService, AboutMe.Domain.Service.Event.EventService>(); //이벤트
            container.RegisterType<AboutMe.Domain.Service.Exhibit.IExhibitService, AboutMe.Domain.Service.Exhibit.ExhibitService>(); //기획전
            container.RegisterType<AboutMe.Domain.Service.Magazine.IMagazineService, AboutMe.Domain.Service.Magazine.MagazineService>(); //매거진
            container.RegisterType<AboutMe.Domain.Service.Shopinfo.IShopinfoService, AboutMe.Domain.Service.Shopinfo.ShopinfoService>(); //매장안내
            container.RegisterType<AboutMe.Domain.Service.Recallbbs.IRecallbbsService, AboutMe.Domain.Service.Recallbbs.RecallbbsService>(); //취소/반품게시판
            container.RegisterType<AboutMe.Domain.Service.Sample.ISampleService, AboutMe.Domain.Service.Sample.SampleService>(); //샘플/체험단 신청
            container.RegisterType<AboutMe.Domain.Service.Winner.IWinnerService, AboutMe.Domain.Service.Winner.WinnerService>(); //당첨자발표
            container.RegisterType<AboutMe.Domain.Service.AdminPoint.IAdminPointService, AboutMe.Domain.Service.AdminPoint.AdminPointService>(); //관리자 포인트
            container.RegisterType<AboutMe.Domain.Service.AdminPromotion.IAdminPromotionService, AboutMe.Domain.Service.AdminPromotion.AdminPromotionService>(); //프로모션관리
            container.RegisterType<AboutMe.Domain.Service.AdminEtc.IAdminEtcErrorLogService, AboutMe.Domain.Service.AdminEtc.AdminEtcErrorLogService>(); //app error핸들링

            container.RegisterType<AboutMe.Domain.Service.AdminCoupon.IAdminCouponService, AboutMe.Domain.Service.AdminCoupon.AdminCouponService>(); //쿠폰관리
            container.RegisterType<AboutMe.Domain.Service.AdminOrder.IAdminOrderService, AboutMe.Domain.Service.AdminOrder.AdminOrderService>(); //주문관리
            container.RegisterType<AboutMe.Domain.Service.Order.IOrderService, AboutMe.Domain.Service.Order.OrderService>(); //Order
            container.RegisterType<AboutMe.Domain.Service.AdminDisplay.IAdminDisplayService, AboutMe.Domain.Service.AdminDisplay.AdminDisplayService>(); //Display관리
            container.RegisterType<AboutMe.Domain.Service.AdminReview.IAdminReviewService, AboutMe.Domain.Service.AdminReview.AdminReviewService>(); //리뷰 관리
            container.RegisterType<AboutMe.Domain.Service.OrderStat.IOrderStatService, AboutMe.Domain.Service.OrderStat.OrderStatService>(); //주문통계

          
            container.RegisterType<AboutMe.Domain.Service.BizPromotion.IBizPromotion, AboutMe.Domain.Service.BizPromotion.BizPromotion>(); //비즈니스로직..쿠폰,프로모션을 같이 이용하는... 



            /**
            container.RegisterInstance<IFilterProvider>("FilterProvider", new FilterProvider(container));
            container.RegisterInstance<IActionFilter>("LogActionFilter", new TraceActionFilter());
            **/

            container.RegisterType<AboutMe.Web.Admin.Controllers.AccountController>(new InjectionConstructor());

            return container;
        }
    }
}
