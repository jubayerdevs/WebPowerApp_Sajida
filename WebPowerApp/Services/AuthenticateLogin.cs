using Microsoft.EntityFrameworkCore;
using WebPowerApp.Context;
using WebPowerApp.Interfaces;
using WebPowerApp.Models;

namespace WebPowerApp.Services
{
    public class AuthenticateLogin : ILogin
    {
        private readonly ApplicationDBContext _dbContext;
        public AuthenticateLogin(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeModel>> getuser()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeModel> AuthenticateUser(string username, string passcode)
        {
            var succeeded = await _dbContext.Employees.FirstOrDefaultAsync(authUser => authUser.Username == username && authUser.Password == passcode && authUser.IsActive == true);
            return succeeded;
        }
    }
}
