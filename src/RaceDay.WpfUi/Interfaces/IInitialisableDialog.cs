namespace RaceDay.WpfUi.Interfaces;

public interface IInitialisableDialog<TModel>
{
    void Initialize(TModel model);
}