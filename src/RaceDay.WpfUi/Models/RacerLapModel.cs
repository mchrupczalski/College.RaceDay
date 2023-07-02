using System;
using System.Windows.Input;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

/// <summary>
///     A model of the Racer Lap
/// </summary>
public class RacerLapModel : ObservableObject
{
    #region Static Fields and Const

    private static readonly float MilesPerKm = 0.621371f;

    #endregion

    #region Fields

    private TimeSpan _lapTime;

    #endregion

    #region Properties

    /// <summary>
    ///     The unique identifier of the Racer Lap
    /// </summary>
    public int LapId { get; init; }
    
    /// <summary>
    ///     The unique identifier of the Race Day
    /// </summary>
    public int RaceDayId { get; init; }
    
    /// <summary>
    ///     The unique identifier of the Race
    /// </summary>
    public int RaceId { get; init; }
    
    /// <summary>
    ///     The unique identifier of the Racer
    /// </summary>
    public int RacerId { get; init; }

    /// <summary>
    ///     The Lap time
    /// </summary>
    public TimeSpan LapTime
    {
        get => _lapTime;
        set
        {
            if (SetField(ref _lapTime, value)) OnPropertyChanged(nameof(LapSpeedMph));
        }
    }

    /// <summary>
    ///     The Lap distance in miles
    /// </summary>
    public float LapDistanceMiles { get; init; }
    
    /// <summary>
    ///     The Lap speed in miles per hour
    /// </summary>
    public float LapSpeedMph => LapDistanceMiles / (float)LapTime.TotalHours;
    

    #endregion
}