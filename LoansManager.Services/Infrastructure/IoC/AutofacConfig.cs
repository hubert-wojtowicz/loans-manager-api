using Autofac;
using LoansManager.DAL.Repositories;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Implementations.Services;
using LoansManager.Services.ServicesContracts;

namespace LoansManager.Services.Infrastructure.IoC
{
    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterType<EncrypterService>().As<IEncypterService>().InstancePerLifetimeScope();

            builder.RegisterType<JwtService>().As<IJwtService>().InstancePerLifetimeScope();
        }
    }
}
