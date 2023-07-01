using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RaceDay.Domain.Interfaces;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

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

    public bool DisplayAsKilometers
    {
        get => _displayAsKilometers;
        set => SetField(ref _displayAsKilometers, value);
    }

    public ObservableCollection<DaySummaryModel> RaceDays { get; } = new();

    public DaySummaryModel? SelectedRaceDay
    {
        get => _selectedRaceDay;
        set => SetField(ref _selectedRaceDay, value);
    }

    public string ViewTitle
    {
        get => _viewTitle;
        private set => SetField(ref _viewTitle, value);
    }

    public ICommand CreateRaceDayCommand { get; }

    #endregion

    #region Constructors

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

    private async void CreateRaceDay(object? obj)
    {
        var newRaceDay = new NewRaceDayModel();
        var result =
            await _dialogService.DisplayDialogAsync<NewRaceDayViewModel, NewRaceDayModel, DaySummaryModel>(newRaceDay);
        if (result != null)
            RaceDays.Add(result);
    }

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