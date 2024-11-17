using OpenQA.Selenium;
using QALabs.Automation.Core;

namespace QALabs.Automation.Tests.Pages;

public class UserListPage : MainPage
{
    public UserListPage(SeleniumWebDriver driver) : base(driver)
    {
    }

    public bool LockUnlockUserWithEmail(string email)
    {
        var status = false;
        IList<IWebElement> rows = SeleniumWebDriver.NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (var row in rows)
        {
            Thread.Sleep(1000);
            var emailCell = row.FindElement(By.XPath("./td[2]"));

            var getEmail = emailCell.Text;

            if (getEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
            {
                row.FindElement(By.XPath("./td[5]")).Click();
                return true;
            }
        }

        return status;
    }
}