using Core;
using Core.Configuration;
using CourseWorkWeb.Tests.Pages;
using CourseWorkWeb.Tests.UtilityLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QALabs.Automation.Tests.Pages;
using static Core.SeleniumWebDriver;

namespace CourseWorkWeb.Tests.CourseWorkWebTests
{
    [TestFixture]
    // [Parallelizable(ParallelScope.Children)]
    public class AreaAndAccessTests : BaseTest
    {
        private IWebDriver driver;
        private MainPage mainPage;
        private LoginPage loginPage;
        private UserListPage userListPage;


        private static IEnumerable<object[]> ValidCredentialsTestData()
        {
            yield return new object[] { PersonalData.AdminUsername, PersonalData.AdminPassword };
            yield return new object[] { PersonalData.CompanyUsername, PersonalData.CompanyPassword };
            yield return new object[] { PersonalData.CustomerUsername, PersonalData.CustomerPassword };
            yield return new object[] { PersonalData.EmployeeUsername, PersonalData.EmployeePassword };
        }

        private static IEnumerable<object[]> InvalidCredentialsTestData()
        {
            yield return new object[] { PersonalData.AdminUsername, "testpass1" };
            yield return new object[] { "Userrandom@gmail.com", PersonalData.CompanyPassword };
            yield return new object[] { "wronguser", PersonalData.CustomerPassword };
        }

        [SetUp]
        public void Setup()
        {

            var options = new ChromeOptions();
            options.AddArgument("disable-infobars");
            options.AddArgument("--no-sandbox");

            // driver = new ChromeDriver(options);
            // driver.Manage().Window.Maximize();
            mainPage = new MainPage(NativeDriver);
            loginPage = new LoginPage(NativeDriver);
            userListPage = new UserListPage(NativeDriver);
        }

        [Test]
        [TestCaseSource(nameof(ValidCredentialsTestData))]
        public void LogInWithValidDataReturnTrue(string email, string password)
        {
            SiteNavigation.GoToBookstore(NativeDriver);

            mainPage.LoginButtonClick();

            bool status = loginPage.Login(email, password);

            mainPage.LogOutButtonClick();

            Assert.That(status, Is.EqualTo(true));
        }

        [Test]
        [TestCaseSource(nameof(InvalidCredentialsTestData))]
        public void LogInWithInvalidDataReturnFalse(string email, string password)
        {
            SiteNavigation.GoToBookstore(NativeDriver);
            mainPage.LoginButtonClick();

            bool status = loginPage.Login(email, password);

            Assert.That(status, Is.EqualTo(false));
        }

        [Test]
        public void AccessDeniedForCustomerToAdminAreaReturnFalse()
        {
            SiteNavigation.GoToBookstore(NativeDriver);

            mainPage.LoginButtonClick();
            bool status = loginPage.Login(PersonalData.CustomerUsername, PersonalData.CustomerPassword);
            bool access = true;
            if (status)
            {
                NativeDriver.GoToUrl($"{Config.Model.Url}/Admin/User");
                if (NativeDriver.IsElementDisplayed(By.XPath("//h1[contains(text(),'Access denied')]")))
                {
                    access = false;
                }
            }

            Assert.That(access, Is.EqualTo(false));
        }

        [Test]
        public void AccessAllowedForAdminToAdminAreaReturnTrue()
        {
            SiteNavigation.GoToBookstore(NativeDriver);

            mainPage.LoginButtonClick();
            bool status = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            
            NativeDriver.GoToUrl($"{Config.Model.Url}/Admin/User");
               
            var isAccessDenied = NativeDriver.IsElementDisplayed(By.XPath("//h1[contains(text(),'Access denied')]"));

            Assert.That(isAccessDenied, Is.EqualTo(false));
        }

        [Test, Order(1)]
        [TestCase("employee@gmail.com")]
        public void LockCustomerByAdminReturnFalse(string email)
        {
            SiteNavigation.GoToBookstore(NativeDriver);

            mainPage.LoginButtonClick();

            bool statusAfterBlock = true;
            bool status = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool access = false;
            if (status)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementManageUserButtonClick();
                userListPage.LockUnlockUserWithEmail(email);
                mainPage.LogOutButtonClick();
                mainPage.LoginButtonClick();
                statusAfterBlock = loginPage.Login(PersonalData.EmployeeUsername, PersonalData.EmployeePassword);
            }
            Assert.That(statusAfterBlock, Is.EqualTo(false));
        }
        
        
        [Test, Order(2)]
        [TestCase("employee@gmail.com")]
        public void UnlockCustomerByAdminReturnTrue(string email)
        {
            SiteNavigation.GoToBookstore(NativeDriver);

            mainPage.LoginButtonClick();
            bool statusAfterBlock = false;
            bool status = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool access = false;
            if (status)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementManageUserButtonClick();
                userListPage.LockUnlockUserWithEmail(email);
                mainPage.LogOutButtonClick();
                mainPage.LoginButtonClick();
                statusAfterBlock = loginPage.Login(PersonalData.EmployeeUsername, PersonalData.EmployeePassword);
            }
            Assert.That(statusAfterBlock, Is.EqualTo(true));
        }
    }
}