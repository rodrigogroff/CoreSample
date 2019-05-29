using Api.Portal.Service;
using Entities.Api.Configuration;
using Entities.Api.Portal;
using NUnit.Framework;

namespace UnitTesting
{
    public class PortalUT_productComment
    {
        [Test]
        public void UT_Portal_productComment()
        {
            var repo = new mockPortalRepository();
            var service = new PortalProductCommentV1(repo);

            if (!service.Exec(null,
                                new AuthenticatedUser() { Id = 1 },
                                new NewProductComment { Comment = "hello" }))
            {
                Assert.Fail("productComment // fail 1");
            }
        }
    }
}
