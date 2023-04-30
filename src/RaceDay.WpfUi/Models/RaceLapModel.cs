using System;

namespace RaceDay.WpfUi.Models;

public class RaceLapModel
{
    public RaceLapModel(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; init; }
    public TimeSpan LapTime { get; set; }
    public RacerModel? Racer { get; set; }
}