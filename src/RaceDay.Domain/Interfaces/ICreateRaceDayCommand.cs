using RaceDay.Domain.Entities;

namespace RaceDay.Domain.Interfaces;

/// <summary>
///     A command for creating Race Day.
/// </summary>
public interface ICreateRaceDayCommand
{
    #region Abstract Members

    /// <summary>
    ///     Creates a new Race Day record and returns the new record
    /// </summary>
    /// <param name="entity">Record to create</param>
    DayEntity? Execute(DayEntity entity);

    #endregion
}