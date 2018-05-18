using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TraningApp.WebAPI.Models;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TraningApp.WebAPI.Data.Models;

namespace TraningApp.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AcountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AcountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Gets(string userName)
        {
    
            var users = _userManager.Users.ToList();
            if (users == null)
                return NoContent();
            return Ok(users);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> Get(string userName)
        {
            var ss = User.Claims;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return NoContent();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterModel model)
        {
            string errmess = string.Empty;
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(x => x.Errors);
                errmess = string.Join("<br/>", errorList.Select(x => x.ErrorMessage));
                return Ok(new Response<object> { IsSuccess = false, Message = errmess });
            }
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                FullName = model.FullName,
                Email = model.UserName + "@gmail.com",
                CreateDate = DateTime.Now
               
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                errmess = string.Join("<br/>", result.Errors.Select(p => p.Description));                
                return Ok(new Response<object> { IsSuccess = false, Message = errmess });
            }
            return Ok(new Response<object> { Result = user, IsSuccess = true });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserRegisterModel model)
        {
            string errmess = string.Empty;
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(x => x.Errors);
                errmess = string.Join("<br/>", errorList.Select(x => x.ErrorMessage));
                return Ok(new Response<object> { IsSuccess = false, Message = errmess });
            }
            IActionResult response = NoContent();
            var user = await _userManager.FindByNameAsync(model.UserName);
            user.FullName = model.FullName;
            var checkUpdate = await _userManager.UpdateAsync(user);
            if (!checkUpdate.Succeeded)
            {
                errmess = string.Join("<br/>", checkUpdate.Errors.Select(p => p.Description));
                return Ok(new Response<object> { IsSuccess = false, Message = errmess });
            }
            string resetpass = await _userManager.GeneratePasswordResetTokenAsync(user);
            var checkresetPass = await _userManager.ResetPasswordAsync(user, resetpass, model.ConfirmPassword);
            if (!checkresetPass.Succeeded)
            {
                errmess = string.Join("<br/>", checkresetPass.Errors.Select(p => p.Description));
                return Ok(new Response<object> { IsSuccess = false, Message = errmess });
            }
            return Ok(new Response<object> { Result = user, IsSuccess = true });


        }
    }
}
