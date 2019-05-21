using Api.User.Service;
using NUnit.Framework;

namespace UnitTesting
{    
    public class UserUT
    {
        [Test]
        public void UT_User_CreateAccount_NameInvalid()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "",                
            }))
            {
                Assert.Fail("CreateAccount // Name empty accepted");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_Name_OK()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (!createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "test",
            }))
            {
                Assert.Fail("CreateAccount // Name Ok failed");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_EmailInvalid_1()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = ""
            }))
            {
                Assert.Fail("CreateAccount // Email invalid 1 accepted");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_EmailInvalid_2()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "fddd"
            }))
            {
                Assert.Fail("CreateAccount // Email invalid 2 accepted");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_EmailInvalid_3()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "fddd@fdsd"
            }))
            {
                Assert.Fail("CreateAccount // Email invalid 3 accepted");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_Email_OK()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (!createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "test@test.com"
            }))
            {
                Assert.Fail("CreateAccount // Email OK failed");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_PasswordInvalid_1()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "x@c.com",
                Password = ""
            }))
            {
                Assert.Fail("CreateAccount // cannot be empty");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_PasswordInvalid_2()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "x@c.com",
                Password = "12345"
            }))
            {
                Assert.Fail("CreateAccount // cannot be empty");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_Password_OK()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (!createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "x@c.com",
                Password = "12345"
            }))
            {
                Assert.Fail("CreateAccount // Password OK failed");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_UserExist()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserExists();
            var createAccMock = new CreateAccountV1(repo);

            if (createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "x@c.com",
                Password = "123456"
            }))
            {
                Assert.Fail("CreateAccount // Existing user cannot create same account");
            }

            #endregion
        }

        [Test]
        public void UT_User_CreateAccount_UserNotExist()
        {
            #region - code - 

            var repo = new mockUserRepositoryUserNotExists();
            var createAccMock = new CreateAccountV1(repo);

            if (!createAccMock.CreateAccount(null, new Gateway.Controllers.NewUserData
            {
                Name = "xxx",
                Email = "x@c.com",
                Password = "123456"
            }))
            {
                Assert.Fail("CreateAccount // Not existing user should create account");
            }

            #endregion
        }
    }
}