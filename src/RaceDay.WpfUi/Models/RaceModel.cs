using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

/// <summary>
///     A model of the Race
/// </summary>
public class RaceModel : ObservableObject
{
    #region Fields

    private bool _isRecordBeaten;
    private TimeSpan _raceLapRecord;
    private float _raceProfit;

    #endregion

    #region Properties

    /// <summary>
    ///     The unique identifier of the Race
    /// </summary>
    public int RaceId { get; init; }

    /// <summary>
    ///     The unique identifier of the Race Day
    /// </summary>
    public int RaceDayId { get; init; }

    /// <summary>
    ///     The name of the Race Day
    /// </summary>
    public string? RaceDayName { get; init; }

    /// <summary>
    ///     The Race Day sign up fee
    /// </summary>
    public float SignUpFee { get; init; }

    /// <summary>
    ///     The Lap distance in kilometers
    /// </summary>
    public float LapDistanceMiles { get; init; }

    /// <summary>
    ///     The All Time Lap Record
    /// </summary>
    public TimeSpan AllTimeLapRecord { get; init; }

    /// <summary>
    ///     The record Lap Time of the Race
    /// </summary>
    public TimeSpan RaceLapRecord
    {
        get => _raceLapRecord;
        set => SetField(ref _raceLapRecord, value);
    }

    /// <summary>
    ///     Indicates whether the <see cref="AllTimeLapRecord" /> has been beaten
    /// </summary>
    public bool IsRecordBeaten
    {
        get => _isRecordBeaten;
        set => SetField(ref _isRecordBeaten, value);
    }

    /// <summary>
    ///     The estimated petrol cost per lap
    /// </summary>
    public float PetrolCostPerLap { get; init; }

    /// <summary>
    ///     The total profit made from the race
    /// </summary>
    public float RaceProfit
    {
        get => _raceProfit;
        set => SetField(ref _raceProfit, value);
    }

    #endregion
}