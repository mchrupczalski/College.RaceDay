using System;

namespace RaceDay.WpfUi.Models;

public class RaceLapModel
{
    #region Properties

    public Guid Guid { get; init; }
    public RaceDayModel RaceDay { get; set; }
    public RaceModel Race { get; set; }
    public LapModel Lap { get; set; }
    public TimeSpan LapTime { get; set; }
    public RacerModel Racer { get; set; }

    #endregion

    #region Constructors

    public RaceLapModel(Guid guid) => Guid = guid;

    #endregion
}