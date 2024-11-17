using OpenQA.Selenium;

namespace QALabs.Automation.Core.Helpers
{
    public class UIElement
    {
        public UIElement(By by)
        {
            By = by;
        }

        public By By { get; set; }

        public UIElement Parent { get; set; }

        public UIElement Click()
        {
            SeleniumWebDriver.NativeDriver.FindElement(this).Click();
            return this;
        }

        public void Click(Func<IWebElement, bool> findElementCondition)
        {
            SeleniumWebDriver.NativeDriver.FindElement(this).Click();
        }

        public string GetText(Func<IWebElement, bool> findElementCondition = null)
        {
            return SeleniumWebDriver.NativeDriver.FindElement(this).Text;
        }
    }
}
