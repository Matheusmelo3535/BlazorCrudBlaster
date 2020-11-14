using Mysql_blazor2.Shared;
using Microsoft.EntityFrameworkCore;
namespace Mysql_blazor2.Server
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
        {  
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set;}
    }






}