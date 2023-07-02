using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using RaceDay.Domain.Entities;
using RaceDay.Domain.Interfaces;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

/// <summary>
///     A view model providing data and logic for the Add Racer view
/// </summary>
public class AddRacerViewModel : DialogViewModelBase<RaceModel>, INavigableViewModel
{
    #region Fields

    private readonly ICreateRaceRacerCommand _createRaceRacerCommand;
    private readonly ICreateRacerCommand _createRacerCommand;
    private readonly IDeleteRaceRacerCommand _deleteRaceRacerCommand;

    private readonly IRacersQuery _racersQuery;
    private RacerModel _newRacer = new();

    #endregion

    #region Properties

    /// <summary>
    ///     The new racer to be added
    /// </summary>
    public RacerModel NewRacer
    {
        get => _newRacer;
        private set => SetField(ref _newRacer, value);
    }

    /// <summary>
    ///     A collection of all racers within the system
    /// </summary>
    public ObservableCollection<RacerModel> AllRacers { get; } = new();

    /// <summary>
    ///     A collection of racers selected for the Race
    /// </summary>
    public ObservableCollection<RacerModel> SelectedRacers { get; } = new();

    /// <summary>
    ///     A command for creating a new racer
    /// </summary>
    public ICommand CreateNewRacerCommand { get; }

    /// <summary>
    ///     A command for cancelling the creation of a new racer
    /// </summary>
    public ICommand CancelNewRacerCommand { get; }

    /// <summary>
    ///     A command for closing the dialog
    /// </summary>
    public ICommand CloseCommand { get; }

    /// <summary>
    ///     A command for removing a racer from selection
    /// </summary>
    public ICommand RemoveRacersCommand { get; }

    /// <summary>
    ///     A command for adding a racer to selection
    /// </summary>
    public ICommand AddSelectedRacersCommand { get; }

    #endregion

    #region Constructors

    /// <summary>
    ///     Initializes a new instance of the <see cref="AddRacerViewModel" /> class.
    /// </summary>
    /// <param name="racersQuery">A query for retrieving racers</param>
    /// <param name="createRacerCommand">A command for creating a racer</param>
    /// <param name="createRaceRacerCommand">A command for adding a Racer to a Race</param>
    /// <param name="deleteRaceRacerCommand">A command for deleting a Racer from a Race</param>
    public AddRacerViewModel(IRacersQuery racersQuery,
                             ICreateRacerCommand createRacerCommand,
                             ICreateRaceRacerCommand createRaceRacerCommand,
                             IDeleteRaceRacerCommand deleteRaceRacerCommand)
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

    /// <summary>
    ///     Indicates whether the Racer can be removed
    /// </summary>
    /// <param name="arg">The Racer to be removed</param>
    /// <returns>True if the Racer can be removed, otherwise false</returns>
    private static bool CanRemoveRacers(object? arg) => true;

    /// <summary>
    ///     An action performed to remove Racer
    /// </summary>
    /// <param name="obj">The Racer to be removed</param>
    private void RemoveRacers(object? obj)
    {
        if (obj is null)
            return;
        var collection = (IList)obj!;
        if (collection.Count == 0)
            return;
        var racers = collection.Cast<RacerModel>().ToArray();

        foreach (var racer in racers)
        {
            bool deleted = _deleteRaceRacerCommand.Execute(Model.RaceId, racer.RacerId);
            if (!deleted)
                continue;

            AllRacers.Add(racer);
            SelectedRacers.Remove(racer);
        }
    }

    /// <summary>
    ///     Indicates whether the selected Racer can be added
    /// </summary>
    /// <param name="arg">The Racer to be added</param>
    /// <returns></returns>
    private static bool CanAddSelectedRacers(object? arg) => true;

    /// <summary>
    ///     An action performed to add a Racer
    /// </summary>
    /// <param name="obj">The Racer to be added</param>
    private void AddSelectedRacers(object? obj)
    {
        if (obj is null)
            return;
        var collection = (IList)obj!;
        if (collection.Count == 0)
            return;
        var racers = collection.Cast<RacerModel>().ToArray();

        foreach (var racer in racers)
        {
            var entity = new RaceRacerEntity { RaceDayId = Model.RaceDayId, RaceId = Model.RaceId, RacerId = racer.RacerId };
            bool added = _createRaceRacerCommand.Execute(entity) != null;

            AllRacers.Remove(racer);
            SelectedRacers.Add(racer);
        }
    }

    /// <summary>
    ///     Indicates whether adding a Racer can be cancelled
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns></returns>
    private static bool CanCancelNewRacer(object? arg) => true;

    /// <summary>
    ///     An action performed to cancel adding a Racer
    /// </summary>
    /// <param name="obj">Not used</param>
    private void CancelNewRacer(object? obj)
    {
        NewRacer = new RacerModel();
    }

    /// <summary>
    ///     Indicates whether a new Racer can be created
    /// </summary>
    /// <param name="arg">Not used</param>
    /// <returns></returns>
    private bool CanCreateNewRacer(object? arg) => NewRacer is { HasErrors: false, HasAllRequiredData: true };

    /// <summary>
    ///     An action performed to create a new Racer
    /// </summary>
    /// <param name="obj">Not used</param>
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

    /// <summary>
    ///     Loads the data for the ViewModel from the persistent storage
    /// </summary>
    private void LoadData()
    {
        AllRacers.Clear();
        SelectedRacers.Clear();

        var allRacers = _racersQuery.GetAll();
        var raceRacers = _racersQuery.GetRacersForRace(Model.RaceId).ToArray();
        var racersNotInRace = allRacers.Except(raceRacers).ToArray();

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