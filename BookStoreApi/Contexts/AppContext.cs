using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Contexts
{
  public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
  {
    // Default schema for the database context
    private const string DefaultSchema = "bookapi";

    // DbSet to represent the collection of books (books table) in our database
    public DbSet<BookModel> Books { get; set; }

    // Constructor to configure the database context
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.HasDefaultSchema(DefaultSchema);

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
      // modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
  }
}