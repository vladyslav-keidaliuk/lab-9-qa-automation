using OpenQA.Selenium;
using QALabs.Automation.Tests.Pages;
using QALabs.Automation.Tests.UtilityLibrary;
using static QALabs.Automation.Core.SeleniumWebDriver;

namespace QALabs.Automation.Tests.Tests;

[TestFixture]
public class CRUDWithObjectsTests : BaseTest
{
    private CategoryPage categoryPage;
    private CompanyPage companyPage;
    private CreateUserPage createUserPage;
    private IWebDriver driver;
    private LoginPage loginPage;
    private MainPage mainPage;
    private ProductAdminPage productAdminPage;


    [SetUp]
    public void Setup()
    {
        NativeDriver.Driver.Manage().Window.Maximize();
        mainPage = new MainPage(NativeDriver);
        loginPage = new LoginPage(NativeDriver);
        categoryPage = new CategoryPage(NativeDriver);
        productAdminPage = new ProductAdminPage(NativeDriver);
        companyPage = new CompanyPage(NativeDriver);
        createUserPage = new CreateUserPage(NativeDriver);
    }

    [Test]
    [Order(1)]
    [TestCase("My Category", "8")]
    [TestCase("My Category1", "9")]
    [TestCase("My Category2", "10")]
    public void CreateNewCategoryTestReturnTrue(string name, string displayOrder)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var createStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementCategoryButtonClick();
            categoryPage.NewCategoryButtonClick();
            createStatus = categoryPage.NewCategoryCreate(name, displayOrder);
        }

        Assert.That(createStatus, Is.EqualTo(true));
    }

    [Test]
    [Order(2)]
    [TestCase("My Category", "Polytech", "25")]
    [TestCase("My Category1", "IIBRT", "50")]
    [TestCase("My Category2", "ICS", "27")]
    public void EditCategoriesTestReturnTrue(string name, string newName, string displayOrder)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();

        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var editStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementCategoryButtonClick();
            editStatus = categoryPage.EditCategory(name, newName, displayOrder);
        }

        Assert.That(editStatus, Is.EqualTo(true));
    }


    [Test]
    [Order(3)]
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
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var createStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementProductButtonClick();
            productAdminPage.NewProductButtonClick();
            createStatus = productAdminPage
                .NewProductCreate(title, description, ISBN, author, listPrice, price, price50, price100);
        }

        Assert.That(createStatus, Is.EqualTo(true));
    }


    [Test]
    [Order(4)]
    [TestCase("Метро 2033", "Vladyslav Book")]
    public void EditProductTitleTestReturnTrue(string name, string newName)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var editStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementProductButtonClick();
            editStatus = productAdminPage.EditTitleInProduct(name, newName);
        }

        Assert.That(editStatus, Is.EqualTo(true));
    }


    [Test]
    [Order(5)]
    [TestCase("Polytech")]
    [TestCase("IIBRT")]
    [TestCase("ICS")]
    public void DeleteCategoriesTestReturnTrue(string name)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var deleteStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementCategoryButtonClick();
            deleteStatus = categoryPage.DeleteCategory(name);
        }

        Assert.That(deleteStatus, Is.EqualTo(true));
    }


    [Test]
    [Order(6)]
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
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var createStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementCompanyButtonClick();
            companyPage.NewCompanyButtonClick();
            createStatus = companyPage
                .NewCompanyCreate(name, phoneNumber, streetAddress, city, state, postalCode);
        }

        Assert.That(createStatus, Is.EqualTo(true));
    }


    [Test]
    [Order(7)]
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
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var editStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementCompanyButtonClick();
            editStatus =
                companyPage.EditCompanyData(name, newName, phoneNumber, streetAddress, city, state, postalCode);
        }

        Assert.That(editStatus, Is.EqualTo(true));
    }

    [Test]
    [Order(8)]
    [TestCase("EPAM")]
    public void DeleteCompanyTestReturnTrue(string name)
    {
        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var deleteStatus = false;
        if (loginStatus)
        {
            mainPage.ContentManagementDropdownClick();
            mainPage.ContentManagementCompanyButtonClick();
            deleteStatus = companyPage.DeleteCompany(name);
        }

        Assert.That(deleteStatus, Is.EqualTo(true));
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
        var random = new Random();
        var email = $"newuser{random.Next(100, 1000000)}@gmail.com";

        SiteNavigation.GoToBookstore(NativeDriver);

        mainPage.LoginButtonClick();
        var loginStatus = loginPage.Login(PersonalData.AdminUsername, PersonalData.AdminPassword);
        var createStatus = false;
        if (loginStatus)
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
            createStatus = createUserPage.CheckUserExist(email);
        }

        Assert.That(createStatus, Is.EqualTo(true));
    }
}