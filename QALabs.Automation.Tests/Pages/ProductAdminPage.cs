using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CourseWorkWeb.Tests.Pages;

public class ProductAdminPage : MainPage
{
    private readonly
        By NewProductButton =
            By.CssSelector(
                "body > div > main > div > div > div.col-6.text-end > a");

    private readonly
        By TitleInput =
            By.CssSelector(
                "#Product_Title");

    private readonly
        By DescriptionInput =
            By.CssSelector(
                "#Product_Description");

    private readonly
        By ISBNInput =
            By.CssSelector(
                "#Product_ISBN");

    private readonly
        By AuthorInput =
            By.CssSelector(
                "#Product_Author");

    private readonly
        By ListPriceInput =
            By.CssSelector(
                "#Product_ListPrice");

    private readonly
        By PriceInput =
            By.CssSelector(
                "#Product_Price");

    private readonly
        By Price50Input =
            By.CssSelector(
                "#Product_Price50");

    private readonly
        By Price100Input =
            By.CssSelector(
                "#Product_Price100");

    private readonly
        By CategoryIdSelector =
            By.CssSelector(
                "#Product_CategoryId");

    private readonly
        By CategoryIdSelectFiction =
            By.CssSelector(
                "#Product_CategoryId > option:nth-child(8)");

    private readonly
        By CreateButton =
            By.CssSelector(
                "body > div > main > div > div.card-body.p-4 > form > div > div.col-10 > div > div.row > div:nth-child(1) > button");

    private readonly
        By DeleteButton =
            By.CssSelector("body > div > main > form > div > div:nth-child(10) > div:nth-child(1) > button");

    private readonly
        By UpdateButton =
            By.CssSelector(
                "body > div > main > div > div.card-body.p-4 > form > div > div.col-10 > div > div.row > div:nth-child(1) > button");


    public ProductAdminPage(SeleniumWebDriver driver) : base(driver)
    {
    }

    public void NewProductButtonClick()
    {
        _waiter.Until(d => d.FindElement(NewProductButton)).Click();
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
        bool status = false;

        _waiter.Until(d => d.FindElement(TitleInput)).SendKeys(title);
        _waiter.Until(d => d.FindElement(DescriptionInput)).SendKeys(description);
        _waiter.Until(d => d.FindElement(ISBNInput)).SendKeys(ISBN);
        _waiter.Until(d => d.FindElement(AuthorInput)).SendKeys(author);

        _waiter.Until(d => d.FindElement(ListPriceInput)).Clear();
        _waiter.Until(d => d.FindElement(ListPriceInput)).SendKeys(listPrice);

        _waiter.Until(d => d.FindElement(PriceInput)).Clear();
        _waiter.Until(d => d.FindElement(PriceInput)).SendKeys(price);

        _waiter.Until(d => d.FindElement(Price50Input)).Clear();
        _waiter.Until(d => d.FindElement(Price50Input)).SendKeys(price50);

        _waiter.Until(d => d.FindElement(Price100Input)).Clear();
        _waiter.Until(d => d.FindElement(Price100Input)).SendKeys(price100);

        _waiter.Until(d => d.FindElement(CategoryIdSelector)).Click();
        Thread.Sleep(500);
        _waiter.Until(d => d.FindElement(CategoryIdSelectFiction)).Click();
        Thread.Sleep(500);

        _waiter.Until(d => d.FindElement(CreateButton)).Click();

        Thread.Sleep(2000);
        IList<IWebElement> rows = _driver.FindElements(By.CssSelector(".table tbody tr"));
        foreach (IWebElement row in rows)
        {
            IWebElement categoryNameCell = row.FindElement(By.XPath("./td[1]"));

            string categoryName = categoryNameCell.Text;

            if (categoryName.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                status = true;
            }
        }

        return status;
    }

    public bool EditTitleInProduct(string name, string newName)
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
                _waiter.Until(d => d.FindElement(TitleInput)).Clear();
                _waiter.Until(d => d.FindElement(TitleInput)).SendKeys(newName);

                _waiter.Until(d => d.FindElement(UpdateButton)).Click();
                return true;
            }
        }

        return status;
    }
}