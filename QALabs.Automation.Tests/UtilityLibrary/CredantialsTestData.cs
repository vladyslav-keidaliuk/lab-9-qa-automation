namespace QALabs.Automation.Tests.UtilityLibrary;

public static class CredantialsTestData
{
    public static IEnumerable<object[]> ValidCredentialsTestData()
    {
        yield return new object[] { PersonalData.AdminUsername, PersonalData.AdminPassword };
        yield return new object[] { PersonalData.CompanyUsername, PersonalData.CompanyPassword };
        yield return new object[] { PersonalData.CustomerUsername, PersonalData.CustomerPassword };
        yield return new object[] { PersonalData.EmployeeUsername, PersonalData.EmployeePassword };
    }

    public static IEnumerable<object[]> InvalidCredentialsTestData()
    {
        yield return new object[] { PersonalData.AdminUsername, "testpass1" };
        yield return new object[] { "Userrandom@gmail.com", PersonalData.CompanyPassword };
        yield return new object[] { "wronguser", PersonalData.CustomerPassword };
    }
}