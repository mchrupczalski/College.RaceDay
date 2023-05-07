using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

/// <summary>
///     A command to create new Race Day.
/// </summary>
public class CreateRaceDayCommand : CommandQueryBase
{
    #region Constructors

    /// <inheritdoc />
    public CreateRaceDayCommand(string connectionString) : base(connectionString)
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
        const string selectSql = " SELECT Id, Name, Fee, LapDistanceKm, PetrolCostPerLap FROM Days WHERE Id = last_insert_rowid();";
        string insertSql =
            $"INSERT INTO Days(Name, Fee, LapDistanceKm, PetrolCostPerLap) VALUES ('{entity.Name}', {entity.Fee}, {entity.LapDistanceKm}, {entity.PetrolCostPerLap});";

        using var cnx = CreateConnection();
        _ = cnx.Query<DayEntity>(insertSql);
        
        var result = cnx.Query<DayEntity>(selectSql)
                        .FirstOrDefault();

        return result;
    }
}