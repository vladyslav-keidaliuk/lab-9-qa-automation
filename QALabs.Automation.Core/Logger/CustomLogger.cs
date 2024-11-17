using NLog;
using OpenQA.Selenium;

namespace QALabs.Automation.Core.Logger;

public static class CustomLogger
{
    private static ILogger _logger = LogManager.GetCurrentClassLogger();

    public static void Info(string message)
    {
         _logger.Info(message);
    }

    public static void Warn(string message)
    {
        _logger.Warn(message);
    }

    public static void Error(Exception ex, string message)
    {
        _logger.Error(ex, message);
    }

    public static void Error(string message)
    {
        _logger.Error(message);
    }

    public static void Error(Exception ex)
    {
        _logger.Error(ex);
    }

    public static void LogScreenshot(IWebDriver driver)
    {
        ITakesScreenshot screenshot = (ITakesScreenshot)driver;
        Screenshot shot = screenshot.GetScreenshot();
        var timeStamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
        var screenshotFileName = $"{timeStamp}_FailureScreenshot.png";
        var folderName = @"errors\";
        var artifactsDirectory = Path.Combine(folderName);
        var screenshotPath = Path.Combine(artifactsDirectory, screenshotFileName);

        if (!Directory.Exists(artifactsDirectory))
        {
            Directory.CreateDirectory(artifactsDirectory);
        }

        shot.SaveAsFile(screenshotPath);
    }
}