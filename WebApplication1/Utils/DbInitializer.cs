
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Utils
{
    public class DbInitializer : IDbInitializer
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }



        public void Initialize()
        {
            
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (!_roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_SuperAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Normal)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new User
                {
                    Email = "Admin@gmail.com",
                    UserName = "Admin@gmail.com",
                }, "Mate1234!").GetAwaiter().GetResult();
                var Appuser = _context.Users.FirstOrDefault(x => x.Email == "Admin@gmail.com");
                if (Appuser != null)
                {
                    _userManager.AddToRoleAsync(Appuser, WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult();
                }

                _userManager.CreateAsync(new User
                {
                    Email = "superadmin@gmail.com",
                    UserName = "superadmin@gmail.com",
                }, "Mate1234!").GetAwaiter().GetResult();
                var Appuser2 = _context.Users.FirstOrDefault(x => x.Email == "superadmin@gmail.com");
                if (Appuser2 != null)
                {
                    _userManager.AddToRoleAsync(Appuser2, WebSiteRoles.WebSite_SuperAdmin).GetAwaiter().GetResult();
                }
            }

            //seeding to tables
            _context.Database.EnsureCreated(); // Ensure database is created
            _context.Database.Migrate(); // Apply pending migrations

            
        }
    }
}
