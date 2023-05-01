using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;

namespace RaceDay.WpfUi.Services;

public class NavigationService : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private INavigableViewModel? _activeViewModel;
    private DialogViewModelBase? _activeDialogViewModel;


    public INavigableViewModel? ActiveViewModel
    {
        get => _activeViewModel;
        private set => SetField(ref _activeViewModel, value);
    }

    public DialogViewModelBase? ActiveDialogViewModel
    {
        get => _activeDialogViewModel;
        set => SetField(ref _activeDialogViewModel, value);
    }

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void NavigateTo<T>() where T : INavigableViewModel
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        ActiveViewModel = viewModel;
        ActiveViewModel.OnNavigatedTo();
    }
    
    public void DisplayDialog<T>() where T : DialogViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        ActiveDialogViewModel = viewModel;
        viewModel.OpenDialog();
    }
    
    public void DisplayDialog<T>(Action? callOnClose) where T : DialogViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        ActiveDialogViewModel = viewModel;
        viewModel.DialogClosingHandler = (sender, args) =>
        {
            callOnClose?.Invoke();
        };
        
        viewModel.OpenDialog();
    }
}