using LIUConnect.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LIUConnect.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Recommendation>()
        .HasOne(r => r.Student)
        .WithMany(s => s.Recommendations) 
        .HasForeignKey(r => r.StudentID)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Recruiter)
                .WithMany(r => r.Vacancies)
                .HasForeignKey(v => v.RecruiterID)
                .OnDelete(DeleteBehavior.Cascade); // Adjust the cascade behavior as needed

            modelBuilder.Entity<Application>()
            .HasOne(a => a.Vacancy)
            .WithMany(v => v.Applications)
            .HasForeignKey(a => a.VacancyID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
             .HasOne(c => c.Vacancy)
             .WithMany(v => v.Comments)
             .HasForeignKey(c => c.VacancyId)
             .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Referral>()
                .HasOne(r => r.Instructor)
                .WithMany()
                .HasForeignKey(r => r.InstructorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Referral>()
                .HasOne(r => r.Student)
                .WithMany()
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Referral>()
                .HasOne(r => r.Vacancy)
                .WithMany()
                .HasForeignKey(r => r.VacancyId)
                .OnDelete(DeleteBehavior.NoAction);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Details> UserDetails { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Referral> Referral { get; set; }
        public DbSet<LoginHistory> loginIndex { get; set; }
    }
}

