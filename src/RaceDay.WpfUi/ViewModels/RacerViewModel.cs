using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RacerViewModel
{
    #region Properties

    public RacerModel Racer { get; }
    public ImageSource? MedalImage { get; }
    
    
    public ObservableCollection<RacerLapModel> Laps { get; } = new();
    public TimeSpan LapRecord { get; set; }
    public float AverageLapSpeed { get; set; }

    public int LapCounter { get; set; }
    public TimeSpan LapTimer { get; set; }
    public bool Started { get; set; }
    public bool DisplayLaps { get; set; }

    #endregion

    #region Constructors

    [Obsolete("For design-time use only", true)]
    public RacerViewModel()
    {
        Racer = new RacerModel
        {
            RaceDayId = 1,
            RaceNumber = 1,
            RacerId = 1,
            RacerName = "Racer Name 01",
            Age = 25
        };

        Laps.Add(new RacerLapModel
        {
            RaceDayId = 1,
            RaceNumber = 1,
            RacerId = 1,
            LapNumber = 1,
            LapTime = new TimeSpan(0, 0, 2, 55, 123),
            LapDistanceKm = 1.234f
        });
        
        LapRecord = new TimeSpan(0, 0, 2, 55, 123);
        AverageLapSpeed = 123.45f;
        LapCounter = 5;
        LapTimer = new TimeSpan(0, 0, 2, 55, 123);
        Started = true;
        DisplayLaps = true;
    }

    public RacerViewModel(RacerModel raceRacerModel) => Racer = raceRacerModel;

    #endregion
}