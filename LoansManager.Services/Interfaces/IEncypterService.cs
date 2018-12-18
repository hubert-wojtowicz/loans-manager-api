using System;
using System.Collections.Generic;
using System.Text;

namespace LoansManager.Services.Interfaces
{
    public interface IEncypterService
    {
        string GetSalt(string value);
        string GetHash(string value, string salt);
    }
}
