using Api.Configuration.Service;
using NUnit.Framework;

namespace UnitTesting
{    
    public class UserUT_comments
    {
        [Test]
        public void UT_User_Comments()
        {
            var repo = new mockUserRepositoryUserExists();
            var service = new UserCommentsV1(repo);

            var dto = service.Comments(null, new Master.Controllers.AuthenticatedUser(), 0, 1);

            if (dto.total != 1)
                Assert.Fail("dto.total");

            if (dto.list.Count != 1)
                Assert.Fail("dto.list");
        }
    }
}
