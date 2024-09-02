using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Test.Service;
using Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _tokenService;
        private readonly UserRepository _userRepository;
        //private readonly EmailService _emailService;

        public AuthController(JwtTokenService tokenService, UserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            //_emailService = emailService;
        }

        [HttpPost("register")]
        
        public async Task<IActionResult> Register([FromBody] Register_Model register)
        {
            var (passwordHash, passwordSalt) = CreatePasswordHash(register.Password);

            var user = new User_Model
            {
                UserName = register.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = register.Role,
                Email = register.Email
            };

            var result = await _userRepository.CreateUserAsync(user);
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest("User registration failed");
        }

        [HttpPost("login")]
       
        public async Task<IActionResult> Login([FromBody] Login_Model login)
        {
            var user = await _userRepository.GetUserAsync(login.UserName);
            if (user == null || !VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _tokenService.GenerateToken(user.UserName, user.Role);
            return Ok(new { token });
        }


        //[HttpPost("verify")]
        //public async Task<IActionResult> Verify([FromBody] VerifyModel verify)
        //{
        //    var user = await _userRepository.GetUserAsync(verify.UserName);
        //    var sessionCode = HttpContext.Session.GetString("VerificationCode");

        //    if (user == null || user.VerificationCode != verify.VerificationCode || sessionCode != verify.VerificationCode)
        //    {
        //        return Unauthorized("Invalid verification code");
        //    }
        //    var token = _tokenService.GenerateToken(user.UserName, user.Role);
        //    return Ok(new { token });

        //}

        private (string passwordHash, string passwordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA256())
            {
                var salt = Convert.ToBase64String(hmac.Key);
                var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return (hash, salt);
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new HMACSHA256(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(computedHash) == storedHash;
            }
        }

        //    private string GenerateVerificationCode()
        //    {
        //        using (var rng = new RNGCryptoServiceProvider())
        //        {
        //            var bytes = new byte[3];
        //            rng.GetBytes(bytes);
        //            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        //        }
        //    }
        //}

       

        //public class VerifyModel
        //{
        //    public string UserName { get; set; }
        //    public string VerificationCode { get; set; }
        //}
    }
}
