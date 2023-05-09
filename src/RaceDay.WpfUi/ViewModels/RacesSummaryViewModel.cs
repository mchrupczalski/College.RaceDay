using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class RacesSummaryViewModel : ViewModelBase
{
    #region Fields

    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    private readonly RaceSummaryQuery _raceDayRacesQuery;
    private readonly RaceViewModel.CreateRaceViewModel _createRaceViewModel;
    private RaceSummaryModel? _selectedRace;
    private string _viewTitle = "Race Day Races";
    private DaySummaryModel? _daySummaryModel;

    #endregion

    #region Properties

    /// <summary>
    ///     A collection of <see cref="RaceSummaryModel" /> objects.
    ///     Each object contains summarized details about a race.
    /// </summary>
    public ObservableCollection<RaceSummaryModel> Races { get; } = new();

    /// <summary>
    ///     The currently selected <see cref="RaceSummaryModel" />.
    /// </summary>
    public RaceSummaryModel? SelectedRace
    {
        get => _selectedRace;
        set => SetField(ref _selectedRace, value);
    }

    public ICommand CreateRaceCommand { get; }
    public ICommand DisplayRaceCommand { get; }

    /// <summary>
    ///     The title of the view, updated when Race Day is selected.
    /// </summary>
    public string ViewTitle
    {
        get => _viewTitle;
        private set => SetField(ref _viewTitle, value);
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design-time only", true)]
    public RacesSummaryViewModel()
    {
        Races.Add(new RaceSummaryModel
        {
            RaceDayId = 1,
            RaceDate = new DateTime(2022, 03, 12),
            TotalRacers = 15,
            TotalLaps = 103,
            BestLapTime = TimeSpan.FromMinutes(1.5),
            BestLapTimeHolder = "John Doe",
            TotalIncome = 5000,
            TotalExpenses = 3000,
            TotalProfit = 2000
        });

        Races.Add(new RaceSummaryModel
        {
            RaceDayId = 1,
            RaceDate = new DateTime(2022, 03, 12),
            TotalRacers = 15,
            TotalLaps = 103,
            BestLapTime = TimeSpan.FromMinutes(1.5),
            BestLapTimeHolder = "John Doe",
            TotalIncome = 5000,
            TotalExpenses = 3000,
            TotalProfit = 2000
        });
    }
#pragma warning restore CS8618

    /// <summary>
    ///     Initializes a new instance of the <see cref="RacesSummaryViewModel" /> class.
    /// </summary>
    /// <param name="dialogService">The dialog service.</param>
    /// <param name="navigationService">The navigation service.</param>
    /// <param name="raceDayRacesQuery">Query to select all Races and their summary details for a given Race Day.</param>
    public RacesSummaryViewModel(DialogService dialogService, NavigationService navigationService, RaceSummaryQuery raceDayRacesQuery, RaceViewModel.CreateRaceViewModel createRaceViewModel)
    {
        _dialogService = dialogService;
        _navigationService = navigationService;
        _raceDayRacesQuery = raceDayRacesQuery;
        _createRaceViewModel = createRaceViewModel;

        CreateRaceCommand = new RelayCommand(CreateRace,   CanCreateRace);
        DisplayRaceCommand = new RelayCommand(DisplayRace, CanDisplayRace);
    }

    #endregion

    /// <summary>
    ///     Indicates whether race details can be displayed.
    /// </summary>
    /// <param name="arg">Not used</param>
    private bool CanDisplayRace(object? arg) => SelectedRace != null;

    private void DisplayRace(object? obj)
    {
        if (SelectedRace == null || _daySummaryModel == null ) return;
        
        var raceModel = new RaceModel()
        {
            RaceId = SelectedRace.RaceId,
            RaceDayId = _daySummaryModel.RaceDayId,
            RaceDayName = _daySummaryModel.RaceDayName,
            SignUpFee = _daySummaryModel.SignUpFee,
            AllTimeLapRecord = SelectedRace.BestLapTime,
            PetrolCostPerLap = _daySummaryModel.PetrolCostPerLap
        };

        var raceVm = _createRaceViewModel(raceModel);
        _navigationService.NavigateTo(raceVm);
    }

    /// <summary>
    ///     Indicates whether the user can create a new Race.
    /// </summary>
    /// <param name="arg">Not used</param>
    private bool CanCreateRace(object? arg) => _daySummaryModel != null;


    /// <summary>
    ///     A method to trigger the new Race creation workflow.
    /// </summary>
    /// <param name="obj">Not used</param>
    private async void CreateRace(object? obj)
    {
        if(_daySummaryModel == null) return;
        var newRace = new NewRaceModel(){RaceDayId = _daySummaryModel.RaceDayId, RaceDayName = _daySummaryModel.RaceDayName, RaceDate = DateTime.Now};
        var result = await _dialogService.DisplayDialogAsync<NewRaceViewModel, NewRaceModel, RaceSummaryModel>(newRace);
        if (result != null) Races.Add(result);

        var raceModel = new RaceModel()
        {
            RaceId = result.RaceId,
            RaceDayId = _daySummaryModel.RaceDayId,
            RaceDayName = _daySummaryModel.RaceDayName,
            SignUpFee = _daySummaryModel.SignUpFee,
            AllTimeLapRecord = result.BestLapTime,
        };

        var raceVm = _createRaceViewModel(raceModel);
        _navigationService.NavigateTo(raceVm);
    }

    /// <summary>
    ///     Loads Race Details for given Race Day.
    /// </summary>
    /// <param name="raceDayId">The Race Day Id.</param>
    public void LoadRaceDayRaces(DaySummaryModel raceDay)
    {
        _daySummaryModel = raceDay;
        
        Races.Clear();
        var raceDtos = _raceDayRacesQuery.GetAll(raceDay.RaceDayId);

        foreach (var race in raceDtos)
        {
            Races.Add(new RaceSummaryModel
            {
                RaceId = race.RaceId,
                RaceDayId = race.RaceDayId,
                RaceDate = DateTime.TryParse(race.RaceDate, out var raceDate) ? raceDate : null,
                TotalRacers = race.TotalRacers,
                TotalLaps = race.TotalLaps,
                BestLapTime = race.BestLapTime,
                BestLapTimeHolder = race.BestLapTimeHolder,
                TotalIncome = race.TotalIncome,
                TotalExpenses = race.TotalExpense,
                TotalProfit = race.TotalIncome - race.TotalExpense
            });
        }
    }

    /// <summary>
    ///     Updates the View Title.
    ///     It will append " - Races" to the title.
    /// </summary>
    /// <param name="title">The title to set.</param>
    public void UpdateViewTitle(string? title) => ViewTitle = $"{title} - Races";
}