using System;

namespace RaceDay.WpfUi.Models;

public class RacerModel
{
    public RacerModel(Guid guid)
    {
        Guid = guid;
    }

    public Guid Guid { get; init; }
    public string Name { get; set; }
    public byte Age { get; set; }
}