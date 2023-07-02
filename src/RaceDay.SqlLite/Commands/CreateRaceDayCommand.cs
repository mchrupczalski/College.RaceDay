using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Infrastructure;

namespace RaceDay.SqlLite.Commands;

/// <inheritdoc cref="ICreateRaceDayCommand" />
public class CreateRaceDayCommand : CommandQueryBase, ICreateRaceDayCommand
{
    #region Constructors

    /// <inheritdoc />
    public CreateRaceDayCommand(string connectionString) : base(connectionString) { }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public DayEntity? Execute(DayEntity entity)
    {
        const string selectSql = " SELECT Id, Name, Fee, LapDistanceKm, PetrolCostPerLap FROM Days WHERE Id = last_insert_rowid();";
        string insertSql =
            $"INSERT INTO Days(Name, Fee, LapDistanceKm, PetrolCostPerLap) VALUES ('{entity.Name}', {entity.Fee}, {entity.LapDistanceKm}, {entity.PetrolCostPerLap});";

        using var cnx = CreateConnection();
        _ = cnx.Query<DayEntity>(insertSql);

        var result = cnx.Query<DayEntity>(selectSql).FirstOrDefault();

        return result;
    }

    #endregion
}