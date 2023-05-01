using System.Data;

namespace RaceDay.MemoryDatabase;

public class Database
{
    #region Static Fields and Const

    private const string TableNameLaps = "Laps";
    private const string TableNameRaceDays = "RaceDays";
    private const string TableNameRaceLaps = "RaceLaps";
    private const string TableNameRaceRacers = "RaceRacers";
    private const string TableNameRacers = "Racers";
    private const string TableNameRaces = "Races";

    #endregion

    #region Fields

    private readonly DataSet _dataSet = new();

    #endregion

    #region Constructors

    #endregion

    public void Initialize()
    {
        CreateDataTables();
        BuildRelations();
    }

    private void BuildRelations()
    {
        var raceDaysTable = _dataSet.Tables[TableNameRaceDays];
        var racesTable = _dataSet.Tables[TableNameRaces];
        var lapsTable = _dataSet.Tables[TableNameLaps];
        var racersTable = _dataSet.Tables[TableNameRacers];
        var raceRacersTable = _dataSet.Tables[TableNameRaceRacers];
        var raceLapsTable = _dataSet.Tables[TableNameRaceLaps];

        var fkRacesRaceDays = new ForeignKeyConstraint("fkRaces_RaceDays", raceDaysTable.Columns["Id"], racesTable.Columns["RaceDayId"]);
        raceDaysTable.Constraints.Add(fkRacesRaceDays);

        var fkLapsRaceDays = new ForeignKeyConstraint("fkLaps_RaceDays", raceDaysTable.Columns["Id"], lapsTable.Columns["RaceDayId"]);
        lapsTable.Constraints.Add(fkLapsRaceDays);

        var fkRaceRacersRaces = new ForeignKeyConstraint("fkRaceRacers_Races", new[] { racesTable.Columns["Id"], racesTable.Columns["RaceDayId"] },
                                                         new[] { raceRacersTable.Columns["RaceId"], raceRacersTable.Columns["RaceDayId"] });
        raceRacersTable.Constraints.Add(fkRaceRacersRaces);

        var fkRaceRacersRacers = new ForeignKeyConstraint("fkRaceRacers_Racers", racersTable.Columns["Id"], raceRacersTable.Columns["RacerId"]);
        raceRacersTable.Constraints.Add(fkRaceRacersRacers);

        var fkRaceLapsLaps = new ForeignKeyConstraint("fkRaceLaps_Laps", lapsTable.Columns["RaceDayId"], raceLapsTable.Columns["RaceDayId"]);
        raceLapsTable.Constraints.Add(fkRaceLapsLaps);

        var fkRaceLapsRaceRacers = new ForeignKeyConstraint("fkRaceLaps_RaceRacers",
                                                            new[] { raceRacersTable.Columns["RaceDayId"], raceRacersTable.Columns["RaceId"], raceRacersTable.Columns["RacerId"] },
                                                            new[] { raceLapsTable.Columns["RaceDayId"], raceLapsTable.Columns["RaceId"], raceLapsTable.Columns["RacerId"] });
        raceLapsTable.Constraints.Add(fkRaceLapsRaceRacers);
    }

    private void CreateDataTables()
    {
        _dataSet.Tables.Add(CreateRaceDaysTable());
        _dataSet.Tables.Add(CreateRacesTable());
        _dataSet.Tables.Add(CreateLapsTable());
        _dataSet.Tables.Add(CreateRacersTable());
        _dataSet.Tables.Add(CreateRaceRacersTable());
        _dataSet.Tables.Add(CreateRaceLapsTable());
    }

    private static DataTable CreateRaceDaysTable()
    {
        var table = new DataTable(TableNameRaceDays);
        var idColumn = table.Columns.Add("Id", typeof(int));
        idColumn.AutoIncrement = true;
        idColumn.AutoIncrementSeed = 1;
        idColumn.AutoIncrementStep = 1;

        table.Columns.Add("Name", typeof(string))
             .AllowDBNull = false;
        table.Columns.Add("Fee", typeof(float))
             .AllowDBNull = false;

        table.PrimaryKey = new[] { idColumn };

        return table;
    }

    private static DataTable CreateRacesTable()
    {
        var table = new DataTable(TableNameRaces);
        var idColumn = table.Columns.Add("Id", typeof(int));
        idColumn.AutoIncrement = true;
        idColumn.AutoIncrementSeed = 1;
        idColumn.AutoIncrementStep = 1;

        var raceDayIdColumn = table.Columns.Add("RaceDayId", typeof(int));
        raceDayIdColumn.AllowDBNull = false;

        table.Columns.Add("RaceDate", typeof(DateTime))
             .AllowDBNull = false;

        table.PrimaryKey = new[] { raceDayIdColumn, idColumn };

        return table;
    }

    private static DataTable CreateLapsTable()
    {
        var table = new DataTable(TableNameLaps);

        var idColumn = table.Columns.Add("RaceDayId", typeof(int));
        idColumn.AllowDBNull = false;

        table.Columns.Add("LapDistanceKm", typeof(float))
             .AllowDBNull = false;
        table.Columns.Add("PetrolCostPerLap", typeof(float))
             .AllowDBNull = false;

        table.PrimaryKey = new[] { idColumn };

        return table;
    }

    private static DataTable CreateRacersTable()
    {
        var table = new DataTable(TableNameRacers);
        var idColumn = table.Columns.Add("Id", typeof(int));
        idColumn.AutoIncrement = true;
        idColumn.AutoIncrementSeed = 1;
        idColumn.AutoIncrementStep = 1;

        table.Columns.Add("Name", typeof(string))
             .AllowDBNull = false;
        table.Columns.Add("Age", typeof(byte))
             .AllowDBNull = false;

        table.PrimaryKey = new[] { idColumn };

        return table;
    }

    private static DataTable CreateRaceRacersTable()
    {
        var table = new DataTable(TableNameRaceRacers);

        var raceDayIdColumn = table.Columns.Add("RaceDayId", typeof(int));
        var raceIdColumn = table.Columns.Add("RaceId",       typeof(int));
        var racerIdColumn = table.Columns.Add("RacerId",     typeof(int));

        table.PrimaryKey = new[] { raceDayIdColumn, raceIdColumn, racerIdColumn };

        return table;
    }

    private static DataTable CreateRaceLapsTable()
    {
        var table = new DataTable(TableNameRaceLaps);

        var raceDayIdColumn = table.Columns.Add("RaceDayId", typeof(int));
        var raceIdColumn = table.Columns.Add("RaceId",       typeof(int));
        var racerIdColumn = table.Columns.Add("RacerId",     typeof(int));
        var lapNumberColumn = table.Columns.Add("LapNumber", typeof(int));

        table.Columns.Add("LapTime", typeof(TimeSpan))
             .AllowDBNull = false;

        table.PrimaryKey = new[] { raceDayIdColumn, raceIdColumn, racerIdColumn, lapNumberColumn };

        return table;
    }
}