using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_products
    {
        [Test]
        public void UT_Admin_products()
        {
            var repo = new mockAdminRepository();
            var service = new AdminProductsV1(repo);

            var resp = service.Exec(null, 1, 1, 0, 10);

            if (resp.list.Count != 1)
                Assert.Fail("products // fail 1");            
        }
    }
}
