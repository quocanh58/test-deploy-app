using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PE_PRN231_SE150627_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceBase<UserAccount> _service;

        public AuthsController(IConfiguration configuration, ServiceBase<UserAccount> service)
        {
            _configuration = configuration;
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest login)
        {
            var user = await _service
                .FindByAsync(x => x.UserEmail == login.Username &&
                                  x.UserPassword == login.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserAccountId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()!)
            //role
        };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SerectKey").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            long expiredToken = 30;
            var token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(expiredToken), 
                 signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            //return Ok(new TokenRequest(jwt, user.Role));
            return Ok(new AccessTokenResponse { AccessToken = jwt, ExpiresIn = expiredToken });
        }
    }
}
