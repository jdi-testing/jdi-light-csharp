using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Forms
{
    public class LoginFormClient : WebPage
    {
        [FindBy(Css = "#name")] TextField Login;
        [FindBy(Css = "#password")] TextField Password;
        [FindBy(Css = "button.btn-login")] public Button LoginButton;
    }
}