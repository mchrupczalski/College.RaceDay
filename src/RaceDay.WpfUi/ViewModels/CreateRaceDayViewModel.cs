using System;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class CreateRaceDayViewModel : DialogViewModelBase
{
    public CreateRaceDayModel NewRaceDay { get; set; } = new();
    public ICommand CancelCommand { get;  }
    public ICommand SaveCommand { get;  }

    public CreateRaceDayViewModel()
    {
        CancelCommand = new RelayCommand(Cancel, CanCancel);
        SaveCommand = new RelayCommand(Save, CanSave);
    }

    #region Overrides of DialogViewModelBase

    /// <inheritdoc />
    public override void OpenDialog()
    {
        base.OpenDialog();
        NewRaceDay = new CreateRaceDayModel();
        NewRaceDay.ForceInitialErrorState = true;
    }

    #endregion

    private void Save(object? obj)
    {
        throw new NotImplementedException();
    }

    private bool CanSave(object? arg) => NewRaceDay is { HasErrors: false, HasAllRequiredData: true };

    private void Cancel(object? obj)
    {
        CloseDialog();
    }

    private static bool CanCancel(object? arg) => true;
}