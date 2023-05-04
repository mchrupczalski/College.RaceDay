using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;
using RaceDay.WpfUi.Models;

namespace RaceDay.WpfUi.Services;

public class NavigationService : ObservableObject
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private INavigableViewModel? _activeViewModel;
    private DialogViewModelBase _activeDialogViewModel;

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


    public void DisplayDialog<T>(Action? callOnClose)
        where T : DialogViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        viewModel.DialogClosingHandler = (sender, args) => { callOnClose?.Invoke(); };

        viewModel.OpenDialog("some message");
    }
    
    public async Task<TResult?> DisplayDialogAsync<TViewModel, TModel, TResult>(TModel model)
        where TViewModel : DialogViewModelBase<TModel, TResult>
    {
        var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
        ActiveDialogViewModel = viewModel;
        
        
        /*if (viewModel.DialogClosingHandler != null)
        {
            object? result = await DialogHost.Show(viewModel, "RootDialog", viewModel.DialogClosingHandler);
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        else
        {
            object? result = await DialogHost.Show(viewModel, "RootDialog");
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }*/

        return default;
    }
}