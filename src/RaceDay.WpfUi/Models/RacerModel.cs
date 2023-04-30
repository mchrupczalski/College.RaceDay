using System;

namespace RaceDay.WpfUi.Models;

public class RacerModel
{
    #region Properties

    public Guid Guid { get; init; }
    public string Name { get; set; }
    public byte Age { get; set; }

    #endregion

    #region Constructors

    public RacerModel(Guid guid) => Guid = guid;

    #endregion
}