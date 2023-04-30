using System;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;

namespace RaceDay.WpfUi.Services;

public class NavigationService : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private ViewModelBase? _activeViewModel;

    public ViewModelBase? ActiveViewModel
    {
        get => _activeViewModel;
        private set => SetField(ref _activeViewModel, value);
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
}