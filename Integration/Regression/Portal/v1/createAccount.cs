using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integration
{
    public partial class Portal : BaseTest
    {
        [TestMethod]
        public void UserCreateAccount()
        {
            string email = "";

            CreateIntegrationUser(ref email);
        }
    }
}
