using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.TableDefinitions;

internal class LapsTableDefinition : TableDefinitionBase<LapEntity>
{
    #region Constructors

    /// <inheritdoc />
    public LapsTableDefinition(TableName tableName = TableName.Laps) : base(tableName)
    {
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override DataColumn[] GetPrimaryKeyColumns()
    {
        var idColumn = Table.Columns[nameof(LapEntity.RaceDayId)] ?? throw new InvalidOperationException("Column not found.");
        idColumn.AutoIncrement = true;
        idColumn.AutoIncrementSeed = 1;
        idColumn.AutoIncrementStep = 1;

        return new[] { idColumn };
    }

    #endregion
}