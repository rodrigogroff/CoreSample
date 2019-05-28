using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_subcategory
    {
        [Test]
        public void UT_Admin_subcategory()
        {
            var repo = new mockAdminRepository();
            var service = new AdminSubCategoryV1(repo);

            var resp = service.Exec(null, new AuthenticatedUser(), 1);

            if (resp.Id != 1)
                Assert.Fail("subcategory // fail 1");

            if (resp.Name != "test")
                Assert.Fail("subcategory // fail 2");
        }
    }
}
