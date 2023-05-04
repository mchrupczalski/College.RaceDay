using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace RaceDay.WpfUi.Infrastructure;

public abstract class DialogViewModelBase : ObservableObject
{
    #region Fields

    private DialogClosingEventHandler? _dialogClosingHandler;
    private bool _dialogHostIsOpen;
    private string _errorMessage = string.Empty;
    private string? _hostName = string.Empty;

    #endregion

    #region Properties

    public string ErrorMessage
    {
        get => _errorMessage;
        protected set
        {
            SetField(ref _errorMessage, value);
            OnPropertyChanged(nameof(DisplayError));
        }
    }

    public bool DisplayError => !string.IsNullOrEmpty(ErrorMessage);

    public bool DialogHostIsOpen
    {
        get => _dialogHostIsOpen;
        set => SetField(ref _dialogHostIsOpen, value);
    }

    public virtual DialogClosingEventHandler? DialogClosingHandler
    {
        get => _dialogClosingHandler;
        set => SetField(ref _dialogClosingHandler, value);
    }

    #endregion

    public virtual void OpenDialog(string hostName)
    {
        DialogHostIsOpen = true;
    }

    public virtual void CloseDialog()
    {
        DialogHostIsOpen = false;
    }
}

public abstract class DialogViewModelBase<TModel, TResult> : DialogViewModelBase
{
    #region Fields

    private TModel _model = default!;
    private TResult? _result = default!;

    #endregion

    #region Properties

    public TModel Model
    {
        get => _model;
        protected set => SetField(ref _model, value);
    }

    public TResult? Result
    {
        get => _result;
        protected set => SetField(ref _result, value);
    }

    #endregion
}