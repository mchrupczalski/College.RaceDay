namespace RaceDay.Domain.Entities;

public record RaceLapEntity
{
    #region Properties

    public int RaceDayId { get; set; }
    public int RaceNumber { get; set; }
    public int RacerId { get; set; }
    public int LapNumber { get; set; }
    public double LapTimeSeconds { get; set; }

    #endregion
}