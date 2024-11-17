using OpenQA.Selenium;
using QALabs.Automation.Core;
using QALabs.Automation.Core.Helpers;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Pages;

public class CompanyPage : MainPage
{
    public CompanyPage(SeleniumWebDriver driver) : base(driver)
    {
    }

    public UIElement NewProductButton => UIElementByXPath("//a[@class='btn btn-primary']");
    public UIElement NameInput => UIElementByCss("#Name");


    public UIElement PhoneNumberInput => UIElementByCss("#PhoneNumber");
    public UIElement StreetAddressInput => UIElementByCss("#StreetAddress");
    public UIElement CityInput => UIElementByCss("#City");
    public UIElement StateInput => UIElementByCss("#State");
    public UIElement PostalCodeInput => UIElementByCss("#PostalCode");
    public UIElement CreateButton => UIElementByXPath("//button[contains(text(), 'Create')]");
    public UIElement DeleteButton => UIElementByXPath("//button[contains(text(), 'Delete')]");
    public UIElement UpdateButton => UIElementByXPath("//button[contains(text(), 'Update')]");

    public UIElement NameCompany(string name)
    {
        return UIElementByXPath($"//tr//td[contains(text(), '{name}')]");
    }

    public void NewCompanyButtonClick()
    {
        NativeDriver.FindElement(NewProductButton).Click();
    }

    public bool NewCompanyCreate(
        string name,
        string phoneNumber,
        string streetAddress,
        string city,
        string state,
        string postalCode
    )
    {
        var status = false;

        NativeDriver.FindElement(NameInput).SendKeys(name);
        NativeDriver.FindElement(PhoneNumberInput).SendKeys(phoneNumber);
        NativeDriver.FindElement(StreetAddressInput).SendKeys(streetAddress);
        NativeDriver.FindElement(CityInput).SendKeys(city);
        NativeDriver.FindElement(StateInput).SendKeys(state);
        NativeDriver.FindElement(PostalCodeInput).SendKeys(postalCode);

        NativeDriver.FindElement(CreateButton).Click();

        var isNameVisible = NativeDriver.FindElement(NameCompany(name)).Displayed;

        return isNameVisible;
    }

    public bool EditCompanyData(
        string name,
        string newName,
        string phoneNumber,
        string streetAddress,
        string city,
        string state,
        string postalCode)
    {
        var status = false;

        IList<IWebElement> rows = NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (var row in rows)
        {
            var productNameCell = row.FindElement(By.XPath("./td[1]"));
            var productName = productNameCell.Text;
            if (productName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                row.FindElement(By.XPath("./td[6]/div/a[1]")).Click();

                Thread.Sleep(1000);
                NativeDriver.FindElement(NameInput).Clear();
                NativeDriver.FindElement(NameInput).SendKeys(newName);

                NativeDriver.FindElement(PhoneNumberInput).Clear();
                NativeDriver.FindElement(PhoneNumberInput).SendKeys(phoneNumber);

                NativeDriver.FindElement(StreetAddressInput).Clear();
                NativeDriver.FindElement(StreetAddressInput).SendKeys(streetAddress);

                NativeDriver.FindElement(CityInput).Clear();
                NativeDriver.FindElement(CityInput).SendKeys(city);

                NativeDriver.FindElement(StateInput).Clear();
                NativeDriver.FindElement(StateInput).SendKeys(state);

                NativeDriver.FindElement(UpdateButton).Click();
                return true;
            }
        }

        return status;
    }

    public bool DeleteCompany(string name)
    {
        var status = false;

        IList<IWebElement> rows = NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (var row in rows)
        {
            var categoryNameCell = row.FindElement(By.XPath("./td[1]"));

            var categoryName = categoryNameCell.Text;

            if (categoryName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                var cell = row.FindElement(By.XPath("./td[6]/div/a[2]"));
                cell.Click();

                NativeDriver.FindElement(DeleteButton).Click();
                return true;
            }
        }

        return status;
    }
}