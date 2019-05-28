using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_editProduct
    {
        [Test]
        public void UT_Admin_EditProduct_NameInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditProductV1(repo);

            if (service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "", Id = 0 }))
                Assert.Fail("EditProduct // Name empty accepted");
        }

        public void UT_Admin_EditProduct_IdInvalid()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditProductV1(repo);

            if (service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "test", Id = 0 }))
                Assert.Fail("EditProduct // Name empty accepted");
        }

        public void UT_Admin_EditProduct_OK()
        {
            var repo = new mockAdminRepository();
            var service = new AdminEditProductV1(repo);

            if (!service.Exec(null, new AuthenticatedUser(), new NewProductData { Name = "test", Id = 1 }))
                Assert.Fail("EditProduct // Data ok should edit");
        }
    }
}
