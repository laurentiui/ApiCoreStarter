﻿// <auto-generated />
using System;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations.Postgres.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210308131426_addWeathe2r")]
    partial class addWeathe2r
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Data.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsAllowed")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAllowed = true,
                            Password = "21232F297A57A5A743894A0E4A801FC3",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            IsAllowed = false,
                            Password = "1",
                            Username = "brown-candies"
                        });
                });

            modelBuilder.Entity("Data.Domain.Entity.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Day")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TemperatureCelsius")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
