﻿// <auto-generated />
using BFR.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BFR.Infrastructure.Database.Migrations
{
    [DbContext(typeof(BFRContext))]
    partial class BFRContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BFR.Core.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("AuthenticationSecret")
                        .HasColumnType("bytea");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BFR.Core.Entities.AccountDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<int>("CurrentStamina")
                        .HasColumnType("integer");

                    b.Property<int>("Diamonds")
                        .HasColumnType("integer");

                    b.Property<long>("Gold")
                        .HasColumnType("bigint");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<int>("MaxStamina")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("AccountDetails");
                });

            modelBuilder.Entity("BFR.Core.Entities.AccountDetails", b =>
                {
                    b.HasOne("BFR.Core.Entities.Account", "Account")
                        .WithOne("AccoundDetails")
                        .HasForeignKey("BFR.Core.Entities.AccountDetails", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BFR.Core.Entities.Account", b =>
                {
                    b.Navigation("AccoundDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
