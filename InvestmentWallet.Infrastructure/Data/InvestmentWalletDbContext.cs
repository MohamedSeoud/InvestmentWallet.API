using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestmentWallet.Infrastructure.Data;

public class InvestmentWalletDbContext : DbContext
{
    public InvestmentWalletDbContext(DbContextOptions<InvestmentWalletDbContext> options)
        : base(options) { }

    public DbSet<Investor> Investors { get; set; }
    public DbSet<Investment> Investments { get; set; }
    public DbSet<InvestmentOpportunity> InvestmentOpportunities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var emailConverter = new ValueConverter<Email, string>(
            email => email.Value,
            value => new Email(value));

        var moneyConverter = new ValueConverter<Money, decimal>(
            money => money.Amount,
            value => new Money(value));
        modelBuilder.Entity<Investor>().OwnsOne(i => i.WalletBalance, wb =>
        {
            wb.Property(p => p.Amount)
              .HasColumnName("WalletBalance")
              .HasColumnType("decimal(18,2)")
              .IsRequired();
        });



        modelBuilder.Entity<Investor>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .HasConversion(emailConverter)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Email");

            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<Investment>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.InvestorId)
                .IsRequired();

            entity.Property(e => e.OpportunityId)
                .IsRequired();

            entity.Property(e => e.OpportunityName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(e => e.InvestmentDate)
                .IsRequired();

            entity.HasOne(i => i.Investor) 
                .WithMany(i => i.Investments)
                .HasForeignKey(i => i.InvestorId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InvestmentOpportunity>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.MinimumInvestment)
                .HasColumnType("decimal(18,2)");
        });

        var opportunities = InvestmentOpportunity.GetDefaultOpportunities();
        modelBuilder.Entity<InvestmentOpportunity>().HasData(opportunities);
    }
}