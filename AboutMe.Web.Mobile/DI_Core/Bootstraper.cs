using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

using AboutMe.Web.Mobile.Common.Filters;

namespace AboutMe.Web.Mobile.DI_Core
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

            container.RegisterType<AboutMe.Domain.Service.Product.IProductService, AboutMe.Domain.Service.Product.ProductService>(); //상품관리
            container.RegisterType<AboutMe.Domain.Service.Member.IMemberService, AboutMe.Domain.Service.Member.MemberService>(); //회원관리
            container.RegisterType<AboutMe.Domain.Service.Cart.ICartService, AboutMe.Domain.Service.Cart.CartService>(); //Cart
            container.RegisterType<AboutMe.Domain.Service.Review.IReviewService, AboutMe.Domain.Service.Review.ReviewService>(); //Review
            container.RegisterType<AboutMe.Domain.Service.Point.IPointService, AboutMe.Domain.Service.Point.PointService>(); //포인트
            container.RegisterType<AboutMe.Domain.Service.Display.IDisplayService, AboutMe.Domain.Service.Display.DisplayService>(); //전시,배너,팝업
            /**
           container.RegisterType<AboutMe.Domain.Service.AdminEtc.IAdminMemberService, AboutMe.Domain.Service.AdminEtc.AdminMemberService>();   //샘플용임
           container.RegisterType<AboutMe.Domain.Service.AdminProduct.IAdminProductService, AboutMe.Domain.Service.AdminProduct.AdminProductService>();
           //container.RegisterType<IController, StoreController>("Store");
           container.RegisterType<AboutMe.Domain.Service.Member.IMemberService, AboutMe.Domain.Service.Member.MemberService>(); //회원관리
           container.RegisterType<AboutMe.Domain.Service.Faq.IFaqService, AboutMe.Domain.Service.Faq.FaqService>(); //Faq
           container.RegisterType<AboutMe.Domain.Service.Product.IProductService, AboutMe.Domain.Service.Product.ProductService>(); //상품관리
           container.RegisterType<AboutMe.Domain.Service.Notice.INoticeService, AboutMe.Domain.Service.Notice.NoticeService>(); //Notice
           container.RegisterType<AboutMe.Domain.Service.Cart.ICartService, AboutMe.Domain.Service.Cart.CartService>(); //Cart
           container.RegisterType<AboutMe.Domain.Service.Order.IOrderService, AboutMe.Domain.Service.Order.OrderService>(); //Order

           container.RegisterType<AboutMe.Domain.Service.Promotion.IPromotionService, AboutMe.Domain.Service.Promotion.PromotionService>(); //Cart
           container.RegisterType<AboutMe.Domain.Service.Review.IReviewService, AboutMe.Domain.Service.Review.ReviewService>(); //Review
           
           container.RegisterInstance<IFilterProvider>("FilterProvider", new FilterProvider(container));
           container.RegisterInstance<IActionFilter>("LogActionFilter", new TraceActionFilter());
           **/

            container.RegisterType<AboutMe.Web.Mobile.Controllers.AccountController>(new InjectionConstructor());

            return container;
        }
    }
}
