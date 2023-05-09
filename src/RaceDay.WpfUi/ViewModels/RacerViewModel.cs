using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RacerViewModel : ObservableObject
{
    #region Delegates

    public delegate RacerViewModel CreateRacerViewModel(RacerModel racerModel);

    #endregion

    #region Fields

    private readonly DispatcherTimer _timer;

    private float _averageLapSpeed;
    private bool _displayLaps;
    private bool _finished;
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

    public bool Finished
    {
        get => _finished;
        set => SetField(ref _finished, value);
    }

    public bool DisplayLaps
    {
        get => _displayLaps;
        set => SetField(ref _displayLaps, value);
    }

    public ICommand ToggleLapsVisibilityCommand { get; }
    public ICommand StartTimerCommand { get; }
    public ICommand StopTimerCommand { get; }
    public ICommand CatchLapTimeCommand { get; }

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

    public RacerViewModel(RacerModel raceRacerModel)
    {
        Racer = raceRacerModel;

        ToggleLapsVisibilityCommand = new RelayCommand(ToggleLapsVisibility, CanToggleLapsVisibility);
        StartTimerCommand = new RelayCommand(StartTimer,                     CanStartTimer);
        StopTimerCommand = new RelayCommand(StopTimer,                       CanStopTimer);
        CatchLapTimeCommand = new RelayCommand(CatchLapTime,                 CanCatchLapTime);

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(100);
        _timer.Tick += TimerOnTick;
    }

    #endregion

    #region Events And Handlers

    private void TimerOnTick(object? sender, EventArgs e)
    {
        LapTimer = LapTimer.Add(TimeSpan.FromMilliseconds(100));
    }

    #endregion

    private bool CanCatchLapTime(object? arg) => Started && !Finished;

    private void CatchLapTime(object? obj)
    {
        var lapTime = LapTimer;
        LapTimer = TimeSpan.Zero;

        Laps.Add(new RacerLapModel
        {
            RaceDayId = 1,
            RaceNumber = 1,
            RacerId = 1,
            LapNumber = ++LapCounter,
            LapTime = lapTime,
            LapDistanceKm = 1.234f
        });

        if (LapRecord == TimeSpan.Zero || lapTime < LapRecord) LapRecord = lapTime;
    }

    private bool CanStopTimer(object? arg) => Started && !Finished;

    private void StopTimer(object? obj)
    {
        CatchLapTime(obj);
        _timer.Stop();
        Finished = true;
    }

    private bool CanStartTimer(object? arg) => !Started && !Finished;

    private void StartTimer(object? obj)
    {
        _timer.Start();
        Started = true;
    }

    private static bool CanToggleLapsVisibility(object? arg) => true;
    private void ToggleLapsVisibility(object? obj) => DisplayLaps = !DisplayLaps;
}