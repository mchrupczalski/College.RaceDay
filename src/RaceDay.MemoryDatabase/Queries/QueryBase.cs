using System.Data;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.Queries;

public abstract class QueryBase
{
    #region Properties

    public InMemoryDatabase Database { get; set; }

    #endregion

    #region Constructors

    protected QueryBase(InMemoryDatabase database) => Database = database;

    #endregion

    protected DataTable GetTable(TableName tableName) => Database.DataSet.Tables[tableName.ToString()] ?? throw new ArgumentNullException(nameof(tableName), "Table not found.");
}