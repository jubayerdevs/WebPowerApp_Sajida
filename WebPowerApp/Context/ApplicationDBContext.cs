using Microsoft.EntityFrameworkCore;
using WebPowerApp.Models;

namespace WebPowerApp.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<ReportModel> Reports { get; set; }
        


    }
}
