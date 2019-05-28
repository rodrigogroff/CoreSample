using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_editCategory
    {
        [Test]
        public void UT_Admin_EditCategory_NameInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditCategoryV1(repo);

            if (service.Exec(null, new NewCategoryData { Name = "", Id = 1 }))
                Assert.Fail("EditCategory // Name empty accepted");
        }

        public void UT_Admin_EditCategory_IdInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditCategoryV1(repo);

            if (service.Exec(null, new NewCategoryData { Name = "xxxx", Id = 9999999 }))
                Assert.Fail("EditCategory // Id invalid");
        }

        [Test]
        public void UT_Admin_EditCategory_OK()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditCategoryV1(repo);

            if (!service.Exec(null, new NewCategoryData { Name = "xxx", Id = 1 }))
                Assert.Fail("EditCategory // Not existing user should create account");
        }
    }
}
