using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Properties

    public NavigationService NavigationService { get; }

    #endregion

    #region Constructors

    public MainViewModel(NavigationService navigationService) => NavigationService = navigationService;

    #endregion
}