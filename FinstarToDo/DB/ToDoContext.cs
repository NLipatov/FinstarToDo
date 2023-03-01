using FinstarToDo.DB.DbSettingsConstants;
using FinstarToDo.DB.Models;
using FinstarToDo.DB.Models.Logging;
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
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Commentary> Comments { get; set; }
        public DbSet<Log> NLogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(ConnectionString.POSTGRESQL));
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
