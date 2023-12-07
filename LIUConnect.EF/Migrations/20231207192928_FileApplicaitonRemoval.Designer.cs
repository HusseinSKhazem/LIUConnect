﻿// <auto-generated />
using System;
using LIUConnect.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LIUConnect.EF.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231207192928_FileApplicaitonRemoval")]
    partial class FileApplicaitonRemoval
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LIUConnect.Core.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("AdminID");

                    b.HasIndex("UserID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Application", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationId"));

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("VacancyID")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApplicationId");

                    b.HasIndex("StudentID");

                    b.HasIndex("VacancyID");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("VacancyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("VacancyId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Details", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Links")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetails");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstructorId"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("InstructorId");

                    b.HasIndex("UserID");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Major", b =>
                {
                    b.Property<int>("MajorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MajorID"));

                    b.Property<string>("MajorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MajorID");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Recommendation", b =>
                {
                    b.Property<int>("RecommnedationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecommnedationId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("RecommnedationId");

                    b.HasIndex("InstructorID");

                    b.HasIndex("StudentID");

                    b.ToTable("Recommendations");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Recruiter", b =>
                {
                    b.Property<int>("RecruiterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecruiterID"));

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<bool?>("isApproved")
                        .HasColumnType("bit");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("officialFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecruiterID");

                    b.HasIndex("UserID");

                    b.ToTable("Recruiters");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Resume", b =>
                {
                    b.Property<int>("ResumeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResumeID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EducationalBackground")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Socials")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<string>("WorkExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("projects")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResumeID");

                    b.HasIndex("StudentID")
                        .IsUnique();

                    b.ToTable("Resume");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"));

                    b.Property<int>("MajorID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("StudentID");

                    b.HasIndex("MajorID");

                    b.HasIndex("UserID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Vacancy", b =>
                {
                    b.Property<int>("VacancyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VacancyId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobOffer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MajorID")
                        .HasColumnType("int");

                    b.Property<int>("RecruiterID")
                        .HasColumnType("int");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Responsibility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkingHours")
                        .HasColumnType("int");

                    b.Property<string>("experience")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("salary")
                        .HasColumnType("int");

                    b.Property<string>("workLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VacancyId");

                    b.HasIndex("MajorID");

                    b.HasIndex("RecruiterID");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Admin", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Application", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LIUConnect.Core.Models.Vacancy", "Vacancy")
                        .WithMany("Applications")
                        .HasForeignKey("VacancyID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Comment", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LIUConnect.Core.Models.Vacancy", "Vacancy")
                        .WithMany("Comments")
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Details", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Instructor", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Recommendation", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.Instructor", "Instructor")
                        .WithMany("Recommendations")
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LIUConnect.Core.Models.Student", "Student")
                        .WithMany("Recommendations")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Recruiter", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Resume", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.Student", "Student")
                        .WithOne("Resume")
                        .HasForeignKey("LIUConnect.Core.Models.Resume", "StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Student", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LIUConnect.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Vacancy", b =>
                {
                    b.HasOne("LIUConnect.Core.Models.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LIUConnect.Core.Models.Recruiter", "Recruiter")
                        .WithMany("Vacancies")
                        .HasForeignKey("RecruiterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("Recruiter");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Instructor", b =>
                {
                    b.Navigation("Recommendations");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Recruiter", b =>
                {
                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Student", b =>
                {
                    b.Navigation("Recommendations");

                    b.Navigation("Resume")
                        .IsRequired();
                });

            modelBuilder.Entity("LIUConnect.Core.Models.User", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Vacancy", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}