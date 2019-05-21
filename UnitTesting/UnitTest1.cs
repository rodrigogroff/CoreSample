using Api.User.Repository;
using Api.User.Service;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var repo = new UserRepository();
            var createAccMock = new CreateAccountV1(repo);

            createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {

            });
        }
    }
}