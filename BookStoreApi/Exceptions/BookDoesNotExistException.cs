namespace BookStoreApi.Exceptions
{
  public class BookDoesNotExistException : Exception
  {
    private Guid Id { get; set; }

    public BookDoesNotExistException(Guid id) : base($"Book with id {id} does not exist")
    {
      this.Id = id;
    }

  }
}