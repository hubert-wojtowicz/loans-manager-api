using Autofac;
using LoansManager.Services.Config.SettingsModels;
using Microsoft.Extensions.Configuration;

namespace LoansManager.Config.IoCModules
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
        }
    }
}
