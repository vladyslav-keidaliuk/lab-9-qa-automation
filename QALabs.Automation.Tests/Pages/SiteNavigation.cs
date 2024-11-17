using QALabs.Automation.Core;
using QALabs.Automation.Core.Configuration;

namespace QALabs.Automation.Tests.Pages;

public class SiteNavigation : MainPage
{
    public SiteNavigation(SeleniumWebDriver driverManager) : base(driverManager)
    {
    }

    public static void GoToBookstore(SeleniumWebDriver driver)
    {
        driver.GoToUrl(Config.Model.Url);
    }
}