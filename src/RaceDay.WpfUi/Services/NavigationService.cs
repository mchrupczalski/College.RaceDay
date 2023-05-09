using System;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;

namespace RaceDay.WpfUi.Services;

public class NavigationService : ObservableObject
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private ViewModelBase? _activeViewModel;

    #endregion

    #region Properties

    public ViewModelBase? ActiveViewModel
    {
        get => _activeViewModel;
        private set => SetField(ref _activeViewModel, value);
    }

    #endregion

    #region Constructors

    public NavigationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    #endregion

    /// <summary>
    ///     Navigates to the specified view model.
    /// </summary>
    /// <typeparam name="T">The type of the view model.</typeparam>
    public void NavigateTo<T>()
        where T : ViewModelBase, INavigableViewModel
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        ActiveViewModel = viewModel;
        viewModel.OnNavigatedTo();
    }
    
    /// <summary>
    ///     Navigates to the specified view model.
    /// </summary>
    /// <param name="viewModel">The view model.</param>
    public void NavigateTo(ViewModelBase viewModel)
    {
        ActiveViewModel = viewModel;
    }
}