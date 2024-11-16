using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CourseWorkWeb.Tests.Pages;

public class CreateUserPage : MainPage
{
    private readonly
        By NewProductButton =
            By.CssSelector(
                "body > div > main > div > div > div.col-6.text-end > a");

    private readonly 
        By EmailInput = 
            By.CssSelector("#Input_Email");

    private readonly
        By NameInput =
            By.CssSelector(
                "#Input_Name");

    private readonly
        By PhoneNumberInput =
            By.CssSelector(
                "#Input_PhoneNumber");

    private readonly 
        By PasswordInput = 
            By.CssSelector("#Input_Password");

    private readonly 
        By ConfirmPasswordInput = 
            By.CssSelector("#Input_ConfirmPassword");
    
    private readonly
        By StreetAddressInput =
            By.CssSelector(
                "#Input_StreetAddress");

    private readonly
        By CityInput =
            By.CssSelector(
                "#Input_City");

    private readonly
        By StateInput =
            By.CssSelector(
                "#Input_State");

    private readonly
        By PostalCodeInput =
            By.CssSelector(
                "#Input_PostalCode");

    private readonly
        By RegisterButton =
            By.CssSelector(
                "#registerSubmit");
    public CreateUserPage(SeleniumWebDriver driver) : base(driver)
    {
    }

    public void NewCompanyButtonClick()
    {
        _waiter.Until(d => d.FindElement(NewProductButton)).Click();
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

                
        _waiter.Until(d => d.FindElement(EmailInput)).SendKeys(email);
        _waiter.Until(d => d.FindElement(NameInput)).SendKeys(name);
        _waiter.Until(d => d.FindElement(PhoneNumberInput)).SendKeys(phoneNumber);
        _waiter.Until(d => d.FindElement(PasswordInput)).SendKeys(password);
        _waiter.Until(d => d.FindElement(ConfirmPasswordInput)).SendKeys(confirmPassword);
        _waiter.Until(d => d.FindElement(StreetAddressInput)).SendKeys(streetAddress);
        _waiter.Until(d => d.FindElement(CityInput)).SendKeys(city);
        _waiter.Until(d => d.FindElement(StateInput)).SendKeys(state);
        _waiter.Until(d => d.FindElement(PostalCodeInput)).SendKeys(postalCode);

        _waiter.Until(d => d.FindElement(RegisterButton)).Click();
        
    }

    public bool CheckUserExist(string email)
    {
        bool status = false;

        Thread.Sleep(2000);
        IList<IWebElement> rows = _driver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (IWebElement row in rows)
        {
            IWebElement userEmailCell = row.FindElement(By.XPath("./td[2]"));

            string userEmail = userEmailCell.Text;

            if (userEmail.Equals(email, StringComparison.OrdinalIgnoreCase))
            {
                status = true;
            }
        }

        return status;
    }
    

    // public bool DeleteCompany(string name)
    // {
    //     bool status = false;
    //
    //     Thread.Sleep(2000);
    //     IList<IWebElement> rows = _driver.FindElements(By.CssSelector(".table tbody tr"));
    //     foreach (IWebElement row in rows)
    //     {
    //         Thread.Sleep(1000);
    //         IWebElement categoryNameCell = row.FindElement(By.XPath("./td[1]"));
    //
    //         string categoryName = categoryNameCell.Text;
    //
    //         if (categoryName.Equals(name, StringComparison.OrdinalIgnoreCase))
    //         {
    //             IWebElement Cell = row.FindElement(By.XPath("./td[6]/div/a[2]"));
    //             Cell.Click();
    //             Thread.Sleep(1000);
    //             _waiter.Until(d => d.FindElement(DeleteButton)).Click();
    //             return true;
    //         }
    //     }
    //
    //     return status;
    // }
    
}