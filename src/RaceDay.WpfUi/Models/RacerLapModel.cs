using System;

namespace RaceDay.WpfUi.Models;

public class RacerLapModel
{
    private static readonly float MilesPerKm = 0.621371f;

    public int RaceDayId { get; set; }
    public int RaceNumber { get; set; }
    public int RacerId { get; set; }
    public int LapNumber { get; set; }
    public TimeSpan LapTime { get; set; }
    public float LapDistanceKm { get; set; }
    public float LapSpeedMph => LapDistanceKm / (float)LapTime.TotalHours * MilesPerKm;
}