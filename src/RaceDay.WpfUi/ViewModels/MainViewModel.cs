using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Properties

    public NavigationService NavigationService { get; }
    public DialogService DialogService { get; }

    #endregion

    #region Constructors

    public MainViewModel(NavigationService navigationService, DialogService dialogService)
    {
        NavigationService = navigationService;
        DialogService = dialogService;
    }

    #endregion
}