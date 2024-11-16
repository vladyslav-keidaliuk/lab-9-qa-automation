using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CourseWorkWeb.Tests.Pages;

public class UserListPage : MainPage
{
    
    public UserListPage(SeleniumWebDriver driver) : base(driver)
    {
        
    }
    
    public bool LockUnlockUserWithEmail(string email)
    {
        bool status = false;
        Thread.Sleep(2000);
        IList<IWebElement> rows = SeleniumWebDriver.NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (IWebElement row in rows)
        {
            Thread.Sleep(1000);
            IWebElement emailCell = row.FindElement(By.XPath("./td[2]"));
            
            string getEmail = emailCell.Text;
            
            if (getEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
            {
                
                IWebElement Cell = row.FindElement(By.XPath("./td[5]"));
                Cell.Click();
                Thread.Sleep(5000);
                return true;
            }
        }
        return status;
    }
    
    
}