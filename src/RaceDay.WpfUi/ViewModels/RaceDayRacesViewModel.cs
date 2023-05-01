using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using RaceDay.Core.Entities;
using RaceDay.Core.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RaceDayRacesViewModel : ViewModelBase
{
    private readonly IRepository<RaceDayEntity> _raceDayRepo;
    private readonly IRepository<RaceEntity> _raceRepo;
    private readonly IRepository<LapEntity> _lapRepo;
    private readonly IRepository<RaceLapEntity> _raceLapRepo;
    private readonly IRepository<RacerEntity> _racerRepo;
    private string _viewTitle = "Race Day Name - Races";

    public ObservableCollection<RaceDayRaceModel> Races { get;  } = new();

    public string ViewTitle
    {
        get => _viewTitle;
        private set => SetField(ref _viewTitle, value);
    }

    [Obsolete("Design-time only", true)]
    public RaceDayRacesViewModel()
    {
        Races.Add(new RaceDayRaceModel()
        {
            Guid = new Guid(),
            RaceDayGuid = new Guid(),
            RaceDate = new DateTime(2022,03,12),
            NumberOfRacers = 15,
            TotalNumberOfLaps = 103,
            BestLapTime = TimeSpan.FromMinutes(1.5),
            BestLapTimeHolder = "John Doe",
            TotalIncome = 5000,
            TotalExpenses = 3000,
            TotalProfit = 2000
        });
        
        Races.Add(new RaceDayRaceModel()
        {
            Guid = new Guid(),
            RaceDayGuid = new Guid(),
            RaceDate = new DateTime(2022, 03, 12),
            NumberOfRacers = 15,
            TotalNumberOfLaps = 103,
            BestLapTime = TimeSpan.FromMinutes(1.5),
            BestLapTimeHolder = "John Doe",
            TotalIncome = 5000,
            TotalExpenses = 3000,
            TotalProfit = 2000
        });
    }

    public RaceDayRacesViewModel(IRepository<RaceDayEntity> raceDayRepo,
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

    public void LoadRaceDayRaces(Guid raceDayGuid)
    {
        Races.Clear();
        var raceDay = _raceDayRepo.GetById(raceDayGuid)
                                  .Result;
        
        var raceLap = _lapRepo.GetAll().Result.ToArray().FirstOrDefault(l => l.RaceDayGuid == raceDayGuid);

        if (raceDay == null || raceLap == null) return;

        var races = _raceRepo.GetAll().Result.ToArray().Where(r => r.RaceDayGuid == raceDay.Guid).ToArray();
        var laps = _raceLapRepo.GetAll().Result.ToArray().Where(l => l.RaceGuid == raceDay.Guid).ToArray();
        var racers = _racerRepo.GetAll().Result.ToArray().Where(r => laps.Any(l => l.RacerGuid == r.Guid)).ToArray();
        
        foreach (var race in races)
        {
            var raceLaps = laps.Where(l => l.RaceGuid == race.Guid)
                               .ToArray();
            var bestLap = raceLaps.MinBy(rl => rl.LapTimeSeconds);
            var raceRacers = racers.Where(r => raceLaps.Any(rl => rl.RacerGuid == r.Guid))
                                   .ToArray();
            float totalIncome = raceDay.SignUpFee * raceRacers.Length;
            float totalExpenses = raceLap.PetrolCostPerLap * raceLaps.Length;
            
            Races.Add(new RaceDayRaceModel()
            {
                Guid = race.Guid,
                RaceDayGuid = race.RaceDayGuid,
                RaceDate = race.RaceDate,
                NumberOfRacers = racers.Count(r => laps.Any(l => l.RacerGuid == r.Guid && l.RaceGuid == race.Guid)),
                TotalNumberOfLaps = raceLaps.Length,
                BestLapTime =  TimeSpan.FromSeconds(bestLap?.LapTimeSeconds ?? 0),
                BestLapTimeHolder = raceRacers.FirstOrDefault(r => r.Guid == bestLap?.RacerGuid)?.Name,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                TotalProfit = totalIncome - totalExpenses
            });
        }
    }

    public void UpdateViewTitle(string? title) => ViewTitle = $"{title} - Races";
}