using System.Collections.ObjectModel;
using Core.Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using QALabs.Automation.Core.Interaction;
using SeleniumExtras.WaitHelpers;

namespace Core;

public class SeleniumWebDriver
{
    private static readonly ThreadLocal<SeleniumWebDriver> _instance = new(() => new SeleniumWebDriver());
    // public static SeleniumWebDriver NativeDriver => _instance.Value;
    // private IWebDriver _driver;
    // public IWebDriver Driver => _driver ??= DriverManager.GetDriver();
    public static SeleniumWebDriver NativeDriver => _instance.Value;
    public IWebDriver Driver => DriverManager.GetDriver();

    private WebDriverWait? Waiter => new(Driver, TimeSpan.FromSeconds(10));
    private Actions Action => new(Driver);
    private IJavaScriptExecutor JavaScriptExecutor => (IJavaScriptExecutor)Driver;

    public void ScrollByElement(By elementBy)
    {
        try
        {
            var element = Waiter!.Until(w => w.FindElement(elementBy));

            while (!element.Displayed)
            {
                JavaScriptExecutor.ExecuteScript($"window.scrollBy(0, {100});");
            }

            Action.MoveToElement(element).Perform();
            JavaScriptExecutor.ExecuteScript($"window.scrollBy(0, {500});");
        }
        catch (Exception e)
        {
            CustomLogger.Error(e, $"Exception at ScrollByElement with locator '{elementBy}' ");
        }
    }

    public void JSExecutorClickOnElementBy(By element)
    {
        try
        {
            ((IJavaScriptExecutor)NativeDriver.Driver).ExecuteScript("arguments[0].click();",
                NativeDriver.Driver.FindElement(element));
        }
        catch (Exception e)
        {
            CustomLogger.Error(e, "Exception at JSExecutorClickOnElementBy");
        }
    }

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

    public bool WaitForCondition(Func<bool> condition)
    {
        WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
        try
        {
            return wait.Until(_ => condition());
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }

    public void WaitUntilElementToBeClickable(By locator)
    {
        Waiter!.Until(ExpectedConditions.ElementToBeClickable(locator));
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

    public void SwipeElementNTimes(By locator, int count)
    {
        try
        {
            for (var i = 0; i < count; i++)
            {
                var element = Waiter!.Until(w => w.FindElement(locator));
                Action
                    .ClickAndHold(element)
                    .DragAndDropToOffset(element, -100, 0)
                    .Release()
                    .Pause(TimeSpan.FromSeconds(2))
                    .Perform();
            }
        }
        catch (Exception e)
        {
            CustomLogger.Error(e, "Exception in SwipeElementNTimes");
        }
    }
}