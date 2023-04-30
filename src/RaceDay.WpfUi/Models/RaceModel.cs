using System;
using System.Collections.ObjectModel;

namespace RaceDay.WpfUi.Models;

public class RaceModel
{
    public RaceModel(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; init; }
    public RaceDayModel? RaceDay { get; set; }

    public DateTime RaceDate { get; set; }
    public ObservableCollection<RaceLapModel> Laps { get; set; } = new();
}