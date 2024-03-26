using Microsoft.EntityFrameworkCore;
using SB_Models.Models;

namespace SB_DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        DbSet<User> Users { get; set; }
    }
}
