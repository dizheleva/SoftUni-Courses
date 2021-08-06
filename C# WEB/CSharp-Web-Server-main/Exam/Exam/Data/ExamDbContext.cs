using Exam.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data
{
    public class ExamDbContext : DbContext
    {
        public DbSet<User> Users { get; init; }

        public DbSet<Car> Cars { get; init; }

        //public DbSet<Issue> Issues { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Exam;Integrated Security=True;");
            }
        }
    }
}
