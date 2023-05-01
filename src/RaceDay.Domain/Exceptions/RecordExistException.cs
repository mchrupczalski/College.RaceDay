namespace RaceDay.Domain.Exceptions;

public class RecordExistException : Exception
{
    public RecordExistException() : base()
    {
    }
    
    public RecordExistException(string message) : base(message)
    {
    }
}