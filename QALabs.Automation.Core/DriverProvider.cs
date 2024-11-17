using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using QALabs.Automation.Core.DriverFactory;

namespace QALabs.Automation.Core;

public enum Browser
{
    Chrome,
    Edge
}

public class DriverProvider
{
    public static IWebDriver GetDriver(Browser browser)
    {
        return browser switch
        {
            Browser.Chrome => new ChromeDriver(),
            Browser.Edge => new EdgeDriver(),
            _ => new ChromeDriver()
        };
    }

    public static BaseDriverFactory GetDriverFactory(Browser browser)
    {
        return browser switch
        {
            Browser.Chrome => new ChromeDriverFactory(),
            Browser.Edge => new EdgeDriverFactory(),
            _ => new ChromeDriverFactory()
        };
    }
}