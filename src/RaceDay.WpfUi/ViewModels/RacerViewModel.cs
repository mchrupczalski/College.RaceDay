using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RacerViewModel : ObservableObject
{
    public delegate RacerViewModel CreateRacerViewModel(RacerModel racerModel);
    
    #region Fields

    private float _averageLapSpeed;
    private bool _displayLaps;
    private int _lapCounter;
    private TimeSpan _lapRecord;
    private TimeSpan _lapTimer;
    private ImageSource? _medalImage;
    private bool _started;

    #endregion

    #region Properties

    public RacerModel Racer { get; }

    public ImageSource? MedalImage
    {
        get => _medalImage;
        set => SetField(ref _medalImage, value);
    }


    public ObservableCollection<RacerLapModel> Laps { get; } = new();

    public TimeSpan LapRecord
    {
        get => _lapRecord;
        set => SetField(ref _lapRecord, value);
    }

    public float AverageLapSpeed
    {
        get => _averageLapSpeed;
        set => SetField(ref _averageLapSpeed, value);
    }

    public int LapCounter
    {
        get => _lapCounter;
        set => SetField(ref _lapCounter, value);
    }

    public TimeSpan LapTimer
    {
        get => _lapTimer;
        set => SetField(ref _lapTimer, value);
    }

    public bool Started
    {
        get => _started;
        set => SetField(ref _started, value);
    }

    public bool DisplayLaps
    {
        get => _displayLaps;
        set => SetField(ref _displayLaps, value);
    }

    #endregion

    #region Constructors

    [Obsolete("For design-time use only", true)]
    public RacerViewModel()
    {
        Racer = new RacerModel
        {
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