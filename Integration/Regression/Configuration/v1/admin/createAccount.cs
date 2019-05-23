using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integration
{
    public partial class Configuration : BaseTest
    {
        [TestMethod]
        public void AdminCreateAccount()
        {
            string email = "";

            CreateIntegrationAdmin(ref email);
        }
    }
}
