namespace Infrastructure.Exceptions
{
    public class GameNotFoundException(string message) : Exception(message)
    {
    }
}
