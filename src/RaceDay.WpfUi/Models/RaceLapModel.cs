using System;

namespace RaceDay.WpfUi.Models;

public class RaceLapModel
{
    public TimeSpan LapTime { get; set; }
    public RacerModel Racer { get; set; }
}