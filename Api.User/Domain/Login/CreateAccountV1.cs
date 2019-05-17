using Gateway.Controllers;

namespace Api.Usuario.Domain
{
    public class CreateAccountV1
    {
        public ServiceError Error = new ServiceError();
        
        public bool CreateAccount(NewUserData newUser)
        {
            
            return true;
        }
    }
}
