using System;
using System.Windows.Input;
using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

/// <summary>
///     A view model providing data and logic for view for creating new Race Days
/// </summary>
public class NewRaceDayViewModel : DialogViewModelBase<NewRaceDayModel, DaySummaryModel>
{
    #region Fields

    private readonly ICreateRaceDayCommand _createRaceDayCommand;
    private readonly IDaySummaryQuery _daySummaryQuery;


    private NewRaceDayModel _newRaceDay = new();

    #endregion

    #region Properties

    /// <summary>
    ///     A model representing a new Race Day
    /// </summary>
    public NewRaceDayModel NewRaceDay
    {
        get => _newRaceDay;
        private set => SetField(ref _newRaceDay, value);
    }

    /// <summary>
    ///     A command for cancelling the operation
    /// </summary>
    public ICommand CancelCommand { get; }

    /// <summary>
    ///     A command for saving data to the persistence storage
    /// </summary>
    public ICommand SaveCommand { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Creates a new instance of the <see cref="NewRaceDayViewModel" /> class
    /// </summary>
    /// <param name="createRaceDayCommand">A command for creating new Race Day in the persistence storage</param>
    /// <param name="daySummaryQuery">A query for retrieving Race Day summaries from the persistence storage</param>
    public NewRaceDayViewModel(ICreateRaceDayCommand createRaceDayCommand, IDaySummaryQuery daySummaryQuery)
    {
        _createRaceDayCommand = createRaceDayCommand;
        _daySummaryQuery = daySummaryQuery;

        CancelCommand = new RelayCommand(Cancel, CanCancel);
        SaveCommand = new RelayCommand(Save,     CanSave);
    }

    #endregion

    /// <summary>
    ///     Saves the data to the persistence storage
    /// </summary>
    /// <param name="obj">Not used</param>
    private void Save(object? obj)
    {
        var entity = new DayEntity
        {
            Name = NewRaceDay.Name,
            Fee = NewRaceDay.SignUpFee.GetValueOrDefault(),
            LapDistanceKm = NewRaceDay.LapDistance.GetValueOrDefault(),
            PetrolCostPerLap = NewRaceDay.PetrolCostPerLap.GetValueOrDefault()
        };

        try
        {
            ErrorMessage = string.Empty;
            var newDay = _createRaceDayCommand.Execute(entity);
            if (newDay != null)
            {
                var resultDto = _daySummaryQuery.GetById(newDay.Id);
                if (resultDto != null)
                    Result = new DaySummaryModel
                    {
                        RaceDayId = resultDto.RaceDayId,
                        RaceDayName = resultDto.RaceDayName,
                        SignUpFee = resultDto.SignUpFee,
                        LapDistanceKilometers = resultDto.LapDistanceKm,
                        PetrolCostPerLap = resultDto.PetrolCostPerLap,
                        TotalRaces = resultDto.TotalRaces,
                        RecordLapTime = TimeSpan.FromSeconds(resultDto.RecordLapTime),
                        RecordHolderName = resultDto.RecordHolderName,
                        TotalIncome = resultDto.TotalIncome,
                        TotalCost = resultDto.TotalCost
                    };
            }

            CloseDialog();
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    /// <summary>
    ///     Determines whether the data can be saved to the persistence storage
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns></returns>
    private bool CanSave(object? arg) => NewRaceDay is { HasErrors: false, HasAllRequiredData: true };

    /// <summary>
    ///     Closes the dialog
    /// </summary>
    /// <param name="obj">Not used</param>
    private void Cancel(object? obj) => CloseDialog();

    /// <summary>
    ///     Determines whether the operation can be cancelled
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns>Always true</returns>
    private static bool CanCancel(object? arg) => true;
}