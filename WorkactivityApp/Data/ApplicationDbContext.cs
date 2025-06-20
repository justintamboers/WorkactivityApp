    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using WorkactivityApp.Models;

namespace WorkactivityApp.Data
    {
        public class ApplicationDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Project>()
                    .OwnsOne(p => p.Time, t =>
                    {
                        t.Property(p => p.StartTime).HasColumnName("StartTime");
                        t.Property(p => p.EndTime).HasColumnName("EndTime");
                    });
            }


        public DbSet<WorkactivityApp.Models.User> Users { get; set; }
            public DbSet<WorkactivityApp.Models.Project> Projects { get; set; }
        }
    }