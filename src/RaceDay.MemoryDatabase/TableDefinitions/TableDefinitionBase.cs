using System.Data;
using System.Diagnostics;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.TableDefinitions;

internal abstract class TableDefinitionBase<TEntity>
    where TEntity : class
{
    #region Fields

    private readonly TableName _tableName;

    private DataTable? _table;

    #endregion

    #region Properties

    internal DataTable Table
    {
        get
        {
            if (_table == null) Create();
            Debug.Assert(_table != null, nameof(_table) + " != null");
            return _table;
        }
    }

    #endregion

    #region Constructors

    protected TableDefinitionBase(TableName tableName) => _tableName = tableName;

    #endregion

    #region Abstract Members

    protected abstract DataColumn[] GetPrimaryKeyColumns();

    #endregion

    private void Create()
    {
        _table = new DataTable(_tableName.ToString());
        var properties = typeof(TEntity).GetProperties();
        foreach (var property in properties)
        {
            _table.Columns.Add(CreateColumn(property.Name));
        }

        _table.PrimaryKey = GetPrimaryKeyColumns();
    }

    private static DataColumn CreateColumn(string propertyName, bool allowNull = false)
    {
        var propertyType = typeof(TEntity).GetProperty(propertyName)
                                         ?.GetType() ?? throw new InvalidOperationException("Property not found.");

        var column = new DataColumn(propertyName, propertyType);
        column.AllowDBNull = allowNull;
        return column;
    }
}