using RaceDay.Domain.Entities;

namespace RaceDay.Domain.Interfaces;

public interface ICreateRaceDayCommand
{
    /// <summary>
    ///     Creates a new Race Day record and returns the new record
    /// </summary>
    /// <param name="entity">Record to create</param>
    DayEntity? Execute(DayEntity entity);
}