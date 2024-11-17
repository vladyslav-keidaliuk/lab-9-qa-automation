using NUnit.Framework.Interfaces;
using QALabs.Automation.Core;
using QALabs.Automation.Core.Logger;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Tests;

[TestFixture]
public class BaseTest
{
    [SetUp]
    public void Setup()
    {
        NativeDriver.Driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                CustomLogger.LogScreenshot(NativeDriver.Driver);
        }
        finally
        {
            DriverManager.QuitDriver();
        }
    }
}