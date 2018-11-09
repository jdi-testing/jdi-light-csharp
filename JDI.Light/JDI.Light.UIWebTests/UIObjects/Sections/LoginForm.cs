using JDI.Core.Attributes;
using JDI.Core.Interfaces.Base;
using JDI.Core.Interfaces.Common;
using JDI.UIWebTests.Entities;
using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Sections
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