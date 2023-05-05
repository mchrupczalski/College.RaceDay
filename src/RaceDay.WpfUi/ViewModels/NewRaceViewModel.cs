using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using RaceDay.Domain.DTOs;
using RaceDay.MemoryDatabase.Commands;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public sealed class NewRaceViewModel : DialogViewModelBase<NewRaceModel, RaceModel>
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
        private set => SetField(ref _viewTitle, value);
    }

    public ICommand ConfirmCommand { get; }
    public ICommand CancelCommand { get; }

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

    #region Events And Handlers

    private void OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
    {
        if (eventargs.Parameter is bool and false) return;

        try
        {
            var dto = new NewRaceDto()
            {
                RaceDayId = Model.RaceDayId,
                RaceDate = Model.RaceDate
            };
            
            var newRace = _createRaceDayRaceCommand.Execute(dto);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    #endregion


    private bool CanCancel(object? arg) => true;

    private void Cancel(object? obj) => CloseDialog();

    private bool CanConfirm(object? arg) => true;

    private void Confirm(object? obj)
    {
        Result = _raceModel;
        CloseDialog();
    }
}