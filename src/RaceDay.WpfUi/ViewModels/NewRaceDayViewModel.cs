using System;
using System.Windows.Input;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Commands;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class NewRaceDayViewModel : DialogViewModelBase<NewRaceDayModel, DaySummaryModel>
{
    #region Fields

    private readonly CreateRaceDayCommand _createRaceDayCommand;
    private readonly DaySummaryQuery _daySummaryQuery;


    private NewRaceDayModel _newRaceDay = new();

    #endregion

    #region Properties

    public NewRaceDayModel NewRaceDay
    {
        get => _newRaceDay;
        private set => SetField(ref _newRaceDay, value);
    }

    public ICommand CancelCommand { get; }
    public ICommand SaveCommand { get; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design time only", true)]
    public NewRaceDayViewModel()

    {
    }
#pragma warning restore CS8618

    public NewRaceDayViewModel(CreateRaceDayCommand createRaceDayCommand, DaySummaryQuery daySummaryQuery)
    {
        _createRaceDayCommand = createRaceDayCommand;
        _daySummaryQuery = daySummaryQuery;

        CancelCommand = new RelayCommand(Cancel, CanCancel);
        SaveCommand = new RelayCommand(Save,     CanSave);
    }

    #endregion

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
                        RecordLapTime = resultDto.RecordLapTime,
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

    private bool CanSave(object? arg) => NewRaceDay is { HasErrors: false, HasAllRequiredData: true };

    private void Cancel(object? obj)
    {
        CloseDialog();
    }

    private static bool CanCancel(object? arg) => true;
}