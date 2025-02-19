﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPDojoWeb;

#nullable disable

namespace TPDojoWeb.Migrations
{
    [DbContext(typeof(HelloWorldDbContext))]
    partial class HelloWorldDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtMartialSamourai", b =>
                {
                    b.Property<int>("ArtMartialsId")
                        .HasColumnType("int");

                    b.Property<int>("SamouraisId")
                        .HasColumnType("int");

                    b.HasKey("ArtMartialsId", "SamouraisId");

                    b.HasIndex("SamouraisId");

                    b.ToTable("ArtMartialSamourai");
                });

            modelBuilder.Entity("TPDojoWeb.BO.Arme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Degat")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Armes");
                });

            modelBuilder.Entity("TPDojoWeb.BO.ArtMartial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ArtMartial");
                });

            modelBuilder.Entity("TPDojoWeb.BO.Samourai", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArmeId")
                        .HasColumnType("int");

                    b.Property<int>("Force")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArmeId");

                    b.ToTable("Samourais");
                });

            modelBuilder.Entity("ArtMartialSamourai", b =>
                {
                    b.HasOne("TPDojoWeb.BO.ArtMartial", null)
                        .WithMany()
                        .HasForeignKey("ArtMartialsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TPDojoWeb.BO.Samourai", null)
                        .WithMany()
                        .HasForeignKey("SamouraisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TPDojoWeb.BO.Samourai", b =>
                {
                    b.HasOne("TPDojoWeb.BO.Arme", "Arme")
                        .WithMany()
                        .HasForeignKey("ArmeId");

                    b.Navigation("Arme");
                });
#pragma warning restore 612, 618
        }
    }
}
