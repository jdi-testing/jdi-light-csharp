using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Tests.Entities;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class LoginForm : Form<User>
    {
        [FindBy(Css = "button.btn-login")] private IButton _loginButton;

        [FindBy(Id = "Login")] [Name("Login")] private ITextField _loginField;

        [FindBy(Id = "Password")] [Name("Password")]
        private ITextField _passwordField;

        [FindBy(Css = "a>div.profile-photo")] private IClickable _profile;

        public new void Submit(User user)
        {
            _profile.Click();
            base.Submit(user);
        }
    }
}