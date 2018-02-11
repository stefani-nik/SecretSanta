using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using SecretSanta.Controllers;
using SecretSanta.Data;
using SecretSanta.Data.IInfrastructure;
using SecretSanta.Data.Infrastructure;
using SecretSanta.Data.IRepositories;
using SecretSanta.Data.Repositories;
using SecretSanta.Service.IServices;
using SecretSanta.Service.Services;
using SecretSanta.Utilities;

[assembly: OwinStartup(typeof(SecretSanta.Startup))]

namespace SecretSanta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SessionAuthorizeAttribute>().AsWebApiActionFilterFor<GroupController>().InstancePerRequest();
            builder.RegisterType<SessionAuthorizeAttribute>().AsWebApiActionFilterFor<UserController>().InstancePerRequest();
            builder.RegisterType<SessionAuthorizeAttribute>().AsWebApiActionFilterFor<AccountController>().InstancePerRequest();


            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
    

            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<GroupService>().As<IGroupService>().InstancePerRequest();
            builder.RegisterType<InvitationService>().As<IInvitationService>().InstancePerRequest();
            builder.RegisterType<ConnectionService>().As<IConnectionService>().InstancePerRequest();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterType<SecretSantaContext>().AsSelf().InstancePerRequest();
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerRequest();

            builder.RegisterType<ApplicationUserRepository>().As<IApplicationUserRepository>().InstancePerRequest();
            builder.RegisterType<GroupRepository>().As<IGroupRepository>().InstancePerRequest();
            builder.RegisterType<InvitationRepository>().As<IInvitationRepository>().InstancePerRequest();
            builder.RegisterType<ConnectionRepository>().As<IConnectionRepository>().InstancePerRequest();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerRequest();


            var config = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ConfigureAuth(app);
        }
    }
}