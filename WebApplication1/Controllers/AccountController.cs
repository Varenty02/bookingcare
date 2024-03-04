using bookingcare.Models;
using bookingcare.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookingcare.Controllers
{
    [Tags("Account")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountModel>>> GetAllAccount()
        {
            try
            {
                return Ok(await _accountRepository.GetAlllUsersAsync());
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountModel>> GetUserById(string id)
        {
            try
            {
                var specialty = await _accountRepository.GetUserByIdAsync(id);
                return specialty == null ? NotFound() : Ok(specialty);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(string id, AccountUpdateModel updateModel)
        {
            //if (_accountRepository.UserExist(id) == null)
            //{
            //    return NotFound();
            //}

            try
            {
                int statusCode=await _accountRepository.UpdateUserAsync(id, updateModel);
                if(statusCode == 204)
                {
                    return NoContent();
                }
                return StatusCode(statusCode);
                
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<ActionResult<string>> AddAccount(AccountCreateModel accountModel)
        {
            try
            {
                var id = await _accountRepository.AddUserAsync(accountModel);
                return CreatedAtAction(nameof(GetUserById), new { id }, accountModel);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            //if (_accountRepository.AccountExists(id) == null)
            //{
            //    return NotFound();
            //}
            try
            {
                var statusCode=await _accountRepository.DeleteUserAsync(id);
                if(statusCode==204)
                    return Ok();
                return StatusCode(statusCode);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
