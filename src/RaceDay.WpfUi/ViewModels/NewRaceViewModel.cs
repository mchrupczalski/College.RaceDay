using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using RaceDay.MemoryDatabase.Commands;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public sealed class NewRaceViewModel : DialogViewModelBase<NewRaceDayRaceModel, RaceModel>
{
    #region Fields

    private readonly CreateRaceDayRaceCommand _createRaceDayRaceCommand;
    private RaceModel? _raceModel;
    private string _viewTitle = "Pick Date";

    #endregion

    #region Properties

    public string ViewTitle
    {
        get => _viewTitle;
        set => SetField(ref _viewTitle, value);
    }

    public ICommand ConfirmCommand { get; }
    public ICommand CancelCommand { get; }

    public NewRaceDayRaceModel RaceDayRaceModel { get; private set; } = new();

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design time only", true)]
    public NewRaceViewModel()
    {
    }
#pragma warning restore CS8618

    public NewRaceViewModel(CreateRaceDayRaceCommand createRaceDayRaceCommand)
    {
        _createRaceDayRaceCommand = createRaceDayRaceCommand;
        DialogClosingHandler = OnDialogClosing;

        ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
        CancelCommand = new RelayCommand(Cancel,   CanCancel);
    }

    #endregion

    #region Overrides

    public async Task<RaceModel?> OpenDialog(NewRaceDayRaceModel raceDayRaceModel)
    {
        RaceDayRaceModel = raceDayRaceModel;
        ViewTitle = $"Pick Date for new {raceDayRaceModel.RaceDayName}";
        _raceModel = null;
        //base.OpenDialog();
        if (DialogClosingHandler != null)
        {
            object? result = await DialogHost.Show(this, "RootDialog", DialogClosingHandler);
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
            return new RaceModel();
        }
        else
        {
            object? result = await DialogHost.Show(this, "RootDialog");
        }

        return null;
    }

    #endregion

    #region Events And Handlers

    private void OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
    {
        if (eventargs.Parameter is bool and false) return;
    }

    #endregion

    private bool CanCancel(object? arg) => true;

    private void Cancel(object? obj)
    {
        CloseDialog();
    }

    private bool CanConfirm(object? arg) => true;

    private void Confirm(object? obj)
    {
        CloseDialog();
        Result = _raceModel;
    }
}