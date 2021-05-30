﻿using System.Collections.Generic;
using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;
using ICheckList = JDI.Light.Interfaces.Complex.ICheckList;

namespace JDI.Light.Tests.UIObjects
{
    public class Html5Page : WebPage
    {
        [FindBy(Css = "#avatar")]
        public FileInput FileInput { get; set; }

        [FindBy(Css = "input[type=file][disabled]")]
        public FileInput DisabledFileInput { get; set; }

        [FindBy(XPath = "//a[@href='/JDI/images/jdi-logo.jpg']")]
        public Link FileDownload { get; set; }

        [FindBy(Css = "#accept-conditions")]
        public CheckBox AcceptConditions { get; set; }

        [FindBy(Css = ".btn-group")]
        public MultiDropdown MultiDropdown { get; set; }

        public MultiSelector Ages { get; set; }

        [FindBy(Id = "blue-button")]
        public IButton BlueButton { get; set; }

        [FindBy(Css = ".red")]
        public IButton RedButton { get; set; }
        
        public IButton DisabledButton { get; set; }

        public IButton SuspendButton { get; set; }

        [FindBy(Css = "h1")]
        public ILabel JdiLabel { get; set; }

        [FindBy(Css = "div:nth-child(12) > div.html-left")]
        public IRadioButtons ColorsRadioButton { get; set; }

        [FindBy(Css = "div:nth-child(11) > div.html-left")]
        public ICheckList WeatherCheckList { get; set; }

        [FindBy(Css = "#booking-time")]
        public IDateTimeSelector BookingTime { get; set; }

        [FindBy(Css = "#month-date")]
        public IDateTimeSelector MonthDate { get; set; }

        [FindBy(Css = "#birth-date")]
        public IDateTimeSelector BirthDate { get; set; }
        
        [FindBy(Css = "#party-time")]
        public IDateTimeSelector PartyTime { get; set; }

        [FindBy(Css = "#autumn-week")]
        public IDateTimeSelector AutumnWeek { get; set; }

        [FindBy(Css = "#volume")]
        public IRange Volume { get; set; }

        [FindBy(Css = "input[type='range'][disabled]")]
        public IRange DisabledRange { get; set; }

        public IRange VolumeRange { get; set; }

        public DropDown DressCode { get; set; }

        public IDropDown DisabledDropdown { get; set; }

        [JDataList("#disabled-dropdown", "#disabled-dropdown > option")]
        public DataList DisabledDropdownAsDataList { get; set; }

        [FindBy(XPath = ".//datalist[@id='ice-cream-flavors']/option")]
        public List<IWebElement> DropdownAsDataList { get; set; }

        [JDataList("#ice-cream", "#ice-cream-flavors > option")]
        public DataList IceCream { get; set; }

        [JDataList("#ice-cream", "#ice-cream-flavors > option")]
        public ComboBox IceCreamComboBox { get; set; }
        
        [FindBy(Css = "[ui=jdi-title]")]
        public TextElement JdiTitle { get; set; }

        public Button GhostButton { get; set; }

        public IProgressBar Progress { get; set; }

        [FindBy(Css = "[ui=jdi-text]")]
        
        public TextElement JdiText { get; set; }

        [FindBy(Css = "#height")]
        public INumberSelector Height { get; set; }
        
        public ColorPicker ColorPicker { get; set; }
        
        public IColorPicker DisabledPicker { get; set; }

        [FindBy(Css = "div.main-content #your-name")]
        public TextField NameTextField { get; set; }

        [FindBy(Css = "#disabled-name")]
        public TextField SurnameTextField { get; set; }
        
        public ITextArea TextArea { get; set; }

        [FindBy(Css = "textarea:nth-child(4)")]
        public ITextArea DisabledTextArea { get; set; }

        [FindBy(Css = "[ui = github-link]")]
        public ILink GithubLink { get; set; }

        public Image JdiLogo { get; set; }
    }
}