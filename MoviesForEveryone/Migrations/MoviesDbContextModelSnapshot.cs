﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesForEveryone.Models;

namespace MoviesForEveryone.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    partial class MoviesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("MoviesForEveryone.Models.Review", b =>
                {
                    b.Property<int>("reviewKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("TheaterId")
                        .HasColumnType("int");

                    b.Property<double>("arcadeRating")
                        .HasColumnType("float");

                    b.Property<string>("arcadeReview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("cleanlinessRating")
                        .HasColumnType("float");

                    b.Property<string>("cleanlinessReview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("concessionsRating")
                        .HasColumnType("float");

                    b.Property<string>("concessionsReview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("experienceRating")
                        .HasColumnType("float");

                    b.Property<string>("experienceReview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("helpfulRatingPercent")
                        .HasColumnType("float");

                    b.Property<int>("numberHelpfulVotes")
                        .HasColumnType("int");

                    b.Property<double>("reviewAvgScore")
                        .HasColumnType("float");

                    b.Property<int>("totalHelpRates")
                        .HasColumnType("int");

                    b.HasKey("reviewKey");

                    b.HasIndex("TheaterId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.Theater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("avgArcade")
                        .HasColumnType("float");

                    b.Property<double>("avgClean")
                        .HasColumnType("float");

                    b.Property<double>("avgConc")
                        .HasColumnType("float");

                    b.Property<double>("avgViewing")
                        .HasColumnType("float");

                    b.Property<string>("theaterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("totalAvg")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Theaters");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.Review", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.Theater", null)
                        .WithMany("textReviews")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesForEveryone.Models.Theater", b =>
                {
                    b.Navigation("textReviews");
                });
#pragma warning restore 612, 618
        }
    }
}
