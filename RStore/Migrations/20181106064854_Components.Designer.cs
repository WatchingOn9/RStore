﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RStore.Models;

namespace RStore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20181106064854_Components")]
    partial class Components
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RStore.Models.CartLine", b =>
                {
                    b.Property<int>("CartLineID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderID");

                    b.Property<int?>("ProductID");

                    b.Property<int>("Quantity");

                    b.HasKey("CartLineID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartLine");
                });

            modelBuilder.Entity("RStore.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<bool>("Disable");

                    b.Property<bool>("IsMainCategory");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("Order");

                    b.Property<int?>("ParentCategoryCategoryID");

                    b.Property<int?>("ParentID");

                    b.HasKey("CategoryID");

                    b.HasIndex("ParentCategoryCategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RStore.Models.Component", b =>
                {
                    b.Property<int>("ComponentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Disable");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.Property<string>("Type");

                    b.HasKey("ComponentID");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("RStore.Models.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ImageName");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int?>("ProductID");

                    b.HasKey("ImageID");

                    b.HasIndex("ProductID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("RStore.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Cancelled");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("ContactPerson")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Currency");

                    b.Property<string>("CustomerRemark");

                    b.Property<decimal>("Discount");

                    b.Property<string>("DiscountDetail");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Line1")
                        .IsRequired();

                    b.Property<string>("Line2");

                    b.Property<string>("Line3");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<decimal>("OtherCharge");

                    b.Property<string>("OtherDetail");

                    b.Property<string>("PhoneNo")
                        .IsRequired();

                    b.Property<bool>("Sent");

                    b.Property<decimal>("ShippingCharge");

                    b.Property<string>("ShippingDetail");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<decimal>("Tax");

                    b.Property<string>("TaxDetail");

                    b.Property<string>("Zip");

                    b.HasKey("OrderID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RStore.Models.Page", b =>
                {
                    b.Property<int>("PageID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("Disable");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("Order");

                    b.Property<string>("PageName");

                    b.Property<int?>("ParentID");

                    b.Property<int?>("ParentPagePageID");

                    b.Property<bool>("ShowProduct");

                    b.Property<string>("Title");

                    b.HasKey("PageID");

                    b.HasIndex("ParentPagePageID");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("RStore.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode");

                    b.Property<int?>("CategoryID");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Currency")
                        .IsRequired();

                    b.Property<string>("Description");

                    b.Property<bool>("Disable");

                    b.Property<string>("ItemCode")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<string>("ThumbImage");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RStore.Models.Setting", b =>
                {
                    b.Property<int>("SettingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<string>("Address3");

                    b.Property<string>("AdminLogo");

                    b.Property<bool>("Closed");

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<bool>("EnableSsl");

                    b.Property<string>("FacebookPageID");

                    b.Property<string>("FacebookPageUrl");

                    b.Property<string>("FaxNo");

                    b.Property<string>("FromEmail");

                    b.Property<string>("Host");

                    b.Property<string>("Logo");

                    b.Property<string>("MetaDescription");

                    b.Property<string>("MetaKeywords");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNo");

                    b.Property<int>("Port");

                    b.Property<string>("RegNo");

                    b.Property<string>("SiteTitle");

                    b.HasKey("SettingID");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("RStore.Models.SlideShow", b =>
                {
                    b.Property<int>("SlideShowID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<bool>("Disable");

                    b.Property<string>("Image");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("Order");

                    b.HasKey("SlideShowID");

                    b.ToTable("SlideShows");
                });

            modelBuilder.Entity("RStore.Models.CartLine", b =>
                {
                    b.HasOne("RStore.Models.Order")
                        .WithMany("Lines")
                        .HasForeignKey("OrderID");

                    b.HasOne("RStore.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("RStore.Models.Category", b =>
                {
                    b.HasOne("RStore.Models.Category", "ParentCategory")
                        .WithMany("Childs")
                        .HasForeignKey("ParentCategoryCategoryID");
                });

            modelBuilder.Entity("RStore.Models.Image", b =>
                {
                    b.HasOne("RStore.Models.Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("RStore.Models.Page", b =>
                {
                    b.HasOne("RStore.Models.Page", "ParentPage")
                        .WithMany()
                        .HasForeignKey("ParentPagePageID");
                });

            modelBuilder.Entity("RStore.Models.Product", b =>
                {
                    b.HasOne("RStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");
                });
#pragma warning restore 612, 618
        }
    }
}
