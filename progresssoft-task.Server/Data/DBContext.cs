using Microsoft.EntityFrameworkCore;
using progresssoft_task.Server.Models;


namespace progresssoft_task.Server.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<BusinessCard> BusinessCards { get; set; }
    }
}
