using LoansManager.BussinesLogic.Dtos.Users;

namespace LoansManager.BussinesLogic.Interfaces
{
    public interface IJwtService
    {
        JwtDto GenerateToken(string userName, string role);
    }
}
