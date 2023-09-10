using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Utils;

namespace WebApplication1.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public AuthService(UserManager<User> userManager, IConfiguration config, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _config = config;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<bool> RegisterUser(string userName, string password)
        {
            var identityUser = new User
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(identityUser, password);
            var Appuser = _context.Users.FirstOrDefault(x => x.Email == userName);
            if (Appuser != null)
            {
                _userManager.AddToRoleAsync(Appuser, WebSiteRoles.WebSite_Normal).GetAwaiter().GetResult();
            }
            return result.Succeeded;
        }

        public async Task<bool> Login(User uu)
        {
            var user = _userManager.FindByEmailAsync(uu.UserName);
            if (user == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(uu, uu.PasswordHash);

        }

        public async Task<string> GenerateTokenStringAsync(User uu)
        {
            var user = await _userManager.FindByIdAsync(uu.Id);
            var role = "Normal";
            if (user != null)
            {
                // Get the roles associated with the user
                var roles = await _userManager.GetRolesAsync(user);
                role = roles.FirstOrDefault();
            }

            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, uu.Email),
                new Claim(ClaimTypes.Role, role),
            };
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddMinutes(60),
                issuer:_config.GetSection("Jwt:Issuer").Value,
                audience:_config.GetSection("Jwt:Audience").Value,
                signingCredentials:signingCred);
            string tokenstring = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenstring;
        }

    }
}
