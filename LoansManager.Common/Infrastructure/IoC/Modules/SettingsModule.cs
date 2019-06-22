using Autofac;
using LoansManager.BussinesLogic.Infrastructure.SettingsModels;
using Microsoft.Extensions.Configuration;

namespace LoansManager.BussinesLogic.Infrastructure.IoC.Modules
{
    public class SettingsModule : Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<JwtSettings>())
                   .SingleInstance();

            builder.RegisterInstance(_configuration.GetSettings<ApiSettings>())
                   .SingleInstance();
        }
    }
}
