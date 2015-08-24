using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

using AboutMe.Web.Front.Common.Filters;

namespace AboutMe.Web.Front.DI_Core
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
            container.RegisterType<AboutMe.Domain.Service.Member.IMemberService, AboutMe.Domain.Service.Member.MemberService>(); //회원관리
            container.RegisterType<AboutMe.Domain.Service.Faq.IFaqService, AboutMe.Domain.Service.Faq.FaqService>(); //Faq
            container.RegisterType<AboutMe.Domain.Service.Product.IProductService, AboutMe.Domain.Service.Product.ProductService>(); //상품관리
            container.RegisterType<AboutMe.Domain.Service.Notice.INoticeService, AboutMe.Domain.Service.Notice.NoticeService>(); //Notice
            container.RegisterType<AboutMe.Domain.Service.Cart.ICartService, AboutMe.Domain.Service.Cart.CartService>(); //Cart
            container.RegisterType<AboutMe.Domain.Service.Order.IOrderService, AboutMe.Domain.Service.Order.OrderService>(); //Order
            container.RegisterType<AboutMe.Domain.Service.Sample.ISampleService, AboutMe.Domain.Service.Sample.SampleService>(); //샘플/체험단 신청
            container.RegisterType<AboutMe.Domain.Service.Winner.IWinnerService, AboutMe.Domain.Service.Winner.WinnerService>(); //당첨자발표

            container.RegisterType<AboutMe.Domain.Service.Promotion.IPromotionService, AboutMe.Domain.Service.Promotion.PromotionService>(); //프로모션
            container.RegisterType<AboutMe.Domain.Service.Coupon.ICouponService, AboutMe.Domain.Service.Coupon.CouponService>(); //쿠폰
            container.RegisterType<AboutMe.Domain.Service.Review.IReviewService, AboutMe.Domain.Service.Review.ReviewService>(); //Review
            container.RegisterType<AboutMe.Domain.Service.Qna.IQnaService, AboutMe.Domain.Service.Qna.QnaService>(); //1:1문의
            container.RegisterType<AboutMe.Domain.Service.Event.IEventService, AboutMe.Domain.Service.Event.EventService>(); //이벤트
            container.RegisterType<AboutMe.Domain.Service.Exhibit.IExhibitService, AboutMe.Domain.Service.Exhibit.ExhibitService>(); //기획전
            container.RegisterType<AboutMe.Domain.Service.Magazine.IMagazineService, AboutMe.Domain.Service.Magazine.MagazineService>(); //매거진
            container.RegisterType<AboutMe.Domain.Service.Shopinfo.IShopinfoService, AboutMe.Domain.Service.Shopinfo.ShopinfoService>(); //매장안내
            /**
            container.RegisterInstance<IFilterProvider>("FilterProvider", new FilterProvider(container));
            container.RegisterInstance<IActionFilter>("LogActionFilter", new TraceActionFilter());
            **/
            container.RegisterType<AboutMe.Domain.Service.Recallbbs.IRecallbbsService, AboutMe.Domain.Service.Recallbbs.RecallbbsService>(); //취소/반품게시판

            container.RegisterType<AboutMe.Domain.Service.BizPromotion.IBizPromotion, AboutMe.Domain.Service.BizPromotion.BizPromotion>(); //비즈니스로직..쿠폰,프로모션을 같이 이용하는... 

            container.RegisterType<AboutMe.Domain.Service.Point.IPointService, AboutMe.Domain.Service.Point.PointService>(); //포인트
            container.RegisterType<AboutMe.Domain.Service.Display.IDisplayService, AboutMe.Domain.Service.Display.DisplayService>(); //전시,배너,팝업

            //-------------------------------------------------------------------------------------------------------


            container.RegisterType<AboutMe.Web.Front.Controllers.AccountController>(new InjectionConstructor());

            return container;
        }
    }
}
