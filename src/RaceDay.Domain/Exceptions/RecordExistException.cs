namespace RaceDay.Domain.Exceptions;

/// <summary>
///     Exception thrown when a record already exists.
/// </summary>
public class RecordExistException : Exception
{
    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecordExistException" /> class.
    /// </summary>
    public RecordExistException() { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecordExistException" /> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public RecordExistException(string message) : base(message) { }

    #endregion
}