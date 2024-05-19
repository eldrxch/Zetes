using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Zetes.Data;

public class ZetesDBContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Project> Projects { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }
    
    public readonly string DBPath = "default.db";
    
    public ZetesDBContext(){        
    }

    public ZetesDBContext(string sqlLiteDBPath)
    {
        DBPath = sqlLiteDBPath ?? DBPath;
    }

    public void Migrate()
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DBPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>().ToTable("Users");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("Claims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("Tokens");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("Logins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");        

        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerId);

        modelBuilder.Entity<Customer>()
        .HasIndex(c => c.Email);        

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Projects)
            .WithOne(p => p.Customer)
            .HasForeignKey(p => p.CustomerId);
        modelBuilder.Entity<Project>()
            .HasKey(p => p.ProjectId);
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CustomerId);
    }
}
