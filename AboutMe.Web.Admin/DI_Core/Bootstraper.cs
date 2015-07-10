using System;
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
            container.RegisterType<AboutMe.Domain.Service.AdminPoint.IAdminPointService, AboutMe.Domain.Service.AdminPoint.AdminPointService>(); //관리자 포인트
            container.RegisterType<AboutMe.Domain.Service.AdminPromotion.IAdminPromotionService, AboutMe.Domain.Service.AdminPromotion.AdminPromotionService>(); //프로모션관리


            /**
            container.RegisterInstance<IFilterProvider>("FilterProvider", new FilterProvider(container));
            container.RegisterInstance<IActionFilter>("LogActionFilter", new TraceActionFilter());
            **/

            container.RegisterType<AboutMe.Web.Admin.Controllers.AccountController>(new InjectionConstructor());

            return container;
        }
    }
}
