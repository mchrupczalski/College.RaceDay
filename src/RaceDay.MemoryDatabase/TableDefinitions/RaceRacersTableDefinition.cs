using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.TableDefinitions;

internal class RaceRacersTableDefinition : TableDefinitionBase<RaceRacerEntity>
{
    #region Constructors

    /// <inheritdoc />
    public RaceRacersTableDefinition(TableName tableName = TableName.RaceRacers) : base(tableName)
    {
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override DataColumn[] GetPrimaryKeyColumns()
    {
        var raceDayIdColumn = Table.Columns[nameof(RaceRacerEntity.RaceDayId)] ?? throw new InvalidOperationException("Column not found.");
        var raceNumberColumn = Table.Columns[nameof(RaceRacerEntity.RaceNumber)] ?? throw new InvalidOperationException("Column not found.");
        var racerIdColumn = Table.Columns[nameof(RaceRacerEntity.RacerId)] ?? throw new InvalidOperationException("Column not found.");

        return new[] { raceDayIdColumn, raceNumberColumn, racerIdColumn };
    }

    #endregion
}