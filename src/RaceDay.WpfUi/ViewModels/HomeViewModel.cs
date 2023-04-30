using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private readonly NavigationService _navigationService;

    #region Properties

    public RaceDaySummaryViewModel RaceDaySummaryViewModel { get; }
    public RaceDayRacesViewModel RaceDayRacesViewModel { get; }
    public CreateRaceDayViewModel CreateRaceDayViewModel { get; }

    public ICommand CreateNewRaceDayCommand { get; set; }

    #endregion

    #region Constructors

    [Obsolete("Design time only", true)]
    public HomeViewModel()
    {
    }

    public HomeViewModel(RaceDaySummaryViewModel raceDaySummaryViewModel, RaceDayRacesViewModel raceDayRacesViewModel, NavigationService navigationService)
    {
        _navigationService = navigationService;
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
        _navigationService.DisplayDialog<CreateRaceDayViewModel>();
    }

    private static bool CanCreateRaceDay(object? arg) => true;

    #endregion
}