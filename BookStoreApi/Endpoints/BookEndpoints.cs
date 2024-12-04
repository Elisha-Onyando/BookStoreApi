using BookStoreApi.Contracts;
using BookStoreApi.Interfaces;

namespace BookStoreApi.Endpoints
{
  public static class BookEndPoint
  {
    public static IEndpointRouteBuilder MapBookEndPoint(this IEndpointRouteBuilder app)
    {
      // Define the endpoints
      // Endpoint to add a new book
      app.MapPost("/", async (CreateBookRequest createBookRequest, IBookService bookService) =>
      {
        var result = await bookService.AddBookAsync(createBookRequest);
        return Results.Created($"/api/v1/books/{result.Id}", result);
      });

      // Endpoint to get all books
      app.MapGet("/", async (IBookService bookService) =>
      {
        var result = await bookService.GetAllBooksAsync();
        return result != null ? Results.Ok(result) : Results.NotFound();
      });

      // Endpoint to get a book by ID
      app.MapGet("/{id:guid}", async (Guid id, IBookService bookService) =>
      {
        var result = await bookService.GetBookByIdAsync(id);
        return result != null ? Results.Ok(result) : Results.NotFound();
      });

      // Endpoint to update a book by ID
      app.MapPut("/{id:guid}", async (Guid id, UpdateBookRequest updateBookRequest, IBookService bookService) =>
      {
        var result = await bookService.UpdateBookAsync(id, updateBookRequest);
        return result != null ? Results.Ok(result) : Results.NotFound();
      });

      // Endpoint to delete a book by ID
      app.MapDelete("/{id:guid}", async (Guid id, IBookService bookService) =>
      {
        var result = await bookService.DeleteBookAsync(id);
        return result ? Results.NoContent() : Results.NotFound();
      });

      return app;
    }
  }
}