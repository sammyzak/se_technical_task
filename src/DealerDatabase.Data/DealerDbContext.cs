using DealerDatabase.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DealerDatabase.Data;

public class DealerDbContext(DbContextOptions<DealerDbContext> options) : DbContext(options)
{
    public DbSet<Dealer> Dealers => Set<Dealer>();
    public DbSet<DealerSourceRecord> DealerSourceRecords => Set<DealerSourceRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dealer>(entity =>
        {
            entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasMany(d => d.SourceRecords)
                .WithOne(s => s.Dealer)
                .HasForeignKey(s => s.DealerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DealerSourceRecord>(entity =>
        {
            entity.ToTable("DealerSourceRecord");

            entity.HasIndex(x => new
            {
                x.Source,
                x.SourceKey
            })
            .IsUnique();
        });
    }
}