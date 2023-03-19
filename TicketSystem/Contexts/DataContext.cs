using Microsoft.EntityFrameworkCore;
using TicketSystem.Models.Entities;

namespace TicketSystem.Contexts;

public class DataContext : DbContext
{
    private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\abdoj\OneDrive\Skrivbord\EC_Utbildning\CS\TicketSystem\TicketSystem\Contexts\sql_db.mdf;Integrated Security=True;Connect Timeout=30";

    #region constructors
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    #endregion

    #region overrides
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CommentEntity>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    #endregion

    public DbSet<AddressEntity> Addresses { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<TicketEntity> Tickets { get; set; } = null!;
    public DbSet<CommentEntity> Comments { get; set; } = null!;
}
