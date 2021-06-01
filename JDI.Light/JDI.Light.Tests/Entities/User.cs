using JDI.Light.Attributes;

namespace JDI.Light.Tests.Entities
{
    public class User
    {
        public static readonly User DefaultUser = new User("Roman", "Jdi1234");

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        [Name("Login")]
        public string Login;

        [Name("Password")]
        public string Password { get; set; }
    }
}