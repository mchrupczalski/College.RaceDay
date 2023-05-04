using System;
using System.Collections.ObjectModel;

namespace RaceDay.WpfUi.Models;

public class RaceRacerModel
{
    public int RaceDayId { get; set; }
    public int RaceNumber { get; set; }
    public int RacerId { get; set; }
    public string? RacerName { get; set; }
    public byte Age { get; set; }
    
}