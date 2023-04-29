using System.Collections.ObjectModel;
using System.Linq;

namespace RaceDay.WpfUi.Models;

public class RaceDayModel
{
    #region Properties

    public string Name { get; init; }

    public float SignUpFee { get; init; }

    public float LapDistanceKilometers { get; set; }
    
    public float LapDistanceMiles => LapDistanceKilometers * 0.621371f;


    public float PetrolCostPerLap { get; set; }


    public int TotalRaces => Races.Count;

    public RaceLapModel? RecordLap => Races.SelectMany(r => r.Laps)
                                           .MinBy(l => l.LapTime);

    private ObservableCollection<RaceModel> Races { get; } = new();

    #endregion
}