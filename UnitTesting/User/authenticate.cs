using Api.User.Service;
using NUnit.Framework;

namespace UnitTesting
{    
    public class UserUT_authenticate : BaseTest
    {
        [Test]
        public void UT_User_Authenticate_LoginInvalid()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);

            if (service.authenticate(null, new Master.Controllers.LoginInformation 
            {
                Login = ""
            }))
            {
                Assert.Fail("Authenticate // Login empty accepted");
            }

            #endregion
        }

        [Test]
        public void UT_User_Authenticate_PasswordInvalid()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);

            if (service.authenticate(null, new Master.Controllers.LoginInformation
            {
                Login = "test@test.com",
                Passwd = ""
            }))
            {
                Assert.Fail("Authenticate // Password empty accepted");
            }

            #endregion
        }

        [Test]
        public void UT_User_Authenticate_PasswordInvalid_2()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);

            if (service.authenticate(null, new Master.Controllers.LoginInformation
            {
                Login = "test@test.com",
                Passwd = "123"
            }))
            {
                Assert.Fail("Authenticate // Password invalid passed");
            }

            #endregion
        }

        [Test]
        public void UT_User_Authenticate_ClientInvalid()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);

            if (service.authenticate(null, new Master.Controllers.LoginInformation
            {
                Login = "test@test.com",
                Passwd = "123456",
                ClientGuid = ""
            }))
            {
                Assert.Fail("Authenticate // ClientGuid invalid passed");
            }

            #endregion
        }

        [Test]
        public void UT_User_Authenticate_OK()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var service = new AuthenticateV1(repo);

            if (!service.authenticate(null, new Master.Controllers.LoginInformation
            {
                Login = "test@test.com",
                Passwd = "123456",
                ClientGuid = GetValidClientGuid()
            }))
            {
                Assert.Fail("Authenticate // Failed!");
            }

            #endregion
        }
    }
}
