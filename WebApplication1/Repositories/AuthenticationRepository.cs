
using bookingcare.Models;
using bookingcare.Models.Authentication;
using bookingcare.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailService;
        private readonly IUrlHelper _urlHelper;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthenticationRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager
            , IEmailSender emailService, IUrlHelper urlHelper, IConfiguration configuration , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _urlHelper = urlHelper;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        public async Task<RegisterUser> Register(RegisterUser registerUser,string roleName)
        {
            AppUser user = new AppUser()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username,
                FirstName= registerUser.FirstName,
                LastName= registerUser.LastName,
                Address = registerUser.Address,
                TwoFactorEnabled = true
            };
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {

                return null;
            }
            await _userManager.AddToRoleAsync(user, roleName);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = _urlHelper.ActionLink("ConfirmEmail", "Authentication", new { token, email = user.Email }, _urlHelper.ActionContext.HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(user.Email!, "Xác nhận email", $@"<a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>bấm vào đây</a>");
            return registerUser;

        }
        public async Task<bool> EmailUserExist(RegisterUser registerUser)
        {
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> RoleExist(RegisterUser registerUser,string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return true;

            }
            return false;
        }
        public async Task<bool> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public async Task<object> Login(LoginModel loginModel)
        {
            var user = await  _userManager.FindByEmailAsync(loginModel.Username);
            if (user == null||!user.TwoFactorEnabled) { 
                return  null;
            }
            if (user.TwoFactorEnabled)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, true);
                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                await _emailService.SendEmailAsync(user.Email!, "OTP", token);

                return  new MessageModel() {Message= "Kiểm tra OTP trong email" };
            }
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }


                var jwtToken = GetToken(authClaims);

                return new TokenModel()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    ValidTo = jwtToken.ValidTo.ToString(),
                };
                //returning the token...

            }
            return null;
        }
        public async Task<object> LoginWithOTP(string code, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var signIn = await _signInManager.TwoFactorSignInAsync("Email", code, false, false);
            if (signIn.Succeeded)
            {
                if (user != null)
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var jwtToken = GetToken(authClaims);

                    return  new TokenModel()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        ValidTo = jwtToken.ValidTo.ToString(),
                    };

                }
            }
            return null;
        }
    }
}
