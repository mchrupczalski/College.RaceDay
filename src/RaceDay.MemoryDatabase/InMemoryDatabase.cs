using System.Data;
using RaceDay.Domain.Entities;
using RaceDay.MemoryDatabase.Enums;
using RaceDay.MemoryDatabase.Helpers;
using RaceDay.MemoryDatabase.TableDefinitions;

namespace RaceDay.MemoryDatabase;

public class InMemoryDatabase
{
    #region Fields

    private readonly string _dataFilePath;

    #endregion

    #region Properties

    public DataSet DataSet { get; } = new();
    public TableDefinitionBase<RaceDayEntity>? RaceDays { get; private set; }
    public TableDefinitionBase<RaceEntity>? Races { get; private set; }
    public TableDefinitionBase<RaceLapEntity>? RaceLaps { get; private set; }
    public TableDefinitionBase<RacerEntity>? Racers { get; private set; }
    public TableDefinitionBase<RaceRacerEntity>? RaceRacers { get; private set; }

    #endregion

    #region Constructors

    public InMemoryDatabase(string dataFilePath) => _dataFilePath = dataFilePath;

    #endregion

    public void Initialize()
    {
        CreateDataTables();
        BuildRelations();
        LoadData();
    }

    private void CreateDataTables()
    {
        RaceDays = new RaceDaysTableDefinition(_dataFilePath);
        RaceLaps = new RaceLapsTableDefinition(_dataFilePath);
        RaceRacers = new RaceRacersTableDefinition(_dataFilePath);
        Races = new RacesTableDefinition(_dataFilePath);
        Racers = new RacersTableDefinition(_dataFilePath);

        var raceDaysTable = RaceDays.Table;
        var raceLapsTable = RaceLaps.Table;
        var raceRacersTable = RaceRacers.Table;
        var racesTable = Races.Table;
        var racersTable = Racers.Table;

        DataSet.Tables.AddRange(new[] { raceDaysTable, raceLapsTable, raceRacersTable, racesTable, racersTable });
    }

    private void BuildRelations()
    {
        var fkDefinitions = new ForeignKeyDefinition[]
        {
            new(TableName.RaceDays,  nameof(RaceDayEntity.Id) , TableName.Races,  nameof(RaceEntity.RaceDayId) ),
            new(TableName.Races, new[] { nameof(RaceEntity.RaceDayId), nameof(RaceEntity.RaceNumber) }, TableName.RaceRacers,
                new[] { nameof(RaceRacerEntity.RaceDayId), nameof(RaceRacerEntity.RaceNumber) }),
            new(TableName.Racers,  nameof(RacerEntity.Id) , TableName.RaceRacers, nameof(RaceRacerEntity.RacerId) ),
            new(TableName.RaceRacers, new[] { nameof(RaceRacerEntity.RaceDayId), nameof(RaceRacerEntity.RaceNumber), nameof(RaceRacerEntity.RacerId) }, TableName.RaceLaps,
                new[] { nameof(RaceLapEntity.RaceDayId), nameof(RaceLapEntity.RaceNumber), nameof(RaceLapEntity.RacerId) }),
            new(TableName.RaceDays,  nameof(RaceDayEntity.Id) , TableName.RaceLaps,  nameof(RaceLapEntity.RaceDayId) )
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
    
    private void LoadData()
    {
        RaceDays?.LoadDataFromFile();
        Races?.LoadDataFromFile();
        Racers?.LoadDataFromFile();
        RaceRacers?.LoadDataFromFile();
        RaceLaps?.LoadDataFromFile();
    }
}