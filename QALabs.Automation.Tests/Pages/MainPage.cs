using OpenQA.Selenium;
using QALabs.Automation.Core;
using QALabs.Automation.Core.Helpers;
using QALabs.Automation.Core.PageObject;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Pages;

public class MainPage : BasePageObject
{
    protected SeleniumWebDriver DriverManager;


    public MainPage(SeleniumWebDriver driverManager)
    {
        DriverManager = driverManager;
    }

    public UIElement LoginButton => UIElementByCss("#login");
    public UIElement RegisterButton => UIElementByCss("#register");
    public UIElement CartButton => UIElementByXPath("//i[@class='bi bi-cart']");
    public UIElement HomeButton => UIElementByXPath("//a[contains(text(),'Home')]");
    public UIElement ManageOrderButton => UIElementByXPath("//a[contains(text(),'Manage Order')]");
    public UIElement ContentManagementDropdown => UIElementByXPath("//a[contains(text(),'Content Management')]");
    public UIElement ContentManagementCategoryButton => UIElementByXPath("//a[contains(text(),'Category')]");
    public UIElement ContentManagementProductButton => UIElementByXPath("//a[contains(text(),'Product')]");
    public UIElement ContentManagementCompanyButton => UIElementByXPath("//a[contains(text(),'Company')]");
    public UIElement ContentManagementCreateUserButton => UIElementByXPath("//a[contains(text(),'Create User')]");
    public UIElement ContentManagementManageUserButton => UIElementByXPath("//a[contains(text(),'Manage User')]");
    public UIElement AddToCartButton => UIElementByXPath("//button[contains(text(), 'Add to Cart')]");
    public UIElement UsernameInsideSystemAfterLogin => UIElementByCss("#manage");
    public UIElement LogOutButton => UIElementByCss("#logout");

    public UIElement DetailsNButton(int n)
    {
        return UIElementByXPath($"(//a[contains(text(),'Details')])[{n}]");
    }

    public UIElement TitleProductInCart(string title)
    {
        return UIElementByXPath($"//h5/strong[contains(text(), '{title}')]");
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
    }

    public void AddToCartButtonClick()
    {
        AddToCartButton.Click();
    }

    public void ScrollDown(int pixels)
    {
        var js = (IJavaScriptExecutor)NativeDriver.Driver;
        js.ExecuteScript($"window.scrollBy(0, {pixels});");
    }

    public bool CheckThatBookExistInsideCart(string title)
    {
        CartButton.Click();

        return NativeDriver.IsElementDisplayed(TitleProductInCart(title).By);
    }
}