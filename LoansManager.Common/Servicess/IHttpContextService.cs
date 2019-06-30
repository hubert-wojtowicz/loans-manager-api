using System;

namespace LoansManager.Common.Services
{
    public interface IHttpContextService
    {
        Uri GetApiUrl(string path);
    }
}