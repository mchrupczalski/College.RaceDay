using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace RaceDay.WpfUi.Infrastructure;

public abstract class DialogViewModelBase : ObservableObject
{
    private bool _dialogHostIsOpen = false;
    private DialogClosingEventHandler? _dialogClosingHandler;

    public bool DialogHostIsOpen
    {
        get => _dialogHostIsOpen;
        set => SetField(ref _dialogHostIsOpen, value);
    }

    public DialogClosingEventHandler? DialogClosingHandler
    {
        get => _dialogClosingHandler;
        set => SetField(ref _dialogClosingHandler, value);
    }

    public virtual void OpenDialog()
    {
        DialogHostIsOpen = true;
    }
    
    public virtual void CloseDialog()
    {
        DialogHostIsOpen = false;
    }
}