using System;

namespace RaceDay.WpfUi.Models;

/// <summary>
///     A model for the Race Summary
/// </summary>
public class RaceSummaryModel
{
    /// <summary>
    ///     The unique identifier of the Race
    /// </summary>
    public int RaceId { get; init; }
    
    /// <summary>
    ///     The unique identifier of the Race Day
    /// </summary>
    public int RaceDayId { get; init; }
    
    /// <summary>
    ///     The date of the race
    /// </summary>
    public DateTime? RaceDate { get; init; }
    
    /// <summary>
    ///     The total number of Racers participating in the Race
    /// </summary>
    public int TotalRacers { get; init; }
    
    /// <summary>
    ///      The total number of Laps completed by all Racers in the Race
    /// </summary>
    public int TotalLaps { get; init; }
    
    /// <summary>
    ///     The best Lap Time in the Race
    /// </summary>
    public TimeSpan BestLapTime { get; init; }
    
    /// <summary>
    ///     The name of the Racer with the best Lap Time
    /// </summary>
    public string? BestLapTimeHolder { get; init; }
    
    /// <summary>
    ///     The total income made from the Race
    /// </summary>
    public float TotalIncome { get; init; }
    
    /// <summary>
    ///     The total expense incurred
    /// </summary>
    public float TotalExpenses { get; init; }
    
    /// <summary>
    ///     The total profit
    /// </summary>
    public float TotalProfit { get; init; }
}