﻿
namespace Master.Controllers
{ 
    public class AuthenticatedUser
    {
        public long Id;

        public string Name;        
        public string Email;
        public string Phone;

        // devolvido no login
        public string Token;
    }
}
