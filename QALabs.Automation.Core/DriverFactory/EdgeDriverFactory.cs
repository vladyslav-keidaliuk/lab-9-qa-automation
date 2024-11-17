using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using QALabs.Automation.Core.Configuration;

namespace QALabs.Automation.Core.DriverFactory;

public class EdgeDriverFactory : BaseDriverFactory
{
    public override IWebDriver CreateDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("--incognito");
        options.AddArgument("--no-sandbox");
        if (Config.Model.IsHeadlessModeOn)
        {
            options.AddArgument("--headless=new");
        }

        return new EdgeDriver(options);
    }
}