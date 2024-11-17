using OpenQA.Selenium;
using QALabs.Automation.Core;
using QALabs.Automation.Core.Helpers;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Pages;

public class CreateUserPage : MainPage
{
    public CreateUserPage(SeleniumWebDriver driver) : base(driver)
    {
    }

    public UIElement NewUserButton => UIElementByXPath("//a[@class='btn btn-primary']");

    public UIElement EmailInput => UIElementByCss("#Input_Email");
    public UIElement NameInput => UIElementByCss("#Input_Name");
    public UIElement PhoneNumberInput => UIElementByCss("#Input_PhoneNumber");
    public UIElement PasswordInput => UIElementByCss("#Input_Password");
    public UIElement ConfirmPasswordInput => UIElementByCss("#Input_ConfirmPassword");
    public UIElement StreetAddressInput => UIElementByCss("#Input_StreetAddress");
    public UIElement CityInput => UIElementByCss("#Input_City");
    public UIElement StateInput => UIElementByCss("#Input_State");
    public UIElement PostalCodeInput => UIElementByCss("#Input_PostalCode");
    public UIElement RegisterButton => UIElementByCss("#registerSubmit");
    public UIElement RoleOption => UIElementByXPath("//*[@id='Input_Role']/option[4]");

    public void NewCompanyButtonClick()
    {
        NativeDriver.FindElement(NewUserButton).Click();
    }

    public void NewCustomerCreate(
        string email,
        string name,
        string phoneNumber,
        string password,
        string confirmPassword,
        string streetAddress,
        string city,
        string state,
        string postalCode
    )
    {
        NativeDriver.FindElement(EmailInput).SendKeys(email);
        NativeDriver.FindElement(NameInput).SendKeys(name);
        NativeDriver.FindElement(PhoneNumberInput).SendKeys(phoneNumber);
        NativeDriver.FindElement(PasswordInput).SendKeys(password);
        NativeDriver.FindElement(ConfirmPasswordInput).SendKeys(confirmPassword);
        NativeDriver.FindElement(StreetAddressInput).SendKeys(streetAddress);
        NativeDriver.FindElement(CityInput).SendKeys(city);
        NativeDriver.FindElement(StateInput).SendKeys(state);
        NativeDriver.FindElement(PostalCodeInput).SendKeys(postalCode);
        NativeDriver.FindElement(RoleOption).Click();


        NativeDriver.FindElement(RegisterButton).Click();
    }

    public bool CheckUserExist(string email)
    {
        var status = false;

        Thread.Sleep(2000);
        IList<IWebElement> rows = NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (var row in rows)
        {
            var userEmailCell = row.FindElement(By.XPath("./td[2]"));

            var userEmail = userEmailCell.Text;

            if (userEmail.Equals(email, StringComparison.OrdinalIgnoreCase)) status = true;
        }

        return status;
    }
}