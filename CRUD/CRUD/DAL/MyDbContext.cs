using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DAL
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<tbl_user> tbl_user { get; set; } = null!;
    }
}
