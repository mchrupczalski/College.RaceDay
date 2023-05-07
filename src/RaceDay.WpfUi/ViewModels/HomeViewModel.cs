using System;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase, INavigableViewModel
{
    #region Properties

    public DaySummaryViewModel RaceDaySummaryViewModel { get; }
    public RacesSummaryViewModel RaceDayRacesViewModel { get; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design time only", true)]
    public HomeViewModel()
    {
    }
#pragma warning restore CS8618

    public HomeViewModel(DaySummaryViewModel raceDaySummaryViewModel, RacesSummaryViewModel raceDayRacesViewModel)
    {
        RaceDaySummaryViewModel = raceDaySummaryViewModel;
        RaceDayRacesViewModel = raceDayRacesViewModel;

        raceDaySummaryViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName != nameof(RaceDaySummaryViewModel.SelectedRaceDay)) return;
            if (raceDaySummaryViewModel.SelectedRaceDay == null) return;

            raceDayRacesViewModel.LoadRaceDayRaces(raceDaySummaryViewModel.SelectedRaceDay);
            raceDayRacesViewModel.UpdateViewTitle(raceDaySummaryViewModel.SelectedRaceDay.RaceDayName);
        };
    }

    #endregion

    #region Implementation of INavigableViewModel

    /// <inheritdoc />
    public void OnNavigatedTo()
    {
        RaceDaySummaryViewModel.LoadData();
    }

    #endregion
}