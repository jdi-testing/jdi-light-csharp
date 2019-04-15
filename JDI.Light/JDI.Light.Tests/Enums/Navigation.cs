using System.ComponentModel;

namespace JDI.Light.Tests.Enums
{
    public enum Navigation
    {
        Home,
        [Description("Contact form")]
        ContactForm,
        Support,
        Dates,
        Service,
        [Description("Complex Table")]
        ComplexTable,
        [Description("Simple Table")]
        SimpleTable,
        [Description("User Table")]
        UserTable,
        [Description("Table with pages")]
        TableWithPages,
        [Description("Different elements")]
        DifferentElements,
        [Description("Metals & Colors")]
        MetalsColors,
        Performance,
        [Description("Elements packs")]
        ElementsPacks,
        [Description("HTML 5")]
        Html5,
        Bootstrap
    }
}
