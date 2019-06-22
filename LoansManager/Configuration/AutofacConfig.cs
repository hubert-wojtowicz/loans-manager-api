using Autofac;
using LoansManager.BussinesLogic.Implementations.Services;
using LoansManager.BussinesLogic.Interfaces;
using LoansManager.DAL.Repositories;
using LoansManager.DAL.Repositories.Interfaces;

namespace LoansManager.WebApi.Configuration
{
    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<LoansService>().As<ILoansService>().InstancePerLifetimeScope();
            builder.RegisterType<LoansRepository>().As<ILoansRepository>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<EncrypterService>().As<IEncypterService>().InstancePerLifetimeScope();

            builder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
        }
    }
}
