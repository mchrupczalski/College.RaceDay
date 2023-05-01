using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.TableDefinitions;

internal class RacersTableDefinition : TableDefinitionBase<RacerEntity>
{
    #region Constructors

    /// <inheritdoc />
    public RacersTableDefinition(TableName tableName = TableName.Racers) : base(tableName)
    {
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override DataColumn[] GetPrimaryKeyColumns()
    {
        var racerIdColumn = Table.Columns[nameof(RacerEntity.Id)] ?? throw new InvalidOperationException("Column not found.");

        return new[] { racerIdColumn };
    }

    #endregion
}