using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;
using RaceDay.WpfUi.Services;

namespace RaceDay.WpfUi.ViewModels;

public class RaceViewModel : ViewModelBase
{
    #region Delegates

    public delegate RaceViewModel CreateRaceViewModel(RaceModel raceModel);

    #endregion

    #region Fields

    private readonly RacerViewModel.CreateRacerViewModel _createRacerViewModel;

    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    private readonly RaceModel _raceModel;
    private readonly RacersQuery _racersQuery;

    #endregion

    #region Properties

    public ObservableCollection<RacerViewModel> Racers { get; } = new();

    public RaceModel RaceModel
    {
        get => _raceModel;
        private init => SetField(ref _raceModel, value);
    }

    public ICommand StopAllCommand { get; }
    public ICommand StartAllCommand { get; }
    public ICommand AddRacerCommand { get; }
    public ICommand GoBackCommand { get; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("For design-time only", true)]
    public RaceViewModel()
    {
        RaceModel = new RaceModel
        {
            RaceDayId = 1,
            RaceDayName = "Race Name 01",
            RaceId = 1,
            SignUpFee = 10.00f,
            AllTimeLapRecord = new TimeSpan(0, 0, 2, 55, 123),
            RaceLapRecord = new TimeSpan(0,    0, 3, 2,  623),
            IsRecordBeaten = true,
            RaceProfit = 100.00f
        };
    }
#pragma warning restore CS8618
    
    public RaceViewModel(RaceModel raceModel,
                         DialogService dialogService,
                         NavigationService navigationService,
                         RacerViewModel.CreateRacerViewModel createRacerViewModel,
                         RacersQuery racersQuery)
    {
        _dialogService = dialogService;
        _navigationService = navigationService;
        _createRacerViewModel = createRacerViewModel;
        _racersQuery = racersQuery;
        RaceModel = raceModel;

        AddRacerCommand = new RelayCommand(AddRacer, CanAddRacer);
        GoBackCommand = new RelayCommand(GoBack,     CanGoBack);

        Racers.CollectionChanged += RacersCollectionChanged;
        
        LoadRacers();
    }

    #endregion

    #region Events And Handlers

    private void RacersCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add && e.Action != NotifyCollectionChangedAction.Remove) return;
        UpdateRaceProfit();
    }

    #endregion

    private void UpdateRaceProfit()
    {
        float totalIncome = RaceModel.SignUpFee * Racers.Count;
        int totalLaps = Racers.Sum(racer => racer.Laps.Count);
        float totalExpense = totalLaps * RaceModel.PetrolCostPerLap;

        RaceModel.RaceProfit = totalIncome - totalExpense;
    }

    private static bool CanGoBack(object? arg) => true;

    private void GoBack(object? obj) => _navigationService.NavigateTo<HomeViewModel>();

    private static bool CanAddRacer(object? arg) => true;

    private async void AddRacer(object? obj)
    {
        await _dialogService.DisplayDialogAsync<AddRacerViewModel, RaceModel>(RaceModel);
        LoadRacers();
    }

    private void LoadRacers()
    {
        var raceRacers = _racersQuery.GetRacersForRace(RaceModel.RaceId)
                                     .ToArray();

        foreach (var racer in Racers)
        {
            var racerInRace = raceRacers.FirstOrDefault(r => r.Id == racer.Racer.RacerId);
            if (racerInRace is not null) continue;
            
            racer.Laps.CollectionChanged -= RacerLapsCollectionChanged;
            Racers.Remove(racer);

        }

        foreach (var racerDto in raceRacers)
        {
            var racer = Racers.FirstOrDefault(r => r.Racer.RacerId == racerDto.Id);
            if (racer is not null) continue;

            var racerModel = new RacerModel
            {
                RacerId = racerDto.Id,
                RacerName = racerDto.Name,
                Age = racerDto.Age,
            };
            var racerViewModel = _createRacerViewModel(racerModel);
            racerViewModel.Laps.CollectionChanged += RacerLapsCollectionChanged;
            Racers.Add(racerViewModel);
        }
    }

    private void RacerLapsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if(e.Action != NotifyCollectionChangedAction.Add && e.Action != NotifyCollectionChangedAction.Remove) return;
        UpdateRaceProfit();
    }
}