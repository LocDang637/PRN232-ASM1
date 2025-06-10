using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Services.LocDPX;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmokeQuit.APIServices.BE.LocDPX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserAccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SystemUserAccountService _userAccountsService;

        public SystemUserAccountController(IConfiguration config, SystemUserAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userAccountsService.GetAccount(request.UserName, request.Password);

            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = GenerateJSONWebToken(user);
            return Ok(new { Token = token, User = new { user.UserName, user.FullName, user.RoleId } });
        }

        private string GenerateJSONWebToken(SystemUserAccount systemUserAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, systemUserAccount.UserName),
                new Claim(ClaimTypes.Email, systemUserAccount.Email),
                new Claim(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                new Claim("UserId", systemUserAccount.UserAccountId.ToString())
            };

            var token = new JwtSecurityToken(
                _config["JwtSettings:Issuer"],
                _config["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpiryInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET: api/SystemUserAccount
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Get()
        {
            var users = await _userAccountsService.GetAccounts();
            return Ok(users);
        }

        public sealed record LoginRequest(string UserName, string Password);
    }
}