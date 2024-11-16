using Core.DriverFactory;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

namespace Core;

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