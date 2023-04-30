using System;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.ViewModels;

public class CreateRaceDayViewModel : ViewModelBase
{
    public CreateRaceDayModel NewRaceDay { get; set; } = new();
    public void CreateRaceDay(Guid raceDayGuid)
    {
        NewRaceDay = new CreateRaceDayModel();
    }
}