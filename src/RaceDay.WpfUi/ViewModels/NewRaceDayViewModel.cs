using System;
using System.Windows.Input;
using RaceDay.Domain.DTOs;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Commands;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class NewRaceDayViewModel : DialogViewModelBase<CreateRaceDayModel,RaceDaySummaryModel>
{
    #region Fields

    private readonly CreateDayCommand _createRaceDayCommand;
    

    private CreateRaceDayModel _newRaceDay = new();

    #endregion

    #region Properties



    public CreateRaceDayModel NewRaceDay
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
    
    public NewRaceDayViewModel(CreateDayCommand createRaceDayCommand)
    {
        _createRaceDayCommand = createRaceDayCommand;

        CancelCommand = new RelayCommand(Cancel, CanCancel);
        SaveCommand = new RelayCommand(Save,     CanSave);
    }

    #endregion

    private void Save(object? obj)
    {
        var dto = new DayEntity()
        {
            Name = NewRaceDay.Name,
            Fee = NewRaceDay.SignUpFee.GetValueOrDefault(),
            LapDistanceKm = NewRaceDay.LapDistance.GetValueOrDefault(),
            PetrolCostPerLap = NewRaceDay.PetrolCostPerLap.GetValueOrDefault()
        };

        try
        {
            ErrorMessage = string.Empty;
            _createRaceDayCommand.Execute(dto);
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