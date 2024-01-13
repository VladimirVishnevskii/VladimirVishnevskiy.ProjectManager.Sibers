using Microsoft.EntityFrameworkCore;
using VladimirVishnevskiy.ProjectManager.Data.Entities;

namespace VladimirVishnevskiy.ProjectManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ProjectManager.db");
    
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<ProjectEntity>().HasKey(x => x.Id);
          modelBuilder.Entity<ProjectEntity>().Property(f => f.Id).ValueGeneratedOnAdd();

          modelBuilder.Entity<EmployeeEntity>().HasKey(x => x.Id);
          modelBuilder.Entity<EmployeeEntity>().Property(f => f.Id).ValueGeneratedOnAdd();

          modelBuilder.Entity<ProjectEntity>().HasMany(x => x.Employees).WithMany(x => x.Projects);
          modelBuilder.Entity<EmployeeEntity>().HasMany(x => x.Projects).WithMany(x => x.Employees);

          modelBuilder.Entity<ProjectEntity>().HasOne(x => x.ProjectManager);
        }

    }

}
