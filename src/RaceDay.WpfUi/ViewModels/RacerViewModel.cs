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

/// <summary>
///     A view model providing data and logic for the Racer view
/// </summary>
public class RacerViewModel : ObservableObject
{
    #region Delegates

    /// <summary>
    ///     A delegate factory method for creating a new RacerViewModel
    /// </summary>
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

    /// <summary>
    ///     The racer to be displayed
    /// </summary>
    public RacerModel Racer { get; }

    /// <summary>
    ///     An image source for displaying the racer's medal.
    ///     This is only used for the top three racers within the race.
    /// </summary>
    public ImageSource? MedalImage
    {
        get => _medalImage;
        set => SetField(ref _medalImage, value);
    }

    /// <summary>
    ///     A collection of all laps for the racer
    /// </summary>
    public ObservableCollection<RacerLapModel> Laps { get; } = new();

    /// <summary>
    ///     The fastest lap time for the racer
    /// </summary>
    public TimeSpan LapRecord => Laps.Count == 0 ? TimeSpan.Zero : Laps.Min(l => l.LapTime);

    /// <summary>
    ///     The average lap speed for the racer
    /// </summary>
    public float AverageLapSpeed => Laps.Count == 0 ? 0 : Laps.Average(l => l.LapSpeedMph);

    /// <summary>
    ///     The total number of laps for the racer
    /// </summary>
    public int LapCounter => Laps.Count;

    /// <summary>
    ///     The time of the current lap
    /// </summary>
    public TimeSpan LapTimer
    {
        get => _lapTimer;
        set => SetField(ref _lapTimer, value);
    }

    /// <summary>
    ///     Indicates whether the Racer already started the Race
    /// </summary>
    public bool Started
    {
        get => _started;
        set => SetField(ref _started, value);
    }

    /// <summary>
    ///     Indicates whether the Races has finished
    /// </summary>
    public bool Finished
    {
        get => _finished;
        set => SetField(ref _finished, value);
    }

    /// <summary>
    ///     A toggle for displaying the racer's laps component in the Ui
    /// </summary>
    public bool DisplayLaps
    {
        get => _displayLaps;
        set => SetField(ref _displayLaps, value);
    }

    /// <summary>
    ///     A command for toggling the laps component visibility
    /// </summary>
    public ICommand ToggleLapsVisibilityCommand { get; }

    /// <summary>
    ///     A command for starting the timer
    /// </summary>
    public ICommand StartTimerCommand { get; }

    /// <summary>
    ///     A command for stopping the timer
    /// </summary>
    public ICommand StopTimerCommand { get; }

    /// <summary>
    ///     A command for catching the lap time
    /// </summary>
    public ICommand CatchLapTimeCommand { get; }

    /// <summary>
    ///     A command for deleting the selected lap
    /// </summary>
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

    /// <summary>
    ///     Creates a new instance of the <see cref="RacerViewModel" /> class
    /// </summary>
    /// <param name="raceModel">The Race model</param>
    /// <param name="raceRacerModel">The Racer model</param>
    /// <param name="racerLapQuery">A query for retrieving RacerLap data</param>
    /// <param name="createRacerLapCommand">A command for creating a new RacerLap</param>
    /// <param name="deleteRaceLapCommand">A command for deleting a RacerLap</param>
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

    /// <summary>
    ///     An event for updating the lap time in the Ui
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TimerOnTick(object? sender, EventArgs e)
    {
        LapTimer = _stopwatch.ElapsedMilliseconds > 0 ? TimeSpan.FromMilliseconds(_stopwatch.ElapsedMilliseconds) : TimeSpan.Zero;
    }

    #endregion

    /// <summary>
    ///     Indicates whether the Lap can be deleted
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns>Always true</returns>
    private static bool CanDeleteLap(object? arg) => true;

    /// <summary>
    ///     An action for deleting the selected lap
    /// </summary>
    /// <param name="obj">The selected lap</param>
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

    /// <summary>
    ///     Loads view model data from the persistent storage
    /// </summary>
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

    /// <summary>
    ///     Indicates whether the Lap time can be caught. <br />
    ///     The Lap time can be caught only if the timer is started and not finished
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns></returns>
    private bool CanCatchLapTime(object? arg) => Started && !Finished;

    /// <summary>
    ///     An action for catching the lap time
    /// </summary>
    /// <param name="obj">Not used</param>
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

    /// <summary>
    ///     Indicates whether the timer can be stopped. <br />
    ///     The timer can be stopped only if it is started and not finished
    /// </summary>
    /// <param name="arg">Not used</param>
    private bool CanStopTimer(object? arg) => Started && !Finished;

    /// <summary>
    ///     An action for stopping the timer
    /// </summary>
    /// <param name="obj">Not used</param>
    private void StopTimer(object? obj)
    {
        if (Started && !Finished)
            CatchLapTime(obj);
        _timer.Stop();
        _stopwatch.Stop();
        Finished = true;
    }

    /// <summary>
    ///     Indicates whether the timer can be started. <br />
    ///     The timer can be started only if it is not started and not finished
    /// </summary>
    /// <param name="arg">Not used</param>
    private bool CanStartTimer(object? arg) => !Started && !Finished;

    /// <summary>
    ///     An action for starting the timer
    /// </summary>
    /// <param name="obj">Not used</param>
    private void StartTimer(object? obj)
    {
        _timer.Start();
        _stopwatch.Start();
        Started = true;
    }

    /// <summary>
    ///     Indicates whether the laps component in the UI can be toggled
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns>Always true</returns>
    private static bool CanToggleLapsVisibility(object? arg) => true;

    /// <summary>
    ///     An action for toggling the laps component visibility in the UI
    /// </summary>
    /// <param name="obj">Not used</param>
    private void ToggleLapsVisibility(object? obj) => DisplayLaps = !DisplayLaps;
}