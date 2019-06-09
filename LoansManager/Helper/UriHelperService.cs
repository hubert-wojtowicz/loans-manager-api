using System;
using Microsoft.AspNetCore.Http;

namespace LoansManager.Helper
{
    public class UriHelperService : IUriHelperService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UriHelperService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Uri GetApiUrl(string path)
            => new Uri(_httpContextAccessor.HttpContext.Request.PathBase.Value + path);
    }
}
