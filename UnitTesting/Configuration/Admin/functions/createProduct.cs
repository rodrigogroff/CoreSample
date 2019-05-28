using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_createProduct
    {
        [Test]
        public void UT_Admin_CreateProduct_NameInvalid()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminCreateProductV1(repo);

            if (service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "" }))
                Assert.Fail("CreateProduct // Name empty accepted");
        }

        [Test]
        public void UT_Admin_CreateProduct_CategID_Invalid()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminCreateProductV1(repo);

            if (service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "test", ProductCategoryID = 0, ProductSubCategoryID = 0 }))
                Assert.Fail("CreateProduct // Category invalid accepted");
        }

        [Test]
        public void UT_Admin_CreateProduct_SubCategID_Invalid()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminCreateProductV1(repo);

            if (service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "test", ProductCategoryID = 1, ProductSubCategoryID = 0 }))
                Assert.Fail("CreateProduct // Subcategory invalid accepted");
        }

        [Test]
        public void UT_Admin_CreateProduct_Product_Exists()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminCreateProductV1(repo);

            if (service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "test", ProductCategoryID = 1, ProductSubCategoryID = 1 }))
                Assert.Fail("CreateProduct // product exist and was creaded");
        }

        [Test]
        public void UT_Admin_CreateProduct_Product_OK()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminCreateProductV1(repo);

            if (!service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "test", ProductCategoryID = 1, ProductSubCategoryID = 2 }))
                Assert.Fail("CreateProduct // OKk and was not created");
        }
    }
}
