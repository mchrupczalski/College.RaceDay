using System.Collections.Specialized;
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

    public HomeViewModel(DaySummaryViewModel raceDaySummaryViewModel, RacesSummaryViewModel raceDayRacesViewModel)
    {
        RaceDaySummaryViewModel = raceDaySummaryViewModel;
        RaceDayRacesViewModel = raceDayRacesViewModel;

        raceDaySummaryViewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName != nameof(RaceDaySummaryViewModel.SelectedRaceDay))
                return;
            if (raceDaySummaryViewModel.SelectedRaceDay == null)
                return;

            raceDayRacesViewModel.Races.CollectionChanged -= RacesCollectionChanged;

            raceDayRacesViewModel.LoadRaceDayRaces(raceDaySummaryViewModel.SelectedRaceDay);
            raceDayRacesViewModel.UpdateViewTitle(raceDaySummaryViewModel.SelectedRaceDay.RaceDayName);

            raceDayRacesViewModel.Races.CollectionChanged += RacesCollectionChanged;
        };
    }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public void OnNavigatedTo()
    {
        RaceDaySummaryViewModel.LoadData();
        if (RaceDaySummaryViewModel.SelectedRaceDay != null)
            RaceDayRacesViewModel.LoadRaceDayRaces(RaceDaySummaryViewModel.SelectedRaceDay);
    }

    #endregion

    #region Events And Handlers

    private void RacesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
        RaceDaySummaryViewModel.LoadData(false);

    #endregion
}