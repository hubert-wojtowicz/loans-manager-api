namespace LoansManager.DAL.Entities
{
    public class UserEntity
    {
        public string UserName { get; set; }

        public string Salt { get; set; }

        public string Password { get; set; }
    }
}
