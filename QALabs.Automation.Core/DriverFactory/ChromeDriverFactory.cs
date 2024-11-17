using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QALabs.Automation.Core.Configuration;

namespace QALabs.Automation.Core.DriverFactory;

public class ChromeDriverFactory : BaseDriverFactory
{
    public override IWebDriver CreateDriver()
    {
        var options = new ChromeOptions();
        options.AddArgument("--incognito");
        options.AddArgument("--no-sandbox");
        if (Config.Model.IsHeadlessModeOn)
        {
            options.AddArgument("--headless=new");
        }
        
        return new ChromeDriver(options);
    }
}