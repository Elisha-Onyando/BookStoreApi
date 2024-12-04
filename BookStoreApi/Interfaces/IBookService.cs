using BookStoreApi.Contracts;

namespace BookStoreApi.Interfaces
{
  public interface IBookService
  {
    Task<BookResponse> AddBookAsync(CreateBookRequest createBookRequest);
    Task<BookResponse> GetBookByIdAsync(Guid id);
    Task<IEnumerable<BookResponse>> GetAllBooksAsync();
    Task<BookResponse> UpdateBookAsync(Guid id, UpdateBookRequest updateBookRequest);
    Task<bool> DeleteBookAsync(Guid id);
  }
}