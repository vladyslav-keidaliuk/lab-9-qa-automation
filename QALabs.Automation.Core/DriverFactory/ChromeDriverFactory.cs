using Core.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Core.DriverFactory;

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