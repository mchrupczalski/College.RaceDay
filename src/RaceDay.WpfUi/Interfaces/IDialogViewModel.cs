using MaterialDesignThemes.Wpf;

namespace RaceDay.WpfUi.Interfaces;

public interface IDialogViewModel
{
    DialogClosingEventHandler ClosingEventHandler();
    DialogOpenedEventHandler OpenedEventHandler();
}