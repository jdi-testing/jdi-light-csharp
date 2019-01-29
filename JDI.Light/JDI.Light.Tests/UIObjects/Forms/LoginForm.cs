using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Entities;

namespace JDI.Light.Tests.UIObjects.Forms
{
    public class LoginForm : Form<User>
    {
        [FindBy(Css = "button.btn-login")]
        public IButton LoginButton;

        [FindBy(XPath = ".//div[@class='logout']/button")]
        public IButton LogoutButton;

        [FindBy(Css = "#name")]
        [Name("Login")]
        public ITextField LoginField;

        [FindBy(Css = "#password")]
        [Name("Password")]
        public ITextField PasswordField;
    }
}