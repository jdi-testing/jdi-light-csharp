using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Entities;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class LoginForm : Form<User>
    {
        [FindBy(Css = "button.btn-login")]
        public IButton LoginButton;

        [FindBy(Css = "#Name")]
        [Name("Login")]
        public ITextField LoginField;

        [FindBy(Css = "#Password")]
        [Name("Password")]
        public ITextField PasswordField;

        [FindBy(Css = "a>div.profile-photo")]
        public IClickable Profile;

        public new void Submit(User user)
        {
            Profile.Click();
            base.Submit(user);
        }
    }
}