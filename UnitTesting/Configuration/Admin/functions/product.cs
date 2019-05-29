using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_product
    {
        [Test]
        public void UT_Admin_product()
        {
            var repo = new mockAdminRepository();
            var service = new AdminProductV1(repo);

            var resp = service.Exec(null, 1);

            if (resp.Id != 1)
                Assert.Fail("product // fail 1");

            if (resp.Name != "test")
                Assert.Fail("product // fail 2");
        }
    }
}
