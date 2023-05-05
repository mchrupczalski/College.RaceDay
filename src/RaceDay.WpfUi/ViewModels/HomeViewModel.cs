using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using RaceDay.SqlLite.Commands;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase, INavigableViewModel
{
    #region Fields

    private readonly NavigationService _navigationService;
    private readonly DialogService _dialogService;

    #endregion

    #region Properties

    public RaceDaySummaryViewModel RaceDaySummaryViewModel { get; }
    public RaceDayRacesViewModel RaceDayRacesViewModel { get; }

    public ICommand CreateNewRaceDayCommand { get; }
    public ICommand StartNewRaceCommand { get; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design time only", true)]
    public HomeViewModel()

    {
    }
#pragma warning restore CS8618

    public HomeViewModel(RaceDaySummaryViewModel raceDaySummaryViewModel, RaceDayRacesViewModel raceDayRacesViewModel, NavigationService navigationService, DialogService dialogService)
    {
        _navigationService = navigationService;
        _dialogService = dialogService;
        RaceDaySummaryViewModel = raceDaySummaryViewModel;
        RaceDayRacesViewModel = raceDayRacesViewModel;

        CreateNewRaceDayCommand = new RelayCommand(CreateRaceDay, CanCreateRaceDay);
        StartNewRaceCommand = new RelayCommand(StartRaceDay, CanStartRaceDay);

        raceDaySummaryViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName != nameof(RaceDaySummaryViewModel.SelectedRaceDay)) return;
            if (raceDaySummaryViewModel.SelectedRaceDay == null) return;

            raceDayRacesViewModel.LoadRaceDayRaces(raceDaySummaryViewModel.SelectedRaceDay.RaceDayId);
            raceDayRacesViewModel.UpdateViewTitle(raceDaySummaryViewModel.SelectedRaceDay.Name);
        };
    }

    private bool CanStartRaceDay(object? arg) => RaceDaySummaryViewModel.SelectedRaceDay != null;

    private async void StartRaceDay(object? obj)
    {
        var newRaceModel = new NewRaceModel()
        {
            RaceDayId = RaceDaySummaryViewModel.SelectedRaceDay!.RaceDayId,
            RaceDayName = RaceDaySummaryViewModel.SelectedRaceDay!.Name,
            RaceDate = DateTime.Today
        };

        var race = await _dialogService.DisplayDialogAsync<NewRaceViewModel, NewRaceModel, RaceModel>(newRaceModel);
    }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public void OnNavigatedTo() => RaceDaySummaryViewModel.LoadData();

    #endregion

    private void CreateRaceDay(object? obj)
    {
        //_dialogService.DisplayDialog<NewRaceDayViewModel>(RaceDaySummaryViewModel.LoadData);
    }

    private static bool CanCreateRaceDay(object? arg) => true;
}