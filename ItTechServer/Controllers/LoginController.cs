using ItTechServer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace ItTechServer.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        ApplicationContext db;
        public LoginController(ApplicationContext context)
        {
            db = context;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel loginData)
        {
            UserModel? user = db.Users.ToList().FirstOrDefault(u =>u.Email == loginData.Email && u.Password == loginData.Password);
            if (user is null) return Unauthorized();


            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
                    
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var respons = new
            {
                accestoken = encodedJwt,
                username = user.Email,
            };

            return Json(respons);
          
        }
    }
}
