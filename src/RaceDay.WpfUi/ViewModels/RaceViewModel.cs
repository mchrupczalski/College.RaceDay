using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using RaceDay.Domain.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

/// <summary>
///     A view model providing data and logic for the Race view
/// </summary>
public class RaceViewModel : ViewModelBase
{
    #region Delegates

    /// <summary>
    ///     A delegate factory method for creating a <see cref="RaceViewModel" /> from a <see cref="RaceModel" />
    /// </summary>
    public delegate RaceViewModel CreateRaceViewModel(RaceModel raceModel);

    #endregion

    #region Fields

    private readonly RacerViewModel.CreateRacerViewModel _createRacerViewModel;

    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    private readonly RaceModel _raceModel;
    private readonly IRacersQuery _racersQuery;

    #endregion

    #region Properties

    /// <summary>
    ///     A collection of view models representing Racers participating in the Race.
    /// </summary>
    public ObservableCollection<RacerViewModel> Racers { get; } = new();

    /// <summary>
    ///     The model of the Race being displayed
    /// </summary>
    public RaceModel RaceModel
    {
        get => _raceModel;
        private init => SetField(ref _raceModel, value);
    }

    /// <summary>
    ///     A command for stopping timers of all Racers
    /// </summary>
    public ICommand StopAllCommand { get; }

    /// <summary>
    ///     A command for starting timers of all Racers
    /// </summary>
    public ICommand StartAllCommand { get; }

    /// <summary>
    ///     A command for adding a Racer to the Race
    /// </summary>
    public ICommand AddRacerCommand { get; }

    /// <summary>
    ///     A command for navigating back
    /// </summary>
    public ICommand GoBackCommand { get; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("For design-time only", true)]
    public RaceViewModel() =>
        RaceModel = new RaceModel
        {
            RaceDayId = 1,
            RaceDayName = "Race Name 01",
            RaceId = 1,
            SignUpFee = 10.00f,
            AllTimeLapRecord = new TimeSpan(0, 0, 2, 55, 123),
            RaceLapRecord = new TimeSpan(0,    0, 3, 2,  623),
            IsRecordBeaten = true,
            RaceProfit = 100.00f
        };
#pragma warning restore CS8618


    /// <summary>
    ///     Creates a new instance of <see cref="RaceViewModel" />
    /// </summary>
    /// <param name="raceModel">The Race model to display</param>
    /// <param name="dialogService">The dialog service</param>
    /// <param name="navigationService">The navigation service</param>
    /// <param name="createRacerViewModel">A delegate factory method for creating a <see cref="RacerViewModel" /></param>
    /// <param name="racersQuery">A query for retrieving Racers</param>
    public RaceViewModel(RaceModel raceModel,
                         DialogService dialogService,
                         NavigationService navigationService,
                         RacerViewModel.CreateRacerViewModel createRacerViewModel,
                         IRacersQuery racersQuery)
    {
        _dialogService = dialogService;
        _navigationService = navigationService;
        _createRacerViewModel = createRacerViewModel;
        _racersQuery = racersQuery;
        RaceModel = raceModel;

        AddRacerCommand = new RelayCommand(AddRacer, CanAddRacer);
        GoBackCommand = new RelayCommand(GoBack,     CanGoBack);
        StartAllCommand = new RelayCommand(StartAll, CanStartAll);
        StopAllCommand = new RelayCommand(StopAll,   CanStopAll);

        Racers.CollectionChanged += RacersCollectionChanged;

        LoadRacers();
    }

    #endregion

    #region Events And Handlers

    /// <summary>
    ///     Handles the <see cref="CollectionChanged" /> event for Lap collection of each Racer.
    ///     Whenever any collection changes it will recalculate the Race profit and retrieve the best Lap in the race.
    /// </summary>
    private void RacerLapsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add && e.Action != NotifyCollectionChangedAction.Remove)
            return;
        UpdateRaceProfit();
        GetBestRaceLap();
    }

    /// <summary>
    ///     Handles the <see cref="CollectionChanged" /> event for the Racers collection.
    ///     Whenever the collection changes it will recalculate the Race profit and retrieve the best Lap in the race.
    /// </summary>
    private void RacersCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add && e.Action != NotifyCollectionChangedAction.Remove)
            return;
        UpdateRaceProfit();
        GetBestRaceLap();
    }

    #endregion

    /// <summary>
    ///     Indicates whether timers for all racers can be stopped
    /// </summary>
    /// <param name="arg">Not used</param>
    private bool CanStopAll(object? arg) => Racers.Any(racer => racer is { Started: true, Finished: false });

    /// <summary>
    ///     Stops timers for all racers
    /// </summary>
    /// <param name="obj">Not used</param>
    private void StopAll(object? obj)
    {
        foreach (var racer in Racers)
        {
            if (racer is { Started: true, Finished: false })
                racer.StopTimerCommand.Execute(null);
        }
    }

    /// <summary>
    ///     Indicates whether timers for all Racers can be started
    /// </summary>
    /// <param name="arg">Not used</param>
    private bool CanStartAll(object? arg) => Racers.Any(racer => racer is { Started: false, Finished: false });

    /// <summary>
    ///     Starts timers for all Racers
    /// </summary>
    /// <param name="obj">Not used</param>
    private void StartAll(object? obj)
    {
        foreach (var racer in Racers)
        {
            if (racer is { Started: false, Finished: false })
                racer.StartTimerCommand.Execute(null);
        }
    }

    /// <summary>
    ///     Gets the best Lap in the Race
    /// </summary>
    private void GetBestRaceLap()
    {
        RaceModel.RaceLapRecord = Racers.SelectMany(racer => racer.Laps).MinBy(lap => lap.LapTime)?.LapTime ?? TimeSpan.Zero;

        if (!RaceModel.IsRecordBeaten)
            RaceModel.IsRecordBeaten = RaceModel.RaceLapRecord < RaceModel.AllTimeLapRecord;
    }

    /// <summary>
    ///     Recalculates the Race profit
    /// </summary>
    private void UpdateRaceProfit()
    {
        float totalIncome = RaceModel.SignUpFee * Racers.Count;
        int totalLaps = Racers.Sum(racer => racer.Laps.Count);
        float totalExpense = totalLaps * RaceModel.PetrolCostPerLap;

        RaceModel.RaceProfit = totalIncome - totalExpense;
    }

    /// <summary>
    ///     Indicates whether the user can navigate back to the Home page
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns>Always true</returns>
    private static bool CanGoBack(object? arg) => true;

    /// <summary>
    ///     Navigates back to the Home page
    /// </summary>
    /// <param name="obj">Not used</param>
    private void GoBack(object? obj)
    {
        foreach (var racer in Racers)
        {
            racer.StopTimerCommand.Execute(null);
            racer.Laps.CollectionChanged -= RacerLapsCollectionChanged;
        }

        Racers.CollectionChanged -= RacersCollectionChanged;
        _navigationService.NavigateTo<HomeViewModel>();
    }

    /// <summary>
    ///     Indicates whether a new Racer can be added to the Race
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns>Always true</returns>
    private static bool CanAddRacer(object? arg) => true;

    /// <summary>
    ///     Opens a dialog for adding a new Racer
    /// </summary>
    /// <param name="obj">Not used</param>
    private async void AddRacer(object? obj)
    {
        await _dialogService.DisplayDialogAsync<AddRacerViewModel, RaceModel>(RaceModel);
        LoadRacers();
    }

    /// <summary>
    ///     Loads data for the Race from the persistent storage
    /// </summary>
    private void LoadRacers()
    {
        var raceRacers = _racersQuery.GetRacersForRace(RaceModel.RaceId).ToArray();

        var racersToRemove = Racers.Where(r => raceRacers.All(rr => rr.Id != r.Racer.RacerId)).ToArray();
        for (int i = 0; i < racersToRemove.Length; i++)
        {
            var racer = racersToRemove[i];
            racer.StopTimerCommand.Execute(null);
            racer.Laps.CollectionChanged -= RacerLapsCollectionChanged;
            Racers.Remove(racer);
        }

        foreach (var racerDto in raceRacers)
        {
            var racer = Racers.FirstOrDefault(r => r.Racer.RacerId == racerDto.Id);
            if (racer is not null)
                continue;

            var racerModel = new RacerModel
            {
                RacerId = racerDto.Id,
                RacerName = racerDto.Name,
                Age = racerDto.Age
            };
            var racerViewModel = _createRacerViewModel(RaceModel, racerModel);
            racerViewModel.Laps.CollectionChanged += RacerLapsCollectionChanged;
            Racers.Add(racerViewModel);
        }

        UpdateRaceProfit();
        GetBestRaceLap();
    }
}