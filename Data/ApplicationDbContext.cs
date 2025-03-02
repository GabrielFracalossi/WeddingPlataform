using Microsoft.EntityFrameworkCore;
using WeddingPlatform.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<WeddingSite> WeddingSites { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<GiftRegistry> GiftRegistry { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeddingSite>()
            .HasOne(w => w.User)
            .WithMany(u => u.WeddingSites)
            .HasForeignKey(w => w.UserId);

        modelBuilder.Entity<WeddingSite>()
            .HasOne(w => w.Template)
            .WithMany(t => t.WeddingSites)
            .HasForeignKey(w => w.TemplateId);

        modelBuilder.Entity<GiftRegistry>()
            .HasOne(g => g.WeddingSite)
            .WithMany(w => w.GiftRegistry)
            .HasForeignKey(g => g.WeddingSiteId);
    }
}