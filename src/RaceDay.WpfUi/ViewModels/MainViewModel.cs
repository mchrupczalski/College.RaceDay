using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _activeViewModel;

    public ViewModelBase ActiveViewModel
    {
        get => _activeViewModel;
        set => SetField(ref _activeViewModel, value);
    }

    public MainViewModel()
    {
        ActiveViewModel = new HomeViewModel();
    }
}