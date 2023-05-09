using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

public class RacerLapModel : ObservableObject
{
    #region Static Fields and Const

    private static readonly float MilesPerKm = 0.621371f;

    #endregion

    #region Fields

    private TimeSpan _lapTime;

    #endregion

    #region Properties

    public int LapId { get; init; }
    public int RaceDayId { get; init; }
    public int RaceId { get; init; }
    public int RacerId { get; init; }

    public TimeSpan LapTime
    {
        get => _lapTime;
        set
        {
            if (SetField(ref _lapTime, value)) OnPropertyChanged(nameof(LapSpeedMph));
        }
    }

    public float LapDistanceMiles { get; init; }
    public float LapSpeedMph => LapDistanceMiles / (float)LapTime.TotalHours;

    #endregion
}