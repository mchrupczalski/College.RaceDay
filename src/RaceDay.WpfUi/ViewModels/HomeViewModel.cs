using System.Collections.ObjectModel;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase
{
    public bool DisplayAsKilometers { get; set; } = true;
    public ObservableCollection<RaceDayModel> RaceDays { get; } = new();
    
}