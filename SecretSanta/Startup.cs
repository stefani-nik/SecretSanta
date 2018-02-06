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
using SecretSanta.Service;
using SecretSanta.Service.IServices;
using SecretSanta.Service.Services;

[assembly: OwinStartup(typeof(SecretSanta.Startup))]

namespace SecretSanta
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

          //  builder.RegisterType<SessionAuthorizeAttribute>().AsWebApiActionFilterFor<GroupsController>().InstancePerRequest();
          //  builder.RegisterType<SessionAuthorizeAttribute>().AsWebApiActionFilterFor<UsersController>().InstancePerRequest();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<GroupService>().As<IGroupService>().InstancePerRequest();
            builder.RegisterType<InvitationService>().As<IInvitationService>().InstancePerRequest();
            builder.RegisterType<ConnectionService>().As<IConnectionService>().InstancePerRequest();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<SecretSantaContext>().AsSelf().InstancePerRequest();
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerRequest();


            var config = GlobalConfiguration.Configuration;
            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ConfigureAuth(app);
        }
    }
}