using Core.Logger;
using Core;
using NUnit.Framework.Interfaces;
using static Core.SeleniumWebDriver;

namespace CourseWorkWeb.Tests.CourseWorkWebTests;

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
            {
                CustomLogger.LogScreenshot(NativeDriver.Driver);
            }
        }
        finally
        {
            DriverManager.QuitDriver();
        }
    }
}