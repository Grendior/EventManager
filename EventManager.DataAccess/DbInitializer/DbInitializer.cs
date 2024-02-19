using EventManager.Models;
using EventManager.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task Initialize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@dev.fiedorowicz.pl",
                    Email = "admin@dev.fiedorowicz.pl",
                    FirstName = "Admin",
                    LastName = "Admin"
                }, "Admin123!").GetAwaiter().GetResult();

                var admin = _dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName == "admin@dev.fiedorowicz.pl");
                if (admin is not null)
                {
                    _userManager.AddToRoleAsync(admin, SD.Role_Admin).GetAwaiter().GetResult();
                }
                
                
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "company@dev.fiedorowicz.pl",
                    Email = "company@dev.fiedorowicz.pl",
                    FirstName = "company",
                    LastName = "company"
                }, "Company123!").GetAwaiter().GetResult();

                var company = _dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName == "company@dev.fiedorowicz.pl");
                if (company is not null)
                {
                    _userManager.AddToRoleAsync(company, SD.Role_Company).GetAwaiter().GetResult();
                }
                
                
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "user@dev.fiedorowicz.pl",
                    Email = "user@dev.fiedorowicz.pl",
                    FirstName = "user",
                    LastName = "user"
                }, "User123!").GetAwaiter().GetResult();

                var user = _dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName == "user@dev.fiedorowicz.pl");
                if (user is not null)
                {
                    _userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();
                }
            }
        }
    }
}
