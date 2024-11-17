using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QALabs.Automation.Core.Helpers;
using QALabs.Automation.Core.Logger;
using SeleniumExtras.WaitHelpers;

namespace QALabs.Automation.Core;

public class SeleniumWebDriver
{
    private static readonly ThreadLocal<SeleniumWebDriver> _instance = new(() => new SeleniumWebDriver());
    
    public static SeleniumWebDriver NativeDriver => _instance.Value;
    public IWebDriver Driver => DriverManager.GetDriver();

    private WebDriverWait? Waiter => new(Driver, TimeSpan.FromSeconds(10));

    public void GoToUrl(string? url)
    {
        try
        {
            Driver.Navigate().GoToUrl(url);
        }
        catch (Exception e)
        {
            CustomLogger.Error(e);
        }
    }

    public IWebElement FindElement(UIElement element)
    {
        try
        {
            return Waiter!.Until(w => w.FindElement(element.By));
        }
        catch (WebDriverTimeoutException ex)
        {
            CustomLogger.Error(ex, "Exception in FindElement.");

            throw new WebDriverTimeoutException();
        }
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        try
        {
            return Waiter!.Until(w => w.FindElements(by));
        }
        catch (Exception ex)
        {
            CustomLogger.Error(ex, "Exception in FindElements.");

            throw new WebDriverTimeoutException();
        }
    }

    public bool IsElementDisplayed(By locator)
    {
        try
        {
            return Waiter!.Until(ExpectedConditions.ElementIsVisible(locator)).Displayed;
        }
        catch
        {
            CustomLogger.Warn($"Element by locator '{locator}'is not displayed");

            return false;
        }
    }
}