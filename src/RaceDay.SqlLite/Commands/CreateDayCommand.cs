using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;
using SQLite;

namespace RaceDay.SqlLite.Commands;

/// <summary>
///     A command to create new Race Day.
/// </summary>
public class CreateDayCommand : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public CreateDayCommand(string connectionString) : base(connectionString)
    {
    }

    #endregion


    /// <summary>
    ///     Executes the command
    /// </summary>
    /// <param name="dto">A <see cref="NewRaceDayDto" /> instance to create a record from.</param>
    /// <returns>The id of the newly created record.</returns>
    public DayEntity? Execute(DayEntity entity)
    {
        // @formatter:off
        const string sql = "INSERT INTO Days(Name, Fee, LapDistanceKm, PetrolCostPerLap)" +
                           "VALUES (@Name, @Fee, @LapDistanceKm, @PetrolCostPerLap);" +
                           "SELECT Id, Name, Fee, LapDistanceKm, PetrolCostPerLap" + 
                           "FROM Days" +
                           "WHERE Id = last_insert_rowid();";
        // @formatter:on

        using var cnx = CreateConnection();
        //cnx.Open();

        var cmd = new SQLiteCommand(cnx);
        cmd.CommandText = sql;
        var result = cnx.Query<DayEntity>(sql).FirstOrDefault();
        /*
        cmd.Parameters.AddWithValue("@Name",     entity.Name);
        cmd.Parameters.AddWithValue("@Fee",      entity.Fee);
        cmd.Parameters.AddWithValue("@Distance", entity.LapDistanceKm);
        cmd.Parameters.AddWithValue("@Cost",     entity.PetrolCostPerLap);
        cmd.Prepare();

        var reader = cmd.ExecuteReader();
        if (!reader.Read()) throw new SqliteException("The record could not be created.", 0);

        var result = new DayEntity
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Fee = reader.GetFloat(2),
            LapDistanceKm = reader.GetFloat(3),
            PetrolCostPerLap = reader.GetFloat(4)
        };
*/
        return result;
    }
}