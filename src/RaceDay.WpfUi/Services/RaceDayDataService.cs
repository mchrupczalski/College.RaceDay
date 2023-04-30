using System;
using System.Collections.Generic;
using System.Linq;
using RaceDay.Core.Entities;
using RaceDay.Core.Interfaces;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.Services;

public class RaceDayDataService
{
    #region Fields

    private readonly IRepository<LapEntity> _lapRepo;
    private readonly IRepository<RaceDayEntity> _raceDayRepo;
    private readonly IRepository<RaceLapEntity> _raceLapRepo;
    private readonly IRepository<RaceEntity> _raceRepo;
    private readonly IRepository<RacerEntity> _racerRepo;

    #endregion

    #region Constructors

    public RaceDayDataService(IRepository<RaceDayEntity> raceDayRepo,
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

    public IEnumerable<RaceDayModel> GetRaceDays()
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

        var racerModels = racers.Select(racer => new RacerModel(racer.Guid) { Name = racer.Name })
                                .ToArray();

        var raceDayModels = new List<RaceDayModel>();
        foreach (var raceDay in raceDays)
        {
            var raceDayLap = laps.FirstOrDefault(l => l.RaceDayGuid == raceDay.Guid);
            var raceDayModel = new RaceDayModel(raceDay.Guid)
            {
                Name = raceDay.Name,
                SignUpFee = raceDay.SignUpFee,
            };
            raceDayModel.Races.Clear();

            var raceDayRaces = races.Where(r => r.RaceDayGuid == raceDay.Guid);
            foreach (var raceDayRace in raceDayRaces)
            {
                var raceModel = new RaceModel(raceDayRace.Guid);
                raceModel.Laps.Clear();

                var raceDayLaps = raceLaps.Where(rl => rl.RaceGuid == raceDayRace.Guid);
                foreach (var lap in raceDayLaps)
                {
                    var racer = racerModels.FirstOrDefault(r => r.Guid == lap.RacerGuid);
                    var lapModel = new RaceLapModel(lap.Guid)
                    {
                        LapTime = TimeSpan.FromSeconds(lap.LapTimeSeconds),
                        Racer = racer
                    };

                    raceModel.Laps.Add(lapModel);
                }

                raceDayModel.Races.Add(raceModel);
            }

            raceDayModels.Add(raceDayModel);
        }

        return raceDayModels;
    }

    public void CreateRaceDay(RaceDayModel raceDayModel)
    {
        var entity = new RaceDayEntity()
        {
            Guid = raceDayModel.Guid,
            Name = raceDayModel.Name,
            SignUpFee = raceDayModel.SignUpFee
        };
    }
}