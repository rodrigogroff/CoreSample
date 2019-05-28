using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_editSubCategory
    {
        [Test]
        public void UT_Admin_EditSubCategory_NameInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditSubCategoryV1(repo);

            if (service.Exec(null, new NewSubCategoryData { Name = "", Id = 1 }))
                Assert.Fail("EditSubCategory // Name empty accepted");
        }

        public void UT_Admin_EditSubCategory_IdInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditSubCategoryV1(repo);

            if (service.Exec(null, new NewSubCategoryData { Name = "xxx", Id = 1 }))
                Assert.Fail("EditSubCategory // Invalid ID");
        }

        [Test]
        public void UT_Admin_EditSubCategory_OK()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditSubCategoryV1(repo);

            if (!service.Exec(null, new NewSubCategoryData { Name = "xxx", Id = 1 }))
                Assert.Fail("EditSubCategory // Not existing user should create account");
        }
    }
}
