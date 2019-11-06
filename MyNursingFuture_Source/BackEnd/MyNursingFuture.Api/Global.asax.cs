using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using MyNursingFuture.BL.Managers;
using Newtonsoft.Json.Serialization;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace MyNursingFuture.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<IUsersManager, UsersManager>(Lifestyle.Scoped);
            container.Register<IEmployersManager, EmployersManager>(Lifestyle.Scoped);
            container.Register<IFrameworkManager, FrameworkManager>(Lifestyle.Scoped);
            container.Register<ICacheManager, CacheManager>(Lifestyle.Scoped);
            container.Register<IAppConfigurationsManager, AppConfigurationsManager>(Lifestyle.Scoped);
            container.Register<IReportManager, ReportManager>(Lifestyle.Scoped);

            container.Register<IJobApplicationManager, JobApplicationManager>(Lifestyle.Scoped);
            container.Register<IJobListingCriteriaManager, JobListingCriteriaManager>(Lifestyle.Scoped);
            container.Register<IJobListingManager, JobListingManager>(Lifestyle.Scoped);
            container.Register<INurseSelfAssessmentAnswersManager, NurseSelfAssessmentAnswersManager>(Lifestyle.Scoped);
            container.Register<IAnswersManager, AnswersManager>(Lifestyle.Scoped);
            container.Register<IQuestionsManager, QuestionsManager>(Lifestyle.Scoped);
            //Template for further 
            /*            
                        container.Register<>(Lifestyle.Scoped);
                        container.Register<>(Lifestyle.Scoped);*/

            container.Register<ICredentialsManager, CredentialsManager>(Lifestyle.Singleton);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
           
            container.Verify();

            AutoMapperConfig.Initialize();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            GlobalConfiguration.Configure(WebApiConfig.Register);

            
        }
    }
}
