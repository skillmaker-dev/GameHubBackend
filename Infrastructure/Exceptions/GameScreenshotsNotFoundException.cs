using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    public class GameScreenshotsNotFoundException(string message) : Exception(message)
    { 
    }
}