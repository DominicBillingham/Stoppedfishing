﻿// <auto-generated />
using System;
using AspNetCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace StoppedFishing.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230801174904_FixTimeblockColumnName")]
    partial class FixTimeblockColumnName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AspNetCore.Data.Models.Meeting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("AspNetCore.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.Property<string>("MeetingsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("MeetingsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("MeetingUser");
                });

            modelBuilder.Entity("StoppedFishing.Data.Models.TimeBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<int>("FinalHour")
                        .HasColumnType("int");

                    b.Property<int>("StartHour")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TimeBlocks");
                });

            modelBuilder.Entity("AspNetCore.Data.Models.User", b =>
                {
                    b.OwnsMany("AspNetCore.Data.Models.SimpleTimeBlock", "SimpleBlocks", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("Day")
                                .HasColumnType("int");

                            b1.Property<int>("SimpleBlock")
                                .HasColumnType("int");

                            b1.Property<int?>("UserId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("Users_SimpleBlocks");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsMany("AspNetCore.Data.Models.SimpleTimeBlock", "TimeBlocks", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("Day")
                                .HasColumnType("int");

                            b1.Property<int>("SimpleBlock")
                                .HasColumnType("int");

                            b1.Property<int>("UserId")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("UserId");

                            b1.ToTable("Users_TimeBlocks");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("SimpleBlocks");

                    b.Navigation("TimeBlocks");
                });

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.HasOne("AspNetCore.Data.Models.Meeting", null)
                        .WithMany()
                        .HasForeignKey("MeetingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspNetCore.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
