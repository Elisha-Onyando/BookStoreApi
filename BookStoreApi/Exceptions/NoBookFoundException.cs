namespace BookStoreApi.Exceptions
{
  public class NoBookFoundException : Exception
  {
    public NoBookFoundException() : base("No books found")
    { }
  }
}