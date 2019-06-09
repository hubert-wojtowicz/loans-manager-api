using LoansManager.Services.Dtos;

namespace LoansManager.Services.ServicesContracts
{
    public interface IJwtService
    {
        JwtDto GenerateToken(string userName, string role);
    }
}
