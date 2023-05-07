namespace RaceDay.Domain.Exceptions;

public class CreateRecordException : Exception
{
    #region Constructors

    public CreateRecordException()
    {
    }

    public CreateRecordException(string message) : base(message)
    {
    }

    public CreateRecordException(string message, Exception exception) : base(message, exception)
    {
    }

    #endregion
}