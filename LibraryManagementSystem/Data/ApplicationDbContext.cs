using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser>? ApplicationUsers {get; set;}
        public DbSet<Category>? Categories {get; set;}
        public DbSet<Book>? Books {get; set;}
        public DbSet<Student>? Students {get; set;}
        public DbSet<BookIssue>? BookIssues {get; set;}
        public DbSet<BookIssueHistory>? BookIssueHistories {get; set;}
    }
}