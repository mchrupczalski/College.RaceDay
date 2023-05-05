using System;
using System.Collections.ObjectModel;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class RaceDayRacesViewModel : ViewModelBase
{
    #region Fields

    private readonly RaceDayRacesQuery _raceDayRacesQuery;
    private string _viewTitle = "Race Day Name - Races";

    #endregion

    #region Properties

    public ObservableCollection<RaceDayRaceModel> Races { get; } = new();

    public string ViewTitle
    {
        get => _viewTitle;
        private set => SetField(ref _viewTitle, value);
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design-time only", true)]
    public RaceDayRacesViewModel()

    {
        Races.Add(new RaceDayRaceModel
        {
            RaceDayId = 1,
            RaceNumber = 1,
            RaceDate = new DateTime(2022, 03, 12),
            NumberOfRacers = 15,
            TotalNumberOfLaps = 103,
            BestLapTime = TimeSpan.FromMinutes(1.5),
            BestLapTimeHolder = "John Doe",
            TotalIncome = 5000,
            TotalExpenses = 3000,
            TotalProfit = 2000
        });

        Races.Add(new RaceDayRaceModel
        {
            RaceDayId = 1,
            RaceNumber = 2,
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
#pragma warning restore CS8618

    public RaceDayRacesViewModel(RaceDayRacesQuery raceDayRacesQuery) => _raceDayRacesQuery = raceDayRacesQuery;

    #endregion

    public void LoadRaceDayRaces(int raceDayId)
    {
        Races.Clear();
        var raceDtos = _raceDayRacesQuery.GetAll(raceDayId);

        foreach (var race in raceDtos)
        {
            Races.Add(new RaceDayRaceModel
            {
                RaceDayId = race.RaceDayId,
                RaceNumber = race.RaceNumber,
                RaceDate = race.RaceDate,
                NumberOfRacers = race.NumberOfRacers,
                TotalNumberOfLaps = race.TotalNumberOfLaps,
                BestLapTime = race.BestLapTime,
                BestLapTimeHolder = race.BestLapTimeHolder,
                TotalIncome = race.TotalIncome,
                TotalExpenses = race.TotalExpenses,
                TotalProfit = race.TotalProfit
            });
        }
    }

    public void UpdateViewTitle(string? title) => ViewTitle = $"{title} - Races";
}