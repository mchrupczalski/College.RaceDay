﻿namespace RaceDay.Domain.Entities;

public record RaceEntity : EntityBase
{
    #region Properties

    public int RaceDayId { get; init; }
    public int RaceNumber { get; init; }
    public DateTime RaceDate { get; init; }

    #endregion
}