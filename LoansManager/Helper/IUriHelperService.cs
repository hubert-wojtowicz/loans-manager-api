using System;

namespace LoansManager.Helper
{
    public interface IUriHelperService
    {
        Uri GetApiUrl(string path);
    }
}