using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Interfaces.Common;
using JDI.Light.Utils;

namespace JDI.Light.Extensions
{
    public static class UIElementExtensions
    {
        public static Button GetButton(this UIElement e, string buttonName)
        {
            var fields = e.GetFields(typeof(IButton));
            switch (fields.Count)
            {
                case 0:
                    throw JDI.Assert.Exception($"Can't find ny buttons on form {e.ToString()}'");
                case 1:
                    return (Button)fields[0].GetValue(e);
                default:
                    var buttons = fields.Select(f => (Button)f.GetValue(e)).ToList();
                    var button = buttons.FirstOrDefault(b => b.Name.SimplifiedEqual(buttonName));
                    if (button == null)
                        throw JDI.Assert.Exception($"Can't find button '{buttonName}' for Element '{e.ToString()}'." +
                                                   $"(Found following buttons: {buttons.Select(el => el.Name).FormattedJoin()})."
                                                       .FromNewLine());
                    return button;
            }
        }
    }
}