using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

/// <summary>
///     Model for creating new Races
/// </summary>
public class NewRaceModel : ObservableObject
{
    #region Fields

    private DateTime _raceDate;

    #endregion

    #region Properties

    /// <summary>
    ///     The Id of the Race Day for which new Race is being created
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The name of the Race Day
    /// </summary>
    public string? RaceDayName { get; init; }

    /// <summary>
    ///     The date of the Race
    /// </summary>
    public DateTime RaceDate
    {
        get => _raceDate;
        set => SetField(ref _raceDate, value);
    }

    #endregion
}