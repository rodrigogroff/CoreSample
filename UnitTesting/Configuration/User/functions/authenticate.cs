using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class UserUT_authenticate : BaseTest
    {
        [Test]
        public void UT_User_Authenticate_LoginInvalid()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.Exec(null, new LoginInformation { Login = "" }, ref ua))
                Assert.Fail("Authenticate // Login empty accepted");
        }

        [Test]
        public void UT_User_Authenticate_PasswordInvalid()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.Exec(null, new LoginInformation { Login = "test@test.com", Passwd = "" }, ref ua))
                Assert.Fail("Authenticate // Password empty accepted");
        }

        [Test]
        public void UT_User_Authenticate_PasswordInvalid_2()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.Exec(null, new LoginInformation { Login = "test@test.com", Passwd = "123" }, ref ua))
                Assert.Fail("Authenticate // Password invalid passed");
        }
        
        [Test]
        public void UT_User_Authenticate_OK()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (!service.Exec(null, new LoginInformation { Login = "test@test.com", Passwd = "123456" }, ref ua))
                Assert.Fail("Authenticate // Failed!");
        }
    }
}
