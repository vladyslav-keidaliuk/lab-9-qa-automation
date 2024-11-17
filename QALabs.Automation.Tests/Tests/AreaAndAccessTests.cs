using OpenQA.Selenium;
using QALabs.Automation.Core.Configuration;
using QALabs.Automation.Tests.Pages;
using QALabs.Automation.Tests.UtilityLibrary;
using static QALabs.Automation.Core.SeleniumWebDriver;
using static QALabs.Automation.Tests.UtilityLibrary.CredantialsTestData;

namespace QALabs.Automation.Tests.Tests;

[TestFixture]
public class AreaAndAccessTests : BaseTest
{
    private MainPage _mainPage;
    private LoginPage _loginPage;
    private UserListPage _userListPage;

    [SetUp]
    public void Setup()
    {
        _mainPage = new MainPage(NativeDriver);
        _loginPage = new LoginPage(NativeDriver);
        _userListPage = new UserListPage(NativeDriver);
    }

    [Test]
    [TestCaseSource(typeof(CredantialsTestData), nameof(ValidCredentialsTestData))]
    public void LogInWithValidDataReturnTrue(string email, string password)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        _mainPage.LoginButtonClick();

        var status = _loginPage.Login(email, password);

        _mainPage.LogOutButtonClick();

        Assert.That(status, Is.EqualTo(true));
    }

    [Test]
    [TestCaseSource(typeof(CredantialsTestData), nameof(InvalidCredentialsTestData))]
    public void LogInWithInvalidDataReturnFalse(string email, string password)
    {
        SiteNavigation.GoToBookstore(NativeDriver);
        _mainPage.LoginButtonClick();

        var status = _loginPage.Login(email, password);

        Assert.That(status, Is.EqualTo(false));
    }

    [Test]
    public void AccessDeniedForCustomerToAdminAreaReturnFalse()
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        _mainPage.LoginButtonClick();
        var status = _loginPage.Login(PersonalData.CustomerUsername, PersonalData.CustomerPassword);
        var access = true;
        if (status)
        {
            NativeDriver.GoToUrl($"{Config.Model.Url}/Admin/User");
            if (NativeDriver.IsElementDisplayed(By.XPath("//h1[contains(text(),'Access denied')]"))) access = false;
        }

        Assert.That(access, Is.EqualTo(false));
    }

    [Test]
    public void AccessAllowedForAdminToAdminAreaReturnTrue()
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        _mainPage.LoginButtonClick();
        var status = _loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);

        NativeDriver.GoToUrl($"{Config.Model.Url}/Admin/User");

        var isAccessDenied = NativeDriver.IsElementDisplayed(By.XPath("//h1[contains(text(),'Access denied')]"));

        Assert.That(isAccessDenied, Is.EqualTo(false));
    }

    [Test]
    [Order(1)]
    [TestCase("employee@gmail.com")]
    public void LockCustomerByAdminReturnFalse(string email)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        _mainPage.LoginButtonClick();

        var statusAfterBlock = true;
        var status = _loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        if (status)
        {
            _mainPage.ContentManagementDropdownClick();
            _mainPage.ContentManagementManageUserButtonClick();
            _userListPage.LockUnlockUserWithEmail(email);
            _mainPage.LogOutButtonClick();
            _mainPage.LoginButtonClick();
            statusAfterBlock = _loginPage.Login(PersonalData.EmployeeUsername, PersonalData.EmployeePassword);
        }

        Assert.That(statusAfterBlock, Is.EqualTo(false));
    }


    [Test]
    [Order(2)]
    [TestCase("employee@gmail.com")]
    public void UnlockCustomerByAdminReturnTrue(string email)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        _mainPage.LoginButtonClick();
        var statusAfterBlock = false;
        var status = _loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        if (status)
        {
            _mainPage.ContentManagementDropdownClick();
            _mainPage.ContentManagementManageUserButtonClick();
            _userListPage.LockUnlockUserWithEmail(email);
            _mainPage.LogOutButtonClick();
            _mainPage.LoginButtonClick();
            statusAfterBlock = _loginPage.Login(PersonalData.EmployeeUsername, PersonalData.EmployeePassword);
        }

        Assert.That(statusAfterBlock, Is.EqualTo(true));
    }
}