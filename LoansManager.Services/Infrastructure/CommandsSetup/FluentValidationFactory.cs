using System;
using Autofac;
using FluentValidation;

namespace LoansManager.Services.Infrastructure.CommandsSetup
{
    public class FluentValidationFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext componentContext;

        public FluentValidationFactory(IComponentContext componentContext)
        {
            this.componentContext = componentContext;
        }

        public override IValidator CreateInstance(Type validatorType)
            => componentContext.Resolve(validatorType) as IValidator;
    }
}