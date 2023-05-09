using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Commands;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public sealed class NewRaceViewModel : DialogViewModelBase<NewRaceModel, RaceSummaryModel>
{
    #region Fields

    private readonly CreateRaceCommand _createRaceDayRaceCommand;
    private readonly RaceSummaryQuery _raceSummaryQuery;
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

    public NewRaceViewModel(CreateRaceCommand createRaceDayRaceCommand, RaceSummaryQuery raceSummaryQuery)
    {
        _createRaceDayRaceCommand = createRaceDayRaceCommand;
        _raceSummaryQuery = raceSummaryQuery;
        DialogClosingHandler = OnDialogClosing;

        ConfirmCommand = new RelayCommand(Confirm, CanConfirm);
        CancelCommand = new RelayCommand(Cancel,   CanCancel);
    }

    #endregion

    #region Events And Handlers

    private void OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
    {
        if (eventargs.Parameter is bool and false) return;
    }

    #endregion


    private bool CanCancel(object? arg) => true;

    private void Cancel(object? obj) => CloseDialog();

    private bool CanConfirm(object? arg) => true;

    private void Confirm(object? obj)
    {
        var entity = new RaceEntity()
        {
            RaceDayId = Model.RaceDayId,
            RaceDate = Model.RaceDate
        };

        try
        {
            var newRace = _createRaceDayRaceCommand.Execute(entity);
            var resultDto = _raceSummaryQuery.GetById(newRace!.Id);
            if (resultDto != null)
                Result = new RaceSummaryModel()
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