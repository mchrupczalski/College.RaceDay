using System;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;

namespace RaceDay.WpfUi.Services;

public class NavigationService : ObservableObject
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private DialogViewModelBase _activeDialogViewModel;
    private INavigableViewModel? _activeViewModel;

    #endregion

    #region Properties

    public INavigableViewModel? ActiveViewModel
    {
        get => _activeViewModel;
        private set => SetField(ref _activeViewModel, value);
    }

    public DialogViewModelBase ActiveDialogViewModel
    {
        get => _activeDialogViewModel;
        private set => SetField(ref _activeDialogViewModel, value);
    }

    #endregion

    #region Constructors

    public NavigationService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    #endregion

    public void NavigateTo<T>()
        where T : INavigableViewModel
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        ActiveViewModel = viewModel;
        ActiveViewModel.OnNavigatedTo();
    }
}