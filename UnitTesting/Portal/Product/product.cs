using Api.Portal.Service;
using NUnit.Framework;

namespace UnitTesting
{    
    public class PortalUT_product
    {
        [Test]
        public void UT_Portal_product()
        {
            var repo = new mockPortalRepository();
            var service = new PortalProductV1(repo);

            var resp = service.Exec(null, null, 1);

            if (resp.Id != 1)
                Assert.Fail("product // fail 1");
        }
    }
}
