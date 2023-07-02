using System;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

/// <summary>
///     A view model providing data and logic for a view for creating new Races
/// </summary>
public sealed class NewRaceViewModel : DialogViewModelBase<NewRaceModel, RaceSummaryModel>
{
    #region Fields

    private readonly ICreateRaceCommand _createRaceDayRaceCommand;
    private readonly IRaceSummaryQuery _raceSummaryQuery;
    private string _viewTitle = "Pick Date";

    #endregion

    #region Properties

    /// <summary>
    ///     The title of the view
    /// </summary>
    public string ViewTitle
    {
        get => _viewTitle;
        private set => SetField(ref _viewTitle, value);
    }

    /// <summary>
    ///     Command for confirming the operation
    /// </summary>
    public ICommand ConfirmCommand { get; }

    /// <summary>
    ///     Command for cancelling the operation
    /// </summary>
    public ICommand CancelCommand { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Creates a new instance of the <see cref="NewRaceViewModel" /> class
    /// </summary>
    /// <param name="createRaceDayRaceCommand">Command for creating new Races in the persistence storage</param>
    /// <param name="raceSummaryQuery">Query for retrieving Race summaries from the persistence storage</param>
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

    /// <summary>
    ///     Handles the dialog closing event
    /// </summary>
    private static void OnDialogClosing(object sender, DialogClosingEventArgs args)
    {
        if (args.Parameter is false)
            return;
    }

    #endregion

    /// <summary>
    ///     Checks if the operation can be cancelled
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns>Always true</returns>
    private static bool CanCancel(object? arg) => true;

    /// <summary>
    ///     Cancels the operation
    /// </summary>
    /// <param name="obj">Not used</param>
    private void Cancel(object? obj) => CloseDialog();

    /// <summary>
    ///     Checks if the operation can be confirmed
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns>Always true</returns>
    private static bool CanConfirm(object? arg) => true;

    /// <summary>
    ///     Confirms the operation
    /// </summary>
    /// <param name="obj">Not used</param>
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