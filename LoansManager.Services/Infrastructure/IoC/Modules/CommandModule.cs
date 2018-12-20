using Autofac;
using LoansManager.Services.CommandHandlers.Dispatcher;
using LoansManager.Services.CommandHandlers.Interface;
using System.Reflection;

namespace LoansManager.Config.IoCModules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .AsClosedTypesOf(typeof(ICommandHandler<>))
                    .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
