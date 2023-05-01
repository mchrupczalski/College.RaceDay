using System.Data;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.Infrastructure;

public abstract class CommandQueryBase
{
    #region Properties

    public InMemoryDatabase Database { get; set; }

    #endregion

    #region Constructors

    protected CommandQueryBase(InMemoryDatabase database) => Database = database;

    #endregion

    protected DataTable GetTable(TableName tableName) => Database.DataSet.Tables[tableName.ToString()] ?? throw new ArgumentNullException(nameof(tableName), "Table not found.");
}