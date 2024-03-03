using bookingcare.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApplication1.Data;

namespace bookingcare.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<AccountModel>> GetAlllUsersAsync()
        {

            var users = await _userManager.Users.ToListAsync();
            var listUserModel= new List<AccountModel>();
            foreach (var user in users)
            {
                listUserModel.Add(new AccountModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Password = user.PasswordHash

                });
            }
            return listUserModel;
        }
        public async Task<AccountModel> GetUserByIdAsync(string id)
        {
            var userData = await _userManager.FindByIdAsync(id);

            if (userData == null)
            {
                return null;
            }

            var user = new AccountModel
            {
                Id = userData.Id,
                UserName = userData.UserName,
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                PhoneNumber = userData.PhoneNumber,
                Address = userData.Address,
                Password = userData.PasswordHash

            };

            return user;
        }
        public async Task<int> UpdateUserAsync(string id, AccountUpdateModel user)
        {
            if (id != user.Id)
            {
                return StatusCodes.Status400BadRequest;
            }

            var userData = await _userManager.FindByIdAsync(id);

            if (userData == null)
            {
                return StatusCodes.Status404NotFound;
            }

            userData.UserName = user.UserName;
            userData.Email = user.Email;
            userData.FirstName = user.FirstName;
            userData.LastName = user.LastName;
            userData.PhoneNumber = user.PhoneNumber;
            userData.Address = user.Address;
            // Cập nhật các trường khác cần thiết tại đây

            var result = await _userManager.UpdateAsync(userData);

            if (result.Succeeded)
            {
                return StatusCodes.Status204NoContent;
            }

            return StatusCodes.Status400BadRequest;
        }
        public async Task<string> AddUserAsync(AccountCreateModel user)
        {
            var userData = new AppUser
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
              

            };

            var result = await _userManager.CreateAsync(userData,user.Password);

            if (result.Succeeded)
            {
                
                return userData.Id;
            }

            return null;
        }
        public async Task<int> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return StatusCodes.Status404NotFound;
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return StatusCodes.Status204NoContent;
            }

            return StatusCodes.Status400BadRequest;
        }
    }
}
