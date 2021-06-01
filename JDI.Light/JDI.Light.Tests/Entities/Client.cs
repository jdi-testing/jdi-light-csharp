using JDI.Light.Attributes;

namespace JDI.Light.Tests.Entities
{
    public class Client
    {
        public readonly Client DefaultClient;

        public Client()
        {
            DefaultClient = new Client("Roman", "Jdi1234");
        }

        public Client(string login, string password)
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