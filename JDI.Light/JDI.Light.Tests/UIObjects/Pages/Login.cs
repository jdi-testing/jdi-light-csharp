using JDI.Light.Elements.Composite;
using JDI.Light.Tests.Entities;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class Login : Form<User>
    {
        public Login(By locator) : base(locator)
        {
        }
    }
}