namespace LoansManager.Services.Resources
{
    public static class RegisterUserCommandValidatorResource
    {
        public const string UserExists = "Provided user already exists.";
        public const string PasswortInvalid = "Password required lenght is between 8 and 20 signs. It should contains at least one: digit, lowercase, uppercase letter, special sign out of  \"@#$%\".";
    }
}
