using bookingcare.Models.Authentication;

namespace bookingcare.Repositories
{
    public interface IAuthenticationRepository
    {
        public Task<RegisterUser> Register(RegisterUser registerUser, string roleName);
        public  Task<bool> EmailUserExist(RegisterUser registerUser);
        public Task<bool> RoleExist(RegisterUser registerUser, string roleName);
        public Task<bool> ConfirmEmail(string token, string email);
        public Task<object> LoginWithOTP(string code, string username);
        public  Task<object> Login(LoginModel loginModel);
    }
}
