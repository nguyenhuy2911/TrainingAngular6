using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TraningApp.WebAPI.Data.Models;
using TraningApp.WebAPI.Models;

namespace TraningApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private IConfiguration _config;

        public TokenController(IConfiguration config, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signManager)
        {
            _config = config;
            _roleManager = roleManager;
            _userManager = userManager;
            _signManager = signManager;
        }

        // Post api/token
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateToken(string userName, string passWord)
        {
            IActionResult response = NoContent();

            var checkLogin = await _signManager.PasswordSignInAsync(userName, passWord, false, true);
            if (checkLogin.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);
                var tokenString = BuildToken(user);
                response = Ok(new Response<object> { Result = new { token = tokenString }, IsSuccess = true });
            }
            else
                response = Ok(new Response<object> { IsSuccess = false, Message = "UserName and PassWord incorect" });

            return response;
        }
        private string BuildToken(IdentityUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List< Claim >
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),
             //  ClaimTypes.
               
            };
            if (user.UserName.ToLower() == "admin")
                claims.Add(new Claim("role", "Admin"));
            else
                claims.Add(new Claim("role", "ApiUser"));
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
