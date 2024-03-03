using bookingcare.Models;

namespace bookingcare.Repositories
{
    public interface IAccountRepository
    {
        public  Task<List<AccountModel>> GetAlllUsersAsync();
        public Task<AccountModel> GetUserByIdAsync(string id);
        public Task<int> UpdateUserAsync(string id, AccountUpdateModel user);
        public Task<string> AddUserAsync(AccountCreateModel user);
        public Task<int> DeleteUserAsync(string id);
    }
}
