using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using RaceDay.WpfUi.Infrastructure;

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

        ActiveDialogViewModel = viewModel;
        ActiveDialogViewModel.OpenDialog();
        ActiveDialogViewModel.PropertyChanged += DialogStateChanged;

        object? result = null;

        if (viewModel.DialogClosingHandler != null)
        {
            result = await DialogHost.Show(viewModel, DialogHostIdentifier, viewModel.DialogClosingHandler);
            Debug.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));
        }
        else
        {
            result = await DialogHost.Show(viewModel, DialogHostIdentifier);
        }
        
        ActiveDialogViewModel.PropertyChanged -= DialogStateChanged;

        return result is false ? default : viewModel.Result;
    }
}