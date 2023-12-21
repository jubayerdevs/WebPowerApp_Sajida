using WebPowerApp.Models;

namespace WebPowerApp.Interfaces
{
    public interface ILogin
    {
        Task<IEnumerable<EmployeeModel>> getuser();
        Task<EmployeeModel> AuthenticateUser(string username, string passcode);
    }
}
