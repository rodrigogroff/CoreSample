using Api.Portal.Service;
using NUnit.Framework;

namespace UnitTesting
{    
    public class PortalUT_categories
    {
        [Test]
        public void UT_Portal_categories()
        {
            var repo = new mockPortalRepository();
            var service = new PortalCategoriesV1(repo);

            var resp = service.Exec(null, 0, 10);

            if (resp.list.Count != 1)
                Assert.Fail("categories // fail 1");

            if (resp.total != 1)
                Assert.Fail("categories // fail 2");

            if (resp.list[0].Id != 1)
                Assert.Fail("categories // fail 3.1");

            if (resp.list[0].Name != "teste")
                Assert.Fail("categories // fail 3.2");
        }
    }
}
