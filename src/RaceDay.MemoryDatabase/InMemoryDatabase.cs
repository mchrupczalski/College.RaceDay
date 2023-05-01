using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Enums;
using RaceDay.MemoryDatabase.Helpers;
using RaceDay.MemoryDatabase.TableDefinitions;

namespace RaceDay.MemoryDatabase;

public class InMemoryDatabase
{
    #region Static Fields and Const

    public DataSet DataSet { get; } = new();

    #endregion

    public void Initialize()
    {
        CreateDataTables();
        BuildRelations();
    }
    
    private void CreateDataTables()
    {
        var lapsTable = new LapsTableDefinition().Table;
        var raceDaysTable = new RaceDaysTableDefinition().Table;
        var raceLapsTable = new RaceLapsTableDefinition().Table;
        var raceRacersTable = new RaceRacersTableDefinition().Table;
        var racesTable = new RacesTableDefinition().Table;
        var racersTable = new RacersTableDefinition().Table;

        DataSet.Tables.AddRange(new[] { lapsTable, raceDaysTable, raceLapsTable, raceRacersTable, racesTable, racersTable });
    }

    private void BuildRelations()
    {
        var fkDefinitions = new ForeignKeyDefinition[]
        {
            new(TableName.RaceDays, new[] { nameof(RaceDayEntity.Id) }, TableName.Races, new[] { nameof(RaceEntity.RaceDayId) }),
            new(TableName.RaceDays, new[] { nameof(RaceDayEntity.Id) }, TableName.Laps, new[] { nameof(LapEntity.RaceDayId) }),
            new(TableName.Races, new[] { nameof(RaceEntity.RaceDayId), nameof(RaceEntity.RaceNumber) }, TableName.RaceRacers,
                new[] { nameof(RaceRacerEntity.RaceDayId), nameof(RaceRacerEntity.RaceNumber) }),
            new(TableName.Racers, new[] { nameof(RacerEntity.Id) }, TableName.RaceRacers, new[] { nameof(RaceRacerEntity.RacerId) }),
            new(TableName.Laps, new[] { nameof(LapEntity.RaceDayId) }, TableName.RaceLaps, new[] { nameof(RaceLapEntity.RaceDayId) }),
            new(TableName.RaceRacers, new[] { nameof(RaceRacerEntity.RaceDayId), nameof(RaceRacerEntity.RaceNumber), nameof(RaceRacerEntity.RacerId) }, TableName.RaceLaps,
                new[] { nameof(RaceLapEntity.RaceDayId), nameof(RaceLapEntity.RaceNumber), nameof(RaceLapEntity.RacerId) })
        };

        foreach (var fkDefinition in fkDefinitions)
        {
            var parentTable = DataSet.Tables[fkDefinition.ParentTableName.ToString()] ??
                              throw new ArgumentNullException(nameof(fkDefinition.ParentTableName), "Parent table not found.");
            var childTable = DataSet.Tables[fkDefinition.ChildTableName.ToString()] ??
                             throw new ArgumentNullException(nameof(fkDefinition.ChildTableName), "Child table not found.");

            var parentColumns = new DataColumn[fkDefinition.ParentColumnNames.Length];
            for (int i = 0; i < fkDefinition.ParentColumnNames.Length; i++)
            {
                parentColumns[i] = parentTable.Columns[fkDefinition.ParentColumnNames[i]] ??
                                   throw new ArgumentNullException(nameof(fkDefinition.ParentColumnNames), "Parent column not found.");
            }

            var childColumns = new DataColumn[fkDefinition.ChildColumnNames.Length];
            for (int i = 0; i < fkDefinition.ChildColumnNames.Length; i++)
            {
                childColumns[i] = childTable.Columns[fkDefinition.ChildColumnNames[i]] ??
                                  throw new ArgumentNullException(nameof(fkDefinition.ChildColumnNames), "Child column not found.");
            }

            var foreignKeyConstraint = new ForeignKeyConstraint(fkDefinition.Name, parentColumns, childColumns);
            childTable.Constraints.Add(foreignKeyConstraint);
        }
    }


}