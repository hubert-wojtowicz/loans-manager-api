using System;

namespace LoansManager.WebApi.Helper
{
    public interface IUriHelperService
    {
        Uri GetApiUrl(string path);
    }
}