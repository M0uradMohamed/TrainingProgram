using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Foreign> Foreignes { get; set; }
        public DbSet<CourseInstructor> coursesInstructors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
            protected override void OnModelCreating(ModelBuilder builder)
            {
            base.OnModelCreating(builder);

            builder.Entity<Instructor>().HasKey(i => new { i.Id, i.Status });

            builder.Entity<Trainee>().HasKey(t => new { t.CourseId, t.EmployeeId });

            builder.Entity<Course>().HasMany(c => c.Instructors).WithMany().UsingEntity<CourseInstructor>();
            
            builder.Entity<Employee>().HasMany(e=>e.Courses).WithMany().UsingEntity<Trainee>();

            builder.Entity<Instructor>().HasOne(i => i.Employee).WithOne()
                .HasForeignKey<Instructor>(i=> i.Id).HasPrincipalKey<Employee>(e=>e.Id).IsRequired(false);

            builder.Entity<Instructor>().HasOne(i => i.Foreign).WithOne()
                .HasForeignKey<Instructor>(i =>  i.Id).HasPrincipalKey<Foreign>(f => f.Id).IsRequired(false);

            builder.Entity<Course>().HasOne(c => c.PrimaryInstructor).WithMany().HasForeignKey(c=>c.PrimaryInstructorId)
                .HasPrincipalKey(i=>i.Id).IsRequired(false);

        }
    }
}
