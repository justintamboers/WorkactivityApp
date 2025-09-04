using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkactivityApp.Models;

namespace WorkactivityApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().ToTable("projects");

            modelBuilder.Entity<Project>()
                .OwnsOne(p => p.Time, t =>
                {
                    t.Property(p => p.StartTime).HasColumnName("starttime");
                    t.Property(p => p.EndTime).HasColumnName("endtime");

                    t.OwnsMany(tt => tt.AddedTimes, at =>
                    {
                        at.WithOwner().HasForeignKey("timeownerid");
                        at.Property<int>("id");
                        at.HasKey("id");

                        at.Property(a => a.StartAddedTime).HasColumnName("startaddedtime");
                        at.Property(a => a.EndAddedTime).HasColumnName("endaddedtime");
                    });
                });
        }
    }
}
