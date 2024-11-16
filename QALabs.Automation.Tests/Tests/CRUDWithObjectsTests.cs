using Core;
using CourseWorkWeb.Tests.Pages;
using CourseWorkWeb.Tests.UtilityLibrary;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CourseWorkWeb.Tests.CourseWorkWebTests
{
    public class CRUDWithObjectsTests
    {
        private IWebDriver driver;
        private MainPage mainPage;
        private LoginPage loginPage;
        private CategoryPage categoryPage;
        private ProductAdminPage productAdminPage;
        private CompanyPage companyPage;
        private CreateUserPage createUserPage;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            mainPage = new MainPage(SeleniumWebDriver.NativeDriver);
            loginPage = new LoginPage(SeleniumWebDriver.NativeDriver);
            categoryPage = new CategoryPage(SeleniumWebDriver.NativeDriver);
            productAdminPage = new ProductAdminPage(SeleniumWebDriver.NativeDriver);
            companyPage = new CompanyPage(SeleniumWebDriver.NativeDriver);
            createUserPage = new CreateUserPage(SeleniumWebDriver.NativeDriver);
        }

        [Test, Order(1)]
        [TestCase("My Category", "8")]
        [TestCase("My Category1", "9")]
        [TestCase("My Category2", "10")]
        public void CreateNewCategoryTestReturnTrue(string name, string displayOrder)
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool CreateStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementCategoryButtonClick();
                categoryPage.NewCategoryButtonClick();
                CreateStatus = categoryPage.NewCategoryCreate(name, displayOrder);
            }

            Assert.That(CreateStatus, Is.EqualTo(true));
        }

        [Test, Order(2)]
        [TestCase("My Category", "Polytech", "25")]
        [TestCase("My Category1", "IIBRT", "50")]
        [TestCase("My Category2", "ICS", "27")]
        public void EditCategoriesTestReturnTrue(string name, string newName, string displayOrder)
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool EditStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementCategoryButtonClick();
                EditStatus = categoryPage.EditCategory(name, newName, displayOrder);
            }

            Assert.That(EditStatus, Is.EqualTo(true));
        }


        [Test, Order(3)]
        [TestCase(
            "Метро 2033",
            "Метро 2033 — культовий роман-антиутопія",
            "978-966-10-6112-4",
            "Ґлуховський Д.",
            "500",
            "450",
            "430",
            "400"
        )]
        public void CreateProductTestReturnTrue(
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
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool CreateStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementProductButtonClick();
                productAdminPage.NewProductButtonClick();
                CreateStatus = productAdminPage
                    .NewProductCreate(title, description, ISBN, author, listPrice, price, price50, price100);
            }

            Assert.That(CreateStatus, Is.EqualTo(true));
        }


        [Test, Order(4)]
        [TestCase("Метро 2033", "Vladyslav Book")]
        public void EditProductTitleTestReturnTrue(string name, string newName)
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool EditStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementProductButtonClick();
                EditStatus = productAdminPage.EditTitleInProduct(name, newName);
            }

            Assert.That(EditStatus, Is.EqualTo(true));
        }


        [Test, Order(5)]
        [TestCase("Polytech")]
        [TestCase("IIBRT")]
        [TestCase("ICS")]
        public void DeleteCategoriesTestReturnTrue(string name)
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool DeleteStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementCategoryButtonClick();
                DeleteStatus = categoryPage.DeleteCategory(name);
            }

            Assert.That(DeleteStatus, Is.EqualTo(true));
        }


        [Test, Order(6)]
        [TestCase("Polytech",
            "0486953248",
            "Shevchenko 1",
            "Odesa",
            "Ukraine",
            "65000"
        )]
        public void CreateCompanyTestReturnTrue(
            string name,
            string phoneNumber,
            string streetAddress,
            string city,
            string state,
            string postalCode
        )
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool CreateStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementCompanyButtonClick();
                companyPage.NewCompanyButtonClick();
                CreateStatus = companyPage
                    .NewCompanyCreate(name, phoneNumber, streetAddress, city, state, postalCode);
            }

            Assert.That(CreateStatus, Is.EqualTo(true));
        }


        [Test, Order(7)]
        [TestCase(
            "Polytech",
            "EPAM",
            "080096513",
            "Main Street 3",
            "Kyiv",
            "Ukraine",
            "63654"
        )]
        public void EditCompanyDataTestReturnTrue(
            string name,
            string newName,
            string phoneNumber,
            string streetAddress,
            string city,
            string state,
            string postalCode)
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool EditStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementCompanyButtonClick();
                EditStatus =
                    companyPage.EditCompanyData(name, newName, phoneNumber, streetAddress, city, state, postalCode);
            }

            Assert.That(EditStatus, Is.EqualTo(true));
        }

        [Test, Order(8)]
        [TestCase("EPAM")]
        public void DeleteCompanyTestReturnTrue(string name)
        {
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool DeleteStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementCompanyButtonClick();
                DeleteStatus = companyPage.DeleteCompany(name);
            }

            Assert.That(DeleteStatus, Is.EqualTo(true));
        }

        [Test]
        [TestCase(
            "NewUser",
            "0956543621",
            "Testpassword_123",
            "Testpassword_123",
            "Loopback Street 5",
            "Rivne",
            "Ukraine",
            "63465"
        )]
        public void RegisterNewUserCustomerFromAdminPanelTestReturnTrue(
            string name,
            string phoneNumber,
            string password,
            string confirmPassword,
            string streetAddress,
            string city,
            string state,
            string postalCode
        )
        {
            Random random = new Random();
            string email = $"newuser{random.Next(100, 1000000)}@gmail.com";
            driver.Url = "https://localhost:7217/";
            mainPage.LoginButtonClick();
            bool LoginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
            bool CreateStatus = false;
            if (LoginStatus)
            {
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementCreateUserButtonClick();
                createUserPage
                    .NewCustomerCreate(
                        email,
                        name,
                        phoneNumber,
                        password,
                        confirmPassword,
                        streetAddress,
                        city,
                        state,
                        postalCode);
                mainPage.ContentManagementDropdownClick();
                mainPage.ContentManagementManageUserButtonClick();
                CreateStatus = createUserPage.CheckUserExist(email);
            }

            Assert.That(CreateStatus, Is.EqualTo(true));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}