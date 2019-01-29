using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Entities;

namespace JDI.Light.Tests.UIObjects.Forms
{
    public class LoginForm : Form<User>
    {
        [FindBy(Css = "button.btn-login")]
        [Name("Login")]
        public IButton LoginButton;
        
        [FindBy(Css = "#name")]
        [Name("Login")]
        public ITextField LoginField;

        [FindBy(Css = "#password")]
        [Name("Password")]
        public ITextField PasswordField;
    }
}