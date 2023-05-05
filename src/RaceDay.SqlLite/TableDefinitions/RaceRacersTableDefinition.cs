using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Enums;

namespace RaceDay.SqlLite.TableDefinitions;

internal class RaceRacersTableDefinition : TableDefinitionBase<RaceRacerEntity>
{
    #region Constructors

    /// <inheritdoc />
    public RaceRacersTableDefinition(string dataFilesRoot, string dataFileName = "RaceRacers.json", TableName tableName = TableName.RaceRacers) : base(tableName, dataFilesRoot, dataFileName)
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