﻿// <auto-generated />
using Kiosk.CatalogWebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kiosk.CatalogWebApi.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    [Migration("20230402163657_AddCatalogTables")]
    partial class AddCatalogTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kiosk.CatalogWebApi.Models.DbModels.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Brand_Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BrandId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Brand_Name");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("Category_Id");

                    b.HasKey("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Kiosk.CatalogWebApi.Models.DbModels.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Category_Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Category_Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Kiosk.CatalogWebApi.Models.DbModels.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Product_Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("integer")
                        .HasColumnName("Brand_Id");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Product_Code");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Product_Name");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("Product_Price");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Kiosk.CatalogWebApi.Models.DbModels.Brand", b =>
                {
                    b.HasOne("Kiosk.CatalogWebApi.Models.DbModels.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Kiosk.CatalogWebApi.Models.DbModels.Product", b =>
                {
                    b.HasOne("Kiosk.CatalogWebApi.Models.DbModels.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });
#pragma warning restore 612, 618
        }
    }
}
