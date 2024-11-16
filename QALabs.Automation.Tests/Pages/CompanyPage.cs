using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CourseWorkWeb.Tests.Pages;

public class CompanyPage : MainPage
{

    private readonly
        By NewProductButton =
            By.CssSelector(
                "body > div > main > div > div > div.col-6.text-end > a");
    
    private readonly
        By NameInput =
            By.CssSelector(
                "#Name");
    private readonly
        By PhoneNumberInput =
            By.CssSelector(
                "#PhoneNumber");
    private readonly
        By StreetAddressInput =
            By.CssSelector(
                "#StreetAddress");
    private readonly
        By CityInput =
            By.CssSelector(
                "#City");
    private readonly
        By StateInput =
            By.CssSelector(
                "#State");
    private readonly
        By PostalCodeInput =
            By.CssSelector(
                "#PostalCode");
    
    private readonly 
        By CreateButton =
            By.CssSelector("body > div > main > div > div.card-body.p-4 > form > div > div > div > div.row > div:nth-child(1) > button");

    private readonly
        By DeleteButton =
            By.CssSelector("body > div > main > form > div > div:nth-child(8) > div:nth-child(1) > button");
    
    private readonly
        By UpdateButton =
            By.CssSelector("body > div > main > div > div.card-body.p-4 > form > div > div > div > div.row > div:nth-child(1) > button");
    
    
    public CompanyPage(SeleniumWebDriver driver) : base(driver)
    {
        
    }
    
    public void NewCompanyButtonClick()
    {
        _waiter.Until(d => d.FindElement(NewProductButton)).Click();
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
        bool status = false;
        
        _waiter.Until(d => d.FindElement(NameInput)).SendKeys(name);
        _waiter.Until(d => d.FindElement(PhoneNumberInput)).SendKeys(phoneNumber);
        _waiter.Until(d => d.FindElement(StreetAddressInput)).SendKeys(streetAddress);
        _waiter.Until(d => d.FindElement(CityInput)).SendKeys(city);
        _waiter.Until(d => d.FindElement(StateInput)).SendKeys(state);
        _waiter.Until(d => d.FindElement(PostalCodeInput)).SendKeys(postalCode);
        
        _waiter.Until(d => d.FindElement(CreateButton)).Click();
        
        Thread.Sleep(2000);
        IList<IWebElement> rows = _driver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (IWebElement row in rows)
        {
            IWebElement companyNameCell = row.FindElement(By.XPath("./td[1]"));
            
            string companyName = companyNameCell.Text;
            
            if (companyName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                status = true;
            }
        }
        return status;
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
        bool status = false;
        
        Thread.Sleep(2000);
        IList<IWebElement> rows = _driver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (IWebElement row in rows)
        {
            Thread.Sleep(1000);
            IWebElement productNameCell = row.FindElement(By.XPath("./td[1]"));
            string productName = productNameCell.Text;
            if (productName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                
                IWebElement Cell = row.FindElement(By.XPath("./td[6]/div/a[1]"));
                Cell.Click();
                Thread.Sleep(1000);
                _waiter.Until(d => d.FindElement(NameInput)).Clear();
                _waiter.Until(d => d.FindElement(NameInput)).SendKeys(newName);
                
                _waiter.Until(d => d.FindElement(PhoneNumberInput)).Clear();
                _waiter.Until(d => d.FindElement(PhoneNumberInput)).SendKeys(phoneNumber);
                
                _waiter.Until(d => d.FindElement(StreetAddressInput)).Clear();
                _waiter.Until(d => d.FindElement(StreetAddressInput)).SendKeys(streetAddress);
                
                _waiter.Until(d => d.FindElement(CityInput)).Clear();
                _waiter.Until(d => d.FindElement(CityInput)).SendKeys(city);
                
                _waiter.Until(d => d.FindElement(StateInput)).Clear();
                _waiter.Until(d => d.FindElement(StateInput)).SendKeys(state);
                
                _waiter.Until(d => d.FindElement(UpdateButton)).Click();
                return true;
            } 
        }
        return status;
    }
    
    public bool DeleteCompany(string name)
    {
        bool status = false;
        
        Thread.Sleep(2000);
        IList<IWebElement> rows = _driver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (IWebElement row in rows)
        {
            Thread.Sleep(1000);
            IWebElement categoryNameCell = row.FindElement(By.XPath("./td[1]"));
            
            string categoryName = categoryNameCell.Text;
            
            if (categoryName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                
                IWebElement Cell = row.FindElement(By.XPath("./td[6]/div/a[2]"));
                Cell.Click();
                Thread.Sleep(1000);
                _waiter.Until(d => d.FindElement(DeleteButton)).Click();
                return true;
            }
        }
        return status;
    }
    
}