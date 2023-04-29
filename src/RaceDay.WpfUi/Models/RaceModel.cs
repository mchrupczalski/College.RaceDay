using System.Collections.ObjectModel;

namespace RaceDay.WpfUi.Models;

public class RaceModel
{
    public ObservableCollection<RaceLapModel> Laps { get; set; } = new();
}