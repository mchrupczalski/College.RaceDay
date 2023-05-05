using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Enums;

namespace RaceDay.SqlLite.TableDefinitions;

internal class RaceDaysTableDefinition : TableDefinitionBase<RaceDayEntity>
{
    #region Constructors

    /// <inheritdoc />
    public RaceDaysTableDefinition(string dataFilesRoot, string dataFileName = "RaceDays.json", TableName tableName = TableName.RaceDays) : base(tableName, dataFilesRoot, dataFileName)
    {
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override DataColumn[] GetPrimaryKeyColumns()
    {
        var idColumn = Table.Columns[nameof(RaceDayEntity.Id)] ?? throw new InvalidOperationException("Column not found.");
        idColumn.AutoIncrement = true;
        idColumn.AutoIncrementSeed = 1;
        idColumn.AutoIncrementStep = 1;

        return new[] { idColumn };
    }

    #endregion
}