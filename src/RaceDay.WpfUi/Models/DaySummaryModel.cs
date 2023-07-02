using System;

namespace RaceDay.WpfUi.Models;

/// <summary>
///     A model for the Race Day Summary
/// </summary>
public record DaySummaryModel
{
    #region Properties

    /// <summary>
    ///     The unique identifier of the Race Day
    /// </summary>
    public int RaceDayId { get; init; }
    
    /// <summary>
    ///     The name of the Race Day
    /// </summary>
    public string? RaceDayName { get; init; }
    
    /// <summary>
    ///     The sign up fee
    /// </summary>
    public float SignUpFee { get; init; }
    
    /// <summary>
    ///     The Lap distance in kilometers
    /// </summary>
    public float LapDistanceKilometers { get; init; }
    
    /// <summary>
    ///     The Lap distance in miles
    /// </summary>
    public float LapDistanceMiles => LapDistanceKilometers * 0.621371f;
    
    /// <summary>
    ///     The estimated petrol cost per lap
    /// </summary>
    public float PetrolCostPerLap { get; init; }
    
    /// <summary>
    ///      The total number of Races for the Race Day
    /// </summary>
    public int TotalRaces { get; init; }
    
    /// <summary>
    ///     The all time Lap Record
    /// </summary>
    public TimeSpan? RecordLapTime { get; init; }
    
    /// <summary>
    ///     The name of the Lap Record holder
    /// </summary>
    public string? RecordHolderName { get; init; }
    
    /// <summary>
    ///     The total income from all Races
    /// </summary>
    public float TotalIncome { get; init; }
    
    /// <summary>
    ///     The total cost of all Races 
    /// </summary>
    public float TotalCost { get; init; }
    
    /// <summary>
    ///     The total profit made
    /// </summary>
    public float TotalProfit => TotalIncome - TotalCost;
    
    /// <summary>
    ///     The average profit per Race
    /// </summary>
    public float AverageProfit => (TotalIncome - TotalCost) / TotalRaces;

    #endregion
}