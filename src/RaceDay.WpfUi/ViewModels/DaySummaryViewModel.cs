using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RaceDay.Domain.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

/// <summary>
///     A view model providing data and logic for the Day Summary view
/// </summary>
public class DaySummaryViewModel : ViewModelBase
{
    #region Fields

    private readonly DialogService _dialogService;
    private readonly IDaySummaryQuery _raceDaySummaryQuery;

    private bool _displayAsKilometers = true;
    private DaySummaryModel? _selectedRaceDay;
    private string _viewTitle = "Race Day Summary";

    #endregion

    #region Properties

    /// <summary>
    ///     Indicates whether to display the distance in miles or kilometers
    /// </summary>
    public bool DisplayAsKilometers
    {
        get => _displayAsKilometers;
        set => SetField(ref _displayAsKilometers, value);
    }

    /// <summary>
    ///     A collection of Race Day summaries
    /// </summary>
    public ObservableCollection<DaySummaryModel> RaceDays { get; } = new();

    /// <summary>
    ///     The Race Day selected in the UI
    /// </summary>
    public DaySummaryModel? SelectedRaceDay
    {
        get => _selectedRaceDay;
        set => SetField(ref _selectedRaceDay, value);
    }

    /// <summary>
    ///     The title of the view
    /// </summary>
    public string ViewTitle
    {
        get => _viewTitle;
        private set => SetField(ref _viewTitle, value);
    }

    /// <summary>
    ///     A command for creating Race Day
    /// </summary>
    public ICommand CreateRaceDayCommand { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Creates a new instance of the <see cref="DaySummaryViewModel" /> class
    /// </summary>
    /// <param name="dialogService">The dialog service</param>
    /// <param name="raceDaySummaryQuery">A query for Race Day summaries</param>
    public DaySummaryViewModel(DialogService dialogService, IDaySummaryQuery raceDaySummaryQuery)
    {
        _dialogService = dialogService;
        _raceDaySummaryQuery = raceDaySummaryQuery;

        CreateRaceDayCommand = new RelayCommand(CreateRaceDay, CanCreateRaceDay);
    }

    #endregion

    /// <summary>
    ///     Indicates whether user can create new Race Day.
    /// </summary>
    /// <param name="arg">Not Used</param>
    private static bool CanCreateRaceDay(object? arg) => true;

    /// <summary>
    ///     An action performed to create a new Race Day
    /// </summary>
    /// <param name="obj"></param>
    private async void CreateRaceDay(object? obj)
    {
        var newRaceDay = new NewRaceDayModel();
        var result = await _dialogService.DisplayDialogAsync<NewRaceDayViewModel, NewRaceDayModel, DaySummaryModel>(newRaceDay);
        if (result != null)
            RaceDays.Add(result);
    }

    /// <summary>
    ///     Loads data from the persistence layer
    /// </summary>
    /// <param name="changeSelectedDay">Indicates whether to change the selected day</param>
    public void LoadData(bool changeSelectedDay = true)
    {
        int? selectedDayId = SelectedRaceDay?.RaceDayId;

        RaceDays.Clear();
        var dtos = _raceDaySummaryQuery.GetAll();

        foreach (var dto in dtos)
        {
            var model = new DaySummaryModel
            {
                RaceDayId = dto.RaceDayId,
                RaceDayName = dto.RaceDayName,
                SignUpFee = dto.SignUpFee,
                LapDistanceKilometers = dto.LapDistanceKm,
                PetrolCostPerLap = dto.PetrolCostPerLap,
                TotalRaces = dto.TotalRaces,
                RecordLapTime = TimeSpan.FromSeconds(dto.RecordLapTime),
                RecordHolderName = dto.RecordHolderName,
                TotalIncome = dto.TotalIncome,
                TotalCost = dto.TotalCost
            };
            RaceDays.Add(model);
        }

        if (!changeSelectedDay)
        {
            SelectedRaceDay = RaceDays.FirstOrDefault(r => r.RaceDayId == selectedDayId);
            return;
        }

        // when data is loaded check if previously selected day is in the collection and select it again
        bool hasSelectedRaceDay = SelectedRaceDay != null && RaceDays.Any(r => r.RaceDayId == selectedDayId);
        if (hasSelectedRaceDay)
            SelectedRaceDay = RaceDays.FirstOrDefault(r => r.RaceDayId == selectedDayId);
        else if (SelectedRaceDay == null && RaceDays.Any())
            SelectedRaceDay = RaceDays.First();
    }
}