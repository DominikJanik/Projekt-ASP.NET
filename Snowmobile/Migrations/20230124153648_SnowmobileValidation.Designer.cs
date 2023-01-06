﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SnowmobileShop.Data;

#nullable disable

namespace SnowmobileShop.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230124153648_SnowmobileValidation")]
    partial class SnowmobileValidation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("SnowmobileShop.Models.Snowmobile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("EngineCapacity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Horsepower")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SnowmobileTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SnowmobileTypeId");

                    b.ToTable("Snowmobiles");
                });

            modelBuilder.Entity("SnowmobileShop.Models.SnowmobileType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SnowmobileTypes");
                });

            modelBuilder.Entity("SnowmobileShop.Models.Snowmobile", b =>
                {
                    b.HasOne("SnowmobileShop.Models.SnowmobileType", "SnowmobileType")
                        .WithMany()
                        .HasForeignKey("SnowmobileTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SnowmobileType");
                });
#pragma warning restore 612, 618
        }
    }
}