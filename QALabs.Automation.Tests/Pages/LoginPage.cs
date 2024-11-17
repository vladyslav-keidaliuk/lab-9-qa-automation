using QALabs.Automation.Core;
using QALabs.Automation.Core.Helpers;

namespace QALabs.Automation.Tests.Pages;

public class LoginPage : MainPage
{
    public LoginPage(SeleniumWebDriver _driver) : base(_driver)
    {
    }

    public TextElement EmailTextBox => TextElementByCss("#Input_Email");
    public TextElement PasswordTextBox => TextElementByCss("#Input_Password");
    public UIElement LogInButton => UIElementByCss("#login-submit");

    public TextElement LockedOut => TextElementByCss("body > div > main > header > h1");

    public bool Login(string email, string password)
    {
        try
        {
            EmailTextBox.EnterText(email);
            PasswordTextBox.EnterText(password);
            LogInButton.Click();

            try
            {
                var username = UsernameInsideSystemAfterLogin.GetText();
                if (username.Equals($"Hello {email}!"))
                    return true;
                return false;
            }
            catch (Exception e)
            {
                var lockedOut = LockedOut.GetValue();
                if (lockedOut.Equals("Locked Out")) return false;
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return false;
    }
}