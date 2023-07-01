using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RacerViewModel : ObservableObject
{
    #region Delegates

    public delegate RacerViewModel CreateRacerViewModel(RaceModel raceModel, RacerModel racerModel);

    #endregion

    #region Fields

    private readonly ICreateRacerLapCommand _createRacerLapCommand;
    private readonly IDeleteRaceLapCommand _deleteRaceLapCommand;

    private readonly RaceModel _raceModel;
    private readonly IRacerLapQuery _racerLapQuery;
    private readonly Stopwatch _stopwatch;

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

    public float AverageLapSpeed => Laps.Count == 0 ? 0 : Laps.Average(l => l.LapSpeedMph);

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
    public ICommand DeleteLapCommand { get; }

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
            LapTime = new TimeSpan(0, 0, 2, 55, 123),
            LapDistanceMiles = 1.234f
        });

        LapTimer = new TimeSpan(0, 0, 2, 55, 123);
        Started = true;
        DisplayLaps = true;
    }

    public RacerViewModel(RaceModel raceModel,
                          RacerModel raceRacerModel,
                          IRacerLapQuery racerLapQuery,
                          ICreateRacerLapCommand createRacerLapCommand,
                          IDeleteRaceLapCommand deleteRaceLapCommand)
    {
        _raceModel = raceModel;
        _racerLapQuery = racerLapQuery;
        _createRacerLapCommand = createRacerLapCommand;
        _deleteRaceLapCommand = deleteRaceLapCommand;
        Racer = raceRacerModel;

        ToggleLapsVisibilityCommand = new RelayCommand(ToggleLapsVisibility, CanToggleLapsVisibility);
        StartTimerCommand = new RelayCommand(StartTimer,                     CanStartTimer);
        StopTimerCommand = new RelayCommand(StopTimer,                       CanStopTimer);
        CatchLapTimeCommand = new RelayCommand(CatchLapTime,                 CanCatchLapTime);
        DeleteLapCommand = new RelayCommand(DeleteLap,                       CanDeleteLap);

        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(1);
        _timer.Tick += TimerOnTick;
        _stopwatch = new Stopwatch();

        LoadData();
    }

    #endregion

    #region Events And Handlers

    private void TimerOnTick(object? sender, EventArgs e)
    {
        LapTimer = _stopwatch.ElapsedMilliseconds > 0
                       ? TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds)
                       : TimeSpan.Zero;
    }

    #endregion

    private static bool CanDeleteLap(object? arg) => true;

    private void DeleteLap(object? obj)
    {
        var lap = obj as RacerLapModel;
        if (lap == null)
            return;

        bool deleted = _deleteRaceLapCommand.Execute(lap.LapId);
        if (!deleted)
            return;

        Laps.Remove(lap);
        OnPropertyChanged(nameof(LapRecord));
        OnPropertyChanged(nameof(LapCounter));
        OnPropertyChanged(nameof(AverageLapSpeed));
    }

    private void LoadData()
    {
        Laps.Clear();
        var laps = _racerLapQuery.GetLapsForRacerInRace(_raceModel.RaceId, Racer.RacerId);
        foreach (var lap in laps)
        {
            Laps.Add(new RacerLapModel
            {
                LapId = lap.Id,
                RaceDayId = _raceModel.RaceDayId,
                RaceId = _raceModel.RaceId,
                RacerId = Racer.RacerId,
                LapTime = TimeSpan.FromSeconds(lap.LapTimeSeconds),
                LapDistanceMiles = _raceModel.LapDistanceMiles
            });
        }
    }

    private bool CanCatchLapTime(object? arg) => Started && !Finished;

    private void CatchLapTime(object? obj)
    {
        var lapTime = LapTimer;
        LapTimer = TimeSpan.Zero;
        _stopwatch.Restart();

        var entity = new RaceLapEntity
        {
            RaceDayId = _raceModel.RaceDayId,
            RaceId = _raceModel.RaceId,
            RacerId = Racer.RacerId,
            LapTimeSeconds = (float)lapTime.TotalSeconds
        };
        var newLap = _createRacerLapCommand.Execute(entity);

        Laps.Add(new RacerLapModel
        {
            LapId = newLap?.Id ?? 0,
            RaceDayId = _raceModel.RaceDayId,
            RaceId = _raceModel.RaceId,
            RacerId = Racer.RacerId,
            LapTime = lapTime,
            LapDistanceMiles = _raceModel.LapDistanceMiles
        });

        OnPropertyChanged(nameof(LapRecord));
        OnPropertyChanged(nameof(AverageLapSpeed));
        OnPropertyChanged(nameof(LapCounter));
    }

    private bool CanStopTimer(object? arg) => Started && !Finished;

    private void StopTimer(object? obj)
    {
        if (Started && !Finished)
            CatchLapTime(obj);
        _timer.Stop();
        _stopwatch.Stop();
        Finished = true;
    }

    private bool CanStartTimer(object? arg) => !Started && !Finished;

    private void StartTimer(object? obj)
    {
        _timer.Start();
        _stopwatch.Start();
        Started = true;
    }

    private static bool CanToggleLapsVisibility(object? arg) => true;
    private void ToggleLapsVisibility(object? obj) => DisplayLaps = !DisplayLaps;
}