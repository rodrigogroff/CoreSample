using Api.Portal.Service;
using NUnit.Framework;

namespace UnitTesting
{    
    public class PortalUT_products
    {
        [Test]
        public void UT_Portal_products()
        {
            var repo = new mockPortalRepository();
            var service = new PortalProductsV1(repo);

            var resp = service.Exec(null, 1,1, 0, 10);

            if (resp.list.Count != 1)
                Assert.Fail("products // fail 1");
        }
    }
}
