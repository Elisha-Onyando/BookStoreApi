using System.Reflection;
using BookStoreApi.Contexts;
using BookStoreApi.Exceptions;
using BookStoreApi.Interfaces;
using BookStoreApi.Services;
using DotNetEnv;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Extensions
{
  public static class ServiceExtensions
  {
    // Extension method that extends IHostApplicationBuider interface
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
      if (builder == null) throw new ArgumentNullException(nameof(builder));
      if (builder.Configuration == null) throw new ArgumentNullException(nameof(builder.Configuration));

      // Adding the database context to the dependency injection container
      builder.Services.AddDbContext<ApplicationContext>(configure =>
      {
        // Configure the db context to use SQL Server as the database provider
        configure.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseString"));
      });

      // Register BookService in the DI container
      builder.Services.AddScoped<IBookService, BookService>();

      // Register GlobalExceptionHandler class in the DI container
      builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

      // Registers the ProblemDetails class in the DI container
      builder.Services.AddProblemDetails();

      // Adding validators from the current assembly
      builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}