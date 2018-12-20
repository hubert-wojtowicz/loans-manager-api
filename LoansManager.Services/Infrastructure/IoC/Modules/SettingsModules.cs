using Autofac;
using LoansManager.Services.Infrastructure.SettingsModels;
using Microsoft.Extensions.Configuration;

namespace LoansManager.Services.Infrastructure.IoC.Modules
{
    public class SettingsModule : Module
    {
        private readonly IConfiguration configuration;

        public SettingsModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(configuration.GetSettings<JwtSettings>())
                   .SingleInstance();

            builder.RegisterInstance(configuration.GetSettings<ApiSettings>())
                   .SingleInstance();
        }
    }
}
