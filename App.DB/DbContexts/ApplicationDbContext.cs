using App.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infrastructures.Dbcontexts
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<Staff> Staffs { set; get; }
        public virtual DbSet<Department> Departments { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Staff>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Staff>().Property(e => e.UpdateAt).ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<Department>().Property(e => e.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property(e => e.CreateAt).ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property(e => e.UpdateAt).ValueGeneratedOnAddOrUpdate();
            //modelBuilder.Entity<Identity>().Property(e => e.Id).HasMaxLength(256);
            //modelBuilder.Entity<Staff>().HasOne<Department>().WithMany(e => e.Staffs);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    var defaultConnection = _configuration.GetValue<string>("DefaultConnection");
        //    optionsBuilder.UseMySQL(defaultConnection);
        //    optionsBuilder.UseSnakeCaseNamingConvention();
        //}
    }
}
