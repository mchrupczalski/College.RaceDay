namespace RaceDay.Domain.DTOs;

/// <summary>
///     Represents the summary of a RaceDay
/// </summary>
public record DaySummaryDto
{
    #region Properties

    /// <summary>
    ///     A unique identifier for the RaceDay
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The name of the RaceDay
    /// </summary>
    public string? RaceDayName { get; init; }

    /// <summary>
    ///     The cost to sign up for the RaceDay
    /// </summary>
    public float SignUpFee { get; init; }

    /// <summary>
    ///     The distance of each lap in kilometers for the RaceDay
    /// </summary>
    public float LapDistanceKm { get; init; }

    /// <summary>
    ///     The cost of petrol per lap for the RaceDay
    /// </summary>
    public float PetrolCostPerLap { get; init; }

    /// <summary>
    ///     The total number of Races within the RaceDay
    /// </summary>
    public int TotalRaces { get; init; }

    /// <summary>
    ///     The total number of Laps within the RaceDay
    /// </summary>
    public float RecordLapTime { get; init; }

    /// <summary>
    ///     Gets or sets the RaceDay Lap Record Holder Name.
    /// </summary>
    public string? RecordHolderName { get; init; }

    /// <summary>
    ///     THe total income for the RaceDay
    /// </summary>
    public float TotalIncome { get; init; }

    /// <summary>
    ///     The total cost for the RaceDay
    /// </summary>
    public float TotalCost { get; init; }

    #endregion
}