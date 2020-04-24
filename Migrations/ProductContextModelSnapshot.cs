﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WishList;

namespace WishList.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("WishList.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("price")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("productMetaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("timeRetreived")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("productMetaId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("WishList.ProductMeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameHtmlId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PriceHtmlId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("VanityName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProductMeta");
                });

            modelBuilder.Entity("WishList.Product", b =>
                {
                    b.HasOne("WishList.ProductMeta", "productMeta")
                        .WithMany("products")
                        .HasForeignKey("productMetaId");
                });
#pragma warning restore 612, 618
        }
    }
}
