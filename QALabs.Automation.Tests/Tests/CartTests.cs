using Core;
using CourseWorkWeb.Tests.Pages;
using CourseWorkWeb.Tests.UtilityLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CourseWorkWeb.Tests.CourseWorkWebTests
{
    public class CartTests
    {
        private IWebDriver driver;
        private MainPage mainPage;
        private LoginPage loginPage;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            mainPage = new MainPage(SeleniumWebDriver.NativeDriver);
            loginPage = new LoginPage(SeleniumWebDriver.NativeDriver);
        }

        [Test, Order(1)]
        public void Add5BookToCart()
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool status = loginPage.Login(PersonalData.CustomerUsername, PersonalData.CustomerPassword);
            if (status)
            {
                for (int i = 1; i < 6; i++)
                {
                    mainPage.ScrollDown(500);
                    mainPage.DetailsButtonNClick(i);
                    mainPage.AddToCartButtonClick();
                }

                status = true;
            }

            mainPage.LogOutButtonClick();
            Assert.That(status, Is.EqualTo(true));
        }

        [Test, Order(2)]
        [TestCase("1984")]
        [TestCase("ЛЮДИНА В ПОШУКАХ СПРАВЖНЬОГО СЕНСУ. ПСИХОЛОГ У КОНЦТАБОРІ")]
        [TestCase("ENGLISH GRAMMAR IN USE 5TH EDITION WITH ANSWERS")]
        [TestCase("КАФЕ НА КРАЮ СВІТУ - СТРЕЛЕКІ ДЖ. П.")]
        [TestCase("ОДНА З ДІВЧАТ")]
        public void CheckThat5BookToCartAddedReturnTrue(string title)
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool bookExist = false;
            bool status = loginPage.Login(PersonalData.CustomerUsername, PersonalData.CustomerPassword);
            if (status)
            {
                mainPage.CartButtonClick();
                bookExist = mainPage.CheckThatBookExistInsideCart(title);
            }

            mainPage.LogOutButtonClick();
            Assert.That(bookExist, Is.EqualTo(true));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}