using Core;
using Internal.BoardGames.Core.Web.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QALabs.Automation.Core.Interaction;

namespace CourseWorkWeb.Tests.Pages;

public class MainPage : BasePageObject
{
    protected readonly IWebDriver _driver;
    protected readonly WebDriverWait _waiter;
    protected SeleniumWebDriver DriverManager;

    public UIElement LoginButton => UIElementByCss("#login");
    public UIElement RegisterButton => UIElementByCss("#register");
    public UIElement CartButton => UIElementByXPath("//a[contains(text(),'Content Management')]");
    public UIElement HomeButton => UIElementByXPath("//a[contains(text(),'Home')]");
    public UIElement ManageOrderButton => UIElementByXPath("//a[contains(text(),'Manage Order')]");
    public UIElement ContentManagementDropdown => UIElementByXPath("//a[contains(text(),'Content Management')]");
    public UIElement ContentManagementCategoryButton => UIElementByXPath("//a[contains(text(),'Category')]");
    public UIElement ContentManagementProductButton => UIElementByXPath("//a[contains(text(),'Product')]");
    public UIElement ContentManagementCompanyButton => UIElementByXPath("//a[contains(text(),'Company')]");
    public UIElement ContentManagementCreateUserButton => UIElementByXPath("//a[contains(text(),'Create User')]");
    public UIElement ContentManagementManageUserButton => UIElementByXPath("//a[contains(text(),'Manage User')]");
    public UIElement AddToCartButton => UIElementByXPath("//button[contains(text(), 'Add to Cart')]");
    public UIElement DetailsNButton(int n) => UIElementByXPath($"(//a[contains(text(),'Details')])[{n}]");


    // public TextElement PasswordTextBox => TextElementByCss("#Input_Password");

    public UIElement UsernameInsideSystemAfterLogin => UIElementByCss("#manage");
    public UIElement LogOutButton => UIElementByCss("#logout");

    // public MainPage(IWebDriver webDriver)
    // {
    //     _driver = webDriver;
    //     _waiter = new WebDriverWait(_driver, TimeSpan.FromSeconds(8));
    // }

    public MainPage(SeleniumWebDriver driverManager)
    {
        this.DriverManager = driverManager;
    }

    public void LoginButtonClick()
    {
        LoginButton.Click();
    }

    public void RegisterButtonClick()
    {
        RegisterButton.Click();
    }

    public void CartButtonClick()
    {
        CartButton.Click();
    }

    public void HomeButtonClick()
    {
        HomeButton.Click();
    }

    public void ManageOrderClick()
    {
       ManageOrderButton.Click();
    }

    public void ContentManagementDropdownClick()
    {
        ContentManagementDropdown.Click();
    }

    public void ContentManagementCategoryButtonClick()
    {
        ContentManagementCategoryButton.Click();
    }

    public void ContentManagementProductButtonClick()
    {
        ContentManagementProductButton.Click();
    }

    public void ContentManagementCompanyButtonClick()
    {
        ContentManagementCompanyButton.Click();
    }

    public void ContentManagementCreateUserButtonClick()
    {
        ContentManagementCreateUserButton.Click();
    }

    public void ContentManagementManageUserButtonClick()
    {
        ContentManagementManageUserButton.Click();
    }
    
    public void LogOutButtonClick()
    {
        LogOutButton.Click();
    }
    
    public void DetailsButtonNClick(int n)
    {
        DetailsNButton(n).Click();
        Thread.Sleep(500);
    }

    public void AddToCartButtonClick()
    {
        AddToCartButton.Click();
        Thread.Sleep(500);
    }

    public void ScrollDown(int pixels)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript($"window.scrollBy(0, {pixels});");
        Thread.Sleep(500);
    }

    public bool CheckThatBookExistInsideCart(string title)
    {
        CartButton.Click();
        for (int i = 1; i < 6; i++)
        {
            string titleatCart = _waiter.Until(d => d.FindElement(By.
                    CssSelector($"body > div > main > form > div.card-body.my-4 > div.row.mb-3.pb-3 > div.col-md-10.offset-md-1 > div:nth-child({i}) > div.col-12.col-lg-6.pt-md-3 > h5 > strong"))
                .Text);
            if (title.Equals(titleatCart))
            {
                return true;
            }
        }
        
        return false;
    }
    
}