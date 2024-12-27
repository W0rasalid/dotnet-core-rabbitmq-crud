using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models.Entities;

namespace SharedLibrary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LogSystemEntity> logSystems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LogSystemEntity>(entity =>
            {
                entity.ToTable("LogSystem");
                // Composite Key
                entity.HasKey(e => new { e.Id });

                entity.Property(e => e.MainMenu).HasColumnType("int");
                entity.Property(e => e.SubMenu).HasColumnType("int");
                entity.Property(e => e.EmpCode).HasColumnType("int");

                entity.Property(e => e.CompanyCode).HasMaxLength(5);
                entity.Property(e => e.DepartmentId).HasColumnType("int");
                entity.Property(e => e.Message).HasColumnType("nvarchar(max)");
                entity.Property(e => e.Operation).HasMaxLength(255);
                entity.Property(e => e.Reference).HasMaxLength(255);
                entity.Property(e => e.IsSuccess).HasColumnType("bit");
                entity.Property(e => e.Request).HasColumnType("nvarchar(max)");
            });

        }
    }
}
