using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RaceDay.WpfUi.Models;

public class RaceDayModel
{
    #region Properties

    public Guid Guid { get; init; }
    public string? Name { get; set; }

    public float SignUpFee { get; set; }
    
    public LapModel Lap { get; set; }
    


    public RaceLapModel? RecordLap => Races.SelectMany(r => r.Laps)
                                           .MinBy(l => l.LapTime);

    public ObservableCollection<RaceModel> Races { get; } = new();

    #endregion

    #region Constructors

    public RaceDayModel(Guid guid) => Guid = guid;

    #endregion
}