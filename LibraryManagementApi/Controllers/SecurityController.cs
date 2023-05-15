using LibraryManagementApi.Models;
using LibraryManagementApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementApi.Controllers
{
    public class SecurityController : Controller
    {
        private IConfiguration _config;
        private LibraryContext _db;
        public SecurityController(LibraryContext context, IConfiguration config)
        {
            _db = context;
            _config = config;
        }

        [Route("/security/createToken")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateToken([FromBody] UserVM login)
        {
            {
                IActionResult response = Unauthorized();
                var user = await AuthenticateUserAsync(login);

                if (user != null)
                {
                    var tokenString = GenerateJSONWebToken(user);
                    response = Ok(new { token = tokenString });
                }

                return response;
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<User> AuthenticateUserAsync(UserVM login)
        {
            User user = null;

            //Validate the User Credentials    
            User? dbUser = _db.Users.FirstOrDefault(x => x.Username == login.Username && x.Password == login.Password);
            if (dbUser != null)
            {
                user = new User { Username = dbUser.Username, Password = dbUser.Password, Role = dbUser.Role };
            }

            return user;
        }
    }
}
