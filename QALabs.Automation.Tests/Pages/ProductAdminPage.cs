using OpenQA.Selenium;
using QALabs.Automation.Core;
using QALabs.Automation.Core.Helpers;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Pages;

public class ProductAdminPage : MainPage
{
    public ProductAdminPage(SeleniumWebDriver driver) : base(driver)
    {
    }

    public UIElement CreateNewProductButton => UIElementByXPath("//a[@class='btn btn-primary']");
    public UIElement TitleInput => UIElementByCss("#Product_Title");
    public UIElement DescriptionInput => UIElementByCss("#Product_Description");
    public UIElement ISBNInput => UIElementByCss("#Product_ISBN");
    public UIElement AuthorInput => UIElementByCss("#Product_Author");
    public UIElement ListPriceInput => UIElementByCss("#Product_ListPrice");
    public UIElement PriceInput => UIElementByCss("#Product_Price");
    public UIElement Price50Input => UIElementByCss("#Product_Price50");
    public UIElement Price100Input => UIElementByCss("#Product_Price100");
    public UIElement CategoryIdSelector => UIElementByCss("#Product_CategoryId");
    public UIElement CategoryIdSelectFiction => UIElementByCss("#Product_CategoryId > option:nth-child(7)");
    public UIElement CreateButton => UIElementByXPath("//button[contains(text(), 'Create')]");
    public UIElement DeleteButton => UIElementByXPath("//button[contains(text(), 'Delete')]");
    public UIElement UpdateButton => UIElementByXPath("//button[contains(text(), 'Update')]");

    public void NewProductButtonClick()
    {
        NativeDriver.FindElement(CreateNewProductButton).Click();
    }

    public bool NewProductCreate(
        string title,
        string description,
        string ISBN,
        string author,
        string listPrice,
        string price,
        string price50,
        string price100
    )
    {
        var status = false;

        NativeDriver.FindElement(TitleInput).SendKeys(title);
        NativeDriver.FindElement(DescriptionInput).SendKeys(description);
        NativeDriver.FindElement(ISBNInput).SendKeys(ISBN);
        NativeDriver.FindElement(AuthorInput).SendKeys(author);

        NativeDriver.FindElement(ListPriceInput).Clear();
        NativeDriver.FindElement(ListPriceInput).SendKeys(listPrice);

        NativeDriver.FindElement(PriceInput).Clear();
        NativeDriver.FindElement(PriceInput).SendKeys(price);

        NativeDriver.FindElement(Price50Input).Clear();
        NativeDriver.FindElement(Price50Input).SendKeys(price50);

        NativeDriver.FindElement(Price100Input).Clear();
        NativeDriver.FindElement(Price100Input).SendKeys(price100);

        NativeDriver.FindElement(CategoryIdSelector).Click();
        Thread.Sleep(500);
        NativeDriver.FindElement(CategoryIdSelectFiction).Click();
        Thread.Sleep(500);
        NativeDriver.FindElement(CreateButton).Click();
        Thread.Sleep(500);

        IList<IWebElement> rows = NativeDriver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (var row in rows)
        {
            var categoryNameCell = row.FindElement(By.XPath("./td[1]"));

            var categoryName = categoryNameCell.Text;

            if (categoryName.Equals(title, StringComparison.OrdinalIgnoreCase)) status = true;
        }

        return status;
    }

    public bool EditTitleInProduct(string name, string newName)
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

                NativeDriver.FindElement(TitleInput).Clear();
                NativeDriver.FindElement(TitleInput).SendKeys(newName);

                NativeDriver.FindElement(UpdateButton).Click();
                return true;
            }
        }

        return status;
    }
}