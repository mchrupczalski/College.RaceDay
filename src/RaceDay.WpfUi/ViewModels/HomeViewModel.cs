using System;
using System.Windows.Input;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase, INavigableViewModel
{
    #region Fields

    private readonly NavigationService _navigationService;

    #endregion

    #region Properties

    public RaceDaySummaryViewModel RaceDaySummaryViewModel { get; }
    public RaceDayRacesViewModel RaceDayRacesViewModel { get; }

    public ICommand CreateNewRaceDayCommand { get; set; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design time only", true)]
    public HomeViewModel()

    {
    }
#pragma warning restore CS8618

    public HomeViewModel(RaceDaySummaryViewModel raceDaySummaryViewModel, RaceDayRacesViewModel raceDayRacesViewModel, NavigationService navigationService)
    {
        _navigationService = navigationService;
        RaceDaySummaryViewModel = raceDaySummaryViewModel;
        RaceDayRacesViewModel = raceDayRacesViewModel;

        CreateNewRaceDayCommand = new RelayCommand(CreateRaceDay, CanCreateRaceDay);

        raceDaySummaryViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName != nameof(RaceDaySummaryViewModel.SelectedRaceDay)) return;
            if (raceDaySummaryViewModel.SelectedRaceDay == null) return;

            raceDayRacesViewModel.LoadRaceDayRaces(raceDaySummaryViewModel.SelectedRaceDay.RaceDayId);
            raceDayRacesViewModel.UpdateViewTitle(raceDaySummaryViewModel.SelectedRaceDay.Name);
        };
    }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public void OnNavigatedTo() => RaceDaySummaryViewModel.LoadData();

    #endregion

    private void CreateRaceDay(object? obj)
    {
        _navigationService.DisplayDialog<CreateRaceDayViewModel>(RaceDaySummaryViewModel.LoadData);
    }

    private static bool CanCreateRaceDay(object? arg) => true;
}