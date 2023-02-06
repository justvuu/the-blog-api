using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;
using TheBlogAPI.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TheBlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenController : ControllerBase
    {

        private IConfiguration _config;

        public AuthenController(IConfiguration config)
        {
            _config = config;
        }

        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login (UserDTO user)
        {
            if (user != null)
            {
                if(user.username == "admin" && user.password == "admin")
                {

                    var token = GenerateToken(user);

                    var result = new
                    {
                        code = 200,
                        message = "Successful",
                        access_token = token,
                    };
                    string jsonData = JsonConvert.SerializeObject(result);

                    JObject jsonObject = JObject.Parse(jsonData);
                    return Ok(result);
                }
                return NotFound(new {
                    code = 404,
                    message = "User does not exist !"
                });
            }
            return NotFound(new
            {
                code = 404,
                message = "User does not exist !"
            });

        }

        private string GenerateToken(UserDTO user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.username),
                new Claim("role", "admin")
            };

            var token = new JwtSecurityToken(_config["JWT:Issuer"],
             _config["JWT:Audience"],
             claims,
             expires: DateTime.Now.AddDays(1),
             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
