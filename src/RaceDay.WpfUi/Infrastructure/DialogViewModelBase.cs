﻿using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace RaceDay.WpfUi.Infrastructure;

public abstract class DialogViewModelBase : ObservableObject
{
    private bool _dialogHostIsOpen = false;

    public bool DialogHostIsOpen
    {
        get => _dialogHostIsOpen;
        set => SetField(ref _dialogHostIsOpen, value);
    }
    
    public void OpenDialog()
    {
        DialogHostIsOpen = true;
    }
}