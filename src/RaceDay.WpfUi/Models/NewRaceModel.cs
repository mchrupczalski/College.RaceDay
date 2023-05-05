using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

public class NewRaceModel : ObservableObject
{
    #region Fields

    private DateTime _raceDate;

    #endregion

    #region Properties

    public int RaceDayId { get; init; }
    public string? RaceDayName { get; init; }

    public DateTime RaceDate
    {
        get => _raceDate;
        set => SetField(ref _raceDate, value);
    }

    #endregion
}