using JDI.Light.Attributes;

namespace JDI.Light.Tests.Entities
{
    public class Client
    {
        public readonly Client DefaultClient;
        public string Login, Password;

        public Client()
        {
            DefaultClient = new Client("Roman", "Jdi1234");
        }

        public Client(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}