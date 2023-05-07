using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;
using RaceDay.WpfUi.Interfaces;

namespace RaceDay.WpfUi.Services;

public class DialogService : ObservableObject
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private DialogViewModelBase? _activeDialogViewModel;

    #endregion

    #region Properties

    public string DialogHostIdentifier { get; } = "RootDialogHost";

    public DialogViewModelBase? ActiveDialogViewModel
    {
        get => _activeDialogViewModel;
        private set => SetField(ref _activeDialogViewModel, value);
    }

    #endregion

    #region Constructors

    public DialogService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    #endregion

    #region Events And Handlers

    private void DialogStateChanged(object? sender, PropertyChangedEventArgs e)
    {
        {
            if (e.PropertyName != nameof(DialogViewModelBase.DialogHostIsOpen)) return;
            if(ActiveDialogViewModel?.DialogHostIsOpen == false)
                DialogHost.Close(DialogHostIdentifier);
        }
    }

    #endregion

    public async Task<TResult?> DisplayDialogAsync<TViewModel, TModel, TResult>(TModel model)
        where TViewModel : DialogViewModelBase<TModel, TResult>
    {
        var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
        viewModel.Model = model;
        viewModel.Result = default;

        ActiveDialogViewModel = viewModel;
        ActiveDialogViewModel.OpenDialog();
        ActiveDialogViewModel.PropertyChanged += DialogStateChanged;

        object? result = null;

        if (viewModel.DialogClosingHandler != null)
        {
            result = await DialogHost.Show(viewModel, DialogHostIdentifier, viewModel.DialogClosingHandler);
        }
        else
        {
            result = await DialogHost.Show(viewModel, DialogHostIdentifier);
        }
        
        ActiveDialogViewModel.PropertyChanged -= DialogStateChanged;

        return result is false ? default : viewModel.Result;
    }
    
    public async void DisplayDialogAsync<TViewModel, TModel>(TModel model)
        where TViewModel : DialogViewModelBase<TModel>, INavigableViewModel
    {
        var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
        viewModel.Model = model;
        viewModel.OnNavigatedTo();

        ActiveDialogViewModel = viewModel;
        ActiveDialogViewModel.OpenDialog();
        ActiveDialogViewModel.PropertyChanged += DialogStateChanged;
        
        if (viewModel.DialogClosingHandler != null)
        {
            await DialogHost.Show(viewModel, DialogHostIdentifier, viewModel.DialogClosingHandler);
        }
        else
        {
            await DialogHost.Show(viewModel, DialogHostIdentifier);
        }
        
        ActiveDialogViewModel.PropertyChanged -= DialogStateChanged;
    }
}