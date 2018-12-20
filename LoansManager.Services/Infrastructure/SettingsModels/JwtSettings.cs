using System;
using System.Collections.Generic;
using System.Text;

namespace LoansManager.Services.Config.SettingsModels
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
