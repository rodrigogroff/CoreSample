using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_authenticate : BaseTest
    {
        [Test]
        public void UT_Admin_Authenticate_LoginInvalid()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.Authenticate(null, new LoginInformation { Login = "" }, ref ua))
                Assert.Fail("Authenticate // Login empty accepted");
        }

        [Test]
        public void UT_Admin_Authenticate_PasswordInvalid()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.Authenticate(null, new LoginInformation { Login = "test@test.com", Passwd = "" }, ref ua))
                Assert.Fail("Authenticate // Password empty accepted");
        }

        [Test]
        public void UT_Admin_Authenticate_PasswordInvalid_2()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.Authenticate(null, new LoginInformation { Login = "test@test.com", Passwd = "123" }, ref ua))
                Assert.Fail("Authenticate // Password invalid passed");
        }
        
        [Test]
        public void UT_Admin_Authenticate_OK()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminAuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (!service.Authenticate(null, new LoginInformation { Login = "test@test.com", Passwd = "123456" }, ref ua))
                Assert.Fail("Authenticate // Failed!");
        }
    }
}
