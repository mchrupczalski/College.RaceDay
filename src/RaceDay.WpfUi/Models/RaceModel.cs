using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Models;

public class RaceModel : ObservableObject
{
    #region Fields

    private bool _isRecordBeaten;
    private TimeSpan _raceLapRecord;
    private float _raceProfit;

    #endregion

    #region Properties

    public int RaceId { get; init; }
    public int RaceDayId { get; init; }
    public string? RaceDayName { get; init; }
    public float SignUpFee { get; init; }
    public TimeSpan AllTimeLapRecord { get; init; }

    public TimeSpan RaceLapRecord
    {
        get => _raceLapRecord;
        set => SetField(ref _raceLapRecord, value);
    }

    public bool IsRecordBeaten
    {
        get => _isRecordBeaten;
        set
        {
            if (value == _isRecordBeaten) return;
            _isRecordBeaten = value;
            OnPropertyChanged();
        }
    }

    public float PetrolCostPerLap { get; init; }

    public float RaceProfit
    {
        get => _raceProfit;
        set => SetField(ref _raceProfit, value);
    }

    #endregion
}