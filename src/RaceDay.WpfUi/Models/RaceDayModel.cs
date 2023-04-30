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

    public float LapDistanceKilometers { get; set; }

    public float LapDistanceMiles => LapDistanceKilometers * 0.621371f;


    public float PetrolCostPerLap { get; set; }


    public int TotalRaces => Races.Count;

    public RaceLapModel? RecordLap => Races.SelectMany(r => r.Laps)
                                           .MinBy(l => l.LapTime);

    public ObservableCollection<RaceModel> Races { get; } = new();

    #endregion

    #region Constructors

    public RaceDayModel(Guid guid) => Guid = guid;

    #endregion
}