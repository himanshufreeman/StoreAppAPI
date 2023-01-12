using Microsoft.EntityFrameworkCore;
using StoreAppAPI.Model;

namespace StoreAppAPI.DataSet
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ProductModel> Product_Details { get; set; }
        public DbSet<CustomerModel> Customer_Details { get; set; }
        public DbSet<BillModel> Bill_Details { get; set;}
        public DbSet<LoginModel> Login_Details { get; set; }
        public DbSet<EmployeeModel> Employee_Details { get; set; }
    }
}
