using System.Collections.Specialized;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;

namespace RaceDay.WpfUi.ViewModels;

/// <summary>
///     A view model providing data and logic for the Home view
/// </summary>
public class HomeViewModel : ViewModelBase, INavigableViewModel
{
    #region Properties

    /// <summary>
    ///     A view model providing data and logic for the Day Summary view
    /// </summary>
    public DaySummaryViewModel RaceDaySummaryViewModel { get; }

    /// <summary>
    ///     A view model providing data and logic for the Races Summary view
    /// </summary>
    public RacesSummaryViewModel RaceDayRacesViewModel { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Creates a new instance of the <see cref="HomeViewModel" /> class
    /// </summary>
    /// <param name="raceDaySummaryViewModel">A view model providing data and logic for the Day Summary view</param>
    /// <param name="raceDayRacesViewModel">A view model providing data and logic for the Races Summary view</param>
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

    /// <summary>
    ///     A handler for the <see cref="INotifyCollectionChanged.CollectionChanged" /> event of the
    ///     <see cref="RaceDayRacesViewModel" />.Races collection.
    ///     If the collection changes, the <see cref="RaceDaySummaryViewModel" /> reloads data
    /// </summary>
    private void RacesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
        RaceDaySummaryViewModel.LoadData(false);

    #endregion
}