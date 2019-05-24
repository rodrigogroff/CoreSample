using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_createCategory
    {
        [Test]
        public void UT_Admin_CreateCategory_NameInvalid()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminCreateCategoryV1(repo);

            if (service.Exec(null, new NewCategoryData { Name = "" }))
                Assert.Fail("CreateCategory // Name empty accepted");
        }
        
        [Test]
        public void UT_Admin_CreateCategory_CategoryExist()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminCreateCategoryV1(repo);

            if (service.Exec(null, new NewCategoryData { Name = "xxx" }))
                Assert.Fail("CreateCategory // Existing user cannot create same account");
        }

        [Test]
        public void UT_Admin_CreateCategory_OK()
        {
            var repo = new mockAdminRepositoryUserNotExists();
            var service = new AdminCreateCategoryV1(repo);

            if (!service.Exec(null, new NewCategoryData { Name = "xxx" }))
                Assert.Fail("CreateCategory // Not existing user should create account");
        }
    }
}
