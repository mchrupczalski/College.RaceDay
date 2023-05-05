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
    private RaceSummaryModel? _selectedRace;
    private string _viewTitle = "Race Day Races";

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
    public RacesSummaryViewModel(DialogService dialogService, NavigationService navigationService, RaceSummaryQuery raceDayRacesQuery)
    {
        _dialogService = dialogService;
        _navigationService = navigationService;
        _raceDayRacesQuery = raceDayRacesQuery;

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
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Indicates whether the user can create a new Race.
    /// </summary>
    /// <param name="arg">Not used</param>
    private static bool CanCreateRace(object? arg) => true;


    /// <summary>
    ///     A method to trigger the new Race creation workflow.
    /// </summary>
    /// <param name="obj">Not used</param>
    private void CreateRace(object? obj)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Loads Race Details for given Race Day.
    /// </summary>
    /// <param name="raceDayId">The Race Day Id.</param>
    public void LoadRaceDayRaces(int raceDayId)
    {
        Races.Clear();
        var raceDtos = _raceDayRacesQuery.GetAll(raceDayId);

        foreach (var race in raceDtos)
        {
            Races.Add(new RaceSummaryModel
            {
                RaceDayId = race.RaceDayId,
                RaceDate = race.RaceDate,
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