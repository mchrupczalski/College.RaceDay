namespace RaceDay.Domain.Exceptions;

/// <summary>
///     Exception thrown when a record cannot be created.
/// </summary>
public class CreateRecordException : Exception
{
    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateRecordException" /> class.
    /// </summary>
    public CreateRecordException() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateRecordException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public CreateRecordException(string message) : base(message) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateRecordException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="exception">The exception that is the cause of the current exception.</param>
    public CreateRecordException(string message, Exception exception) : base(message, exception) { }

    #endregion
}