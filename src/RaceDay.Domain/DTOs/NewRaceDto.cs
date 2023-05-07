namespace RaceDay.Domain.DTOs;

public record NewRaceDto
{
    #region Properties

    public int RaceDayId { get; init; }
    public DateTime RaceDate { get; init; }

    #endregion
}