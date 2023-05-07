using System;
using System.Windows.Input;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class RaceViewModel : ViewModelBase
{
    private readonly DialogService _dialogService;

    public delegate RaceViewModel CreateRaceViewModel(RaceModel raceModel);

    private RaceModel _raceModel;

    public RaceModel RaceModel
    {
        get => _raceModel;
        private set => SetField(ref _raceModel, value);
    }

    public ICommand StopAllCommand { get; }
    public ICommand StartAllCommand { get; }
    public ICommand AddRacerCommand { get; }
    public ICommand GoBackCommand { get; }
    


    [Obsolete("For design-time only", true)]
    public RaceViewModel()
    {
        RaceModel = new RaceModel()
        {
            RaceDayId = 1,
            RaceDayName = "Race Name 01",
            RaceId = 1,
            SignUpFee = 10.00f,
            AllTimeLapRecord = new TimeSpan(0, 0, 2, 55, 123),
            RaceLapRecord = new TimeSpan(0, 0, 3, 2, 623),
            IsRecordBeaten = true,
            RaceProfit = 100.00f,
        };
        
        RaceModel.Racers.Add(new RacerModel()
        {
            RacerId = 1,
            RacerName = "Racer Name 01",
        });
        
        RaceModel.Racers.Add(new RacerModel()
        {
            RacerId = 2,
            RacerName = "Racer Name 02",
        });
    }

    public RaceViewModel(RaceModel raceModel, DialogService dialogService)
    {
        _dialogService = dialogService;
        RaceModel = raceModel;
        
        AddRacerCommand = new RelayCommand(AddRacer, CanAddRacer);
    }

    private bool CanAddRacer(object? arg) => true;

    private void AddRacer(object? obj)
    {
        _dialogService.DisplayDialogAsync<AddRacerViewModel, RaceModel>(RaceModel);
    }
}