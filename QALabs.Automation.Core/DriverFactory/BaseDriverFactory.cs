using OpenQA.Selenium;

namespace QALabs.Automation.Core.DriverFactory;

public abstract class BaseDriverFactory
{
    public abstract IWebDriver CreateDriver();
}