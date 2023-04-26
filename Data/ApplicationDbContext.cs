using Labb2LINQ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Labb2LINQ.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Class> Class { get; set; } = default!;
        public DbSet<Course> Course { get; set; } = default!;
        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Teacher> Teacher { get; set; } = default!;

    }
}