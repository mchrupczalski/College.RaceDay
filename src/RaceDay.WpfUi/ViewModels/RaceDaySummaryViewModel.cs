using System;
using System.Collections.ObjectModel;
using System.Linq;
using RaceDay.Core.Entities;
using RaceDay.Core.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RaceDaySummaryViewModel : ViewModelBase
{
    #region Fields

    private readonly IRepository<LapEntity> _lapRepo;
    private readonly IRepository<RaceDayEntity> _raceDayRepo;
    private readonly IRepository<RaceLapEntity> _raceLapRepo;
    private readonly IRepository<RaceEntity> _raceRepo;
    private readonly IRepository<RacerEntity> _racerRepo;
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

    [Obsolete("Design-time only", true)]
    public RaceDaySummaryViewModel()
    {
        RaceDays.Add(new RaceDaySummaryModel
        {
            Id = new Guid(),
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
            Id = new Guid(),
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

    public RaceDaySummaryViewModel(IRepository<RaceDayEntity> raceDayRepo,
                                   IRepository<RaceEntity> raceRepo,
                                   IRepository<LapEntity> lapRepo,
                                   IRepository<RaceLapEntity> raceLapRepo,
                                   IRepository<RacerEntity> racerRepo)
    {
        _raceDayRepo = raceDayRepo;
        _raceRepo = raceRepo;
        _lapRepo = lapRepo;
        _raceLapRepo = raceLapRepo;
        _racerRepo = racerRepo;
    }

    #endregion

    public void LoadData()
    {
        var laps = _lapRepo.GetAll()
                           .Result.ToArray();
        var raceDays = _raceDayRepo.GetAll()
                                   .Result.ToArray();
        var races = _raceRepo.GetAll()
                             .Result.ToArray();
        var raceLaps = _raceLapRepo.GetAll()
                                   .Result.ToArray();
        var racers = _racerRepo.GetAll()
                               .Result.ToArray();

        RaceDays.Clear();

        foreach (var raceDay in raceDays)
        {
            var raceDayLap = laps.FirstOrDefault(l => l.RaceDayGuid == raceDay.Guid);
            var raceDayRaces = races.Where(r => r.RaceDayGuid == raceDay.Guid)
                                    .ToArray();
            var raceDayRaceLaps = raceLaps.Where(rl => raceDayRaces.Any(r => r.Guid == rl.RaceGuid));
            var recordLap = raceDayRaceLaps.MinBy(rl => rl.LapTimeSeconds);
            var recordLapRacer = racers.FirstOrDefault(r => recordLap != null && r.Guid == recordLap.RacerGuid);

            var model = new RaceDaySummaryModel
            {
                Id = raceDay.Guid,
                Name = raceDay.Name,
                SignUpFee = raceDay.SignUpFee,
                LapDistanceKilometers = raceDayLap?.LapDistanceKm ?? 0,
                PetrolCostPerLap = raceDayLap?.PetrolCostPerLap ?? 0,
                TotalRaces = raceDayRaces.Length,
                RecordLap = TimeSpan.FromSeconds(recordLap?.LapTimeSeconds ?? 0),
                RecordHolderName = recordLapRacer?.Name ?? "N/A"
                // ToDo: AverageProfit
            };

            RaceDays.Add(model);
        }

        bool hasSelectedRaceDay = SelectedRaceDay != null && RaceDays.Any(r => r.Id == SelectedRaceDay.Id);
        if (SelectedRaceDay == null) SelectedRaceDay = RaceDays.FirstOrDefault();
        else if (SelectedRaceDay != null && !hasSelectedRaceDay) SelectedRaceDay = RaceDays.FirstOrDefault();
    }
}