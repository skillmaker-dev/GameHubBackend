using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    public class PlatformsNotFoundException(string message) : Exception(message)
    {
    }
}