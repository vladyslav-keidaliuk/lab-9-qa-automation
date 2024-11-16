using OpenQA.Selenium;

namespace Core.DriverFactory;

public abstract class BaseDriverFactory
{
    public abstract IWebDriver CreateDriver();
}