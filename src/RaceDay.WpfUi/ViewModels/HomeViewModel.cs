using System;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.ViewModels;

public class HomeViewModel : ViewModelBase
{
    #region Properties

    public RaceDaySummaryViewModel RaceDaySummaryViewModel { get; }

    #endregion

    #region Constructors

    [Obsolete("Design time only", true)]
    public HomeViewModel()
    {
    }

    public HomeViewModel(RaceDaySummaryViewModel raceDaySummaryViewModel) => RaceDaySummaryViewModel = raceDaySummaryViewModel;

    #endregion
}