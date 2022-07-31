using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Models
{
    public class HRDatabaseContext : DbContext
    {
        public HRDatabaseContext(DbContextOptions<HRDatabaseContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(@"Data Source=MOHAMEDSHERIF;initial catalog = EmployeeDb ; Integrated Security=SSPI;");
        //}
    }
}

