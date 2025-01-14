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
        public DbSet<CourseInstructor> CoursesInstructors { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<CourseNature> CourseNatures { get; set; }
        public DbSet<TrainingSpecialist> TrainingSpecialists { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
            protected override void OnModelCreating(ModelBuilder builder)
            {
            base.OnModelCreating(builder);


            builder.Entity<Trainee>().HasKey(t => new {  t.EmployeeId , t.CourseId });

            builder.Entity<CourseInstructor>().HasKey(j => new { j.CourseId, j.InstructorId });

            builder.Entity<CourseInstructor>()
            .HasOne(ci => ci.Course)
            .WithMany(c => c.CoursesInstructors)
            .HasForeignKey(ci => ci.CourseId).HasPrincipalKey(c => c.Id).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<CourseInstructor>()
            .HasOne(ci => ci.Instructor)
            .WithMany(i => i.CoursesInstructors)
            .HasForeignKey(ci => ci.InstructorId).HasPrincipalKey(i => i.Id).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Course>().HasMany(c => c.Instructors).WithMany(i => i.Courses).UsingEntity<CourseInstructor>();

            builder.Entity<Employee>().HasMany(e => e.Courses).WithMany(c => c.Employees).UsingEntity<Trainee>();

            builder.Entity<Trainee>()
                .HasOne(ci => ci.Employee)
                .WithMany(e => e.Trainees)
                .HasForeignKey(ci => ci.EmployeeId).HasPrincipalKey(e => e.Id).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Trainee>()
                .HasOne(ci => ci.Course)
                .WithMany(c => c.Trainees)
                .HasForeignKey(ci => ci.CourseId).HasPrincipalKey(c => c.Id).OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Instructor>().HasOne(i => i.Employee).WithOne(e=>e.instructor)
                .HasForeignKey<Instructor>(i => i.EmployeeId).HasPrincipalKey<Employee>(e => e.Id).IsRequired(false);

            builder.Entity<Course>().HasOne(c => c.PrimaryInstructor).WithMany(i => i.PrimaryCourses).HasForeignKey(c => c.PrimaryInstructorId)
                .HasPrincipalKey(i => i.Id).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
