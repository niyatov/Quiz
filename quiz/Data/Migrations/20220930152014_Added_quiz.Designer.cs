// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using quiz.Data;

#nullable disable

namespace quiz.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220930152014_Added_quiz")]
    partial class Added_quiz
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("quiz.Entities.Quiz", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("PasswordHash")
                        .IsUnique();

                    b.ToTable("Quizzes");
                });
#pragma warning restore 612, 618
        }
    }
}
