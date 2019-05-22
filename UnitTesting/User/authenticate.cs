using Api.User.Service;
using NUnit.Framework;

namespace UnitTesting
{    
    public class UserUT_authenticate
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
                Assert.Fail("Authenticate // Login empty accepted");
            }

            #endregion
        }
    }
}
