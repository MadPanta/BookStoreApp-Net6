using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreApp.API.Data
{
    public partial class BookStoreDBContext : IdentityDbContext<ApiUser>
    {
        public BookStoreDBContext()
        {
        }

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Bio).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EAE6AF2FCF")
                    .IsUnique();

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Summary).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_ToTable");
            });

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER",
                    Id= "846ef4e5-73a5-471c-bbd1-4105b2286e6f"
                },
               
                new IdentityRole
                {
                    Name="Administrator",
                    NormalizedName="ADMINISTRATOR",
                    Id= "2a1a417b-7950-4b34-ace7-b7895654daa2"
                }
                );

            var hasher = new PasswordHasher<ApiUser>();
            modelBuilder.Entity<ApiUser>().HasData(
               new ApiUser
               {
                   Id = "76d17262-756a-4863-b372-88aecf2fa2fc",
                   Email = "admin@bookstore.com",
                   NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                   UserName = "admin@bookstore.com",
                   NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                   FirstName = "System",
                   LastName = "Admin",
                   PasswordHash = hasher.HashPassword(null, "P@ssword1")

               },
                 new ApiUser
                 {
                     Id = "5b14fa75-2eec-4884-96f0-ea7b09a627a8",
                     Email = "user@bookstore.com",
                     NormalizedEmail = "USER@BOOKSTORE.COM",
                     UserName = "user@bookstore.com",
                     NormalizedUserName = "USER@BOOKSTORE.COM",
                     FirstName = "System",
                     LastName = "User",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1")

                 }



             
               );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string> //Admin Role
            {
                RoleId = "2a1a417b-7950-4b34-ace7-b7895654daa2",
                UserId = "76d17262-756a-4863-b372-88aecf2fa2fc"
                },
                new IdentityUserRole<string> //User Role
                {
                    RoleId = "846ef4e5-73a5-471c-bbd1-4105b2286e6f",
                    UserId = "5b14fa75-2eec-4884-96f0-ea7b09a627a8"
                }
                );


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
