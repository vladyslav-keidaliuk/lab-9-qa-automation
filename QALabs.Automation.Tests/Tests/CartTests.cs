using OpenQA.Selenium;
using QALabs.Automation.Tests.Pages;
using QALabs.Automation.Tests.UtilityLibrary;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Tests;

[TestFixture]
public class CartTests : BaseTest
{
    private IWebDriver _driver;
    private LoginPage _loginPage;
    private MainPage _mainPage;


    [SetUp]
    public void Setup()
    {
        NativeDriver.Driver.Manage().Window.Maximize();
        _mainPage = new MainPage(NativeDriver);
        _loginPage = new LoginPage(NativeDriver);
    }

    [Test]
    [Order(1)]
    public void Add5BookToCart()
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        _mainPage.LoginButtonClick();
        var loginStatus = _loginPage.Login(PersonalData.CustomerUsername, PersonalData.CustomerPassword);
        if (loginStatus)
        {
            for (var i = 1; i < 6; i++)
            {
                _mainPage.ScrollDown(500);
                _mainPage.DetailsButtonNClick(i);
                _mainPage.AddToCartButtonClick();
            }

            loginStatus = true;
        }

        _mainPage.LogOutButtonClick();
        Assert.That(loginStatus, Is.EqualTo(true));
    }

    [Test]
    [Order(2)]
    [TestCase("Людина в пошуках справжнього сенсу. Психолог у концтаборі")]
    [TestCase("English Grammar in Use 5th Edition with Answers")]
    [TestCase("Кафе на краю світу - Стрелекі Дж. П.")]
    [TestCase("Одна з дівчат")]
    [TestCase("Третій візит до кафе на краю світу")]
    public void CheckThat5BookToCartAddedReturnTrue(string title)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        _mainPage.LoginButtonClick();

        var bookExist = false;
        var status = _loginPage.Login(PersonalData.CustomerUsername, PersonalData.CustomerPassword);

        if (status)
        {
            _mainPage.CartButtonClick();
            bookExist = _mainPage.CheckThatBookExistInsideCart(title);
        }

        _mainPage.LogOutButtonClick();

        Assert.That(bookExist, Is.EqualTo(true));
    }
}