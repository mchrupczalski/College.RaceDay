using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RacerViewModel : ObservableObject
{
    private readonly RaceModel _raceModel;

    #region Delegates

    public delegate RacerViewModel CreateRacerViewModel(RaceModel raceModel, RacerModel racerModel);

    #endregion

    #region Fields

    private readonly DispatcherTimer _timer;
    
    private bool _displayLaps;
    private bool _finished;
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

    public TimeSpan LapRecord => Laps.Count == 0 ? TimeSpan.Zero : Laps.Min(l => l.LapTime);

    public float AverageLapSpeed => Laps.Count ==  0 ? 0 : Laps.Average(l => l.LapSpeedMph);

    public int LapCounter => Laps.Count;

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
            RaceId = 1,
            RacerId = 1,
            LapNumber = 1,
            LapTime = new TimeSpan(0, 0, 2, 55, 123),
            LapDistanceMiles = 1.234f
        });
        
        LapTimer = new TimeSpan(0, 0, 2, 55, 123);
        Started = true;
        DisplayLaps = true;
    }

    public RacerViewModel(RaceModel raceModel, RacerModel raceRacerModel)
    {
        _raceModel = raceModel;
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
            RaceDayId = _raceModel.RaceDayId,
            RaceId = _raceModel.RaceId,
            RacerId = Racer.RacerId,
            LapTime = lapTime,
            LapDistanceMiles = _raceModel.LapDistanceMiles,
        });
        
        OnPropertyChanged(nameof(LapRecord));
        OnPropertyChanged(nameof(AverageLapSpeed));
        OnPropertyChanged(nameof(LapCounter));
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