using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.TableDefinitions;

internal class RaceLapsTableDefinition : TableDefinitionBase<RaceLapEntity>
{
    #region Constructors

    /// <inheritdoc />
    public RaceLapsTableDefinition(string dataFilesRoot, string dataFileName = "RaceLaps.json",TableName tableName = TableName.RaceLaps) : base(tableName, dataFilesRoot, dataFileName)
    {
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override DataColumn[] GetPrimaryKeyColumns()
    {
        var raceDayIdColumn = Table.Columns[nameof(RaceLapEntity.RaceDayId)] ?? throw new InvalidOperationException("Column not found.");
        var raceNumberColumn = Table.Columns[nameof(RaceLapEntity.RaceNumber)] ?? throw new InvalidOperationException("Column not found.");
        var racerIdColumn = Table.Columns[nameof(RaceLapEntity.RacerId)] ?? throw new InvalidOperationException("Column not found.");
        var lapNoColumn = Table.Columns[nameof(RaceLapEntity.LapNumber)] ?? throw new InvalidOperationException("Column not found.");

        return new[] { raceDayIdColumn, raceNumberColumn, racerIdColumn, lapNoColumn };
    }

    #endregion
}