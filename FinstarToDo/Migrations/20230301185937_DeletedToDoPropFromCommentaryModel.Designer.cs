﻿// <auto-generated />
using System;
using FinstarToDo.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinstarToDo.Migrations
{
    [DbContext(typeof(ToDoContext))]
    [Migration("20230301185937_DeletedToDoPropFromCommentaryModel")]
    partial class DeletedToDoPropFromCommentaryModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinstarToDo.DB.Models.Commentary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ToDoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ToDoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FinstarToDo.DB.Models.ToDo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<int>("Color")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Category")
                        .IsUnique();

                    b.HasIndex("Header")
                        .IsUnique();

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("FinstarToDo.DB.Models.Commentary", b =>
                {
                    b.HasOne("FinstarToDo.DB.Models.ToDo", null)
                        .WithMany("Commentaries")
                        .HasForeignKey("ToDoId");
                });

            modelBuilder.Entity("FinstarToDo.DB.Models.ToDo", b =>
                {
                    b.Navigation("Commentaries");
                });
#pragma warning restore 612, 618
        }
    }
}
