using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;

namespace JDI.Light.Tests.UIObjects
{
    public class Html5Page : WebPage
    {
        [FindBy(Css = "#avatar")]
        public FileInput FileInput { get; set; }

        [FindBy(XPath = "//a[@href='/JDI/images/jdi-logo.jpg']")]
        public Link FileDownload { get; set; }
		
        [FindBy(Css = ".btn-group")]
        public MultiDropdown MultiDropdown { get; set; }

        [FindBy(Css = "#ages")]
        public MultiSelector Ages { get; set; }

        [FindBy(Id = "blue-button")]
        public IButton BlueButton { get; set; }

        [FindBy(Css = "h1")]
        public ILabel JdiLabel { get; set; }

        [FindBy(Css = "div:nth-child(12) > div.html-left")]
        public IRadioButtons ColorsRadioButton { get; set; }

        [FindBy(Css = "div:nth-child(11) > div.html-left")]
        public ICheckList WeatherCheckList { get; set; }

        [FindBy(Css = "#booking-time")]
        public IDateTimeSelector BookingTime { get; set; }

        [FindBy(Css = "#month-date")]
        public IDateTimeSelector MonthOfHolidays { get; set; }

        [FindBy(Css = "#birth-date")]
        public IDateTimeSelector BirthDate { get; set; }
        
        [FindBy(Css = "#party-time")]
        public IDateTimeSelector PartyTime { get; set; }

        [FindBy(Css = "#autumn-week")]
        public IDateTimeSelector AutumnDateTime { get; set; }

        [FindBy(Css = "#volume")]
        public IRange VolumeRange { get; set; }

        public IDropDown DressCode { get; set; }

        public IDropDown DisabledDropdown { get; set; }
        
        [JDataList("#ice-cream", "#ice-cream-flavors > option")]
        public DataList IceCream { get; set; }

        [JDataList("#ice-cream", "#ice-cream-flavors > option")]
        public ComboBox IceCreamComboBox { get; set; }

        public IProgressBar Progress { get; set; }

        [FindBy(Css = "#height")]
        public INumberSelector Height { get; set; }
        
        public IColorPicker ColorPicker { get; set; }
        
        public IColorPicker DisabledPicker { get; set; }
    }
}