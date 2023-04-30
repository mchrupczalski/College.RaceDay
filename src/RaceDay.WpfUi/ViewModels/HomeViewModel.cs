using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private RaceDaySummaryViewModel _raceDaySummaryViewModel;

    #region Properties

    public RaceDaySummaryViewModel RaceDaySummaryViewModel
    {
        get => _raceDaySummaryViewModel;
        private set => SetField(ref _raceDaySummaryViewModel, value);
    }

    public RaceDayRacesViewModel RaceDayRacesViewModel { get; private set; }

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
        
        raceDaySummaryViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName != nameof(RaceDaySummaryViewModel.SelectedRaceDay)) return;
            if (raceDaySummaryViewModel.SelectedRaceDay != null) raceDayRacesViewModel.LoadRaceDayRaces(raceDaySummaryViewModel.SelectedRaceDay.Guid);
        };
    }

    #endregion
}