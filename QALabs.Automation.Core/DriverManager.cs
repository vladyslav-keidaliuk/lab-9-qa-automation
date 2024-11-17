using OpenQA.Selenium;
using QALabs.Automation.Core.Configuration;

namespace QALabs.Automation.Core;

public class DriverManager
{
    private static ThreadLocal<IWebDriver> Driver = new();

    public static IWebDriver GetDriver()
    {
        if (Driver.Value == null)
        {
            var browserSelected = Enum.TryParse(Config.Model.Browser, out Browser browser) ? browser : Browser.Chrome;
            Driver.Value = DriverProvider.GetDriverFactory(browserSelected).CreateDriver();
        }

        return Driver.Value;
    }

    public static void QuitDriver()
    {
        SeleniumWebDriver.NativeDriver.Driver.Close();

        if (Driver.Value != null)
        {
            Driver.Value.Quit();
            Driver.Value.Dispose();
            Driver.Value = null;
        }
    }
}