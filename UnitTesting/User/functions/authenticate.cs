using Api.User.Service;
using Master.Controllers;
using NUnit.Framework;

namespace UnitTesting
{    
    public class UserUT_authenticate : BaseTest
    {
        [Test]
        public void UT_User_Authenticate_LoginInvalid()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.authenticate(null, new Master.Controllers.LoginInformation { Login = "" }, ref ua))
                Assert.Fail("Authenticate // Login empty accepted");
        }

        [Test]
        public void UT_User_Authenticate_PasswordInvalid()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.authenticate(null, new Master.Controllers.LoginInformation { Login = "test@test.com", Passwd = "" }, ref ua))
                Assert.Fail("Authenticate // Password empty accepted");
        }

        [Test]
        public void UT_User_Authenticate_PasswordInvalid_2()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.authenticate(null, new Master.Controllers.LoginInformation { Login = "test@test.com", Passwd = "123" }, ref ua))
                Assert.Fail("Authenticate // Password invalid passed");
        }

        [Test]
        public void UT_User_Authenticate_ClientInvalid()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (service.authenticate(null, new Master.Controllers.LoginInformation { Login = "test@test.com", Passwd = "123456", ClientGuid = "" }, ref ua))
                Assert.Fail("Authenticate // ClientGuid invalid passed");
        }

        [Test]
        public void UT_User_Authenticate_OK()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);
            var ua = new AuthenticatedUser();

            if (!service.authenticate(null, new Master.Controllers.LoginInformation { Login = "test@test.com", Passwd = "123456", ClientGuid = GetValidClientGuid() }, ref ua))
                Assert.Fail("Authenticate // Failed!");
        }
    }
}
