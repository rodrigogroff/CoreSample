using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void createAccount()
        {
            string email = "", clientGuid = "";

            CreateIntegrationUser(ref email, ref clientGuid);
        }
    }
}
