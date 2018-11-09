using JDI.Core.Attributes;

namespace JDI.UIWebTests.Entities
{
    public class User
    {
        public static User DefaultUser = new User("epam", "1234");

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        [Name("Login")] public string Login { get; set; }

        [Name("Password")] public string Password { get; set; }
    }
}