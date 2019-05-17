using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{    
    public class BaseController : ControllerBase
    {
        protected UsuarioAutenticado ObtemUsuarioAutenticado()
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            return new UsuarioAutenticado
            {
                Id = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value,
                Celular = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Celular")?.Value,
                Email = tokenS.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value,
                Nome = tokenS.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value,                
            };
        }
    }
}
