﻿// <auto-generated />
using System;
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
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.MFEUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("settingsKey")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("settingsKey");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.MovieOpinions", b =>
                {
                    b.Property<int>("opinionKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("MFEUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("liked")
                        .HasColumnType("bit");

                    b.Property<string>("movieTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("opinionKey");

                    b.HasIndex("MFEUserId");

                    b.ToTable("Opinions");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.NegativeKeys", b =>
                {
                    b.Property<int>("negativeKeyKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("MFEUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("keyword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("negativeKeyKey");

                    b.HasIndex("MFEUserId");

                    b.ToTable("NegativeKeys");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.PositiveKeys", b =>
                {
                    b.Property<int>("positiveKeyKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("MFEUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("keyword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("positiveKeyKey");

                    b.HasIndex("MFEUserId");

                    b.ToTable("PositiveKeys");
                });

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

            modelBuilder.Entity("MoviesForEveryone.Models.UserSettings", b =>
                {
                    b.Property<int>("settingsKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("radiusSetting")
                        .HasColumnType("int");

                    b.Property<string>("setCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("settingsKey");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.MFEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.MFEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesForEveryone.Models.MFEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.MFEUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesForEveryone.Models.MFEUser", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.UserSettings", "settings")
                        .WithMany()
                        .HasForeignKey("settingsKey");

                    b.Navigation("settings");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.MovieOpinions", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.MFEUser", null)
                        .WithMany("opinions")
                        .HasForeignKey("MFEUserId");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.NegativeKeys", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.MFEUser", null)
                        .WithMany("negativeKeys")
                        .HasForeignKey("MFEUserId");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.PositiveKeys", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.MFEUser", null)
                        .WithMany("positiveKeys")
                        .HasForeignKey("MFEUserId");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.Review", b =>
                {
                    b.HasOne("MoviesForEveryone.Models.Theater", null)
                        .WithMany("textReviews")
                        .HasForeignKey("TheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MoviesForEveryone.Models.MFEUser", b =>
                {
                    b.Navigation("negativeKeys");

                    b.Navigation("opinions");

                    b.Navigation("positiveKeys");
                });

            modelBuilder.Entity("MoviesForEveryone.Models.Theater", b =>
                {
                    b.Navigation("textReviews");
                });
#pragma warning restore 612, 618
        }
    }
}
