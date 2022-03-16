﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Database.DAL;

namespace Project.Database.Migrations
{
    [DbContext(typeof(ExamProjectContext))]
    [Migration("20220315195909_55555")]
    partial class _55555
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Project.Database.Entity.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("CategoryType")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Project.Database.Entity.Exam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExamDuration")
                        .HasColumnType("int");

                    b.Property<string>("ExamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExamText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SuccessScore")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Project.Database.Entity.ExamQuestion", b =>
                {
                    b.Property<int>("ExamID")
                        .HasColumnType("int");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.HasKey("ExamID", "QuestionID");

                    b.HasIndex("QuestionID");

                    b.ToTable("ExamQuestions");
                });

            modelBuilder.Entity("Project.Database.Entity.Option", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsTrue")
                        .HasColumnType("bit");

                    b.Property<string>("OptionText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("QuestionID");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("Project.Database.Entity.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("QuestionText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("QuestionType")
                        .HasColumnType("tinyint");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Project.Database.Entity.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Mail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project.Database.Entity.UserExam", b =>
                {
                    b.Property<int>("ExamID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TakingExamDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserScore")
                        .HasColumnType("int");

                    b.HasKey("ExamID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("UserExams");
                });

            modelBuilder.Entity("Project.Database.Entity.Exam", b =>
                {
                    b.HasOne("Project.Database.Entity.Category", "Category")
                        .WithMany("Exams")
                        .HasForeignKey("CategoryID");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Project.Database.Entity.ExamQuestion", b =>
                {
                    b.HasOne("Project.Database.Entity.Exam", "Exam")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("ExamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Database.Entity.Question", "Question")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Project.Database.Entity.Option", b =>
                {
                    b.HasOne("Project.Database.Entity.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Project.Database.Entity.Question", b =>
                {
                    b.HasOne("Project.Database.Entity.Category", "Category")
                        .WithMany("Questions")
                        .HasForeignKey("CategoryID");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Project.Database.Entity.UserExam", b =>
                {
                    b.HasOne("Project.Database.Entity.Exam", "Exam")
                        .WithMany("UserExams")
                        .HasForeignKey("ExamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Database.Entity.User", "User")
                        .WithMany("UserExams")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project.Database.Entity.Category", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Project.Database.Entity.Exam", b =>
                {
                    b.Navigation("ExamQuestions");

                    b.Navigation("UserExams");
                });

            modelBuilder.Entity("Project.Database.Entity.Question", b =>
                {
                    b.Navigation("ExamQuestions");

                    b.Navigation("Options");
                });

            modelBuilder.Entity("Project.Database.Entity.User", b =>
                {
                    b.Navigation("UserExams");
                });
#pragma warning restore 612, 618
        }
    }
}
