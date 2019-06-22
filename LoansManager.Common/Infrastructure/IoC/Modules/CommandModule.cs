using Autofac;
using FluentValidation;
using LoansManager.BussinesLogic.Infrastructure.CommandsSetup;

namespace LoansManager.BussinesLogic.Infrastructure.IoC.Modules
{
    public class CommandModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                    .AsClosedTypesOf(typeof(ICommandHandler<>))
                    .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<FluentValidationFactory>().AsSelf();

            builder.RegisterType<CommandBus>()
                .As<ICommandBus>()
                .InstancePerLifetimeScope();
        }
    }
}
