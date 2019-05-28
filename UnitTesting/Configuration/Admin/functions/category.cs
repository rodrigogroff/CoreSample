using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_category
    {
        [Test]
        public void UT_Admin_category()
        {
            var repo = new mockAdminRepository();
            var service = new AdminCategoryV1(repo);

            var resp = service.Exec(null, new AuthenticatedUser(), 1);

            if (resp.Id != 1)
                Assert.Fail("category // fail 1");

            if (resp.Name != "test")
                Assert.Fail("category // fail 2");
        }
    }
}
