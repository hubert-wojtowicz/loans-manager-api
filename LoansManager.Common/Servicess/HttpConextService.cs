using System;
using Microsoft.AspNetCore.Http;

namespace LoansManager.Common.Services
{
    public class HttpConextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpConextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Uri GetApiUrl(string path)
            => new Uri(_httpContextAccessor.HttpContext.Request.PathBase.Value + path);
    }
}
