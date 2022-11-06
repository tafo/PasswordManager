using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Web.Domain;
using PasswordManager.Web.Infrastructure.Security;

namespace PasswordManager.Web.Data;

public class PasswordManagerContext : DbContext
{
    private readonly IPasswordHasher _passwordHasher;
    public PasswordManagerContext(DbContextOptions options, [FromServices] IPasswordHasher passwordHasher) : base(options)
    {
        _passwordHasher = passwordHasher;
    }
    
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<PasswordEntity> Passwords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>(entityModelBuilder =>
        {
            entityModelBuilder.ToTable("Account");
            entityModelBuilder.HasKey(e => e.Id);
            entityModelBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
            entityModelBuilder.HasIndex(e => e.Email).IsUnique();
            entityModelBuilder.Property(e => e.Email).IsRequired().HasMaxLength(1024);

            entityModelBuilder.HasData(new AccountEntity
            {
                Id = 1,
                Email = "admin@domain.com",
                PasswordHash = _passwordHasher.HashPassword("8MggZmuNbF")
            });

        });
        
        modelBuilder.Entity<CategoryEntity>(entityModelBuilder =>
        {
            entityModelBuilder.ToTable("Category");
            entityModelBuilder.HasKey(e => e.Id);
            entityModelBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
            entityModelBuilder.Property(e => e.Name).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<PasswordEntity>(entityModelBuilder =>
        {
            
        });
    }
}