using Core;
using CourseWorkWeb.Tests.UtilityLibrary;
using Internal.BoardGames.Core.Web.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using QALabs.Automation.Core.Interaction;

namespace CourseWorkWeb.Tests.Pages;

public class LoginPage : MainPage
{
    // private readonly By EmailTextBox = By.CssSelector(
    //     "#Input_Email");

    public TextElement EmailTextBox => TextElementByCss("#Input_Email");

    public TextElement PasswordTextBox => TextElementByCss("#Input_Password");

    public UIElement LogInButton => UIElementByCss("#login-submit");

    private readonly
        By LoginForm = By.XPath("#account");

    public TextElement LockedOut => TextElementByCss("body > div > main > header > h1");
    
    public LoginPage(SeleniumWebDriver _driver) : base(_driver)
    {
        
    }

    public bool Login(string email, string password)
    {
        try
        {
            EmailTextBox.EnterText(email);
            PasswordTextBox.EnterText(password);
            LogInButton.Click();

            try
            {
                string username = UsernameInsideSystemAfterLogin.GetText();
                if (username.Equals($"Hello {email}!"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                string lockedOut = LockedOut.GetValue();
                if (lockedOut.Equals("Locked Out"))
                {
                    return false;
                }
            }
            
        }
        catch (Exception e)
        {
            return false;
        }

        return false;
    }
}