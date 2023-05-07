using MaterialDesignThemes.Wpf;

namespace RaceDay.WpfUi.Infrastructure;

/// <summary>
///     Base class for view models that are used to display dialogs.
/// </summary>
public abstract class DialogViewModelBase : ObservableObject
{
    #region Fields

    private DialogClosingEventHandler? _dialogClosingHandler;
    private bool _dialogHostIsOpen;
    private string _errorMessage = string.Empty;

    #endregion

    #region Properties

    /// <summary>
    ///     The error message to display in the dialog.
    /// </summary>
    public string ErrorMessage
    {
        get => _errorMessage;
        protected set
        {
            SetField(ref _errorMessage, value);
            OnPropertyChanged(nameof(DisplayError));
        }
    }

    /// <summary>
    ///     Whether or not to display the error message in the dialog.
    /// </summary>
    public bool DisplayError => !string.IsNullOrEmpty(ErrorMessage);

    /// <summary>
    ///     Indicates whether or not the dialog is open.
    ///     This is used to bind to the DialogHost.IsOpen property.
    ///     Dialog closes when this is set to false.
    /// </summary>
    public bool DialogHostIsOpen
    {
        get => _dialogHostIsOpen;
        private set => SetField(ref _dialogHostIsOpen, value);
    }

    /// <summary>
    ///     The dialog closing handler.
    ///     This is used to bind to the DialogHost.DialogClosing attached property.
    ///     This can be used to prevent the dialog from closing or implement custom logic when the dialog closes.
    /// </summary>
    public virtual DialogClosingEventHandler? DialogClosingHandler
    {
        get => _dialogClosingHandler;
        set => SetField(ref _dialogClosingHandler, value);
    }

    #endregion

    /// <summary>
    ///     Opens the dialog.
    /// </summary>
    public void OpenDialog() => DialogHostIsOpen = true;

    /// <summary>
    ///     Closes the dialog.
    /// </summary>
    public void CloseDialog() => DialogHostIsOpen = false;
}

/// <summary>
///     Base class for view models that are used to display dialogs, with a result.
/// </summary>
/// <typeparam name="TResult">A result that is returned when the dialog closes.</typeparam>
public abstract class DialogViewModelBase<TResult> : DialogViewModelBase
{
    #region Fields

    private TResult? _result;

    #endregion

    #region Properties

    /// <summary>
    ///     The result that is returned when the dialog closes.
    ///     If no value is set, the dialog is assumed to have been cancelled.
    /// </summary>
    public TResult? Result
    {
        get => _result;
        protected set => SetField(ref _result, value);
    }

    #endregion
}

/// <summary>
///     Base class for view models that are used to display dialogs, with a model and result.
/// </summary>
/// <typeparam name="TModel">A model that is used to display data in the dialog.</typeparam>
/// <typeparam name="TResult">A result that is returned when the dialog closes.</typeparam>
public abstract class DialogViewModelBase<TModel, TResult> : DialogViewModelBase
{
    #region Fields

    private TModel _model = default!;
    private TResult? _result;

    #endregion

    #region Properties

    /// <summary>
    ///     The model that is used to display data in the dialog.
    /// </summary>
    public TModel Model
    {
        get => _model;
        set => SetField(ref _model, value);
    }

    /// <summary>
    ///     The result that is returned when the dialog closes.
    ///     If no value is set, the dialog is assumed to have been cancelled.
    /// </summary>
    public TResult? Result
    {
        get => _result;
        set => SetField(ref _result, value);
    }

    #endregion
}