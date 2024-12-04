using BookStoreApi.Contexts;
using BookStoreApi.Contracts;
using BookStoreApi.Exceptions;
using BookStoreApi.Interfaces;
using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services
{
  public class BookService : IBookService
  {
    private readonly ApplicationContext _context; // Database context
    private readonly ILogger<BookService> _logger; // Logger for logging information and errors

    // Class Constructor
    public BookService(ApplicationContext context, ILogger<BookService> logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task<BookResponse> AddBookAsync(CreateBookRequest createBookRequest)
    {
      try
      {
        var book = new BookModel
        {
          Title = createBookRequest.Title,
          Author = createBookRequest.Author,
          Description = createBookRequest.Description,
          Category = createBookRequest.Category,
          Language = createBookRequest.Language,
          TotalPages = createBookRequest.TotalPages
        };

        // Add the book to the database
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Book added successfully.");

        // Return the details of the created book
        return new BookResponse
        {
          Id = book.Id,
          Title = book.Title,
          Author = book.Author,
          Description = book.Description,
          Category = book.Category,
          Language = book.Language,
          TotalPages = book.TotalPages
        };
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error adding book: {ex.Message}");
        throw;
      }
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
      try
      {
        // Find the book by its ID
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
          _logger.LogWarning($"Book with ID {id} not found.");
          throw new BookDoesNotExistException(id);
        }

        // Remove the book from the database
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Book with ID {id} deleted successfully.");
        return true;
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error deleting book: {ex.Message}");
        throw;
      }
    }

    public async Task<IEnumerable<BookResponse>> GetAllBooksAsync()
    {
      try
      {
        // Get all books from the database
        var books = await _context.Books.ToListAsync();

        if (books == null)
        {
          _logger.LogWarning($"No book found.");
          throw new NoBookFoundException();
        }

        // Return the details of all books
        return books.Select(book => new BookResponse
        {
          Id = book.Id,
          Title = book.Title,
          Author = book.Author,
          Description = book.Description,
          Category = book.Category,
          Language = book.Language,
          TotalPages = book.TotalPages
        });
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error retrieving books: {ex.Message}");
        throw;
      }
    }

    public async Task<BookResponse> GetBookByIdAsync(Guid id)
    {
      try
      {
        // Find the book by its ID
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
          _logger.LogWarning($"Book with ID {id} not found.");
          throw new BookDoesNotExistException(id);
        }

        // Return the details of the book
        return new BookResponse
        {
          Id = book.Id,
          Title = book.Title,
          Author = book.Author,
          Description = book.Description,
          Category = book.Category,
          Language = book.Language,
          TotalPages = book.TotalPages
        };
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error while retrieving book: {ex.Message}");
        throw;
      };
    }

    public async Task<BookResponse> UpdateBookAsync(Guid id, UpdateBookRequest updateBookRequest)
    {
      try
      {
        // Find the existing book by its ID
        var existingBook = await _context.Books.FindAsync(id);
        if (existingBook == null)
        {
          _logger.LogWarning($"Book with ID {id} not found.");
          throw new BookDoesNotExistException(id);
        }

        // Update the book details
        existingBook.Title = updateBookRequest.Title;
        existingBook.Author = updateBookRequest.Author;
        existingBook.Description = updateBookRequest.Description;
        existingBook.Category = updateBookRequest.Category;
        existingBook.Language = updateBookRequest.Language;
        existingBook.TotalPages = updateBookRequest.TotalPages;

        // Save the changes to the database
        await _context.SaveChangesAsync();
        _logger.LogInformation("Book updated successfully.");

        // Return the details of the updated book
        return new BookResponse
        {
          Id = existingBook.Id,
          Title = existingBook.Title,
          Author = existingBook.Author,
          Description = existingBook.Description,
          Category = existingBook.Category,
          Language = existingBook.Language,
          TotalPages = existingBook.TotalPages
        };
      }
      catch (Exception ex)
      {
        _logger.LogError($"Error updating book: {ex.Message}");
        throw;
      }
    }
  }
}