namespace mantis_tests.model
{
    public class AccountData
    {
        public AccountData()
        {
        }

        public AccountData(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}