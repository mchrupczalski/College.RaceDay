using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class DaySummaryViewModel : ViewModelBase
{
    #region Fields

    private readonly DialogService _dialogService;
    private readonly DaySummaryQuery _raceDaySummaryQuery;

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

#pragma warning disable CS8618
    [Obsolete("Design-time only", true)]
    public DaySummaryViewModel()
    {
        RaceDays.Add(new DaySummaryModel
        {
            RaceDayId = 1,
            RaceDayName = "Test Race Day 1",
            SignUpFee = 300,
            LapDistanceKilometers = 5,
            PetrolCostPerLap = 10,
            TotalRaces = 3,
            RecordLapTime = TimeSpan.FromMinutes(1.5),
            RecordHolderName = "Test Racer",
            TotalIncome = 1000f,
            TotalCost = 100f
        });

        RaceDays.Add(new DaySummaryModel
        {
            RaceDayId = 2,
            RaceDayName = "Test Race Day 2",
            SignUpFee = 3000,
            LapDistanceKilometers = 15,
            PetrolCostPerLap = 100,
            TotalRaces = 300,
            RecordLapTime = TimeSpan.FromMinutes(1.8),
            RecordHolderName = "Test Racer",
            TotalIncome = 1000f,
            TotalCost = 100f
        });
    }
#pragma warning restore CS8618

    public DaySummaryViewModel(DialogService dialogService, DaySummaryQuery raceDaySummaryQuery)
    {
        _dialogService = dialogService;
        _raceDaySummaryQuery = raceDaySummaryQuery;
        
        CreateRaceDayCommand = new RelayCommand(CreateRaceDay, CanCreateRaceDay);
    }

    /// <summary>
    ///     Indicates whether user can create new Race Day.
    /// </summary>
    /// <param name="arg">Not Used</param>
    private static bool CanCreateRaceDay(object? arg) => true;

    private void CreateRaceDay(object? obj)
    {
        throw new NotImplementedException();
    }

    #endregion

    public void LoadData()
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
                RecordLapTime = dto.RecordLapTime,
                RecordHolderName = dto.RecordHolderName,
                TotalIncome = dto.TotalIncome,
                TotalCost = dto.TotalCost
            };
            RaceDays.Add(model);
        }

        // when data is loaded check if previously selected day is in the collection and select it again
        bool hasSelectedRaceDay = SelectedRaceDay != null && RaceDays.Any(r => r.RaceDayId == selectedDayId);
        if(hasSelectedRaceDay) SelectedRaceDay = RaceDays.FirstOrDefault(r => r.RaceDayId == selectedDayId);
        if(SelectedRaceDay == null && RaceDays.Any()) SelectedRaceDay = RaceDays.First();
    }
}