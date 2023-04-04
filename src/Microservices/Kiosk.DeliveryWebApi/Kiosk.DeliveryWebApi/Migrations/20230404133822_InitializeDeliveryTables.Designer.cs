﻿// <auto-generated />
using Kiosk.DeliveryWebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kiosk.DeliveryWebApi.Migrations
{
    [DbContext(typeof(DeliveryDbContext))]
    [Migration("20230404133822_InitializeDeliveryTables")]
    partial class InitializeDeliveryTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kiosk.DeliveryWebApi.Models.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Issue_Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IssueId"));

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer")
                        .HasColumnName("Issue_Status");

                    b.Property<double>("Payment")
                        .HasColumnType("double precision")
                        .HasColumnName("Issue_Payment");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("Issue_TotalPrice");

                    b.HasKey("IssueId");

                    b.ToTable("Issues");
                });
#pragma warning restore 612, 618
        }
    }
}
