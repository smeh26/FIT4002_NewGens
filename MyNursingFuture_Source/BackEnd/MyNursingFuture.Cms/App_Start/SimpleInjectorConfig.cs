using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MyNursingFuture.BL.Managers;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace MyNursingFuture.Cms.App_Start
{
    public class SimpleInjectorConfig
    {
        public static void Configure()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();

            //REGISTER SERVICES
            RegisterServices(container);

            //REGISTER AUTOMMAPER
            RegisterAutomapper(container);

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterAutomapper(Container container)
        {
            var profiles = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as AutoMapper.Profile);
                }
            });

            container.RegisterSingleton<MapperConfiguration>(config);
            container.Register<IMapper>(() => config.CreateMapper(container.GetInstance));
        }

        private static void RegisterServices(Container container)
        {
            // Register your types, for instance:
            container.Register<ISectionsManager, SectionsManager>(Lifestyle.Transient);
            container.Register<IContentItemsManager, ContentItemsManager>(Lifestyle.Transient);
            container.Register<IEnumManager, EnumManager>(Lifestyle.Transient);
            container.Register<ILinksManager, LinksManager>(Lifestyle.Transient);
            container.Register<ISectorsManager, SectorsManager>(Lifestyle.Transient);
            container.Register<ISectorViewsManager, SectorViewsManager>(Lifestyle.Transient);
            container.Register<IRolesManager, RolesManager>(Lifestyle.Transient);
            container.Register<IDomainsManager, DomainsManager>(Lifestyle.Transient);
            container.Register<IActionsManager, ActionsManager>(Lifestyle.Transient);
            container.Register<IAspectsManager, AspectsManager>(Lifestyle.Transient);
            container.Register<IDefinitionsManager, DefinitionsManager>(Lifestyle.Transient);
            container.Register<IPostCardsManager, PostCardsManager>(Lifestyle.Transient);
            container.Register<IDataExtractManager, DataExtractManager>(Lifestyle.Transient);
            container.Register<IEndorsedLogosManager, EndorsedLogosManager>(Lifestyle.Transient);
            container.Register<IMenuManager, MenusManager>(Lifestyle.Transient);
            container.Register<IArticlesManager, ArticlesManager>(Lifestyle.Transient);
            container.Register<ICategoriesManager, CategoriesManager>(Lifestyle.Transient);
            container.Register<IImageManager, ImageManager>(Lifestyle.Transient);
            container.Register<IQuizzesManager, QuizzesManager>(Lifestyle.Transient);
            container.Register<IQuestionsManager, QuestionsManager>(Lifestyle.Transient);
            container.Register<IAdministratorsManager, AdministratorsManager>(Lifestyle.Transient);
            container.Register<ILogChangesManager, LogChangesManager>(Lifestyle.Transient);
            container.Register<ICredentialsManager, CredentialsManager>(Lifestyle.Singleton);
            container.Register<IEmailManager, EmailManager>(Lifestyle.Transient);
        }
    }
}