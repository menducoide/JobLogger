using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JobLoggerCORE.Data
{
    public partial class JobLoggerContext : DbContext
    {
        public JobLoggerContext()
        {
        }

        public JobLoggerContext(DbContextOptions<JobLoggerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogConfiguration> LogConfiguration { get; set; }
        public virtual DbSet<LogMessage> LogMessage { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder().Build();
                //string connString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(config, "JobLoggerEntities");


                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
               optionsBuilder.UseSqlServer("Server=matias-pc\\sqlexpress;Database=JobLogger;Trusted_Connection=True;user id=user;password=123456;");
               // optionsBuilder.UseSqlServer(connString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<LogMessage>(entity =>
            {
                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.LogMessage)
                    .HasForeignKey(d => d.IdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LogMessage_Type");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
