using System;
using Microsoft.Extensions.Configuration;

namespace LoansManager.BussinesLogic.Infrastructure.IoC.Modules
{
    public static class SettingsExtensions
    {
        /// <summary>
        /// Bind model <typeparam name="T"/> with key from appsettings.json. If model name is SomeSettings, then key Some is mapped or if model name does not contain Settings suffix, then binded key is the same as model name.
        /// </summary>
        /// <typeparam name="T">Model to be mapped on.</typeparam>
        /// <param name="configuration">Microsoft.Extension.Configuration.IConfiguration abstraction implementation.</param>
        /// <returns>Setting's model.</returns>
        public static T GetSettings<T>(this IConfiguration configuration)
            where T : new()
        {
            if (configuration == null)
            {
                throw new ArgumentNullException($"{nameof(configuration)} can not be null");
            }

            var section = typeof(T).Name.Replace("Settings", string.Empty, StringComparison.InvariantCulture);
            var configurationValue = new T();
            configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}
