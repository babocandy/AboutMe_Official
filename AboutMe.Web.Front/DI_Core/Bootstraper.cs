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

            container.RegisterType<AboutMe.Domain.Service.AdminEtc.IAdminUserService, AboutMe.Domain.Service.AdminEtc.AdminUserService>(); //관리자관리
            container.RegisterType<AboutMe.Domain.Service.Faq.IFaqService, AboutMe.Domain.Service.Faq.FaqService>(); //Faq
            container.RegisterType<AboutMe.Domain.Service.Notice.INoticeService, AboutMe.Domain.Service.Notice.NoticeService>(); //Notice
            container.RegisterType<AboutMe.Domain.Service.Cart.ICartService, AboutMe.Domain.Service.Cart.CartService>(); //Cart

            /**
            container.RegisterInstance<IFilterProvider>("FilterProvider", new FilterProvider(container));
            container.RegisterInstance<IActionFilter>("LogActionFilter", new TraceActionFilter());
            **/

            container.RegisterType<AboutMe.Web.Front.Controllers.AccountController>(new InjectionConstructor());

            return container;
        }
    }
}
