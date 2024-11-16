using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using Core.Configuration;

namespace Core.DriverFactory;

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