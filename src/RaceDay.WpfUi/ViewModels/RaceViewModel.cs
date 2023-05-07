using System;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RaceViewModel : ViewModelBase
{
    public RaceModel RaceModel { get; }

    [Obsolete("For design-time only", true)]
    public RaceViewModel()
    {
        RaceModel = new RaceModel()
        {
            RaceDayId = 1,
            RaceDayName = "Race Name 01",
            RaceId = 1,
            SignUpFee = 10.00f,
            AllTimeLapRecord = new TimeSpan(0, 0, 2, 55, 123),
            RaceLapRecord = new TimeSpan(0, 0, 3, 2, 623),
            IsRecordBeaten = true,
            RaceProfit = 100.00f,
        };
        
        RaceModel.Racers.Add(new RacerModel()
        {
            RaceDayId = 1,
            RaceNumber = 1,
            RacerId = 1,
            RacerName = "Racer Name 01",
        });
        
        RaceModel.Racers.Add(new RacerModel()
        {
            RaceDayId = 1,
            RaceNumber = 1,
            RacerId = 2,
            RacerName = "Racer Name 02",
        });
    }

    public RaceViewModel(RaceModel raceModel)
    {
        RaceModel = raceModel;
    }
}