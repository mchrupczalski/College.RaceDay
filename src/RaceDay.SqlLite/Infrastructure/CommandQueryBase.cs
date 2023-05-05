using SQLite;

namespace RaceDay.SqlLite.Infrastructure;

/// <summary>
///     The base class for all command and query classes.
/// </summary>
public abstract class CommandQueryBase
{
    #region Properties

    /// <summary>
    ///     The connection string to the database.
    /// </summary>
    protected SQLiteConnectionString ConnectionString { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Creates a new instance of the <see cref="CommandQueryBase" /> class.
    /// </summary>
    /// <param name="dbPath">The connection string to the database.</param>
    protected CommandQueryBase(string dbPath) => ConnectionString = new SQLiteConnectionString(dbPath);

    #endregion

    /// <summary>
    ///     Creates a new <see cref="SQLiteConnection" /> instance.
    /// </summary>
    protected SQLiteConnection CreateConnection() => new(ConnectionString);
}