using OpenQA.Selenium;

namespace QALabs.Automation.Core.Helpers
{
    public class TextElement : UIElement
    {
        public TextElement(By by)
            : base(by)
        { }

        public TextElement EnterText(string textValue, bool cleartext = true)
        {
            if (cleartext)
            {
                ClearText();
            }

            SeleniumWebDriver.NativeDriver.FindElement(this).SendKeys(textValue);

            return this;
        }

        public void ClearText()
        {
            SeleniumWebDriver.NativeDriver.FindElement(this).Clear();
        }

        public string GetValue()
        {
            return GetText();
        }
    }
}
