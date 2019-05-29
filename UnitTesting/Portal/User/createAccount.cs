using Api.Configuration.Service;
using Api.Portal.Service;
using Entities.Api.Configuration;
using Entities.Api.Portal;
using NUnit.Framework;

namespace UnitTesting
{    
    public class UserUT_createAccount
    {
        [Test]
        public void UT_User_CreateAccount_NameInvalid()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCreateAccountV1(repo);

            if (service.Exec(null, new NewUserData { Name = "" }))
                Assert.Fail("CreateAccount // Name empty accepted");
        }

        [Test]
        public void UT_User_CreateAccount_EmailInvalid_1()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCreateAccountV1(repo);

            if (service.Exec(null, new NewUserData { Name = "xxx", Email = "" }))
                Assert.Fail("CreateAccount // Email invalid 1 accepted");
        }

        [Test]
        public void UT_User_CreateAccount_EmailInvalid_2()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCreateAccountV1(repo);

            if (service.Exec(null, new NewUserData { Name = "xxx", Email = "fddd" }))
                Assert.Fail("CreateAccount // Email invalid 2 accepted");
        }

        [Test]
        public void UT_User_CreateAccount_EmailInvalid_3()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCreateAccountV1(repo);

            if (service.Exec(null, new NewUserData { Name = "xxx", Email = "fddd@fdsd" }))
                Assert.Fail("CreateAccount // Email invalid 3 accepted");
        }

        [Test]
        public void UT_User_CreateAccount_PasswordInvalid_1()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCreateAccountV1(repo);

            if (service.Exec(null, new NewUserData { Name = "xxx", Email = "x@c.com", Password = "" }))
                Assert.Fail("CreateAccount // cannot be empty");
        }

        [Test]
        public void UT_User_CreateAccount_PasswordInvalid_2()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCreateAccountV1(repo);

            if (service.Exec(null, new NewUserData { Name = "xxx", Email = "x@c.com", Password = "12345" }))
                Assert.Fail("CreateAccount // cannot be empty");
        }

        [Test]
        public void UT_User_CreateAccount_UserExist()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCreateAccountV1(repo);

            if (service.Exec(null, new NewUserData { Name = "xxx", Email = "x@c.com", Password = "123456" }))
                Assert.Fail("CreateAccount // Existing user cannot create same account");
        }

        [Test]
        public void UT_User_CreateAccount_OK()
        {
            var repo = new mockUserRepositoryUserNotExists();
            var service = new UserCreateAccountV1(repo);

            if (!service.Exec(null, new NewUserData { Name = "xxx", Email = "x@c.com", Password = "123456" }))
                Assert.Fail("CreateAccount // Not existing user should create account");
        }
    }
}
