namespace LoansManager.BussinesLogic.Interfaces
{
    public interface IEncypterService
    {
        string GetSalt(string value);

        string GetHash(string value, string salt);
    }
}
