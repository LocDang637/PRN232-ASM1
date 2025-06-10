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

            // Debug information
            var debugInfo = new
            {
                TokenGenerated = !string.IsNullOrEmpty(token),
                TokenLength = token?.Length ?? 0,
                User = new { user.UserName, user.FullName, user.RoleId, user.UserAccountId },
                JwtSettings = new
                {
                    Issuer = _config["JwtSettings:Issuer"],
                    Audience = _config["JwtSettings:Audience"],
                    KeyLength = _config["JwtSettings:Key"]?.Length ?? 0,
                    ExpiryMinutes = _config["JwtSettings:ExpiryInMinutes"]
                },
                TokenExpiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpiryInMinutes"]))
            };

            return Ok(new
            {
                Token = token,
                User = new { user.UserName, user.FullName, user.RoleId },
                Debug = debugInfo
            });
        }

        private string GenerateJSONWebToken(SystemUserAccount systemUserAccount)
        {
            var key = _config["JwtSettings:Key"];
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];
            var expiryMinutes = _config["JwtSettings:ExpiryInMinutes"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("JWT settings are not properly configured");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, systemUserAccount.UserName ?? ""),
                new Claim(ClaimTypes.Email, systemUserAccount.Email ?? ""),
                new Claim(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                new Claim("UserId", systemUserAccount.UserAccountId.ToString()),
                new Claim("FullName", systemUserAccount.FullName ?? ""),
                new Claim(ClaimTypes.NameIdentifier, systemUserAccount.UserAccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, systemUserAccount.UserAccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            var expiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(expiryMinutes));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiry,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Log token generation for debugging
            Console.WriteLine($"Generated token for user: {systemUserAccount.UserName}");
            Console.WriteLine($"Token expires at: {expiry}");
            Console.WriteLine($"Token first 50 chars: {tokenString.Substring(0, Math.Min(50, tokenString.Length))}...");

            return tokenString;
        }

        // Validate a token manually (for debugging)
        [HttpPost("validate-token")]
        [AllowAnonymous]
        public IActionResult ValidateToken([FromBody] TokenValidationRequest request)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JwtSettings:Issuer"],
                    ValidAudience = _config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };

                var principal = tokenHandler.ValidateToken(request.Token, validationParameters, out SecurityToken validatedToken);

                var claims = principal.Claims.Select(c => new { c.Type, c.Value }).ToList();

                return Ok(new
                {
                    Valid = true,
                    Claims = claims,
                    TokenType = validatedToken.GetType().Name,
                    Expiry = validatedToken.ValidTo
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Valid = false,
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message
                });
            }
        }

        // GET: api/SystemUserAccount
        [HttpGet]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Get()
        {
            var users = await _userAccountsService.GetAccounts();
            return Ok(users);
        }

        [HttpGet("test-no-auth")]
        [AllowAnonymous]
        public IActionResult TestNoAuth()
        {
            return Ok(new
            {
                Status = "API is working",
                Time = DateTime.UtcNow,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            });
        }

        [HttpGet("test-auth")]
        [Authorize]
        public IActionResult TestAuth()
        {
            var userName = User.Identity?.Name;
            var userId = User.FindFirst("UserId")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Status = "Authentication working",
                UserName = userName,
                UserId = userId,
                Role = role,
                Time = DateTime.UtcNow,
                Claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList()
            });
        }

        public sealed record LoginRequest(string UserName, string Password);
        public sealed record TokenValidationRequest(string Token);
    }
}