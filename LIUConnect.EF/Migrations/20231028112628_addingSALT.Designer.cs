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
    [Migration("20231028112628_addingSALT")]
    partial class addingSALT
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

                    b.Property<int?>("VacancyId")
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
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

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("RecruiterID");

                    b.HasIndex("UserID");

                    b.ToTable("Recruiters");
                });

            modelBuilder.Entity("LIUConnect.Core.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"));

                    b.Property<byte[]>("CVFileContent")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

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

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkingHours")
                        .HasColumnType("int");

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
                        .OnDelete(DeleteBehavior.Restrict)
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

                    b.HasOne("LIUConnect.Core.Models.Vacancy", null)
                        .WithMany("Comments")
                        .HasForeignKey("VacancyId");

                    b.Navigation("User");
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
