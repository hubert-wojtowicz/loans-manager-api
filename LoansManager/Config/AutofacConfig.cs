using Autofac;
using LoansManager.Services.Implementations;
using LoansManager.Services.Interfaces;

namespace LoansManager.Config
{
    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }
    }
}
