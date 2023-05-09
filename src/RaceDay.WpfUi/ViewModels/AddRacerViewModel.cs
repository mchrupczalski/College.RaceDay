using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using RaceDay.Domain.Entities;
using RaceDay.SqlLite.Commands;
using RaceDay.SqlLite.Queries;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class AddRacerViewModel : DialogViewModelBase<RaceModel>, INavigableViewModel
{
    #region Fields

    private readonly CreateRaceRacerCommand _createRaceRacerCommand;
    private readonly DeleteRaceRacerCommand _deleteRaceRacerCommand;
    private readonly CreateRacerCommand _createRacerCommand;

    private readonly RacersQuery _racersQuery;
    private RacerModel _newRacer = new();

    #endregion

    #region Properties

    public RacerModel NewRacer
    {
        get => _newRacer;
        private set => SetField(ref _newRacer, value);
    }

    public ObservableCollection<RacerModel> AllRacers { get; } = new();
    public ObservableCollection<RacerModel> SelectedRacers { get; } = new();
    
    public ICommand CreateNewRacerCommand { get; }
    public ICommand CancelNewRacerCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand RemoveRacersCommand { get; }
    public ICommand AddSelectedRacersCommand { get; }

    #endregion

    #region Constructors

#pragma warning disable CS8618
    [Obsolete("Design time only", true)]
    public AddRacerViewModel()
    {
    }
#pragma warning restore CS8618
    public AddRacerViewModel(RacersQuery racersQuery, CreateRacerCommand createRacerCommand, CreateRaceRacerCommand createRaceRacerCommand, DeleteRaceRacerCommand deleteRaceRacerCommand)
    {
        _racersQuery = racersQuery;
        _createRacerCommand = createRacerCommand;
        _createRaceRacerCommand = createRaceRacerCommand;
        _deleteRaceRacerCommand = deleteRaceRacerCommand;

        CreateNewRacerCommand = new RelayCommand(CreateNewRacer,       CanCreateNewRacer);
        CancelNewRacerCommand = new RelayCommand(CancelNewRacer,       CanCancelNewRacer);
        AddSelectedRacersCommand = new RelayCommand(AddSelectedRacers, CanAddSelectedRacers);
        RemoveRacersCommand = new RelayCommand(RemoveRacers,           CanRemoveRacers);

        CloseCommand = new RelayCommand(CloseDialog, CanCloseDialog);
    }

    #endregion

    #region Interfaces Implement

    /// <inheritdoc />
    public void OnNavigatedTo()
    {
        LoadData();
    }

    #endregion

    private static bool CanRemoveRacers(object? arg) => true;

    private void RemoveRacers(object? obj)
    {
        if(obj is null) return;
        var collection = (System.Collections.IList)obj!;
        if (collection.Count == 0) return;
        var racers = collection.Cast<RacerModel>()
                               .ToArray();
        
        foreach (var racer in racers)
        {
            bool deleted = _deleteRaceRacerCommand.Execute(Model.RaceId, racer.RacerId);
            if (!deleted) continue;
            
            AllRacers.Add(racer);
            SelectedRacers.Remove(racer);
        }
    }

    private static bool CanAddSelectedRacers(object? arg) => true;

    private void AddSelectedRacers(object? obj)
    {
        if(obj is null) return;
        var collection = (System.Collections.IList)obj!;
        if (collection.Count == 0) return;
        var racers = collection.Cast<RacerModel>()
                               .ToArray();
        
        foreach (var racer in racers)
        {
            var entity = new RaceRacerEntity(){RaceDayId = Model.RaceDayId, RaceId = Model.RaceId, RacerId = racer.RacerId};
            bool added = _createRaceRacerCommand.Execute(entity) != null;
            
            AllRacers.Remove(racer);
            SelectedRacers.Add(racer);
        }
    }

    private static bool CanCancelNewRacer(object? arg) => true;

    private void CancelNewRacer(object? obj)
    {
        NewRacer = new RacerModel();
    }

    private bool CanCreateNewRacer(object? arg) => NewRacer is { HasErrors: false, HasAllRequiredData: true };

    private void CreateNewRacer(object? obj)
    {
        var racer = new RacerEntity
        {
            Name = NewRacer.RacerName,
            Age = NewRacer.Age ?? 0
        };

        try
        {
            var newRacer = _createRacerCommand.Execute(racer);
            var raceRacer = new RaceRacerEntity
            {
                RaceDayId = Model.RaceDayId,
                RaceId = Model.RaceId,
                RacerId = newRacer.Id
            };

            var newRaceRacer = _createRaceRacerCommand.Execute(raceRacer);
            var model = new RacerModel
            {
                RacerId = newRaceRacer.RacerId,
                RacerName = newRacer.Name,
                Age = newRacer.Age
            };

            SelectedRacers.Add(model);

            CancelNewRacer(null);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
    }

    public void LoadData()
    {
        AllRacers.Clear();
        SelectedRacers.Clear();

        var allRacers = _racersQuery.GetAll();
        var raceRacers = _racersQuery.GetRacersForRace(Model.RaceId)
                                     .ToArray();
        var racersNotInRace = allRacers.Except(raceRacers)
                                       .ToArray();

        foreach (var racer in racersNotInRace)
        {
            var racerModel = new RacerModel { RacerId = racer.Id, RacerName = racer.Name, Age = racer.Age };
            AllRacers.Add(racerModel);
        }

        foreach (var racer in raceRacers)
        {
            var racerModel = new RacerModel { RacerId = racer.Id, RacerName = racer.Name, Age = racer.Age };
            SelectedRacers.Add(racerModel);
        }
    }
}