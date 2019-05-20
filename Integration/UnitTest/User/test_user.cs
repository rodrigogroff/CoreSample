using Api.User.Repository;
using Api.User.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integration
{
    [TestClass]
    public class UserUT : BaseUnitTest
    {
        [TestMethod]
        public void UT_User_Authenticate()
        {
            var repo = new UserRepository();
            var createAccMock = new CreateAccountV1(repo);

            createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {

            });
        }
    }
}
