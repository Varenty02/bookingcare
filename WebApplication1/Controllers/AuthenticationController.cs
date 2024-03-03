using bookingcare.Models;
using bookingcare.Models.Authentication;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace bookingcare.Controllers
{
    [Tags("Authentication")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            try
            {
                if (await _authenticationRepository.EmailUserExist(registerUser))
                {
                    return new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
                if (!await _authenticationRepository.RoleExist(registerUser,"User"))
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                
                if (await _authenticationRepository.Register(registerUser, "User") == null)
                    return BadRequest();
                
                return Ok($"Tạo user có {registerUser.Email} thành công");
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                if (await _authenticationRepository.ConfirmEmail(token, email))
                    return Ok();
                return NotFound();

            }catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (await _authenticationRepository.Login(loginModel) == null)
            {
                return Unauthorized();
            }

            return Ok(await _authenticationRepository.Login(loginModel));
            


        }

        [HttpPost]
        public async Task<IActionResult> LoginWithOTP(string code, string username)
        {
            if(await _authenticationRepository.LoginWithOTP(code, username)==null)
                return NotFound();
            return Ok(await _authenticationRepository.LoginWithOTP(code, username));
        }
    }
}
