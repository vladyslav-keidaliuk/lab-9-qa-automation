using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CourseWorkWeb.Tests.Pages;

public class CategoryPage : MainPage
{

    private readonly
        By NewCategoryButton =
            By.CssSelector(
                "body > div > main > div > div > div.col-6.text-end > a");
    
    private readonly
        By CategoryNameInput =
            By.CssSelector(
                "#Name");

    private readonly
        By DisplayOrderInput =
            By.CssSelector(
                "#DisplayOrder");
    
    private readonly 
        By CreateButton =
            By.CssSelector("body > div > main > div > div.card-body.p-4 > form > div > div.row > div:nth-child(1) > button");

    private readonly
        By DeleteButton =
            By.CssSelector("body > div > main > form > div > div:nth-child(4) > div:nth-child(1) > button");

    private readonly
        By EditCategoryNameInput =
            By.CssSelector("#Name");
    
    private readonly
        By EditDisplayOrderInput =
            By.CssSelector("#DisplayOrder");

    private readonly
        By UpdateButton =
            By.CssSelector("body > div > main > form > div > div:nth-child(4) > div:nth-child(1) > button");
    
    
    public CategoryPage(SeleniumWebDriver driver) : base(driver)
    {
        
    }
    
    public void NewCategoryButtonClick()
    {
        _waiter.Until(d => d.FindElement(NewCategoryButton)).Click();
    }
    
    public bool NewCategoryCreate(string name, string displayOrder)
    {
        bool status = false;
        _waiter.Until(d => d.FindElement(CategoryNameInput)).SendKeys(name);
        _waiter.Until(d => d.FindElement(DisplayOrderInput)).SendKeys(displayOrder);
        _waiter.Until(d => d.FindElement(CreateButton)).Click();
        
        Thread.Sleep(2000);
        IList<IWebElement> rows = _driver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (IWebElement row in rows)
        {
            IWebElement categoryNameCell = row.FindElement(By.XPath("./td[1]"));
            
            string categoryName = categoryNameCell.Text;
            
            if (categoryName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                status = true;
            }
        }
        return status;
    }
    
    public bool EditCategory(string name, string newName, string displayOrder)
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
                
                IWebElement Cell = row.FindElement(By.XPath("./td[3]/div/a[1]"));
                Cell.Click();
                Thread.Sleep(1000);
                _waiter.Until(d => d.FindElement(EditCategoryNameInput)).Clear();
                _waiter.Until(d => d.FindElement(EditCategoryNameInput)).SendKeys(newName);
                _waiter.Until(d => d.FindElement(EditDisplayOrderInput)).Clear();
                _waiter.Until(d => d.FindElement(EditDisplayOrderInput)).SendKeys(displayOrder);
                
                _waiter.Until(d => d.FindElement(UpdateButton)).Click();
                return true;
            }
        }
        return status;
    }
    
    public bool DeleteCategory(string name)
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
                
                IWebElement Cell = row.FindElement(By.XPath("./td[3]/div/a[2]"));
                Cell.Click();
                Thread.Sleep(1000);
                _waiter.Until(d => d.FindElement(DeleteButton)).Click();
                return true;
            }
        }
        return status;
    }
    
}