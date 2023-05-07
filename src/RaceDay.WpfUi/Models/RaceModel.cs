using System;
using System.Collections.ObjectModel;

namespace RaceDay.WpfUi.Models;

public class RaceModel
{
    public int RaceId { get; set; }
    public int RaceDayId { get; set; }
    public string? RaceDayName { get; set; }
    public float SignUpFee { get; set; }
    public TimeSpan AllTimeLapRecord { get; set; }
    public ObservableCollection<RacerModel> Racers { get; } = new();
    public TimeSpan RaceLapRecord { get; set; }
    public bool IsRecordBeaten { get; set; }
    public float RaceProfit { get; set; }
}