using System;
using System.Windows.Input;
using RaceDay.Core.Entities;
using RaceDay.Core.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class CreateRaceDayViewModel : DialogViewModelBase
{
    #region Fields

    private readonly IRepository<LapEntity> _lapRepository;
    private readonly IRepository<RaceDayEntity> _raceDayRepository;
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

    public CreateRaceDayViewModel(IRepository<RaceDayEntity> raceDayRepository, IRepository<LapEntity> lapRepository)
    {
        _raceDayRepository = raceDayRepository;
        _lapRepository = lapRepository;

        CancelCommand = new RelayCommand(Cancel, CanCancel);
        SaveCommand = new RelayCommand(Save,     CanSave);
    }

    #endregion

    #region Overrides

    /// <inheritdoc />
    public override void OpenDialog()
    {
        base.OpenDialog();
        NewRaceDay = new CreateRaceDayModel
        {
            ForceInitialErrorState = true
        };
    }

    #endregion

    private void Save(object? obj)
    {
        var race = new RaceDayEntity
        {
            Guid = Guid.NewGuid(),
            Name = NewRaceDay.Name,
            SignUpFee = NewRaceDay.SignUpFee.GetValueOrDefault()
        };

        var lap = new LapEntity
        {
            Guid = Guid.NewGuid(),
            RaceDayGuid = race.Guid,
            LapDistanceKm = NewRaceDay.LapDistance.GetValueOrDefault(),
            PetrolCostPerLap = NewRaceDay.PetrolCostPerLap.GetValueOrDefault()
        };

        _raceDayRepository.Create(race);
        _lapRepository.Create(lap);

        CloseDialog();
    }

    private bool CanSave(object? arg) => NewRaceDay is { HasErrors: false, HasAllRequiredData: true };

    private void Cancel(object? obj)
    {
        CloseDialog();
    }

    private static bool CanCancel(object? arg) => true;
}