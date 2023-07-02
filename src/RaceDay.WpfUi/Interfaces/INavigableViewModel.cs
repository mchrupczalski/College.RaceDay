namespace RaceDay.WpfUi.Interfaces;

/// <summary>
///     Interface for view models that need to be notified when navigated to
/// </summary>
public interface INavigableViewModel
{
    #region Abstract Members

    /// <summary>
    ///     Called when the view model is navigated to
    /// </summary>
    void OnNavigatedTo();

    #endregion
}