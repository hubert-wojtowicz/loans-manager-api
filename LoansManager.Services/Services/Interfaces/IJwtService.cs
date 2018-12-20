using LoansManager.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoansManager.Services.Interfaces
{
    public interface IJwtService
    {
        JwtDto GenerateToken(string username, string role);
    }
}
