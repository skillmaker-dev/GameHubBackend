using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    public class GenresNotFoundException(string message) : Exception(message)
    {
    }
}