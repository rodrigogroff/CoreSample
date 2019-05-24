using Api.Configuration.Service;
using Entities.Api.Configuration;
using NUnit.Framework;

namespace UnitTesting
{    
    public class AdminUT_subcategories
    {
        [Test]
        public void UT_Admin_subcategories()
        {
            var repo = new mockAdminRepositoryUserExists();
            var service = new AdminSubCategoriesV1(repo);

            var resp = service.Exec(null, new AuthenticatedUser(), 1, 0, 10);

            if (resp.list.Count != 1)
                Assert.Fail("subcategories // fail 1");

            if (resp.total != 1)
                Assert.Fail("subcategories // fail 2");

            if (resp.list[0].Id != 1)
                Assert.Fail("subcategories // fail 3.1");

            if (resp.list[0].Name != "teste")
                Assert.Fail("subcategories // fail 3.2");
        }
    }
}
