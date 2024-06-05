using Microsoft.EntityFrameworkCore;

namespace SchoolWebApp.Models {
    public class ApplicationDbContext : DbContext {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) {
        }
    }
}
