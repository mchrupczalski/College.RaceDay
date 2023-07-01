using System;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public sealed class NewRaceViewModel : DialogViewModelBase<NewRaceModel, RaceSummaryModel>
{
    #region Fields

    private readonly ICreateRaceCommand _createRaceDayRaceCommand;
    private readonly IRaceSummaryQuery _raceSummaryQuery;
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

    public NewRaceViewModel(ICreateRaceCommand createRaceDayRaceCommand, IRaceSummaryQuery raceSummaryQuery)
    {
        _createRaceDayRaceCommand = createRaceDayRaceCommand;
        _raceSummaryQuery = raceSummaryQuery;
        DialogClosingHandler = OnDialogClosing;

        ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
        CancelCommand = new RelayCommand(Cancel,   CanCancel);
    }

    #endregion

    #region Events And Handlers

    private static void OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
    {
        if (eventargs.Parameter is bool and false)
            return;
    }

    #endregion


    private static bool CanCancel(object? arg) => true;

    private void Cancel(object? obj) => CloseDialog();

    private static bool CanConfirm(object? arg) => true;

    private void Confirm(object? obj)
    {
        var entity = new RaceEntity
        {
            RaceDayId = Model.RaceDayId,
            RaceDate = Model.RaceDate
        };

        try
        {
            var newRace = _createRaceDayRaceCommand.Execute(entity);
            var resultDto = _raceSummaryQuery.GetById(newRace!.Id);
            if (resultDto != null)
                Result = new RaceSummaryModel
                {
                    RaceId = resultDto.RaceId,
                    RaceDayId = resultDto.RaceDayId,
                    RaceDate = DateTime.TryParse(resultDto.RaceDate, out var raceDate) ? raceDate : null,
                    TotalRacers = resultDto.TotalRacers,
                    TotalLaps = resultDto.TotalLaps,
                    BestLapTime = TimeSpan.FromSeconds(resultDto.BestLapTime),
                    BestLapTimeHolder = resultDto.BestLapTimeHolder,
                    TotalIncome = resultDto.TotalIncome,
                    TotalExpenses = resultDto.TotalExpense,
                    TotalProfit = resultDto.TotalIncome - resultDto.TotalExpense
                };

            CloseDialog();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }
}