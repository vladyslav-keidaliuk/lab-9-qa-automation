using OpenQA.Selenium;
using QALabs.Automation.Core;
using QALabs.Automation.Core.Helpers;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Pages;

public class CategoryPage : MainPage
{
    public CategoryPage(SeleniumWebDriver driver) : base(driver)
    {
    }

    public UIElement CreateNewCategoryButton => UIElementByXPath("//a[@class='btn btn-primary']");
    public UIElement CategoryNameInput => UIElementByCss("#Name");
    public UIElement DisplayOrderInput => UIElementByCss("#DisplayOrder");
    public UIElement CreateButton => UIElementByXPath("//button[contains(text(), 'Create')]");
    public UIElement DeleteButton => UIElementByXPath("//button[contains(text(), 'Delete')]");
    public UIElement EditCategoryNameInput => UIElementByCss("#Name");
    public UIElement EditDisplayOrderInput => UIElementByCss("#DisplayOrder");
    public UIElement UpdateButton => UIElementByXPath("//button[contains(text(), 'Update')]");

    public UIElement NameProduct(string name)
    {
        return UIElementByXPath($"//tr//td[contains(text(), '{name}')]");
    }

    public void NewCategoryButtonClick()
    {
        NativeDriver.FindElement(CreateNewCategoryButton).Click();
    }

    public bool NewCategoryCreate(string name, string displayOrder)
    {
        NativeDriver.FindElement(CategoryNameInput).SendKeys(name);
        NativeDriver.FindElement(DisplayOrderInput).SendKeys(displayOrder);
        NativeDriver.FindElement(CreateButton).Click();

        var isNameVisible = NativeDriver.FindElement(NameProduct(name)).Displayed;

        return isNameVisible;
    }

    public bool EditCategory(string name, string newName, string displayOrder)
    {
        var status = false;

        IList<IWebElement> rows = NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (var row in rows)
        {
            var categoryNameCell = row.FindElement(By.XPath("./td[1]"));

            var categoryName = categoryNameCell.Text;

            if (categoryName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                var cell = row.FindElement(By.XPath("./td[3]/div/a[1]"));
                cell.Click();
                NativeDriver.FindElement(EditCategoryNameInput).Clear();
                NativeDriver.FindElement(EditCategoryNameInput).SendKeys(newName);
                NativeDriver.FindElement(EditDisplayOrderInput).Clear();
                NativeDriver.FindElement(EditDisplayOrderInput).SendKeys(displayOrder);
                NativeDriver.FindElement(UpdateButton).Click();
                return true;
            }
        }

        return status;
    }

    public bool DeleteCategory(string name)
    {
        var status = false;

        IList<IWebElement> rows = NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (var row in rows)
        {
            var categoryNameCell = row.FindElement(By.XPath("./td[1]"));

            var categoryName = categoryNameCell.Text;

            if (categoryName.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                var Cell = row.FindElement(By.XPath("./td[3]/div/a[2]"));
                Cell.Click();

                NativeDriver.FindElement(DeleteButton).Click();
                return true;
            }
        }

        return status;
    }
}