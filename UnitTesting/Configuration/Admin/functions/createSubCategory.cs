using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_createSubCategory
    {
        [Test]
        public void UT_Admin_CreateSubCategory_NameInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminCreateSubCategoryV1(repo);

            if (service.Exec(null, new NewSubCategoryData { ProductCategoryID = 1, Name = "" }))
                Assert.Fail("CreateSubCategory // Name empty accepted");
        }
        
        [Test]
        public void UT_Admin_CreateCategory_CategoryInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminCreateSubCategoryV1(repo);

            if (service.Exec(null, new NewSubCategoryData { ProductCategoryID = 0, Name = "xxx" }))
                Assert.Fail("CreateSubCategory // Category ID invalid passed");
        }

        public void UT_Admin_CreateCategory_CategoryNotExist()
        {
            var repo = new mockAdminRepository();
            var service = new AdminCreateSubCategoryV1(repo);

            if (service.Exec(null, new NewSubCategoryData { ProductCategoryID = 1, Name = "xxx" }))
                Assert.Fail("CreateSubCategory // Category ID invalid passed");
        }

        [Test]
        public void UT_Admin_CreateCategory_OK()
        {
            var repo = new mockAdminRepository();
            var service = new AdminCreateSubCategoryV1(repo);

            if (!service.Exec(null, new NewSubCategoryData { ProductCategoryID = 1, Name = "xxx" }))
                Assert.Fail("CreateSubCategory // ok not passed");
        }
    }
}
