using System;
using System.Windows;
using System.Windows.Input;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase
{
    #region Properties

    public RaceDaySummaryViewModel RaceDaySummaryViewModel { get; }
    public RaceDayRacesViewModel RaceDayRacesViewModel { get; }

    public ICommand CreateNewRaceDayCommand { get; set; }

    #endregion

    #region Constructors

    [Obsolete("Design time only", true)]
    public HomeViewModel()
    {
    }

    public HomeViewModel(RaceDaySummaryViewModel raceDaySummaryViewModel, RaceDayRacesViewModel raceDayRacesViewModel)
    {
        RaceDaySummaryViewModel = raceDaySummaryViewModel;
        RaceDayRacesViewModel = raceDayRacesViewModel;
        
        CreateNewRaceDayCommand = new RelayCommand(CreateRaceDay, CanCreateRaceDay);

        raceDaySummaryViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName != nameof(RaceDaySummaryViewModel.SelectedRaceDay)) return;
            if (raceDaySummaryViewModel.SelectedRaceDay != null) raceDayRacesViewModel.LoadRaceDayRaces(raceDaySummaryViewModel.SelectedRaceDay.Guid);
        };
    }

    private void CreateRaceDay(object? obj)
    {
        var w = new Window();
        var vm = new CreateRaceDayViewModel();
        w.Content = vm;
        w.ShowDialog();
    }

    private static bool CanCreateRaceDay(object? arg) => true;

    #endregion
}