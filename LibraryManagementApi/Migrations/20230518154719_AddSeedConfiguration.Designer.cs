﻿// <auto-generated />
using System;
using LibraryManagementApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibraryManagementApi.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20230518154719_AddSeedConfiguration")]
    partial class AddSeedConfiguration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagementApi.Data.Models.BookRental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("ReturnedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("LibraryManagementApi.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jane Austen"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Harper Lee"
                        },
                        new
                        {
                            Id = 3,
                            Name = "George Orwell"
                        },
                        new
                        {
                            Id = 4,
                            Name = "F. Scott Fitzgerald"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Herman Melville"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Charlotte Brontë"
                        },
                        new
                        {
                            Id = 7,
                            Name = "J.D. Salinger"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Virginia Woolf"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Aldous Huxley"
                        },
                        new
                        {
                            Id = 10,
                            Name = "J.R.R. Tolkien"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Jane Austen"
                        },
                        new
                        {
                            Id = 12,
                            Name = "J.K. Rowling"
                        },
                        new
                        {
                            Id = 13,
                            Name = "J.R.R. Tolkien"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Lewis Carroll"
                        },
                        new
                        {
                            Id = 15,
                            Name = "C.S. Lewis"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Miguel de Cervantes"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Fyodor Dostoevsky"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Homer"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Mary Shelley"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Emily Brontë"
                        });
                });

            modelBuilder.Entity("LibraryManagementApi.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<int>("Stock")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Title");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Stock = 4,
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Stock = 8,
                            Title = "To Kill a Mockingbird"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 3,
                            Stock = 6,
                            Title = "1984"
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 4,
                            Stock = 2,
                            Title = "The Great Gatsby"
                        },
                        new
                        {
                            Id = 5,
                            AuthorId = 5,
                            Stock = 10,
                            Title = "Moby-Dick"
                        },
                        new
                        {
                            Id = 6,
                            AuthorId = 6,
                            Stock = 3,
                            Title = "Jane Eyre"
                        },
                        new
                        {
                            Id = 7,
                            AuthorId = 7,
                            Stock = 5,
                            Title = "The Catcher in the Rye"
                        },
                        new
                        {
                            Id = 8,
                            AuthorId = 8,
                            Stock = 7,
                            Title = "To the Lighthouse"
                        },
                        new
                        {
                            Id = 9,
                            AuthorId = 9,
                            Stock = 9,
                            Title = "Brave New World"
                        },
                        new
                        {
                            Id = 10,
                            AuthorId = 10,
                            Stock = 12,
                            Title = "The Lord of the Rings"
                        },
                        new
                        {
                            Id = 11,
                            AuthorId = 11,
                            Stock = 4,
                            Title = "Pride and Prejudice"
                        },
                        new
                        {
                            Id = 12,
                            AuthorId = 12,
                            Stock = 15,
                            Title = "Harry Potter and the Sorcerer's Stone"
                        },
                        new
                        {
                            Id = 13,
                            AuthorId = 13,
                            Stock = 11,
                            Title = "The Hobbit"
                        },
                        new
                        {
                            Id = 14,
                            AuthorId = 14,
                            Stock = 6,
                            Title = "Alice's Adventures in Wonderland"
                        },
                        new
                        {
                            Id = 15,
                            AuthorId = 15,
                            Stock = 8,
                            Title = "The Chronicles of Narnia"
                        },
                        new
                        {
                            Id = 16,
                            AuthorId = 16,
                            Stock = 4,
                            Title = "Don Quixote"
                        },
                        new
                        {
                            Id = 17,
                            AuthorId = 17,
                            Stock = 7,
                            Title = "Crime and Punishment"
                        },
                        new
                        {
                            Id = 18,
                            AuthorId = 18,
                            Stock = 5,
                            Title = "The Odyssey"
                        },
                        new
                        {
                            Id = 19,
                            AuthorId = 19,
                            Stock = 3,
                            Title = "Frankenstein"
                        },
                        new
                        {
                            Id = 20,
                            AuthorId = 20,
                            Stock = 6,
                            Title = "Wuthering Heights"
                        });
                });

            modelBuilder.Entity("LibraryManagementApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("BookLendsFlag")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasAlternateKey("Username");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookLendsFlag = false,
                            Password = "1a1dc91c907325c69271ddf0c944bc72",
                            Role = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            BookLendsFlag = false,
                            Password = "253d405ed295f2c55d6f3ddf455f31e4",
                            Role = "User",
                            Username = "John.Snow"
                        });
                });

            modelBuilder.Entity("LibraryManagementApi.Models.Book", b =>
                {
                    b.HasOne("LibraryManagementApi.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("LibraryManagementApi.Models.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}