using Autofac;
using LoansManager.DAL.Repositories;
using LoansManager.DAL.Repositories.Interfaces;
using LoansManager.Services.Implementations;
using LoansManager.Services.Interfaces;

namespace LoansManager.Config
{
    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        }
    }
}
