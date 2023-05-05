using System;
using System.Collections.ObjectModel;
using System.Linq;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RaceDaySummaryViewModel : ViewModelBase
{
    #region Fields

    private readonly DaySummaryQuery _raceDaySummaryQuery;

    private bool _displayAsKilometers = true;
    private RaceDaySummaryModel? _selectedRaceDay;

    #endregion

    #region Properties

    public bool DisplayAsKilometers
    {
        get => _displayAsKilometers;
        set => SetField(ref _displayAsKilometers, value);
    }

    public ObservableCollection<RaceDaySummaryModel> RaceDays { get; } = new();

    public RaceDaySummaryModel? SelectedRaceDay
    {
        get => _selectedRaceDay;
        set => SetField(ref _selectedRaceDay, value);
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design-time only", true)]
    public RaceDaySummaryViewModel()
    {
        RaceDays.Add(new RaceDaySummaryModel
        {
            RaceDayId = 1,
            Name = "Test Race Day 1",
            SignUpFee = 300,
            LapDistanceKilometers = 5,
            PetrolCostPerLap = 10,
            TotalRaces = 3,
            RecordLap = TimeSpan.FromMinutes(1.5),
            RecordHolderName = "Test Racer",
            AverageProfit = 5000
        });

        RaceDays.Add(new RaceDaySummaryModel
        {
            RaceDayId = 2,
            Name = "Test Race Day 2",
            SignUpFee = 3000,
            LapDistanceKilometers = 15,
            PetrolCostPerLap = 100,
            TotalRaces = 300,
            RecordLap = TimeSpan.FromMinutes(1.8),
            RecordHolderName = "Test Racer",
            AverageProfit = 50000
        });
    }
#pragma warning restore CS8618

    public RaceDaySummaryViewModel(DaySummaryQuery raceDaySummaryQuery) => _raceDaySummaryQuery = raceDaySummaryQuery;

    #endregion

    public void LoadData()
    {
        RaceDays.Clear();
        var dtos = _raceDaySummaryQuery.GetAll();

        foreach (var dto in dtos)
        {
            var model = new RaceDaySummaryModel
            {
                RaceDayId = dto.RaceDayId,
                Name = dto.RaceDayName,
                SignUpFee = dto.SignUpFee,
                LapDistanceKilometers = dto.LapDistanceKm,
                PetrolCostPerLap = dto.PetrolCostPerLap,
                TotalRaces = dto.TotalRaces,
                RecordLap = dto.RecordLapTime,
                RecordHolderName = dto.RecordHolderName
            };
            RaceDays.Add(model);
        }

        bool hasSelectedRaceDay = SelectedRaceDay != null && RaceDays.Any(r => r.RaceDayId == SelectedRaceDay.RaceDayId);
        if (SelectedRaceDay == null) SelectedRaceDay = RaceDays.FirstOrDefault();
        else if (SelectedRaceDay != null && !hasSelectedRaceDay) SelectedRaceDay = RaceDays.FirstOrDefault();
    }
}