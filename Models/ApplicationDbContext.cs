namespace Marknadsplatsen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<AccountUser> AccountUsers { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<Category> Categories { get; set; }
}
