using JDI.Light.Attributes;

namespace JDI.Light.Tests.Entities
{
    public class Client
    {
        public static Client DefaultClient = new Client("epam", "1234");
        public string Login, Password;

        public Client(string login, string password)
        {
            Login = login;
            Password = password;
        }
        
    }
}