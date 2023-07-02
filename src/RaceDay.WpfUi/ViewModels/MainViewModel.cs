using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

/// <summary>
///     A view model providing data and logic for the Main view
/// </summary>
public class MainViewModel : ViewModelBase
{
    #region Properties

    /// <summary>
    ///     The navigation service, responsible for navigating between views
    /// </summary>
    public NavigationService NavigationService { get; }

    /// <summary>
    ///     The dialog service, responsible for displaying dialogs
    /// </summary>
    public DialogService DialogService { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Creates a new instance of the <see cref="MainViewModel" /> class
    /// </summary>
    /// <param name="navigationService">The navigation service, responsible for navigating between views</param>
    /// <param name="dialogService">The dialog service, responsible for displaying dialogs</param>
    public MainViewModel(NavigationService navigationService, DialogService dialogService)
    {
        NavigationService = navigationService;
        DialogService = dialogService;
    }

    #endregion
}