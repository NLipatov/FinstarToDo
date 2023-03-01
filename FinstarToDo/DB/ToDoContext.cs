using FinstarToDo.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace FinstarToDo.DB
{
    public class ToDoContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ToDoContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        DbSet<ToDo> ToDos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasIndex(x => x.Category).IsUnique();
            modelBuilder.Entity<ToDo>().HasIndex(x=>x.Header).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
