using System;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;

namespace RaceDay.WpfUi.Services;

public class NavigationService : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private ViewModelBase? _activeViewModel;
    private DialogViewModelBase? _activeDialogViewModel;


    public ViewModelBase? ActiveViewModel
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
    
    public void NavigateTo<T>() where T : ViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        ActiveViewModel = viewModel;
    }
    
    public void DisplayDialog<T>() where T : DialogViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        ActiveDialogViewModel = viewModel;
        viewModel.OpenDialog();
    }
}