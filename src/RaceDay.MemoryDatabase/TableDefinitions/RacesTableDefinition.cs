using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Enums;

namespace RaceDay.MemoryDatabase.TableDefinitions;

internal class RacesTableDefinition : TableDefinitionBase<RaceEntity>
{
    #region Constructors

    /// <inheritdoc />
    public RacesTableDefinition(string dataFilesRoot, string dataFileName = "Races.json", TableName tableName = TableName.Races) : base(tableName, dataFilesRoot, dataFileName)
    {
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    protected override DataColumn[] GetPrimaryKeyColumns()
    {
        var raceDayIdColumn = Table.Columns[nameof(RaceEntity.RaceDayId)] ?? throw new InvalidOperationException("Column not found");
        var raceNumberColumn = Table.Columns[nameof(RaceEntity.RaceNumber)] ?? throw new InvalidOperationException("Column not found");

        return new[] { raceDayIdColumn, raceNumberColumn };
    }

    #endregion
}