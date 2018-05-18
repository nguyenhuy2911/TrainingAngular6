using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TraningApp.WebAPI.Data.Models;
using TraningApp.WebAPI.Models;

namespace TraningApp.WebAPI.Data
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Initialize()
        {
            _context.Database.Migrate();
            if (_context.Roles.Any(r => r.Name == "admin")) return;
            var createRoleResult =  _roleManager.CreateAsync(new IdentityRole("admin"));
            var saveUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                CreateDate = DateTime.Now
            };
            var createUserResult = await _userManager.CreateAsync(saveUser, "123@Abcd");          
            if (createUserResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(saveUser.UserName);
                await _userManager.AddToRoleAsync(user, "admin");
            }
        }
    }
}
