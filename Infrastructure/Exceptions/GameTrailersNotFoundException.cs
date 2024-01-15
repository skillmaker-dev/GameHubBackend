using System.Runtime.Serialization;

namespace Infrastructure.Exceptions
{
    public class GameTrailersNotFoundException(string message) : Exception(message)
    {

    }
}