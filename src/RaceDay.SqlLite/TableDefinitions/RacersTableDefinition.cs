using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Enums;

namespace RaceDay.SqlLite.TableDefinitions;

internal class RacersTableDefinition : TableDefinitionBase<RacerEntity>
{
    #region Constructors

    /// <inheritdoc />
    public RacersTableDefinition(string dataFilesRoot, string dataFileName = "Racers.json", TableName tableName = TableName.Racers) : base(tableName, dataFilesRoot, dataFileName)
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